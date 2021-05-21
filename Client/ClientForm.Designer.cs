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
            this.users = new System.Windows.Forms.ListBox();
            this.callButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // users
            // 
            this.users.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.users.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.users.FormattingEnabled = true;
            this.users.ItemHeight = 23;
            this.users.Location = new System.Drawing.Point(12, 12);
            this.users.Name = "users";
            this.users.Size = new System.Drawing.Size(293, 188);
            this.users.TabIndex = 10;
            // 
            // callButton
            // 
            this.callButton.Location = new System.Drawing.Point(311, 12);
            this.callButton.Name = "callButton";
            this.callButton.Size = new System.Drawing.Size(141, 27);
            this.callButton.TabIndex = 11;
            this.callButton.Text = "Call";
            this.callButton.UseVisualStyleBackColor = true;
            this.callButton.Click += new System.EventHandler(this.callButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(311, 186);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 28);
            this.button1.TabIndex = 12;
            this.button1.Text = "Disconnect";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(477, 226);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.callButton);
            this.Controls.Add(this.users);
            this.Name = "ClientForm";
            this.Text = "Connection Maganger";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox users;
        private System.Windows.Forms.Button callButton;
        private System.Windows.Forms.Button button1;
    }
}

