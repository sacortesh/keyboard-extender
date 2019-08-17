using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class ShowDesktopBehavior : BehaviorBase
    {
        public ShowDesktopBehavior() : base("show-desktop", 2)
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
                    WindowsManagementExternals.ShowDesktop();
                    break;
                case 2:
                    WindowsManagementExternals.RestoreAfterShowDesktop();
                    break;
                default:
                    break;
            }
        }
    }
}

