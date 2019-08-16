using Avangarde.KeyboardExtenderPlugins.Externals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Avangarde.KeyboardExtenderPlugins.Plugins
{
    public class MaximizeBehavior : BehaviorBase
    {
        public MaximizeBehavior() : base("maximize", 1)
        {
            
        }

        public override void Behavior(int modifier)
        {
            base.Behavior(modifier);

            //my new behavior;
            //validate modifier
            WindowsManagementExternals.MaximizeActiveWindow();
        }
    }
}
