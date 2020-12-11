using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HexEditor
{
    static class DataLoader
    {
        public static void LoadData(Panel target, byte[] source)
        {
            target.Controls.Clear();

            const int spacing = 0;
            int x = 0;
            int y = 0;
            int tabIndex = 1;

            foreach (var b in source)
            {
                var ctrl = new ByteControl(b);
                ctrl.Location = new Point(x, y);
                ctrl.TabIndex = tabIndex;
                ctrl.TabStop = true;
                target.Controls.Add(ctrl);

                tabIndex++;
                x += ctrl.Size.Width + spacing; ;
                if (x + ctrl.Size.Width > target.Size.Width)
                {
                    x = 0;
                    y += ctrl.Size.Height;
                }
            }
        }
    }
}
