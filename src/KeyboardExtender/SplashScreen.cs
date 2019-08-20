using Avangarde.KeyboardExtender.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Avangarde.KeyboardExtender
{
    public partial class SplashScreen : Form
    {
        Timer tmr;

        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Shown(object sender, EventArgs e)
        {
            ProjectManager.Instance.RunMainLogic();

            tmr = new Timer();

            //set time interval 3 sec

            tmr.Interval = 3000;

            //starts the timer

            tmr.Start();
            tmr.Tick += Tmr_Tick;
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            //after 3 sec stop the timer

            tmr.Stop();            
            
            //hide this form

            this.Hide();
        }
    }
}
