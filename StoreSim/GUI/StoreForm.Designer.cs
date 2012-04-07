namespace StoreSim.GUI
{
    partial class StoreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openINI = new System.Windows.Forms.Button();
            this.openSim = new System.Windows.Forms.Button();
            this.simulate = new System.Windows.Forms.Button();
            this.simLabel = new System.Windows.Forms.Label();
            this.iniLabel = new System.Windows.Forms.Label();
            this.simDetails = new System.Windows.Forms.Button();
            this.iniDetails = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // openINI
            // 
            this.openINI.Location = new System.Drawing.Point(12, 41);
            this.openINI.Name = "openINI";
            this.openINI.Size = new System.Drawing.Size(102, 23);
            this.openINI.TabIndex = 0;
            this.openINI.Text = "Open INI";
            this.openINI.UseVisualStyleBackColor = true;
            this.openINI.Click += new System.EventHandler(this.openINI_Click);
            // 
            // openSim
            // 
            this.openSim.Location = new System.Drawing.Point(12, 12);
            this.openSim.Name = "openSim";
            this.openSim.Size = new System.Drawing.Size(104, 23);
            this.openSim.TabIndex = 1;
            this.openSim.Text = "Open Simulation";
            this.openSim.UseVisualStyleBackColor = true;
            this.openSim.Click += new System.EventHandler(this.openSim_Click);
            // 
            // simulate
            // 
            this.simulate.Enabled = false;
            this.simulate.Location = new System.Drawing.Point(13, 227);
            this.simulate.Name = "simulate";
            this.simulate.Size = new System.Drawing.Size(75, 23);
            this.simulate.TabIndex = 2;
            this.simulate.Text = "Simulate";
            this.simulate.UseVisualStyleBackColor = true;
            this.simulate.Click += new System.EventHandler(this.simulate_Click);
            // 
            // simLabel
            // 
            this.simLabel.AutoSize = true;
            this.simLabel.Location = new System.Drawing.Point(201, 17);
            this.simLabel.Name = "simLabel";
            this.simLabel.Size = new System.Drawing.Size(72, 13);
            this.simLabel.TabIndex = 3;
            this.simLabel.Text = "None Loaded";
            // 
            // iniLabel
            // 
            this.iniLabel.AutoSize = true;
            this.iniLabel.Location = new System.Drawing.Point(201, 46);
            this.iniLabel.Name = "iniLabel";
            this.iniLabel.Size = new System.Drawing.Size(72, 13);
            this.iniLabel.TabIndex = 4;
            this.iniLabel.Text = "None Loaded";
            // 
            // simDetails
            // 
            this.simDetails.Enabled = false;
            this.simDetails.Location = new System.Drawing.Point(120, 12);
            this.simDetails.Name = "simDetails";
            this.simDetails.Size = new System.Drawing.Size(75, 23);
            this.simDetails.TabIndex = 5;
            this.simDetails.Text = "Details";
            this.simDetails.UseVisualStyleBackColor = true;
            this.simDetails.Click += new System.EventHandler(this.simDetails_Click);
            // 
            // iniDetails
            // 
            this.iniDetails.Enabled = false;
            this.iniDetails.Location = new System.Drawing.Point(120, 41);
            this.iniDetails.Name = "iniDetails";
            this.iniDetails.Size = new System.Drawing.Size(75, 23);
            this.iniDetails.TabIndex = 6;
            this.iniDetails.Text = "Details";
            this.iniDetails.UseVisualStyleBackColor = true;
            this.iniDetails.Click += new System.EventHandler(this.iniDetails_Click);
            // 
            // StoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 262);
            this.Controls.Add(this.iniDetails);
            this.Controls.Add(this.simDetails);
            this.Controls.Add(this.iniLabel);
            this.Controls.Add(this.simLabel);
            this.Controls.Add(this.simulate);
            this.Controls.Add(this.openSim);
            this.Controls.Add(this.openINI);
            this.Name = "StoreForm";
            this.Text = "StoreForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button openINI;
        private System.Windows.Forms.Button openSim;
        private System.Windows.Forms.Button simulate;
        private System.Windows.Forms.Label simLabel;
        private System.Windows.Forms.Label iniLabel;
        private System.Windows.Forms.Button simDetails;
        private System.Windows.Forms.Button iniDetails;
    }
}