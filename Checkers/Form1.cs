using Checkers.Core;
using Checkers.Core.Enums;
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

        private Button[,] Field;

        private Board Board;

        private Core.Enums.Color currentPlayer;

        private TcpSocket socket;

        public Form1()
        {
            InitializeComponent();

            this.Field = new Button[8, 8];
            this.Board = new Board();

            this.InitializateField();
            this.UpdateTable();
            this.InitializeGame();


            this.socket = new TcpSocket("127.0.0.1", 40000);
            socket.OnReceiveSocket += Socket_OnReceiveSocket;
            socket.StartServer();

            
        }

        private void Socket_OnReceiveSocket(object sender, byte[] data)
        {
            MessageBox.Show("Socket chegou");
        }

        private void InitializeGame()
        {
            Random r = new Random((int)DateTime.Now.Ticks);
            var random = r.Next(2);
            this.currentPlayer = (Checkers.Core.Enums.Color)random;
            MessageBox.Show(string.Format("O jogador de cor '{0}' inicia", currentPlayer.ToString()));
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
                        this.Field[i, j] = button;
                        this.Controls.Add(button);
                    }
                }
                startWhite = (startWhite == 0 ? 1 : 0);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if(!socket.IsConnected)
                socket.Connect("127.0.0.1", 41000);

            this.socket.Send(Encoding.UTF8.GetBytes("Olá mundo"));

        }


        private Point GetButtonCoordinate(Button btn)
        {
            var coord = btn.Name.Split('x');
            return new Point(Convert.ToInt32(coord[0]), Convert.ToInt32(coord[1]));
        }

        private void UpdateTable()
        {
            foreach (var piece in this.Board.Pieces) 
            {
                var button = this.Field[piece.X, piece.Y];
                button.Text = piece.Color.ToString();
                //button.Enabled = (currentPlayer == piece.Color);
            }
        }



    }
}
