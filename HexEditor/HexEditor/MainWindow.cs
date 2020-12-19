using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexEditor
{
    public partial class MainWindow : Form
    {
        private ByteControl selectedControl = new ByteControl(0);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    LoadFile(fileName);
                    saveButton.Enabled = true;
                }
            }
        }

        private void LoadFile(string fileName)
        {
            using (FileStream fs = File.Open(fileName, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                var data = br.ReadBytes((int)fs.Length);
                DataLoader.LoadData(dataPanel, data);
                byteDataPanel1.SetBytes(data);
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            dataPanel.Controls.Clear();
            saveButton.Enabled = false;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var bytesQuery = from Control ctrl in dataPanel.Controls select ((ByteControl)ctrl).Data;
                byte[] data = bytesQuery.ToArray();
                System.IO.File.WriteAllBytes(dialog.FileName, data);
            }


        }
    }
}
