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
            InitializeComponent();
           // ok.Top = cancel.Top = button1.Top = this.Height - 60;
        }

        private void SimDetails_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Items");
            dt.Columns.Add("Delay");
            for (int i = 0; i < simCustomers.Length; i++)
            {
                dt.Rows.Add(simCustomers[i].items.ToString(), simCustomers[i].delay.ToString());
            }
            dataGridView1.DataSource = dt;
        }

        private void applySim()
        {
            simCustomers = new Customer.CustomerStart[dataGridView1.Rows.Count - 1];

            DataTable dt = (DataTable)dataGridView1.DataSource;
            
            for(int i = 0; i < simCustomers.Length; i++)
            {
                simCustomers[i].items = int.Parse((string)dt.Rows[i][0]);
                simCustomers[i].delay = int.Parse((string)dt.Rows[i][1]);
            }
            appliedsimCustomers = simCustomers;
        }

        private void save_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "ini";

            DialogResult result = saveFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                applySim();
                System.IO.Stream fs = saveFileDialog1.OpenFile();
                bool success = Program.writeSim(simCustomers, fs);
                fs.Close();
                if (success)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Program.lastException.Message);
                }
            }
        }

        private void ok_Click(object sender, EventArgs e)
        {
            applySim();
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
