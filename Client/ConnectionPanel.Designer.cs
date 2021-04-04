namespace ClientApp
{
    partial class ConnectionPanel
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
            this.userLabel = new System.Windows.Forms.Label();
            this.ip_addr_lbl = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.userBox = new System.Windows.Forms.TextBox();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userLabel.Location = new System.Drawing.Point(12, 50);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(80, 18);
            this.userLabel.TabIndex = 0;
            this.userLabel.Text = "Username";
            // 
            // ip_addr_lbl
            // 
            this.ip_addr_lbl.AutoSize = true;
            this.ip_addr_lbl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ip_addr_lbl.Location = new System.Drawing.Point(12, 99);
            this.ip_addr_lbl.Name = "ip_addr_lbl";
            this.ip_addr_lbl.Size = new System.Drawing.Size(84, 18);
            this.ip_addr_lbl.TabIndex = 1;
            this.ip_addr_lbl.Text = "IP Address";
            // 
            // connectBtn
            // 
            this.connectBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.connectBtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.connectBtn.Location = new System.Drawing.Point(192, 149);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 27);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Connect";
            this.connectBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // userBox
            // 
            this.userBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userBox.Location = new System.Drawing.Point(123, 47);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(144, 26);
            this.userBox.TabIndex = 3;
            // 
            // addressBox
            // 
            this.addressBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addressBox.Location = new System.Drawing.Point(123, 96);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(144, 26);
            this.addressBox.TabIndex = 4;
            // 
            // ConnectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 197);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.ip_addr_lbl);
            this.Controls.Add(this.userLabel);
            this.Name = "ConnectionPanel";
            this.Text = "ConnectionPanel";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label userLabel;
        private System.Windows.Forms.Label ip_addr_lbl;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TextBox userBox;
        private System.Windows.Forms.TextBox addressBox;
    }
}