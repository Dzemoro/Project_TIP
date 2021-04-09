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
            this.exitButton = new System.Windows.Forms.Button();
            this.miniButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.connectBtn.BackColor = System.Drawing.Color.DimGray;
            this.connectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.connectBtn.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.connectBtn.Location = new System.Drawing.Point(181, 149);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(86, 27);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Connect";
            this.connectBtn.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.connectBtn.UseVisualStyleBackColor = false;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // userBox
            // 
            this.userBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.userBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.userBox.Location = new System.Drawing.Point(123, 47);
            this.userBox.Name = "userBox";
            this.userBox.Size = new System.Drawing.Size(144, 26);
            this.userBox.TabIndex = 3;
            // 
            // addressBox
            // 
            this.addressBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.addressBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addressBox.Location = new System.Drawing.Point(123, 96);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(144, 26);
            this.addressBox.TabIndex = 4;
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.DimGray;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Location = new System.Drawing.Point(273, -1);
            this.exitButton.Margin = new System.Windows.Forms.Padding(0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(25, 23);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "X";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // miniButton
            // 
            this.miniButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.miniButton.BackColor = System.Drawing.Color.DimGray;
            this.miniButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.miniButton.FlatAppearance.BorderSize = 0;
            this.miniButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.miniButton.Location = new System.Drawing.Point(250, -1);
            this.miniButton.Margin = new System.Windows.Forms.Padding(3, 0, 0, 3);
            this.miniButton.Name = "miniButton";
            this.miniButton.Size = new System.Drawing.Size(23, 23);
            this.miniButton.TabIndex = 6;
            this.miniButton.Text = "_";
            this.miniButton.UseVisualStyleBackColor = false;
            this.miniButton.Click += new System.EventHandler(this.miniButton_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(252, 23);
            this.panel1.TabIndex = 7;
            // 
            // ConnectionPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(294, 197);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.miniButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.userBox);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.ip_addr_lbl);
            this.Controls.Add(this.userLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
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
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button miniButton;
        private System.Windows.Forms.Panel panel1;
    }
}