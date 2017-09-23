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
                    return this.PickedPiece.GetPredictions(this.Game.Board);
                else return null;
            }
        }

        public CheckersSocket()
        {
            InitializeComponent();

            this.Field = new List<Button>();

            this.InitializateField();
            this.UpdateTable();

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
            this.UpdateTable();
        }

        private void Socket_OnReceiveSocket(byte[] data)
        {
            try
            {
                //if (this.Game == null)
                //{
                //    this.player = new WhitePlayer();
                //    this.socket.Connect(Constantes.CONNECTING_IP, Constantes.CONNECTING_PORT);
                //}

                this.Game = Serializer.Deserialize<Game>(data);
                this.player = this.Game.CurrentPlayer;
                //if (this.CheckPlayerTurn())
                //{
                //this.player = this.Game.CurrentPlayer;
                //}                
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
            this.player = new WhitePlayer();
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
                    this.PickedPiece = this.Game.Board[coordinate.X, coordinate.Y];
                    if (!this.player.Owns(this.PickedPiece))
                    {
                        MessageBox.Show("Essa peça não é sua!");
                        this.PickedPiece = null;
                        return;
                    }
                    else
                    {
                        //var predictions = this.PickedPiece.Predict(this.Game.Board);
                        //foreach(var item in predictions.Predictions)
                        //{
                        //    var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                        //                                   GetButtonCoordinate(x).Y == item.Y);
                        //    btn.BackColor = Color.Yellow;
                        //}
                        
                    }
                }
                else
                {
                    var clickedSlot = this.Game.Board[coordinate.X, coordinate.Y];
                    if (clickedSlot != null && this.player.Owns(clickedSlot))
                    {
                        this.PickedPiece = clickedSlot;
                    }
                    
                    if (this.PickedPiece.IsMovimentValid(this.Game.Board, coordinate))
                    {
                        var canEat = this.PickedPiece.CanEat(Game.Board);
                        this.PickedPiece.Move(this.Game.Board, coordinate);
                        var canEatAfter = this.PickedPiece.CanEat(Game.Board);

                        if (!(canEat && canEatAfter))
                            canEat = false;

                        if (!canEat)
                        {
                            this.Game.SwapPlayers();
                        }
                        this.socket.Send(Serializer.Serialize(this.Game));
                        this.PickedPiece = null;
                    }
                }
            }
        }


        private Point GetButtonCoordinate(Button btn)
        {
            var coord = btn.Name.Split('x');
            return new Point(Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1]));
        }

        private void UpdateTable()
        {
            //Limpa o tabuleiro
            foreach (var item in this.Field)
            {
                item.Text = "";
                item.BackColor = Color.Black;
            }

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
}