using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class TopLeftBehavior : BehaviorBase
    {
        public TopLeftBehavior() : base("top-left", 3)
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
                    MoveTopLeftWithHalf();
                    break;
                case 2:
                    MoveTopLeftWithThird();
                    break;
                case 3:
                    MoveTopLeftWithTwoThirds();
                    break;
                default:
                    break;
            }
        }

        private void MoveTopLeftWithTwoThirds()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, 2*(width /3), height / 2, true);

        }

        private void MoveTopLeftWithThird()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width / 3, height / 2, true);
        }

        private void MoveTopLeftWithHalf()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width / 2, height / 2, true);

        }

        
    }
}
