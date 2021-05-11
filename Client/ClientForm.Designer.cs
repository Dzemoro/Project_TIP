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
            this.send_button = new System.Windows.Forms.Button();
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
            this.startSbutton = new System.Windows.Forms.Button();
            this.micGroup = new System.Windows.Forms.GroupBox();
            this.users = new System.Windows.Forms.ListBox();
            this.callButton = new System.Windows.Forms.Button();
            this.audioPanel.SuspendLayout();
            this.micGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // send_button
            // 
            this.send_button.Location = new System.Drawing.Point(686, 155);
            this.send_button.Name = "send_button";
            this.send_button.Size = new System.Drawing.Size(75, 23);
            this.send_button.TabIndex = 0;
            this.send_button.Text = "Send";
            this.send_button.UseVisualStyleBackColor = true;
            this.send_button.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // message
            // 
            this.message.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.message.Location = new System.Drawing.Point(608, 27);
            this.message.Multiline = true;
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(153, 52);
            this.message.TabIndex = 1;
            // 
            // addressbox
            // 
            this.addressbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addressbox.Location = new System.Drawing.Point(608, 85);
            this.addressbox.MaxLength = 15;
            this.addressbox.Name = "addressbox";
            this.addressbox.Size = new System.Drawing.Size(153, 24);
            this.addressbox.TabIndex = 2;
            // 
            // messageLbl
            // 
            this.messageLbl.AutoSize = true;
            this.messageLbl.Location = new System.Drawing.Point(528, 34);
            this.messageLbl.Name = "messageLbl";
            this.messageLbl.Size = new System.Drawing.Size(50, 13);
            this.messageLbl.TabIndex = 3;
            this.messageLbl.Text = "Message";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(528, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP Address";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(528, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Received";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(605, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 6;
            // 
            // audioPanel
            // 
            this.audioPanel.Controls.Add(this.audioSList);
            this.audioPanel.Location = new System.Drawing.Point(32, 27);
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
            this.refreshB.Location = new System.Drawing.Point(299, 27);
            this.refreshB.Name = "refreshB";
            this.refreshB.Size = new System.Drawing.Size(128, 29);
            this.refreshB.TabIndex = 7;
            this.refreshB.Text = "Refresh sources";
            this.refreshB.UseVisualStyleBackColor = true;
            this.refreshB.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // startSbutton
            // 
            this.startSbutton.Location = new System.Drawing.Point(299, 62);
            this.startSbutton.Name = "startSbutton";
            this.startSbutton.Size = new System.Drawing.Size(128, 29);
            this.startSbutton.TabIndex = 8;
            this.startSbutton.Text = "Start/Stop";
            this.startSbutton.UseVisualStyleBackColor = true;
            this.startSbutton.Click += new System.EventHandler(this.StartSbutton_Click);
            // 
            // micGroup
            // 
            this.micGroup.Controls.Add(this.audioPanel);
            this.micGroup.Controls.Add(this.startSbutton);
            this.micGroup.Controls.Add(this.refreshB);
            this.micGroup.Location = new System.Drawing.Point(12, 12);
            this.micGroup.Name = "micGroup";
            this.micGroup.Size = new System.Drawing.Size(453, 244);
            this.micGroup.TabIndex = 9;
            this.micGroup.TabStop = false;
            this.micGroup.Text = "Microphone Settings";
            // 
            // users
            // 
            this.users.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.users.FormattingEnabled = true;
            this.users.ItemHeight = 23;
            this.users.Location = new System.Drawing.Point(12, 262);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(293, 188);
            this.users.TabIndex = 10;
            // 
            // callButton
            // 
            this.callButton.Location = new System.Drawing.Point(311, 262);
            this.callButton.Name = "callButton";
            this.callButton.Size = new System.Drawing.Size(141, 27);
            this.callButton.TabIndex = 11;
            this.callButton.Text = "Call";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new System.EventHandler(this.callButton_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 620);
            this.Controls.Add(this.callButton);
            this.Controls.Add(this.users);
            this.Controls.Add(this.micGroup);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.messageLbl);
            this.Controls.Add(this.addressbox);
            this.Controls.Add(this.message);
            this.Controls.Add(this.send_button);
            this.Name = "ClientForm";
            this.Text = "Connection Maganger";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.audioPanel.ResumeLayout(false);
            this.micGroup.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button send_button;
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
        private System.Windows.Forms.Button startSbutton;
        private System.Windows.Forms.GroupBox micGroup;
        private System.Windows.Forms.ListBox users;
        private System.Windows.Forms.Button callButton;
    }
}

