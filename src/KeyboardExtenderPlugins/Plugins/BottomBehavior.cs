using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class BottomBehavior : BehaviorBase
    {
        public BottomBehavior() : base("bottom", 3)
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
                    MoveBottomWithFullWidthSize();
                    break;
                case 2:
                    MoveBottomWithThirdWidthSize();
                    break;
                case 3:
                    MoveBottomWithTwoThirdsWidthSize();
                    break;
                default:
                    break;
            }
        }

        private void MoveBottomWithTwoThirdsWidthSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top + offsetVertical, 2*(width /3), height/2, true);

        }

        private void MoveBottomWithThirdWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left+offset, top + offsetVertical, newWidth, height / 2, true);
        }

        private void MoveBottomWithFullWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left, top + offsetVertical, width, height / 2, true);

        }

        
    }
}
