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
        private const int FONT_SIZE = 16;
        private static readonly Brush TEXT_BRUSH = Brushes.Black;
        private static readonly Brush SELECTED_BACKGROUND = Brushes.White;

        private const double COURIER_NEW_ASPECT_RATIO = .42;
        private int BYTE_DRAW_WIDTH { get { return (int)(2 * font.Size * (1 - COURIER_NEW_ASPECT_RATIO)); } }
        private const int BYTE_SPACING = 10;
        private Font font = new Font(new FontFamily("Courier New"), FONT_SIZE, GraphicsUnit.Pixel); // TODO: Dispose of `font` when this object is slated to be disposed of
        
        private int ByteHeight
        {
            get { return font.Height; }
        }
        private int ByteWidth
        {
            get 
            {
                return -1 + (int)font.Size * 3 * 5 / 8; 
            }
            // Trial-and-error has shown that the font's width is five-eighths of the font height.
            // Multiply that by 3 because each byte is displayed by three characters -- one for each nibble and then a space between bytes
            // And then throw in a fudge factor.  Because "what is the width, in pixels, of a character of this monospace font" apparently has no answer.
        }

        private int? _selectedIndex;
        private int? SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (!value.HasValue || (0 <= value && value <= Bytes.Count))
                {
                    _selectedIndex = value;
                    Invalidate();
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        private int? SelectedLine
        {
            get
            {
                if (SelectedIndex.HasValue)
                {
                    return SelectedIndex / BYTES_PER_LINE;
                }
                else
                {
                    return null;
                }
            }
        }

        private int? SelectedColumn
        {
            get
            {
                if (SelectedIndex.HasValue)
                {
                    return SelectedIndex % BYTES_PER_LINE;
                }
                else
                {
                    return null;
                }
            }
        }

        private List<byte> _bytes = new List<byte>();
        public List<byte> Bytes
        {
            get { return _bytes; }
            set
            {
                _bytes = value ?? new List<byte>();
                RecalculateVerticalScroll();
                Invalidate();
            }
        }
        private const int BYTES_PER_LINE = 16;

        private void RecalculateVerticalScroll()
        {
            int totalRenderHeight = LineCount * ByteHeight;
            verticalScroll.Maximum = Math.Max(0, totalRenderHeight - Size.Height + ByteHeight);
            verticalScroll.Enabled = verticalScroll.Maximum > 0;
            verticalScroll.Value = 0;
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

            int startingLine = (int)Math.Ceiling( (double)verticalScroll.Value / ByteHeight ) - 1;
            // IE, startingLine is the smallest int `i` such that (i * ByteHeight) + ByteHeight >= Value
            int endingLine = startingLine + (int)Math.Ceiling( (double)Size.Height / ByteHeight );
            // IE, endingLine is startingLine plus the largest int `j` such that `j` lines of data fit in the control
            endingLine = Math.Min(endingLine, LineCount - 1); // Friggin' off-by-one errors related to lines being implicitly zero-indexed

            if (SelectedIndex.HasValue)
            {
                const int FUDGE_FACTOR = 3;
                int drawX = SelectedColumn.Value * (BYTE_DRAW_WIDTH + BYTE_SPACING) + FUDGE_FACTOR;
                int drawY = (SelectedLine.Value * ByteHeight) - verticalScroll.Value;
                Point start = new Point(drawX, drawY);
                Rectangle background = new Rectangle(start, new Size(BYTE_DRAW_WIDTH + FUDGE_FACTOR, ByteHeight));

                e.Graphics.FillRectangle(SELECTED_BACKGROUND, background);
            }

            for (int i = startingLine; i <= endingLine; i++)
            {
                int startIndex = i * BYTES_PER_LINE;
                int endIndex = startIndex + BYTES_PER_LINE - 1;
                int drawX = 0;
                int drawY = (i * ByteHeight) - verticalScroll.Value;
                var lineData = Bytes.Skip(startIndex).Take(BYTES_PER_LINE);
                foreach (byte b in lineData)
                {
                    e.Graphics.DrawString(b.ToString("x2"), font, TEXT_BRUSH, drawX, drawY);
                    drawX += BYTE_DRAW_WIDTH + BYTE_SPACING;
                }
            }
        }

        private void verticalScroll_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ByteDataPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int lineIndex = (e.Y - verticalScroll.Value) / ByteHeight;

            int w = BYTE_DRAW_WIDTH;
            int s = BYTE_SPACING;
            int k = (int)Math.Ceiling((double)(e.X - w) / (s + w));
            if (k <= e.X / (double)(w + s))
            {
                SelectedIndex = k + 16 * lineIndex;
            }
            else
            {
                SelectedIndex = null;
            }

            // OK, I understand that math above looks mysterious.
            // The idea is that the first byte in a line starts at pixel 0, and ends at pixel w
            // The second byte in a line starts at pixel w + s, and ends at pixel (w + s) + w
            // The third byte in a line starts at pixel 2(w + s), and ends at pixel 2(w + s) + s
            // And so on
            // So the user has clicked on a byte, iff there is an integer k such that k(w + s) <= e.X <= k(w + s) + s
            // If that k exists, it must be greater than (e.X - w) / (w + s), but less than e.X / (w + s)
            // (I leave deriving that inequality as an exercise for the reader.)
        }

        public void IncrementSelectedIndex()
        {
            if (SelectedIndex.HasValue && SelectedIndex < Bytes.Count)
            {
                SelectedIndex++;
            }
        }

        public void DecrementSelectedIndex()
        {
            if (SelectedIndex.HasValue && SelectedIndex > 0)
            {
                SelectedIndex--;
            }
        }
    }
}