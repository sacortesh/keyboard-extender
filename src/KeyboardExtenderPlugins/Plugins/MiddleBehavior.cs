using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class MiddleBehavior : BehaviorBase
    {
        public MiddleBehavior() : base("middle", 3)
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
                    FillScreenCentered();
                    break;
                case 2:
                    FillScreenCenteredWithThirdSize();
                    break;
                case 3:
                    FillScreenCenteredWithTwoThirdsSize();
                    break;
                default:
                    break;
            }
        }

        private void FillScreenCenteredWithTwoThirdsSize()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = 2 * (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left + offset, top, 2*(width /3), height, true);

        }

        private void FillScreenCenteredWithThirdSize()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int newWidth = (width / 3);
            int missingSpace = width - newWidth;
            int offset = missingSpace / 2;


            WindowsManagementExternals.MoveWindow(activeWindow, left+offset, top, newWidth, height, true);
        }

        private void FillScreenCentered()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width, height, true);

        }

        
    }
}
