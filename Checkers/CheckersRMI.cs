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
        private Player player;
        private Piece PickedPiece { get; set; }
        private Timer TableUpdater;

        private RemoteHandle remoteHandle = null; 

        public CheckersRMI()
        {
            InitializeComponent();

            this.Field = new List<Button>();

            this.InitializateField();
            this.UpdateTable();

            this.TableUpdater = new Timer();
            TableUpdater.Tick += TableUpdater_Tick;
            TableUpdater.Interval = 50;
            TableUpdater.Start();

        }

        private void TableUpdater_Tick(object sender, EventArgs e)
        {
            if(this.Game != null)
                this.UpdateTable();
        }

        private void InitializeGame(object sender, EventArgs e)
        {
            //Instancia o objeto jogo
            var game = new Game();
            game.RafflePlayer();

            //Inicializa no controlador de jogo
            GameController.GetInstance().Initialize(game);
            
            //Sempre quem inicializa o jogo é o White player
            GameController.GetInstance().SetWhitePlayer();
            
            //Efetua a conexão remota
            remoteHandle = new RemoteHandle(Constantes.CONNECTING_IP, Constantes.CONNECTING_PORT, Constantes.PORT);
            remoteHandle.Register<RemoteGame>("Game");

            //Pega o objeto remoto
            var remoteObj = remoteHandle.GetRemoteObject<IRemoteGame>("Game");

            //Passa o jogo atual por parâmetro
            remoteObj.Initialize(this.Game);

            //Seta o player remoto
            remoteObj.SetRedPlayer();
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
            if (GameController.GetInstance().IsMyTurn)
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
                        var predictions = this.PickedPiece.Predict(this.Game.Board);
                        foreach(var item in predictions.Predictions)
                        {
                            var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                           GetButtonCoordinate(x).Y == item.Y);
                            btn.BackColor = Color.Yellow;
                        }
                        foreach (var item in predictions.Predictions)
                        {
                            var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                           GetButtonCoordinate(x).Y == item.Y);
                            btn.BackColor = Color.Yellow;
                        }
                    }
                }
                else
                {
                    var clickedSlot = this.Game.Board[coordinate.X, coordinate.Y];
                    if (clickedSlot != null && this.player.Owns(clickedSlot))
                    {

                        this.PickedPiece = clickedSlot;
                        this.CleanColors();
                        var predictions = PickedPiece.Predict(this.Game.Board);
                        foreach (var item in predictions.Predictions)
                        {
                            var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                           GetButtonCoordinate(x).Y == item.Y);
                            btn.BackColor = Color.Yellow;
                        }
                        foreach (var item in predictions.Predictions)
                        {
                            var btn = this.Field.Find(x => GetButtonCoordinate(x).X == item.X &&
                                                           GetButtonCoordinate(x).Y == item.Y);
                            btn.BackColor = Color.Yellow;
                        }
                    }
                    
                    if (this.PickedPiece.IsMovimentValid(this.Game.Board, coordinate))
                    {
                        //Efetua o movimento remotamente
                        var remoteObj = this.remoteHandle.GetRemoteObject<IRemoteGame>("Game");
                        remoteObj.Move(this.PickedPiece, coordinate);
                        remoteObj.SwapPlayer();

                        this.PickedPiece.Move(this.Game.Board, coordinate);
                        if (!this.PickedPiece.CanEat(Game.Board))
                        {
                            this.Game.SwapPlayers();
                        }
                    }
                }
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


        private void UpdateTable()
        {
            //Limpa o tabuleiro
            foreach (var item in this.Field)
            {
                item.Text = "";
                if(this.PickedPiece == null)
                {
                    item.BackColor = Color.Black;
                }
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
            }
        }
    }
}