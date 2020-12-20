namespace HexEditor
{
    partial class ByteDataPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verticalScroll = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // verticalScroll
            // 
            this.verticalScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.verticalScroll.Location = new System.Drawing.Point(648, 0);
            this.verticalScroll.Name = "verticalScroll";
            this.verticalScroll.Size = new System.Drawing.Size(17, 415);
            this.verticalScroll.TabIndex = 0;
            this.verticalScroll.ValueChanged += new System.EventHandler(this.verticalScroll_ValueChanged);
            // 
            // ByteDataPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.verticalScroll);
            this.Name = "ByteDataPanel";
            this.Size = new System.Drawing.Size(665, 415);
            this.Scroll += new System.Windows.Forms.ScrollEventHandler(this.ByteDataPanel_Scroll);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ByteDataPanel_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.VScrollBar verticalScroll;
    }
}
