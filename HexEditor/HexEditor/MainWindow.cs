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

            button2_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = dialog.FileName;
                    LoadFile(fileName);
                }
            }
        }

        private void LoadFile(string fileName)
        {
            using (BinaryReader br = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                int bytesToRead = 100;
                var data = br.ReadBytes(bytesToRead);
                DataLoader.LoadData(dataPanel, data);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            const string name = "C:\\Users\\Matt\\Documents\\0_MyActualDocuments\\Programming\\data.txt";

            LoadFile(name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            byte[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 0xa, 0xb, 0xc, 0xd, 0xe, 0xf };
            DataLoader.LoadData(dataPanel, data);
        }
    }
}
