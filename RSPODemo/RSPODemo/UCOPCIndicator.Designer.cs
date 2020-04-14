namespace RSPODemo
{
    partial class UCOPCIndicator
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxMain = new System.Windows.Forms.GroupBox();
            this.labelSine = new System.Windows.Forms.Label();
            this.labelRandom = new System.Windows.Forms.Label();
            this.labelRamp = new System.Windows.Forms.Label();
            this.textBoxSine = new System.Windows.Forms.TextBox();
            this.textBoxRandom = new System.Windows.Forms.TextBox();
            this.textBoxRamp = new System.Windows.Forms.TextBox();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.groupBoxMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMain
            // 
            this.groupBoxMain.Controls.Add(this.textBoxInfo);
            this.groupBoxMain.Controls.Add(this.labelSine);
            this.groupBoxMain.Controls.Add(this.labelRandom);
            this.groupBoxMain.Controls.Add(this.labelRamp);
            this.groupBoxMain.Controls.Add(this.textBoxSine);
            this.groupBoxMain.Controls.Add(this.textBoxRandom);
            this.groupBoxMain.Controls.Add(this.textBoxRamp);
            this.groupBoxMain.Location = new System.Drawing.Point(3, 3);
            this.groupBoxMain.Name = "groupBoxMain";
            this.groupBoxMain.Size = new System.Drawing.Size(137, 152);
            this.groupBoxMain.TabIndex = 2;
            this.groupBoxMain.TabStop = false;
            this.groupBoxMain.Text = "OPC I";
            // 
            // labelSine
            // 
            this.labelSine.AutoSize = true;
            this.labelSine.Location = new System.Drawing.Point(21, 75);
            this.labelSine.Name = "labelSine";
            this.labelSine.Size = new System.Drawing.Size(28, 13);
            this.labelSine.TabIndex = 5;
            this.labelSine.Text = "Sine";
            // 
            // labelRandom
            // 
            this.labelRandom.AutoSize = true;
            this.labelRandom.Location = new System.Drawing.Point(21, 49);
            this.labelRandom.Name = "labelRandom";
            this.labelRandom.Size = new System.Drawing.Size(47, 13);
            this.labelRandom.TabIndex = 4;
            this.labelRandom.Text = "Random";
            // 
            // labelRamp
            // 
            this.labelRamp.AutoSize = true;
            this.labelRamp.Location = new System.Drawing.Point(21, 23);
            this.labelRamp.Name = "labelRamp";
            this.labelRamp.Size = new System.Drawing.Size(35, 13);
            this.labelRamp.TabIndex = 3;
            this.labelRamp.Text = "Ramp";
            // 
            // textBoxSine
            // 
            this.textBoxSine.Location = new System.Drawing.Point(74, 71);
            this.textBoxSine.Name = "textBoxSine";
            this.textBoxSine.Size = new System.Drawing.Size(50, 20);
            this.textBoxSine.TabIndex = 2;
            // 
            // textBoxRandom
            // 
            this.textBoxRandom.Location = new System.Drawing.Point(74, 45);
            this.textBoxRandom.Name = "textBoxRandom";
            this.textBoxRandom.Size = new System.Drawing.Size(50, 20);
            this.textBoxRandom.TabIndex = 1;
            // 
            // textBoxRamp
            // 
            this.textBoxRamp.Location = new System.Drawing.Point(74, 19);
            this.textBoxRamp.Name = "textBoxRamp";
            this.textBoxRamp.Size = new System.Drawing.Size(50, 20);
            this.textBoxRamp.TabIndex = 0;
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(24, 126);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(100, 20);
            this.textBoxInfo.TabIndex = 6;
            // 
            // UCOPCIndicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMain);
            this.Name = "UCOPCIndicator";
            this.Size = new System.Drawing.Size(145, 161);
            this.groupBoxMain.ResumeLayout(false);
            this.groupBoxMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMain;
        private System.Windows.Forms.Label labelSine;
        private System.Windows.Forms.Label labelRandom;
        private System.Windows.Forms.Label labelRamp;
        private System.Windows.Forms.TextBox textBoxSine;
        private System.Windows.Forms.TextBox textBoxRandom;
        private System.Windows.Forms.TextBox textBoxRamp;
        private System.Windows.Forms.TextBox textBoxInfo;
    }
}
