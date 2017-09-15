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
    public partial class Form1 : Form
    {

        private List<Button> Field;

        private Game Game = null;
        private Player player;
        private Piece PeekedPiece { get; set; }

        private bool ActiveTurn { get; set; }

        private TcpSocket socket;

        private Timer TableUpdater;

        public Form1()
        {
            InitializeComponent();

            this.Field = new List<Button>();

            this.InitializateField();
            this.UpdateTable();

            this.socket = new TcpSocket("127.0.0.1", 40000);
            socket.OnReceiveSocket += Socket_OnReceiveSocket;
            socket.StartServer();

            this.TableUpdater = new Timer();
            TableUpdater.Tick += TableUpdater_Tick;
            TableUpdater.Interval = 50;
            TableUpdater.Start();
        }

        private void TableUpdater_Tick(object sender, EventArgs e)
        {
            this.UpdateTable();
        }

        private void Socket_OnReceiveSocket(object sender, byte[] data)
        {
            //if(this.Game == null)
            //{
            //    this.player = new RedPlayer();
            //}
            this.Game = Serializer.Deserialize<Game>(data);
            this.ActiveTurn = true;

        }

        private void InitializeGame(object sender, EventArgs e)
        {
            this.Game = new Game();
            this.Game.PositionatePieces();
            //this.player = new BlackPlayer();
            this.socket.Connect("127.0.0.1", 40000);
            this.Game.RafflePlayer();
            this.socket.Send(Serializer.Serialize(this.Game));
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
                        var button = new Button();
                        button.Size = new System.Drawing.Size(50, 50);
                        button.Top = i * 50;
                        button.Left = j * 50;
                        button.Name = string.Format("{0}x{1}", i, j);
                        button.BackColor = System.Drawing.Color.Black;
                        button.ForeColor = System.Drawing.Color.White;
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
            this.ActiveTurn = false;
            var coordinate = this.GetButtonCoordinate((Button)sender);
            if (this.PeekedPiece == null)
            {
                this.PeekedPiece = this.Game.Board[coordinate.X, coordinate.Y];
            }
            else
            {
                var moviment = new Moviment(coordinate);
                if (this.PeekedPiece.IsMovimentValid(moviment))
                {
                    this.PeekedPiece.Move(moviment);
                }
                this.PeekedPiece = null;
                this.socket.Send(Serializer.Serialize(this.Game));
            }


        }


        private Point GetButtonCoordinate(Button btn)
        {
            var coord = btn.Name.Split('x');
            return new Point(Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1]));
        }

        private void UpdateTable()
        {

            foreach (var item in this.Field)
            {
                item.Text = "";
            }

            if (this.Game != null)
            {
                foreach (var piece in this.Game.Board.BlackPieces)
                {
                    var btn = this.Field.Find(x => 
                        this.GetButtonCoordinate(x).X == piece.X &&
                        this.GetButtonCoordinate(x).Y == piece.Y
                    );
                    btn.Text = "Black";
                }

                foreach (var piece in this.Game.Board.RedPieces)
                {
                    var btn = this.Field.Find(x =>
                        this.GetButtonCoordinate(x).X == piece.X &&
                        this.GetButtonCoordinate(x).Y == piece.Y
                    );
                    btn.Text = "Red";
                }
            }
        }
    }
}
