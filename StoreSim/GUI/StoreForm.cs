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
    public partial class StoreForm : Form
    {
        StoreParams sp = null;
        Store s = null;
        Customer.CustomerStart[] simCustomers = null;

        public StoreForm()
        {
            InitializeComponent();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            
        }

        private void openSim_Click(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "sim";

            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                simCustomers = Program.readSimulation(openFileDialog.OpenFile());
                simDetails.Enabled = simCustomers != null;
                if (simCustomers != null)
                {
                    simulate.Enabled = true;
                    simLabel.Text = simCustomers.Length + " Customers Loaded";
                }
                else
                {
                    MessageBox.Show(Program.lastException.Message);
                }
            }
        }

        private void openINI_Click(object sender, EventArgs e)
        {
            openFileDialog.DefaultExt = "ini";

            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                sp = Program.readSettings(openFileDialog.OpenFile());
                iniDetails.Enabled = sp != null;
                if (sp != null)
                {
                    simulate.Enabled = true;
                    iniLabel.Text = "INI Loaded";
                }
                else
                {
                    MessageBox.Show(Program.lastException.Message);
                }
            }
        }

        private void simulate_Click(object sender, EventArgs e)
        {
            s = new Store(sp);
            foreach (Customer.CustomerStart cs in simCustomers)
            {
                new Customer(cs);
            }
        }

        private void simDetails_Click(object sender, EventArgs e)
        {

        }

        private void iniDetails_Click(object sender, EventArgs e)
        {

        }
    }
}
