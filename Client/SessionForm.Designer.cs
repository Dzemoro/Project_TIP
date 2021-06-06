
namespace ClientApp
{
    public partial class SessionForm
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
            this.discButton = new System.Windows.Forms.Button();
            this.micGroup = new System.Windows.Forms.GroupBox();
            this.audioPanel = new System.Windows.Forms.Panel();
            this.audioSList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.startSbutton = new System.Windows.Forms.Button();
            this.refreshB = new System.Windows.Forms.Button();
            this.micGroup.SuspendLayout();
            this.audioPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Talking to Michael";
            // 
            // discButton
            // 
            this.discButton.Location = new System.Drawing.Point(12, 291);
            this.discButton.Name = "discButton";
            this.discButton.Size = new System.Drawing.Size(83, 29);
            this.discButton.TabIndex = 2;
            this.discButton.Text = "Disconnect";
            this.discButton.UseVisualStyleBackColor = true;
            this.discButton.Click += new System.EventHandler(this.discButton_Click);
            // 
            // micGroup
            // 
            this.micGroup.Controls.Add(this.audioPanel);
            this.micGroup.Controls.Add(this.startSbutton);
            this.micGroup.Controls.Add(this.refreshB);
            this.micGroup.Location = new System.Drawing.Point(12, 41);
            this.micGroup.Name = "micGroup";
            this.micGroup.Size = new System.Drawing.Size(453, 244);
            this.micGroup.TabIndex = 10;
            this.micGroup.TabStop = false;
            this.micGroup.Text = "Microphone Settings";
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
            this.audioSList.BackColor = System.Drawing.SystemColors.InactiveCaption;
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
            // startSbutton
            // 
            this.startSbutton.Location = new System.Drawing.Point(299, 62);
            this.startSbutton.Name = "startSbutton";
            this.startSbutton.Size = new System.Drawing.Size(128, 29);
            this.startSbutton.TabIndex = 8;
            this.startSbutton.Text = "Unmute";
            this.startSbutton.UseVisualStyleBackColor = true;
            this.startSbutton.Click += new System.EventHandler(this.StartSbutton_Click);
            // 
            // refreshB
            // 
            this.refreshB.Location = new System.Drawing.Point(299, 27);
            this.refreshB.Name = "refreshB";
            this.refreshB.Size = new System.Drawing.Size(128, 29);
            this.refreshB.TabIndex = 7;
            this.refreshB.Text = "Refresh sources";
            this.refreshB.UseVisualStyleBackColor = true;
            this.refreshB.Click += new System.EventHandler(this.RefreshB_Click);
            // 
            // SessionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSlateGray;
            this.ClientSize = new System.Drawing.Size(545, 328);
            this.Controls.Add(this.micGroup);
            this.Controls.Add(this.discButton);
            this.Controls.Add(this.label1);
            this.Name = "SessionForm";
            this.Text = "SessionForm";
            this.micGroup.ResumeLayout(false);
            this.audioPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button discButton;
        private System.Windows.Forms.GroupBox micGroup;
        private System.Windows.Forms.Panel audioPanel;
        private System.Windows.Forms.ListView audioSList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button startSbutton;
        private System.Windows.Forms.Button refreshB;
    }
}