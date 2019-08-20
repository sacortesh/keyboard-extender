using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class TopBehavior : BehaviorBase
    {
        public TopBehavior() : base("top", 3)
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
                    MoveTopWithFullSize();
                    break;
                case 2:
                    MoveTopWithThirdSize();
                    break;
                case 3:
                    MoveTopWithTwoThirdsSize();
                    break;
                default:
                    break;
            }
        }

        private void MoveTopWithTwoThirdsSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, 2*(width /3), height/2, true);

        }

        private void MoveTopWithThirdSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;


            WindowsManagementExternals.MoveWindow(activeWindow, left+offset, top, newWidth, height / 2, true);
        }

        private void MoveTopWithFullSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width, height / 2, true);

        }

        
    }
}
