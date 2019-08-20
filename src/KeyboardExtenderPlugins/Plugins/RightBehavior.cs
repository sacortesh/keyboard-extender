using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class RightBehavior : BehaviorBase
    {
        public RightBehavior() : base("right", 3)
        {

        }

        public override void Behavior(int modifier)
        {
            base.Behavior(modifier);

            //my new behavior;
            //validate modifier
            switch (modifier)
            {
                case 1:
                    MoveRightWithHalfWidthSize();
                    break;
                case 2:
                    MoveRightWithThirdWidthSize();
                    break;
                case 3:
                    MoveRightWithTwoThirdsWidthSize();
                    break;
                default:
                    break;
            }
        }

        private void MoveRightWithTwoThirdsWidthSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, 2 * (width / 3), height, true);

        }

        private void MoveRightWithThirdWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace;


            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, newWidth, height, true);
        }

        private void MoveRightWithHalfWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 2);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, newWidth, height, true);

        }


    }
}
