using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSPODemo
{
    public partial class UCOPCIndicator : UserControl
    {
        public UCOPCIndicator()
        {
            InitializeComponent();
        }

        public void Initialization(string groupTitle, string textInfo)
        {
            groupBoxMain.Text = groupTitle;
            textBoxInfo.Text = textInfo;
            textBoxRamp.Text = String.Empty;
            textBoxRandom.Text = String.Empty;
            textBoxSine.Text = String.Empty;
        }

        public Double RAMP
        {
            get
            {
                double temp = 0;
                double.TryParse(textBoxRamp.Text, out temp);
                return temp;
            }
            set
            {
                textBoxRamp.Text = value.ToString("F3");
            }
        }

        public Double RAND
        {
            get
            {
                double temp = 0;
                double.TryParse(textBoxRandom.Text, out temp);
                return temp;
            }
            set
            {
                textBoxRandom.Text = value.ToString("F3");
            }
        }

        public Double SINE
        {
            get
            {
                double temp = 0;
                double.TryParse(textBoxSine.Text, out temp);
                return temp;
            }
            set
            {
                textBoxSine.Text = value.ToString("F3");
            }
        }

        public String INFO
        {
            get
            {
                return textBoxInfo.Text;
            }
            set
            {
                textBoxInfo.Text = value.ToString();
            }
        }
    }
}
