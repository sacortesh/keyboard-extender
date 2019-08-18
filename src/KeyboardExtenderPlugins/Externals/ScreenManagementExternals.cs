using Avangarde.KeyboardExtenderPlugins.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtenderPlugins.Externals
{
    public static class ScreenManagementExternals
    {
        private static List<ScreenVO> screens;

        public static void InitializeScreens()
        {
            if (screens == null)
            {
                screens = new List<ScreenVO>();
            }
            else
            {
                //TODO clean better?
                screens = new List<ScreenVO>();
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                screens.Add(new ScreenVO(screen));
            }
        }
    }
}
