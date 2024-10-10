namespace TcpServerGUI
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIP = new System.Windows.Forms.TextBox();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.txtIncomingMessages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessageToSend = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server IP Address";
            // 
            // txtServerIP
            // 
            this.txtServerIP.Location = new System.Drawing.Point(345, 20);
            this.txtServerIP.Name = "txtServerIP";
            this.txtServerIP.ReadOnly = true;
            this.txtServerIP.Size = new System.Drawing.Size(366, 44);
            this.txtServerIP.TabIndex = 1;
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(746, 12);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(251, 70);
            this.btnStartServer.TabIndex = 2;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // txtIncomingMessages
            // 
            this.txtIncomingMessages.Location = new System.Drawing.Point(31, 175);
            this.txtIncomingMessages.Multiline = true;
            this.txtIncomingMessages.Name = "txtIncomingMessages";
            this.txtIncomingMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIncomingMessages.Size = new System.Drawing.Size(757, 288);
            this.txtIncomingMessages.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(35, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(410, 68);
            this.label2.TabIndex = 0;
            this.label2.Text = "Incoming Messages";
            // 
            // txtMessageToSend
            // 
            this.txtMessageToSend.Location = new System.Drawing.Point(31, 560);
            this.txtMessageToSend.Name = "txtMessageToSend";
            this.txtMessageToSend.Size = new System.Drawing.Size(552, 44);
            this.txtMessageToSend.TabIndex = 4;
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(681, 552);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(194, 58);
            this.btnSendMessage.TabIndex = 5;
            this.btnSendMessage.Text = "Send";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 37);
            this.label3.TabIndex = 6;
            this.label3.Text = "Message to Send";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1335, 961);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.txtMessageToSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIncomingMessages);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.txtServerIP);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIP;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.TextBox txtIncomingMessages;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessageToSend;
        private System.Windows.Forms.Button btnSendMessage;
        private System.Windows.Forms.Label label3;
    }
}

