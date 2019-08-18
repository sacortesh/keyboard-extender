using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class BottomLeftBehavior : BehaviorBase
    {
        public BottomLeftBehavior() : base("bottom-left", 3)
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
                    MoveBottomLeftWithHalf();
                    break;
                case 2:
                    MoveBottomLeftWithThird();
                    break;
                case 3:
                    MoveBottomLeftWithTwoThirds();
                    break;
                default:
                    break;
            }
        }

        private void MoveBottomLeftWithTwoThirds()
        {

            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left, top + offsetVertical, 2 * (width / 3), height / 2, true);

        }

        private void MoveBottomLeftWithThird()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left, top + offsetVertical, width / 3, height / 2, true);
        }

        private void MoveBottomLeftWithHalf()
        {
            IntPtr activeWindow;
            int top, left, height, width;
            activeWindow = WindowsManagementExternals.GetActiveWindowAndScreen(out top, out left, out height, out width);

            int offsetVertical = height / 2;

            WindowsManagementExternals.MoveWindow(activeWindow, left, top + offsetVertical, width / 2, height / 2, true);

        }


    }
}
