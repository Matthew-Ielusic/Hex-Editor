namespace HexEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.openButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.dataPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.byteDataPanel1 = new HexEditor.ByteDataPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(51, 161);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(83, 46);
            this.openButton.TabIndex = 0;
            this.openButton.Text = "Open File";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(51, 41);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(83, 39);
            this.resetButton.TabIndex = 0;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // dataPanel
            // 
            this.dataPanel.AutoScroll = true;
            this.dataPanel.AutoScrollMinSize = new System.Drawing.Size(0, 416);
            this.dataPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dataPanel.Location = new System.Drawing.Point(230, 23);
            this.dataPanel.Name = "dataPanel";
            this.dataPanel.Size = new System.Drawing.Size(578, 415);
            this.dataPanel.TabIndex = 1;
            // 
            // saveButton
            // 
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point(51, 223);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(83, 46);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.openButton);
            this.groupBox1.Controls.Add(this.resetButton);
            this.groupBox1.Controls.Add(this.saveButton);
            this.groupBox1.Location = new System.Drawing.Point(14, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(187, 309);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Controls";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(587, 462);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "(Original Implementation)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1040, 465);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(198, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "(Experimental Implementation)";
            // 
            // byteDataPanel1
            // 
            this.byteDataPanel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.byteDataPanel1.Bytes = ((System.Collections.Generic.List<byte>)(resources.GetObject("byteDataPanel1.Bytes")));
            this.byteDataPanel1.Location = new System.Drawing.Point(890, 23);
            this.byteDataPanel1.Name = "byteDataPanel1";
            this.byteDataPanel1.Size = new System.Drawing.Size(578, 415);
            this.byteDataPanel1.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1489, 491);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.byteDataPanel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dataPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainWindow";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Panel dataPanel;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private ByteDataPanel byteDataPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

