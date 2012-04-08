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
    partial class INIDetails : Form
    {
        public StoreParams appliedSP = null;
        private StoreParams sp = null;
        private List<TextBox> boxes = new List<TextBox>();

        public INIDetails(StoreParams sp)
        {
            this.sp = sp;
            int i = 0;
            int height = 30;
            foreach (System.Reflection.PropertyInfo pi in sp.GetType().GetProperties())
            {
                Label lbl = new Label();
                lbl.Name = pi.Name + "lbl";
                lbl.Text = pi.Name;
                lbl.Width = 160;
                lbl.Top = i*height + 10;
                this.Controls.Add(lbl);

                TextBox box = new TextBox();
                box.Name = pi.Name;
                box.Text = pi.GetValue(sp, null).ToString();
                box.Width = 60;
                box.Left = 170;
                box.Top = i * height + 10;
                this.Controls.Add(box);
                boxes.Add(box);
                ++i;
            }
            InitializeComponent();
            this.Height = i * height + 100;
            this.Width = 170 + 60 + 20;
            ok.Top = cancel.Top = button1.Top = this.Height - 60;
        }

        private void INIDetails_Load(object sender, EventArgs e)
        {

        }

        private void applySP()
        {
            foreach (TextBox box in boxes)
            {
                sp.reflectionSet(box.Name, box.Text);
            }
            appliedSP = sp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.DefaultExt = "ini";

            DialogResult result = saveFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                applySP();
                System.IO.Stream fs = saveFileDialog1.OpenFile();
                bool success = Program.writeSettings(sp, fs);
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
            applySP();
            this.Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
