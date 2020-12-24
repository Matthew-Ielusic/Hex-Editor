using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexEditor
{
    internal class TrailerByte
    {
        private int? _savedNibble = null;
        public int? SavedNibble
        {
            get { return _savedNibble; }
            set
            {
                if (0 > value || value > 15)
                {
                    throw new ArgumentException();
                }

                _savedNibble = value;
            }
        }
        private const string PLACEHOLDER = "_";

        public string Display
        {
            get { return (SavedNibble?.ToString("x1") ?? PLACEHOLDER) + PLACEHOLDER; }
        }

        public bool CanAcceptNibble
        {
            get { return SavedNibble == null; }
        }

        public void Reset()
        {
            SavedNibble = null;
        }
    }
}
