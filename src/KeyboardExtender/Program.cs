using Avangarde.KeyboardExtender.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtender
{
    static class Program
    {
        

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            NotifyIcon notifyIconMain = new System.Windows.Forms.NotifyIcon();

            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));

            notifyIconMain.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIconMain.Icon")));
            notifyIconMain.Text = "KeyboardExtender vX.Y";
            notifyIconMain.Visible = true;

            ContextMenu cm = new ContextMenu();
            cm.MenuItems.Add("Quit", new EventHandler(QuitApplication_Click));

            notifyIconMain.ContextMenu = cm;

            Application.Run(new SplashScreen());
        }

        private static void QuitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
