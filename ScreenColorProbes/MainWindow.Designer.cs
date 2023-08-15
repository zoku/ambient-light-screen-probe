namespace ScreenColorGrabber
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.screenList = new System.Windows.Forms.ComboBox();
            this.horizontalScale = new System.Windows.Forms.TrackBar();
            this.verticalScale = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.horizontalScale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalScale)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            // 
            // screenList
            // 
            this.screenList.DropDownWidth = 150;
            this.screenList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.screenList.FormattingEnabled = true;
            this.screenList.Location = new System.Drawing.Point(63, 21);
            this.screenList.Name = "screenList";
            this.screenList.Size = new System.Drawing.Size(121, 21);
            this.screenList.TabIndex = 0;
            // 
            // horizontalScale
            // 
            this.horizontalScale.Location = new System.Drawing.Point(63, 506);
            this.horizontalScale.Maximum = 50;
            this.horizontalScale.Name = "horizontalScale";
            this.horizontalScale.Size = new System.Drawing.Size(500, 45);
            this.horizontalScale.TabIndex = 1;
            this.horizontalScale.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // verticalScale
            // 
            this.verticalScale.Location = new System.Drawing.Point(12, 12);
            this.verticalScale.Maximum = 50;
            this.verticalScale.Name = "verticalScale";
            this.verticalScale.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.verticalScale.Size = new System.Drawing.Size(45, 500);
            this.verticalScale.TabIndex = 2;
            this.verticalScale.TickStyle = System.Windows.Forms.TickStyle.Both;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 671);
            this.Controls.Add(this.verticalScale);
            this.Controls.Add(this.horizontalScale);
            this.Controls.Add(this.screenList);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Screen Color Probes";
            ((System.ComponentModel.ISupportInitialize)(this.horizontalScale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.verticalScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.ComboBox screenList;
        private System.Windows.Forms.TrackBar horizontalScale;
        private System.Windows.Forms.TrackBar verticalScale;
    }
}