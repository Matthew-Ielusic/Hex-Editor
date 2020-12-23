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
        /* Class Constants */
        private const int BYTES_PER_LINE = 16;
        private const int FONT_SIZE = 16;
        private static readonly Brush TEXT_BRUSH = Brushes.Black;
        private static readonly Brush SELECTED_BACKGROUND = Brushes.White;

        private const double COURIER_NEW_ASPECT_RATIO = .42;
        private const int BYTE_SPACING = 10;
        private static readonly Font font = new Font(new FontFamily("Courier New"), FONT_SIZE, GraphicsUnit.Pixel); // TODO: Dispose of `font` when this object is slated to be disposed of

        /* End Class Constants */

        /* Instance Fields */

        private int? selectedNibble = null;

        private List<byte> _data = new List<byte>();
        private List<byte> Data
        {
            get { return _data; }
            set
            {
                _data = value ?? new List<byte>();
                RecalculateVerticalScroll();
                Invalidate();
            }
        }

        private int? _selectedIndex = null;
        private int? SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (!value.HasValue || (0 <= value && value <= Data.Count))
                {
                    _selectedIndex = value;
                    selectedNibble = 4 | (value & 0); // Set selectedNibble to null if value is null; else set it to 4
                    EnsureSelectedIsVisible();
                    Invalidate();
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        private Dictionary<Keys, Action> inputKeyBehavior; // Initialized in the constructor

        /* End Instance Fields */

        /* Helper Derived Properties */
        private int? SelectedLine
        {
            get
            {
                return SelectedIndex / BYTES_PER_LINE;
            }
        }

        private int? SelectedColumn
        {
            get
            {
                return SelectedIndex % BYTES_PER_LINE;
            }
        }

        private int LineCount
        {
            get
            {
                double quotient = (double)Data.Count / BYTES_PER_LINE;
                if (quotient > Math.Floor(quotient))
                {
                    return 1 + (int)Math.Floor(quotient);
                }
                else
                {
                    return (int)Math.Floor(quotient);
                }
            }
        }

        private int ByteHeight
        {
            get { return font.Height; }
        }
        private int ByteWidth
        {
            get { return (int)(2 * font.Size * (1 - COURIER_NEW_ASPECT_RATIO)); }
        }

        /* End Helper Derived Properties */

        /* Public */
        public ByteDataPanel()
        {
            InitializeComponent();

            this.inputKeyBehavior = new Dictionary<Keys, Action>()
            {
                [Keys.Right] = IncrementSelectedColumn,
                [Keys.Left] = DecrementSelectedColumn,
                [Keys.Up] = DecrementSelectedRow,
                [Keys.Down] = IncrementSelectedRow
            };
        }

        public byte[] AssembleData()
        {
            return Data.ToArray();
        }

        public void SetData(byte[] newData)
        {
            Data = new List<byte>(newData);
        }

        /* End Public */

        /* Event Handlers */

        private void ByteDataPanel_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = e.Delta;
            int deltaThreshold = SystemInformation.MouseWheelScrollDelta;
            int direction = (delta > 0) ? 1 : -1;
            while (delta * direction >= deltaThreshold)
            {
                delta -= direction * deltaThreshold;
                int newScroll = verticalScroll.Value - (SystemInformation.MouseWheelScrollLines * direction);
                newScroll = Math.Max(verticalScroll.Minimum, Math.Min(verticalScroll.Maximum, newScroll));
                // Clamp newScroll to be be between the Minimum and Maximum vertical scrolls
                // (VScrollBar throws an exception if an out-of-range value is passed to its Value property
                verticalScroll.Value = newScroll;
            }
        }

        private void ByteDataPanel_Scroll(object sender, ScrollEventArgs e)
        {
            this.verticalScroll.Value = e.NewValue;
        }

        private void ByteDataPanel_Paint(object sender, PaintEventArgs e)
        {
            int startingLine = (int)Math.Ceiling( (double)verticalScroll.Value / ByteHeight ) - 1;
            // IE, startingLine is the smallest int `i` such that (i * ByteHeight) + ByteHeight >= Value
            int endingLine = startingLine + (int)Math.Ceiling( (double)Size.Height / ByteHeight );
            // IE, endingLine is startingLine plus the largest int `j` such that `j` lines of data fit in the control
            endingLine = Math.Min(endingLine, LineCount - 1); // Friggin' off-by-one errors related to lines being implicitly zero-indexed

            if (SelectedIndex.HasValue)
            {
                const int FUDGE_FACTOR = 3;
                int drawX = SelectedColumn.Value * (ByteWidth + BYTE_SPACING) + FUDGE_FACTOR;
                int drawY = (SelectedLine.Value * ByteHeight) - verticalScroll.Value;
                Point start = new Point(drawX, drawY);
                Rectangle background = new Rectangle(start, new Size(ByteWidth + FUDGE_FACTOR, ByteHeight));

                e.Graphics.FillRectangle(SELECTED_BACKGROUND, background);
            }

            for (int i = startingLine; i <= endingLine; i++)
            {
                int startIndex = i * BYTES_PER_LINE;
                int endIndex = startIndex + BYTES_PER_LINE - 1;
                int drawX = 0;
                int drawY = (i * ByteHeight) - verticalScroll.Value;
                var lineData = Data.Skip(startIndex).Take(BYTES_PER_LINE);
                foreach (byte b in lineData)
                {
                    e.Graphics.DrawString(b.ToString("x2"), font, TEXT_BRUSH, drawX, drawY);
                    drawX += ByteWidth + BYTE_SPACING;
                }
            }
        }

        private void verticalScroll_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void ByteDataPanel_MouseClick(object sender, MouseEventArgs e)
        {
            int lineIndex = (e.Y + verticalScroll.Value) / ByteHeight;

            int w = ByteWidth;
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



        private void ByteDataPanel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (selectedNibble.HasValue)
            {
                char keyChar = Char.ToLower(e.KeyChar);

                int? nibbleValue = null;
                if (Char.IsNumber(keyChar))
                {
                    nibbleValue = keyChar - '0';
                }
                else if ('a' <= keyChar && keyChar <= 'f')
                {
                    nibbleValue = keyChar - 'a' + 0xa;
                }

                if (nibbleValue.HasValue)
                {
                    SetCurrentNibble(nibbleValue.Value);
                    e.Handled = true;
                }
            }
        }

        private void ByteDataPanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (inputKeyBehavior.TryGetValue(e.KeyCode, out Action action))
            {
                action.Invoke();
            }
        }

        private void ByteDataPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            e.IsInputKey = inputKeyBehavior.ContainsKey(e.KeyCode);
        }

        /* End Event Handlers */

        /* Helper Methods */

        private void SetCurrentNibble(int value)
        {
            int shiftAmount = selectedNibble.Value;
            int dataMask = 0xf << (4 - shiftAmount);


            Data[SelectedIndex.Value] = (byte)((Data[SelectedIndex.Value] & dataMask) | (value << shiftAmount));
            selectedNibble = 4 - selectedNibble;
            Invalidate();
        }

        private void EnsureSelectedIsVisible()
        {
            if (SelectedIndex.HasValue)
            {
                int minVisibleLine = (int)Math.Ceiling((double)verticalScroll.Value / ByteHeight);
                int maxVisibleLine = -1 + (int)Math.Floor((double)(verticalScroll.Value + Size.Height) / ByteHeight);
                if (SelectedLine < minVisibleLine)
                {
                    verticalScroll.Value = ByteHeight * SelectedLine.Value;
                }
                else if (SelectedLine > maxVisibleLine)
                {
                    verticalScroll.Value = Math.Max(0, ByteHeight * (SelectedLine.Value + 1) - Size.Height);
                }
            }
        }

        private void RecalculateVerticalScroll()
        {
            int totalRenderHeight = LineCount * ByteHeight;
            verticalScroll.Maximum = Math.Max(0, totalRenderHeight - Size.Height + ByteHeight);
            verticalScroll.Enabled = verticalScroll.Maximum > 0;
            verticalScroll.Value = 0;
        }

        private void DecrementSelectedColumn()
        {
            if (SelectedIndex > 0)
                SelectedIndex--;
        }

        private void IncrementSelectedColumn()
        {
            if (SelectedIndex < Data.Count)
                SelectedIndex++;
        }

        private void DecrementSelectedRow()
        {
            if (SelectedIndex >= BYTES_PER_LINE)
                SelectedIndex -= BYTES_PER_LINE;
        }

        private void IncrementSelectedRow()
        {
            if (SelectedIndex + BYTES_PER_LINE < Data.Count)
                SelectedIndex += BYTES_PER_LINE;
        }

        /* End Helper Methods */
    }
}