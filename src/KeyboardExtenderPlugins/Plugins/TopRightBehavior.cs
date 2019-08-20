using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class TopRightBehavior : BehaviorBase
    {
        public TopRightBehavior() : base("top-right", 3)
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
                    MoveTopRightWithHalfWidthSize();
                    break;
                case 2:
                    MoveTopRightWithThirdWidthSize();
                    break;
                case 3:
                    MoveTopRightWithTwoThirdsWidthSize();
                    break;
                default:
                    break;
            }
        }

        private void MoveTopRightWithTwoThirdsWidthSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace ;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, 2*(width /3), height/2, true);

        }

        private void MoveTopRightWithThirdWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace ;


            WindowsManagementExternals.MoveWindow(activeWindow, left+offset, top, newWidth, height / 2, true);
        }

        private void MoveTopRightWithHalfWidthSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 2);
            int missingSpace = width - newWidth;
            int offset = missingSpace;

            WindowsManagementExternals.MoveWindow(activeWindow, left+ offset, top, width, height / 2, true);

        }

        
    }
}
