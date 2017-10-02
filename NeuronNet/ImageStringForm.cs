using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NeuronNet
{
    public partial class ImageStringForm : Form
    {
        public string exampleName
        {
            get;
            set;
        }
        public ImageStringForm(string filename, string example)
        {
            InitializeComponent();
            exampleName = example;
            textBox1.Text = example;
            pictureBox1.Image = new Bitmap(filename, true);
        }

        private void ImageStringForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            exampleName = textBox1.Text;
        }
    }
}
