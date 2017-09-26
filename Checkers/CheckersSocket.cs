using Checkers.Core;
using Checkers.Core.Pieces;
using Checkers.Core.Players;
using Checkers.Socket;
using Checkers.Socket.TCP;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class CheckersSocket : Form
    {

        private List<Button> Field;

        private Game Game = null;
        private Player player;
        private Piece PickedPiece { get; set; }

        private TcpSocket socket;

        private Timer TableUpdater;

        private Prediction Predictions
        {
            get
            {
                if (this.PickedPiece != null && this.Game != null)
                {
                    if (this.Eaten)
                        return this.PickedPiece.PredictToEat(this.Game.Board);
                    else
                        return this.PickedPiece.GetPredictions(this.Game.Board);
                }
                else return null;
            }
        }

        private bool Eaten { get; set; } = false;

        public CheckersSocket()
        {
            InitializeComponent();

            this.Field = new List<Button>();

            this.InitializateField();
            this.Tick();

            try
            {
                this.socket = new TcpSocket(Constantes.IP, Constantes.PORT);
                socket.OnReceiveSocket += Socket_OnReceiveSocket;
                socket.StartServer();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao inicializar o socket: {ex.Message}");
                this.Close();
            }


            this.TableUpdater = new Timer();
            TableUpdater.Tick += TableUpdater_Tick;
            TableUpdater.Interval = 50;
            TableUpdater.Start();
        }

        private void TableUpdater_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

        private void Socket_OnReceiveSocket(byte[] data)
        {
            try
            {
                var game = Serializer.Deserialize<Game>(data);
                if (this.Game == null)
                {
                    this.player = game.RedPlayer;
                    this.socket.Connect(Constantes.CONNECTING_IP, Constantes.CONNECTING_PORT);
                }

                this.Game = game;

                if (this.CheckPlayerTurn())
                {
                    this.player = this.Game.CurrentPlayer;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Um erro ocorreu: {ex.Message}");
            }

        }

        private bool CheckPlayerTurn()
        {
            if (this.Game != null && this.player != null)
                return (this.Game.CurrentPlayer is WhitePlayer && this.player is WhitePlayer ||
                        this.Game.CurrentPlayer is RedPlayer && this.player is RedPlayer);
            else return false;
        }


        private void InitializeGame(object sender, EventArgs e)
        {
            this.Game = new Game();
            this.Game.PositionatePieces();
            this.player = Game.WhitePlayer;
            this.Game.RafflePlayer();

            try
            {
                this.socket.Connect(Constantes.CONNECTING_IP, Constantes.CONNECTING_PORT);
                this.socket.Send(Serializer.Serialize(this.Game));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Não foi possível enviar o socket: {ex.Message}.");
            }
        }

        private void InitializateField()
        {
            //Posiciona os botões
            int startWhite = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if ((startWhite + j) % 2 == 0)
                    {
                        var button = new Button
                        {
                            Size = new System.Drawing.Size(50, 50),
                            Top = i * 50,
                            Left = j * 50,
                            Name = string.Format("{0}x{1}", i, j),
                            BackColor = System.Drawing.Color.Black,
                            ForeColor = System.Drawing.Color.White
                        };
                        button.Click += Button_Click;
                        this.Field.Add(button);
                        this.Controls.Add(button);
                    }
                }
                startWhite = (startWhite == 0 ? 1 : 0);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (this.CheckPlayerTurn())
            {
                var coordinate = this.GetButtonCoordinate((Button)sender);

                if (this.PickedPiece == null)
                {
                    var pickedPiece = this.Game.Board[coordinate.X, coordinate.Y];
                    if (!this.player.Owns(pickedPiece))
                    {
                        MessageBox.Show("Essa peça não é sua!");
                        return;
                    }
                    else
                    {
                        this.PickedPiece = pickedPiece;
                    }
                }
                else //Se tiver uma peça pickada;
                {
                    //Se clicou em outra peça, o usuário quer trocar a peça selecionada
                    var clickedSlot = this.Game.Board[coordinate.X, coordinate.Y];
                    if (clickedSlot != null && this.player.Owns(clickedSlot))
                    {
                        this.PickedPiece = clickedSlot;
                    }
                    else //vai efetuar alguma jogada (movimento)
                    {
                        if (this.Eaten)
                        {
                            var validMoviment = this.PickedPiece.PredictToEat(this.Game.Board).Predictions.Exists(prediction => prediction.X == coordinate.X && prediction.Y == coordinate.Y);
                            if (validMoviment)
                            {
                                var eaten = this.PickedPiece.Move(this.Game.Board, coordinate);
                                if (eaten)
                                    this.ResolveEaten();
                                else
                                {
                                    this.Game.SwapPlayers();
                                    this.Eaten = false;
                                }
                            }
                        }
                        else
                        {
                            if (this.PickedPiece.IsMovimentValid(this.Game.Board, coordinate))
                            {
                                var eaten = this.PickedPiece.Move(this.Game.Board, coordinate);
                                if (eaten)
                                    this.ResolveEaten();
                                else
                                {
                                    this.Game.SwapPlayers();
                                    this.Eaten = false;
                                }
                            }
                        }

                        this.PickedPiece = null;
                        this.socket.Send(Serializer.Serialize(this.Game));
                    }
                }
            }
        }


        public void ResolveEaten()
        {
            if (!this.PickedPiece.CanEat(Game.Board))
            {
                this.Game.SwapPlayers();
                this.Eaten = false;
            }
            else
            {
                this.Eaten = true;
            }
        }


        private Point GetButtonCoordinate(Button btn)
        {
            var coord = btn.Name.Split('x');
            return new Point(Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1]));
        }

        private void Tick()
        {

            //Checa se há um jogo na ativa. Caso positivo, desabilita o botão
            this.btnInitialize.Enabled = (Game == null);

            //Atualiza o título da janela com o status do jogador
            var myTurn = this.CheckPlayerTurn();
            if (!this.btnInitialize.Enabled)
                this.Text = (myTurn ? "Sua vez de jogar" : "Vez do oponente");

            //Limpa o tabuleiro
            foreach (var item in this.Field)
            {
                item.Text = "";
                item.BackColor = Color.Black;
            }

            try
            {
                if (this.Game != null)
                {
                    foreach (var piece in this.Game.Board.WhitePieces)
                    {
                        var btn = this.Field.Find(x =>
                            this.GetButtonCoordinate(x).X == piece.X &&
                            this.GetButtonCoordinate(x).Y == piece.Y
                        );
                        btn.ForeColor = Color.White;
                        btn.Text = "White";
                    }

                    foreach (var piece in this.Game.Board.RedPieces)
                    {
                        var btn = this.Field.Find(x =>
                            this.GetButtonCoordinate(x).X == piece.X &&
                            this.GetButtonCoordinate(x).Y == piece.Y
                        );
                        btn.ForeColor = Color.Red;
                        btn.Text = "Red";
                    }

                    if (this.Predictions != null)
                    {
                        if (this.player.Owns(this.PickedPiece))
                        {
                            foreach (var item in this.Predictions.Predictions)
                            {
                                var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                               GetButtonCoordinate(x).Y == item.Y);
                                btn.BackColor = Color.Yellow;
                            }
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}