using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;

namespace HexEditor
{
    public partial class ByteDataPanel : UserControl
    {
        const int FONT_SIZE = 12;

        private Font font = new Font(FontFamily.GenericMonospace, FONT_SIZE); // TODO: Dispose of `font` when this object is slated to be disposed of


        private List<byte> _bytes;
        public List<byte> Bytes
        {
            get { return _bytes; }
            set
            {
                if (value != null)
                    _bytes = value;
                else
                    _bytes = new List<byte>();
                RecalculateVerticalScroll();
                Invalidate();
            }
        }
        const double BYTES_PER_LINE = 16;

        private void RecalculateVerticalScroll()
        {
            int numberOfLines = (int)(Math.Ceiling(Bytes.Count / BYTES_PER_LINE));
            int difference = Math.Max(0, numberOfLines - CompletelyDisplayedLines);
            verticalScroll.Maximum = difference;
            verticalScroll.Enabled = difference > 0;
        }

        private int CompletelyDisplayedLines
        {
            get { return Size.Height / font.Height; }
        }

        public int LineCount
        {
            get
            {
                double quotient = (double)Bytes.Count / BYTES_PER_LINE;
                if (quotient > Math.Floor(quotient))
                {
                    return 1 + (int)quotient;
                }
                else
                {
                    return (int)quotient;
                }
            }
        }

        public ByteDataPanel()
        {
            InitializeComponent();

            Bytes = new List<byte>();
        }

        public void SetBytes(byte[] newData)
        {
            Bytes = new List<byte>(newData);
        }

        private void ByteDataPanel_Scroll(object sender, ScrollEventArgs e)
        {
            this.verticalScroll.Value = e.NewValue;
        }

        private void ByteDataPanel_Paint(object sender, PaintEventArgs e)
        {
            const int spacing = 0;
            int x = 0;
            int y = -(verticalScroll.Value * font.Height);
            int tabIndex = 1;

            int byteWidth = (int)(font.Size * 2); // Size seems to be slightly larger than the size of a single character (even for this monospaced font)
            foreach (var b in Bytes)
            {
                e.Graphics.DrawString(b.ToString("x2"), font, Brushes.Black, x, y);

                x += byteWidth;
                if (x + byteWidth + verticalScroll.Width > Size.Width)
                {
                    x = 0;
                    y += font.Height;
                }
            }
        }

        private void verticalScroll_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}