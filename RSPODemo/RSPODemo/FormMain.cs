using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSPODemo
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение выхода", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            textBoxRamp.Text = Math.PI.ToString();
            OPC1.RAMP = Math.PI;
            OPC1.RAND = rnd.NextDouble();
            OPC1.SINE = Math.Sin(rnd.Next());

            OPC2.RAMP = Math.PI;
            OPC2.RAND = rnd.NextDouble();
            OPC2.SINE = Math.Sin(rnd.Next());

            OPC3.RAMP = Math.PI;
            OPC3.RAND = rnd.NextDouble();
            OPC3.SINE = Math.Sin(rnd.Next());

            OPC4.RAMP = Math.PI;
            OPC4.RAND = rnd.NextDouble();
            OPC4.SINE = Math.Sin(rnd.Next());
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            OPC1.Initialization("OPC I", "Information");
            OPC2.Initialization("OPC II", "Information");
            OPC3.Initialization("OPC III", "Information");
            OPC4.Initialization("OPC IV", "Information");
        }
    }
}
