namespace RSPODemo
{
    partial class FormMain
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
            this.components = new System.ComponentModel.Container();
            this.buttonClose = new System.Windows.Forms.Button();
            this.groupBoxOPCServerSettings = new System.Windows.Forms.GroupBox();
            this.buttonOPCSave = new System.Windows.Forms.Button();
            this.labelServerIdentifier = new System.Windows.Forms.Label();
            this.labelPortNumber = new System.Windows.Forms.Label();
            this.labelHostName = new System.Windows.Forms.Label();
            this.textBoxServerIdentifier = new System.Windows.Forms.TextBox();
            this.textBoxPortNumber = new System.Windows.Forms.TextBox();
            this.textBoxHostName = new System.Windows.Forms.TextBox();
            this.buttonOPCConnect = new System.Windows.Forms.Button();
            this.OPC4 = new RSPODemo.UCOPCIndicator();
            this.OPC3 = new RSPODemo.UCOPCIndicator();
            this.OPC2 = new RSPODemo.UCOPCIndicator();
            this.OPC1 = new RSPODemo.UCOPCIndicator();
            this.statusStripMain = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelOPCConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerMain = new System.Windows.Forms.Timer(this.components);
            this.groupBoxOPCServerSettings.SuspendLayout();
            this.statusStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.Location = new System.Drawing.Point(422, 317);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(144, 23);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // groupBoxOPCServerSettings
            // 
            this.groupBoxOPCServerSettings.Controls.Add(this.buttonOPCSave);
            this.groupBoxOPCServerSettings.Controls.Add(this.labelServerIdentifier);
            this.groupBoxOPCServerSettings.Controls.Add(this.labelPortNumber);
            this.groupBoxOPCServerSettings.Controls.Add(this.labelHostName);
            this.groupBoxOPCServerSettings.Controls.Add(this.textBoxServerIdentifier);
            this.groupBoxOPCServerSettings.Controls.Add(this.textBoxPortNumber);
            this.groupBoxOPCServerSettings.Controls.Add(this.textBoxHostName);
            this.groupBoxOPCServerSettings.Controls.Add(this.buttonOPCConnect);
            this.groupBoxOPCServerSettings.Location = new System.Drawing.Point(6, 170);
            this.groupBoxOPCServerSettings.Name = "groupBoxOPCServerSettings";
            this.groupBoxOPCServerSettings.Size = new System.Drawing.Size(566, 96);
            this.groupBoxOPCServerSettings.TabIndex = 10;
            this.groupBoxOPCServerSettings.TabStop = false;
            this.groupBoxOPCServerSettings.Text = "OPC Server Settings";
            // 
            // buttonOPCSave
            // 
            this.buttonOPCSave.Location = new System.Drawing.Point(313, 64);
            this.buttonOPCSave.Name = "buttonOPCSave";
            this.buttonOPCSave.Size = new System.Drawing.Size(97, 23);
            this.buttonOPCSave.TabIndex = 8;
            this.buttonOPCSave.Text = "SAVE";
            this.buttonOPCSave.UseVisualStyleBackColor = true;
            this.buttonOPCSave.Click += new System.EventHandler(this.buttonOPCSave_Click);
            // 
            // labelServerIdentifier
            // 
            this.labelServerIdentifier.AutoSize = true;
            this.labelServerIdentifier.Location = new System.Drawing.Point(203, 22);
            this.labelServerIdentifier.Name = "labelServerIdentifier";
            this.labelServerIdentifier.Size = new System.Drawing.Size(81, 13);
            this.labelServerIdentifier.TabIndex = 6;
            this.labelServerIdentifier.Text = "Server Identifier";
            // 
            // labelPortNumber
            // 
            this.labelPortNumber.AutoSize = true;
            this.labelPortNumber.Location = new System.Drawing.Point(122, 22);
            this.labelPortNumber.Name = "labelPortNumber";
            this.labelPortNumber.Size = new System.Drawing.Size(66, 13);
            this.labelPortNumber.TabIndex = 5;
            this.labelPortNumber.Text = "Port Number";
            // 
            // labelHostName
            // 
            this.labelHostName.AutoSize = true;
            this.labelHostName.Location = new System.Drawing.Point(16, 22);
            this.labelHostName.Name = "labelHostName";
            this.labelHostName.Size = new System.Drawing.Size(60, 13);
            this.labelHostName.TabIndex = 4;
            this.labelHostName.Text = "Host Name";
            // 
            // textBoxServerIdentifier
            // 
            this.textBoxServerIdentifier.Location = new System.Drawing.Point(205, 38);
            this.textBoxServerIdentifier.Name = "textBoxServerIdentifier";
            this.textBoxServerIdentifier.Size = new System.Drawing.Size(355, 20);
            this.textBoxServerIdentifier.TabIndex = 3;
            // 
            // textBoxPortNumber
            // 
            this.textBoxPortNumber.Location = new System.Drawing.Point(125, 38);
            this.textBoxPortNumber.Name = "textBoxPortNumber";
            this.textBoxPortNumber.Size = new System.Drawing.Size(75, 20);
            this.textBoxPortNumber.TabIndex = 2;
            // 
            // textBoxHostName
            // 
            this.textBoxHostName.Location = new System.Drawing.Point(19, 38);
            this.textBoxHostName.Name = "textBoxHostName";
            this.textBoxHostName.Size = new System.Drawing.Size(100, 20);
            this.textBoxHostName.TabIndex = 1;
            // 
            // buttonOPCConnect
            // 
            this.buttonOPCConnect.Location = new System.Drawing.Point(416, 64);
            this.buttonOPCConnect.Name = "buttonOPCConnect";
            this.buttonOPCConnect.Size = new System.Drawing.Size(144, 23);
            this.buttonOPCConnect.TabIndex = 0;
            this.buttonOPCConnect.Text = "CONNECT";
            this.buttonOPCConnect.UseVisualStyleBackColor = true;
            this.buttonOPCConnect.Click += new System.EventHandler(this.buttonOPCConnect_Click);
            // 
            // OPC4
            // 
            this.OPC4.INFO = "";
            this.OPC4.Location = new System.Drawing.Point(435, 12);
            this.OPC4.Name = "OPC4";
            this.OPC4.RAMP = 0D;
            this.OPC4.RAND = 0D;
            this.OPC4.SINE = 0D;
            this.OPC4.Size = new System.Drawing.Size(145, 152);
            this.OPC4.TabIndex = 9;
            // 
            // OPC3
            // 
            this.OPC3.INFO = "";
            this.OPC3.Location = new System.Drawing.Point(292, 12);
            this.OPC3.Name = "OPC3";
            this.OPC3.RAMP = 0D;
            this.OPC3.RAND = 0D;
            this.OPC3.SINE = 0D;
            this.OPC3.Size = new System.Drawing.Size(145, 152);
            this.OPC3.TabIndex = 7;
            // 
            // OPC2
            // 
            this.OPC2.INFO = "";
            this.OPC2.Location = new System.Drawing.Point(149, 12);
            this.OPC2.Name = "OPC2";
            this.OPC2.RAMP = 0D;
            this.OPC2.RAND = 0D;
            this.OPC2.SINE = 0D;
            this.OPC2.Size = new System.Drawing.Size(145, 167);
            this.OPC2.TabIndex = 6;
            // 
            // OPC1
            // 
            this.OPC1.INFO = "";
            this.OPC1.Location = new System.Drawing.Point(6, 12);
            this.OPC1.Name = "OPC1";
            this.OPC1.RAMP = 0D;
            this.OPC1.RAND = 0D;
            this.OPC1.SINE = 0D;
            this.OPC1.Size = new System.Drawing.Size(145, 167);
            this.OPC1.TabIndex = 5;
            // 
            // statusStripMain
            // 
            this.statusStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelOPCConnection});
            this.statusStripMain.Location = new System.Drawing.Point(0, 367);
            this.statusStripMain.Name = "statusStripMain";
            this.statusStripMain.Size = new System.Drawing.Size(584, 22);
            this.statusStripMain.SizingGrip = false;
            this.statusStripMain.TabIndex = 11;
            this.statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelOPCConnection
            // 
            this.toolStripStatusLabelOPCConnection.Name = "toolStripStatusLabelOPCConnection";
            this.toolStripStatusLabelOPCConnection.Size = new System.Drawing.Size(99, 17);
            this.toolStripStatusLabelOPCConnection.Text = "OPC: UNKNOWN";
            // 
            // timerMain
            // 
            this.timerMain.Interval = 1000;
            this.timerMain.Tick += new System.EventHandler(this.timerMain_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 389);
            this.Controls.Add(this.statusStripMain);
            this.Controls.Add(this.groupBoxOPCServerSettings);
            this.Controls.Add(this.OPC4);
            this.Controls.Add(this.OPC3);
            this.Controls.Add(this.OPC2);
            this.Controls.Add(this.OPC1);
            this.Controls.Add(this.buttonClose);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RSPO Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.groupBoxOPCServerSettings.ResumeLayout(false);
            this.groupBoxOPCServerSettings.PerformLayout();
            this.statusStripMain.ResumeLayout(false);
            this.statusStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private UCOPCIndicator OPC1;
        private UCOPCIndicator OPC2;
        private UCOPCIndicator OPC3;
        private UCOPCIndicator OPC4;
        private System.Windows.Forms.GroupBox groupBoxOPCServerSettings;
        private System.Windows.Forms.Label labelServerIdentifier;
        private System.Windows.Forms.Label labelPortNumber;
        private System.Windows.Forms.Label labelHostName;
        private System.Windows.Forms.TextBox textBoxServerIdentifier;
        private System.Windows.Forms.TextBox textBoxPortNumber;
        private System.Windows.Forms.TextBox textBoxHostName;
        private System.Windows.Forms.Button buttonOPCConnect;
        private System.Windows.Forms.Button buttonOPCSave;
        private System.Windows.Forms.StatusStrip statusStripMain;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelOPCConnection;
        private System.Windows.Forms.Timer timerMain;
    }
}

