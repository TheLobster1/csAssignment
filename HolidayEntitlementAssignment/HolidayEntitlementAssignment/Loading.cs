using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolidayEntitlementAssignment
{
    public partial class Loading : Form
    {
        public Loading()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ControlBox = false;
            this.Text = String.Empty;
        }
        Timer tmr;

        void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }

        private void Loading_Shown(object sender, EventArgs e)
        {
            tmr = new Timer();
            tmr.Interval = 3000;
            tmr.Start();
            tmr.Tick += tmr_Tick;
        }
    }
}
