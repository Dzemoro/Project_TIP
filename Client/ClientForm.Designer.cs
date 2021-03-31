namespace ClientApp
{
    partial class ClientForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.send = new System.Windows.Forms.Button();
            this.message = new System.Windows.Forms.TextBox();
            this.addressbox = new System.Windows.Forms.TextBox();
            this.messageLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.audioPanel = new System.Windows.Forms.Panel();
            this.audioSList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.refreshB = new System.Windows.Forms.Button();
            this.startSButton = new System.Windows.Forms.Button();
            this.audioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(193, 140);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(75, 23);
            this.send.TabIndex = 0;
            this.send.Text = "Send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.button1_Click);
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.message.Location = new System.Drawing.Point(115, 12);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(153, 52);
            this.message.TabIndex = 1;
            // 
            // addressbox
            // 
            this.addressbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addressbox.Location = new System.Drawing.Point(115, 70);
            this.addressbox.MaxLength = 15;
            this.addressbox.Name = "addressbox";
            this.addressbox.Size = new System.Drawing.Size(153, 24);
            this.addressbox.TabIndex = 2;
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(35, 19);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(50, 13);
            this.messageLbl.TabIndex = 3;
            this.messageLbl.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Received";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 6;
            // 
            // audioPanel
            // 
            this.audioPanel.Controls.Add(this.audioSList);
            this.audioPanel.Location = new System.Drawing.Point(285, 3);
            this.audioPanel.Name = "audioPanel";
            this.audioPanel.Size = new System.Drawing.Size(261, 169);
            this.audioPanel.TabIndex = 0;
            // 
            // audioSList
            // 
            this.audioSList.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.audioSList.AllowColumnReorder = true;
            this.audioSList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.audioSList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.audioSList.HideSelection = false;
            this.audioSList.Location = new System.Drawing.Point(0, 0);
            this.audioSList.MultiSelect = false;
            this.audioSList.Name = "audioSList";
            this.audioSList.Size = new System.Drawing.Size(261, 169);
            this.audioSList.TabIndex = 0;
            this.audioSList.UseCompatibleStateImageBehavior = false;
            this.audioSList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Device";
            this.columnHeader1.Width = 160;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Channels";
            // 
            // refreshB
            // 
            this.refreshB.Location = new System.Drawing.Point(552, 3);
            this.refreshB.Name = "refreshB";
            this.refreshB.Size = new System.Drawing.Size(128, 29);
            this.refreshB.TabIndex = 7;
            this.refreshB.Text = "Refresh sources";
            this.refreshB.UseVisualStyleBackColor = true;
            this.refreshB.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // startSButton
            // 
            this.startSButton.Location = new System.Drawing.Point(552, 38);
            this.startSButton.Name = "startSButton";
            this.startSButton.Size = new System.Drawing.Size(128, 29);
            this.startSButton.TabIndex = 8;
            this.startSButton.Text = "Start/Stop";
            this.startSButton.UseVisualStyleBackColor = true;
            this.startSButton.Click += new System.EventHandler(this.startSButton_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 337);
            this.Controls.Add(this.startSButton);
            this.Controls.Add(this.refreshB);
            this.Controls.Add(this.audioPanel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.addressbox);
            this.Controls.Add(this.message);
            this.Controls.Add(this.send);
            this.Name = "ClientForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.audioPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.TextBox addressbox;
        private System.Windows.Forms.Label messageLbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel audioPanel;
        private System.Windows.Forms.ListView audioSList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button refreshB;
        private System.Windows.Forms.Button startSButton;
    }
}

