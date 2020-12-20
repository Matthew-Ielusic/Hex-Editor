﻿using System;
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

        private void MainWindow_Load(object sender, EventArgs e)
        {
            byte[] data = { 41, 185, 193, 243, 73, 16, 80, 18, 65, 152, 192, 209, 79, 167, 35, 41, 173, 232, 1, 190, 102, 27, 162, 45, 148, 225, 236, 222, 222, 218, 5, 26, 111, 122, 164, 106, 103, 38, 5, 177, 106, 202, 7, 170, 37, 75, 6, 118, 236, 240, 141, 152, 57, 51, 196, 242, 78, 150, 245, 6, 91, 173, 168, 117, 60, 4, 150, 28, 155, 96, 136, 150, 221, 61, 15, 109, 146, 98, 131, 54, 20, 223, 109, 125, 199, 139, 89, 249, 125, 4, 34, 77, 183, 85, 206, 23, 206, 221, 89, 18, 114, 111, 241, 52, 35, 98, 203, 177, 200, 101, 17, 13, 14, 181, 122, 192, 223, 43, 245, 19, 185, 237, 193, 31, 179, 25, 55, 141, 125, 223, 3, 68, 118, 113, 240, 207, 59, 243, 178, 80, 102, 151, 64, 112, 66, 200, 93, 57, 134, 35, 143, 120, 175, 93, 186, 182, 221, 130, 238, 194, 148, 59, 138, 86, 109, 159, 186, 14, 48, 76, 222, 182, 187, 253, 31, 108, 226, 23, 220, 149, 244, 91, 111, 24, 69, 229, 228, 44, 173, 151, 176, 12, 42, 198, 111, 65, 200, 71, 235, 98, 46, 204, 59, 102, 174, 150, 60, 60, 97, 24, 21, 199, 38, 39, 207, 231, 48, 47, 201, 10, 68, 168, 182, 213, 231, 40, 123, 104, 211, 49, 195, 176, 219, 93, 46, 220, 100, 224, 115, 100, 110, 188, 84, 197, 184, 196, 26, 155, 112, 45, 127, 109, 111, 50, 96, 84, 138, 225, 26, 207, 94, 156, 158, 237, 241, 211, 161, 118, 242, 51, 164, 88, 133, 81, 91, 102, 251, 141, 25, 170, 133, 185, 145, 63, 200, 66, 56, 175, 246, 219, 134, 222, 38, 230, 158, 150, 163, 135, 233, 126, 177, 74, 249, 244, 80, 22, 48, 88, 177, 32, 166, 188, 131, 75, 59, 216, 134, 7, 4, 197, 140, 56, 246, 2, 161, 5, 17, 90, 105, 143, 206, 5, 232, 204, 155, 78, 23, 117, 7, 225, 157, 146, 45, 32, 14, 146, 137, 172, 153, 71, 129, 177, 79, 131, 31, 142, 64, 196, 168, 177, 193, 49, 215, 156, 100, 245, 205, 229, 38, 202, 41, 98, 115, 21, 80, 253, 79, 139, 193, 23, 202, 104, 16, 24, 241, 45, 46, 22, 182, 112, 232, 206, 99, 34, 0, 138, 97, 190, 193, 150, 160, 248, 143, 194, 86, 204, 8, 236, 204, 217, 121, 128, 84, 4, 15, 156, 206, 89, 22, 228, 119, 51, 41, 103, 150, 219, 65, 191, 113, 47, 30, 44, 193, 29, 238, 53, 228, 206, 139, 152, 201, 219, 150, 89, 70, 195, 178, 97, 127, 117, 131, 78, 167, 45, 161, 134, 96, 14, 28, 232, 88, 163, 159, 133, 117, 204, 126, 52, 146, 11, 21, 193, 64, 26, 9, 53, 128, 77, 37, 158, 9, 87, 110, 50, 210, 216, 89, 131, 241, 176, 214, 3, 18, 156, 77, 65, 50, 0, 79, 121, 4, 227, 139, 194, 42, 175, 118, 50, 48, 199, 77, 125, 88, 186, 122, 107, 203, 39, 222, 125, 59, 100, 71, 44, 61, 229, 3, 41, 180, 226, 131, 166, 102, 167, 191, 181, 149, 107, 251, 250, 44, 195, 191, 213, 138, 232, 216, 88, 139, 167, 50, 114, 139, 148, 90, 188, 170, 111, 210, 219, 114, 187, 133, 101, 253, 4, 146, 106, 93, 65, 202, 57, 46, 49, 143, 179, 183, 210, 94, 3, 120, 155, 28, 61, 10, 73, 87, 251, 233, 235, 157, 154, 181, 104, 206, 52, 18, 151, 22, 59, 107, 136, 239, 194, 235, 68, 232, 214, 124, 36, 61, 62, 166, 72, 109, 16, 127, 2, 225, 111, 99, 212, 182, 171, 77, 70, 79, 154, 204, 79, 204, 206, 215, 94, 89, 159, 47, 209, 107, 243, 135, 46, 17, 170, 240, 200, 137, 168, 109, 227, 122, 79, 187, 96, 246, 193, 170, 247, 147, 247, 28, 1, 124, 236, 107, 86, 35, 108, 192, 200, 91, 132, 204, 117, 233, 29, 64, 170, 14, 39, 97, 232, 131, 224, 205, 86, 155, 10, 93, 238, 143, 237, 156, 201, 122, 78, 171, 147, 180, 150, 155, 240, 68, 176, 122, 64, 124, 7, 58, 232, 226, 250, 255, 110, 82, 98, 163, 41, 250, 61, 249, 45, 172, 169, 96, 24, 172, 230, 132, 205, 42, 56, 237, 94, 20, 118, 56, 221, 183, 122, 147, 121, 183, 57, 49, 239, 133, 121, 128, 29, 15, 22, 104, 156, 74, 95, 61, 96, 142, 209, 56, 175, 87, 6, 67, 177, 6, 40, 179, 61, 157, 111, 232, 244, 35, 95, 71, 40, 40, 202, 12, 123, 19, 81, 142, 36, 204, 16, 74, 140, 35, 130, 212, 66, 239, 20, 191, 123, 188, 57, 28, 113, 103, 28, 58, 21, 21, 105, 13, 54, 115, 150, 98, 175, 33, 77, 46, 213, 231, 115, 109, 98, 188, 212, 84, 169, 196, 186, 182, 21, 150, 18, 4, 143, 248, 90, 228, 42, 5, 121, 210, 227, 137, 108, 234, 50, 183, 15, 82, 151, 212, 4, 190, 211, 40, 32, 65, 6, 87, 104, 11, 203, 240, 245, 81, 97, 14, 110, 182, 75, 246, 143, 128, 119, 130, 220, 233, 35, 69, 66, 82, 208, 175, 152, 237, 118, 224, 19, 113, 203, 140, 77, 125, 155, 91, 228, 143, 191, 132, 66, 150, 200, 84, 37, 7, 70, 147, 102, 60, 2, 85, 215, 166, 232, 75, 30, 131, 126, 216, 102, 208, 157, 99, 99, 93, 70, 16, 100, 211, 185, 23, 31, 51, 194, 88, 153, 62, 235, 221, 245, 114, 224, 200, 207, 12, 170, 47, 28, 38, 53, 141, 124, 188, 186, 228, 32, 203, 145, 87, 59, 199, 89, 95, 38, 151, 99, 52, 226, 114, 240, 89, 9, 211, 19, 132, 110, 183, 58, 170, 141, 35, 20, 43, 224, 179, 21, 45, 100, 144, 104, 4, 12, 135, 100, 100, 127, 185, 46, 53, 236 };
            DataLoader.LoadData(dataPanel, data);
            byteDataPanel1.SetBytes(data);
        }
    }
}
