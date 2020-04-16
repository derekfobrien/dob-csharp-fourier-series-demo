using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FourierSeriesDemo
{
    public partial class Form1 : Form
    {
        // The Fourier Series Object
        public FourierSeries Fourier = new FourierSeries();

        // Constructor for the Form
        public Form1()
        {
            InitializeComponent();
        }

        // the method that is called when the form (the window) is resized
        private void ResizeControls()
        {
            int Gap = 15;

            PicDisplay.Width = ClientSize.Width - PicDisplay.Left - Gap;
            PicDisplay.Height = ClientSize.Height - PicDisplay.Top - Gap;
        }

        // the method that runs when the form is loaded 
        private void Form1_Load(object sender, EventArgs e)
        {
            ResizeControls();
        }

        // the method that runs when the form is resized
        private void Form1_Resize(object sender, EventArgs e)
        {
            ResizeControls();
        }

        // the method that runs when the "One More Iteration" button is clicked
        private void BtnPlot_Click(object sender, EventArgs e)
        {
            int it = Fourier.GetIters();
            Fourier.SetIters(it + 1);

            LblIters.Text = it.ToString();

            Fourier.PlotGraph(PicDisplay);
        }

        // the method that runs whent the "Open File" button is clicked
        private void BtnOpen_Click(object sender, EventArgs e)
        {
            string MyFileName;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    MyFileName = openFileDialog1.FileName;
                    Fourier.SetPeriod(TxtPeriod.Text);
                    if (Fourier.ReadArray(MyFileName, LisCurve) == 0)
                    {
                        BtnPlot.Enabled = true;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.ToString());
                }
            }
        }
    }
}
