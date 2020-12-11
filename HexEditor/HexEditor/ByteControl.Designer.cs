namespace HexEditor
{
    partial class ByteControl
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
            this.dataLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLabel.Location = new System.Drawing.Point(0, 0);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(34, 22);
            this.dataLabel.TabIndex = 0;
            this.dataLabel.Text = "__";
            this.dataLabel.Click += new System.EventHandler(this.dataLabel_Click);
            // 
            // ByteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Controls.Add(this.dataLabel);
            this.Name = "ByteControl";
            this.Size = new System.Drawing.Size(34, 30);
            this.Enter += new System.EventHandler(this.ByteControl_Enter);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ByteControl_KeyPress);
            this.Leave += new System.EventHandler(this.ByteControl_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dataLabel;
    }
}
