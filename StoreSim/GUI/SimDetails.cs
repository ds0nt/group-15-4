using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StoreSim.GUI
{
    partial class SimDetails : Form
    {
        Customer.CustomerStart[] simCustomers;
        public Customer.CustomerStart[] appliedsimCustomers = null;
        public SimDetails(Customer.CustomerStart[] simCustomers)
        {
            this.simCustomers = simCustomers;

            int i = 0;
            int height = 30;
            foreach (Customer.CustomerStart cs in simCustomers)
            {
                Label lbl = new Label();
                lbl.Name = i + "lbl";
                lbl.Text = "#" + i + ":";
                lbl.Width = 10;
                lbl.Top = i*height + 10;
                this.Controls.Add(lbl);

                TextBox box = new TextBox();
                box.Name = i + "btn";
               // box.Text = pi.GetValue(sp, null).ToString();
                box.Width = 60;
                box.Left = 170;
                box.Top = i * height + 10;
                this.Controls.Add(box);
               // boxes.Add(box);
                ++i;
            }
            InitializeComponent();
            this.Height = i * height + 100;
            this.Width = 170 + 60 + 20;
           // ok.Top = cancel.Top = button1.Top = this.Height - 60;
        }

        private void SimDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
