using System;
using System.Drawing;
using System.Windows.Forms;

namespace HexEditor
{
    public partial class ByteControl : UserControl
    {
        public static readonly Color SELECTED_BACK_COLOR = Color.White;
        public static readonly Color SELECTED_TEXT_COLOR = Color.Black;

        public readonly Color DEFAULT_BACK_COLOR;
        public readonly Color DEFAULT_TEXT_COLOR;

        private int currentNibbleIndex = 4;

        private byte _data;
        public byte Data
        {
            get { return _data; }
            set
            {
                _data = value;
                dataLabel.Text = value.ToString("x2");
            }
        }

        private bool _selected = false;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if (value)
                {
                    ColorToSelected();
                } else
                {
                    ColorToUnselected();
                }
            }
        }

        public ByteControl(byte data)
        {
            InitializeComponent();
            Data = data;
         
            DEFAULT_BACK_COLOR = BackColor;
            DEFAULT_TEXT_COLOR = dataLabel.ForeColor;
        }

        private void ColorToSelected()
        {
            BackColor           = SELECTED_BACK_COLOR;
            dataLabel.ForeColor = SELECTED_TEXT_COLOR;
        }

        private void ColorToUnselected()
        {
            BackColor           = DEFAULT_BACK_COLOR;
            dataLabel.ForeColor = DEFAULT_TEXT_COLOR;
        }

        private void dataLabel_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void ByteControl_Enter(object sender, EventArgs e)
        {
            Selected = true;
        }

        private void ByteControl_Leave(object sender, EventArgs e)
        {
            Selected = false;
        }

        private void setCurrentNibble(int value)
        {
            // Sets the most signifigant nibble if currentNibbleIndex is 1; set the least significant nibble if currentNibbleIndex is 0
            int shiftAmount = currentNibbleIndex;
            int dataMask = 0xf << (4 - shiftAmount);

            
            Data = (byte)( (value << shiftAmount) | (Data & dataMask) );
            currentNibbleIndex = 4 - currentNibbleIndex;
        }

        private void ByteControl_KeyPress(object sender, KeyPressEventArgs e)
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
                setCurrentNibble(nibbleValue.Value);
            }
        }
    }
}
