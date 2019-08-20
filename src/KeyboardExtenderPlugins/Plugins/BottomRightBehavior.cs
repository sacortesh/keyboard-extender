using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class BottomRightBehavior : BehaviorBase
    {
        public BottomRightBehavior() : base("bottom-right", 3)
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
                    MoveBottomRightWithHalfWidthSize();
                    break;
                case 2:
                    MoveBottomRightWithThirdWidthSize();
                    break;
                case 3:
                    MoveBottomRightWithTwoThirdsWidthSize();
                    break;
                default:
                    break;
            }
        }

        private void MoveBottomRightWithTwoThirdsWidthSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            int verticalOffset = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top + verticalOffset, 2 * (width / 3), height / 2, true);

        }

        private void MoveBottomRightWithThirdWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            int verticalOffset = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top + verticalOffset, newWidth, height / 2, true);
        }

        private void MoveBottomRightWithHalfWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 2);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            int verticalOffset = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top + verticalOffset, width / 2, height / 2, true);

        }


    }
}
