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
        private const int BYTES_PER_LINE = 16;

        private void RecalculateVerticalScroll()
        {
            int totalRenderHeight = LineCount * font.Height;
            verticalScroll.Maximum = Math.Max(0, totalRenderHeight - Size.Height + font.Height);
            verticalScroll.Enabled = verticalScroll.Maximum > 0;
            verticalScroll.Value = 0;
        }

        private int CompletelyDisplayedLines
        {
            get { return Size.Height / font.Height; }
        }

        private int LineCount
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
            int y = -verticalScroll.Value;

            for (int i = 0; i < LineCount; i++)
            {
                int startIndex = i * BYTES_PER_LINE;
                int endIndex = startIndex + BYTES_PER_LINE - 1;
                {
                    var lineData = Bytes.Skip(startIndex).Take(BYTES_PER_LINE);
                    string lineView = lineData.Select(b => b.ToString("x2"))
                                              .Aggregate((a, b) => a + " " + b);
                    e.Graphics.DrawString(lineView, font, Brushes.Black, 0, y);
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