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
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            InitializeComponent();
        }

        private void startSocket_Click(object sender, EventArgs e)
        {
            var socketForm = new CheckersSocket();
            socketForm.ShowDialog();
        }

        private void startRMI_Click(object sender, EventArgs e)
        {
            var rmiForm = new CheckersRMI();
            rmiForm.ShowDialog();
        }
    }
}
