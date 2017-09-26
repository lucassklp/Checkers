using Checkers.Core;
using Checkers.Core.Pieces;
using Checkers.Core.Players;
using Checkers.RMI;
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
    public partial class CheckersRMI : Form
    {

        private List<Button> Field;

        public Game Game
        {
            get
            {
                return GameController.GetInstance().Game;
            }
        }
        private Player player
        {
            get
            {
                return GameController.GetInstance().Player;
            }
        }

        private Prediction Predictions
        {
            get
            {
                if (this.PickedPiece != null && this.Game != null)
                {
                    if(this.Eaten)
                        return this.PickedPiece.PredictToEat(this.Game.Board);
                    else
                        return this.PickedPiece.GetPredictions(this.Game.Board);
                }
                else return null;
            }
        }

        private bool Eaten { get; set; } = false;

        private Piece PickedPiece { get; set; }
        private Timer TableUpdater;

        private RemoteHandle remoteHandle = null; 

        public CheckersRMI()
        {
            InitializeComponent();

            this.Field = new List<Button>();

            this.TableUpdater = new Timer();
            TableUpdater.Tick += TableUpdater_Tick;
            TableUpdater.Interval = 50;
            TableUpdater.Start();

            this.InitializateField();

            this.remoteHandle = new RemoteHandle(Constantes.CONNECTING_IP, Constantes.CONNECTING_PORT, Constantes.PORT);
            remoteHandle.Register<RemoteGame>("Game");
        }

        private void TableUpdater_Tick(object sender, EventArgs e)
        {
            this.Tick();
        }

        private void InitializeGame(object sender, EventArgs e)
        {
            //Instancia o objeto jogo
            var game = new Game();
            game.RafflePlayer();
            game.PositionatePieces();

            //Inicializa no controlador de jogo
            GameController.GetInstance().Initialize(game);
            
            //Sempre quem inicializa o jogo é o White player
            GameController.GetInstance().SetWhitePlayer();

            try
            {
                //Pega o objeto remoto
                var remoteObj = remoteHandle.GetRemoteObject<IRemoteGame>("Game");

                //Passa o jogo atual por parâmetro
                remoteObj.Initialize(this.Game);

                //Seta o player remoto
                remoteObj.SetRedPlayer();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao inicializar o jogo: {ex.Message}");
                this.Close();
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
                            ForeColor = System.Drawing.Color.White,
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
            if (GameController.GetInstance().IsMyTurn)
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
                    if (clickedSlot != null)
                    {
                        this.PickedPiece = clickedSlot; //Efetua a troca
                    }
                    else //vai efetuar alguma jogada (movimento)
                    {
                        if (this.Eaten)
                        {
                            var validMoviment = this.PickedPiece.PredictToEat(this.Game.Board).Predictions.Exists(prediction => prediction.X == coordinate.X && prediction.Y == coordinate.Y);
                            if (validMoviment)
                            {
                                try
                                {
                                    var remoteObj = this.remoteHandle.GetRemoteObject<IRemoteGame>("Game");
                                    remoteObj.Move(this.PickedPiece, coordinate);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ocorreu um erro no jogo: {ex.Message}");
                                    this.Close();
                                }
                                
                                var eaten = this.PickedPiece.Move(this.Game.Board, coordinate);
                                if (eaten)
                                    this.ResolveEaten();
                                else
                                {
                                    this.SwapPlayer();
                                }
                            }
                        }
                        else
                        {
                            if (this.PickedPiece.IsMovimentValid(this.Game.Board, coordinate))
                            {
                                try
                                {
                                    var remoteObj = this.remoteHandle.GetRemoteObject<IRemoteGame>("Game");
                                    remoteObj.Move(this.PickedPiece, coordinate);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show($"Ocorreu um erro no jogo: {ex.Message}");
                                    this.Close();
                                }
                                
                                var eaten = this.PickedPiece.Move(this.Game.Board, coordinate);
                                if (eaten)
                                    this.ResolveEaten();
                                else
                                {
                                    this.SwapPlayer();
                                }
                            }
                        }
                    }
                }
            }
        }


        public void SwapPlayer()
        {
            this.Game.SwapPlayers();
            try
            {
                var remoteObj = this.remoteHandle.GetRemoteObject<IRemoteGame>("Game");
                remoteObj.SwapPlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro no jogo: {ex.Message}");
                this.Close();
            }
            this.Eaten = false;
            this.PickedPiece = null;
        }

        public void ResolveEaten()
        {
            if (!this.PickedPiece.CanEat(Game.Board))
            {
                this.SwapPlayer();
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


        private void CleanColors()
        {
            foreach (var item in this.Field)
            {
                item.BackColor = Color.Black;
            }
        }


        private void Tick()
        {
            //Checa se há um jogo na ativa. Caso positivo, desabilita o botão
            this.btnInitialize.Enabled = (GameController.GetInstance().Game == null);

            //Atualiza o título da janela com o status do jogador
            var myTurn = GameController.GetInstance().IsMyTurn;
            if(!this.btnInitialize.Enabled)
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
                }

                if (this.Predictions != null)
                {
                    foreach (var item in this.Predictions.Predictions)
                    {
                        var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                       GetButtonCoordinate(x).Y == item.Y);
                        btn.BackColor = Color.Yellow;
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