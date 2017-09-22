namespace Checkers
{
    partial class StartScreen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.startSocket = new System.Windows.Forms.Button();
            this.startRMI = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startSocket
            // 
            this.startSocket.Location = new System.Drawing.Point(45, 89);
            this.startSocket.Name = "startSocket";
            this.startSocket.Size = new System.Drawing.Size(106, 71);
            this.startSocket.TabIndex = 0;
            this.startSocket.Text = "Iniciar Jogo (Socket)";
            this.startSocket.UseVisualStyleBackColor = true;
            this.startSocket.Click += new System.EventHandler(this.startSocket_Click);
            // 
            // startRMI
            // 
            this.startRMI.Location = new System.Drawing.Point(199, 89);
            this.startRMI.Name = "startRMI";
            this.startRMI.Size = new System.Drawing.Size(106, 71);
            this.startRMI.TabIndex = 1;
            this.startRMI.Text = "Iniciar Jogo (RMI)";
            this.startRMI.UseVisualStyleBackColor = true;
            this.startRMI.Click += new System.EventHandler(this.startRMI_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(45, 37);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(186, 26);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Bem vindo ao jogo da dama. \nSelecione um modo para iniciar o jogo";
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(355, 261);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.startRMI);
            this.Controls.Add(this.startSocket);
            this.MaximizeBox = false;
            this.Name = "StartScreen";
            this.Text = "Jogo da Dama- Sistemas Distribuídos [Lucas, Rodrigo]";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSocket;
        private System.Windows.Forms.Button startRMI;
        private System.Windows.Forms.Label lblMessage;
    }
}