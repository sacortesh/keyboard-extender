using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Models
{
    public class ScreenVO
    {

        public Screen screen = null;

        public int _minimumPositionX;
        public int _minimumPositionY;
        public int _maximumPositionX;
        public int _maximumPositionY;

        public ScreenVO(Screen scr)
        {
            screen = scr;

            this._minimumPositionX = scr.WorkingArea.Left;
            this._minimumPositionY = scr.WorkingArea.Top;
            this._maximumPositionX = scr.WorkingArea.Right;
            this._maximumPositionY = scr.WorkingArea.Bottom;
        }

        public override string ToString()
        {
            return screen.DeviceName;
        }

    }
}
