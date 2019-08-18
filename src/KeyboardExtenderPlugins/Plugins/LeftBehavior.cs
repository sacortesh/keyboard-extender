using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class LeftBehavior : BehaviorBase
    {
        public LeftBehavior() : base("left", 3)
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
                    MoveLeftWithHalf();
                    break;
                case 2:
                    MoveLeftWithThird();
                    break;
                case 3:
                    MoveLeftWithTwoThirds();
                    break;
                default:
                    break;
            }
        }

        private void MoveLeftWithTwoThirds()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, 2*(width /3), height, true);

        }

        private void MoveLeftWithThird()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width / 3, height, true);
        }

        private void MoveLeftWithHalf()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            WindowsManagementExternals.MoveWindow(activeWindow, left, top, width / 2, height, true);

        }

        
    }
}
