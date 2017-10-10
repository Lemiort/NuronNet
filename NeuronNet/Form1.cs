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
    public partial class Form1 : Form
    {
        List<List<double>> sources;
        List<string> names;
        HammingNetwork hammingNet;
        HebbNetwork hebbNet;
        KohonenNetwork kohonenNet;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// распознавание сетью Хэмминга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if( openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                List<double> source = new List<double>();
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName, true);
                Bitmap image = new Bitmap(openFileDialog1.FileName, true);

                for (int x = 0; x < image.Width; x++)
                    for(int y = 0; y< image.Height; y++)
                    {
                        double val = image.GetPixel(x, y).R +
                                            image.GetPixel(x,y).G +
                                            image.GetPixel(x, y).B;
                        if (val <= 128+128+128)
                            val = 1;
                        else
                            val = -1;
                        source.Add(val);
                    }
                if (source.Count != hammingNet.VectorSize)
                    MessageBox.Show("Размер картинки не соответствует образцам!");
                else
                    MessageBox.Show("Мы думаем, что это " +
                        names[hammingNet.FindImage(source)]);
            }
        }

        /// <summary>
        /// обучение сети Хэмминга
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                sources = new List<List<double>>();

                hammingNet = new HammingNetwork();
                hammingNet.K = 0.1;
                

                names = new List<string>();
                hammingNet.NeuronCount = openFileDialog2.FileNames.Count();
                hammingNet.Eps = 0.5 / hammingNet.NeuronCount;

                for (int i=0; i< openFileDialog2.FileNames.Count(); i++)
                {
                    ImageStringForm gfrom1 = new ImageStringForm(openFileDialog2.FileNames[i],
                                            i.ToString());
                    if (gfrom1.ShowDialog() == DialogResult.OK)
                        names.Add(gfrom1.exampleName);
                    else
                        MessageBox.Show("Ошибка");

                    Bitmap bitmap =  new Bitmap(openFileDialog2.FileNames[i]);
                    sources.Add(new List<double>());
                    hammingNet.VectorSize = bitmap.Width * bitmap.Height;
                    hammingNet.MaxValue = bitmap.Width * bitmap.Height;

                    for (int w= 0; w< bitmap.Width; w++)
                    {
                        for ( int h=0; h< bitmap.Height; h++)
                        {
                            double val = bitmap.GetPixel(w, h).R +
                                            bitmap.GetPixel(w, h).G +
                                            bitmap.GetPixel(w, h).B;
                            if (val <= 128+128+128)
                                val = 1;
                            else
                                val = -1;
                            sources[i].Add(val);
                        }
                    }
                }
                hammingNet.Init(sources);
            }
        }

        /// <summary>
        /// распознавание сетью Хэбба
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                List<double> source = new List<double>();
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName, true);
                Bitmap image = new Bitmap(openFileDialog1.FileName, true);

                for (int x = 0; x < image.Width; x++)
                    for (int y = 0; y < image.Height; y++)
                    {
                        double val = image.GetPixel(x, y).R +
                                            image.GetPixel(x, y).G +
                                            image.GetPixel(x, y).B;
                        if (val <= 128 + 128 + 128)
                            val = 1;
                        else
                            val = 0;
                        source.Add(val);
                    }
                if (source.Count != hebbNet.VectorSize)
                    MessageBox.Show("Размер картинки не соответствует образцам!");
                else
                    MessageBox.Show("Мы думаем, что это " +
                        names[hebbNet.FindImage(source)]);
            }
        }

        /// <summary>
        /// обучение сети хэбба
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                sources = new List<List<double>>();

                hebbNet = new HebbNetwork();

                names = new List<string>();
                for (int i = 0; i < openFileDialog2.FileNames.Count(); i++)
                {
                    ImageStringForm gfrom1 = new ImageStringForm(openFileDialog2.FileNames[i],
                                            i.ToString());
                    if (gfrom1.ShowDialog() == DialogResult.OK)
                        names.Add(gfrom1.exampleName);
                    else
                        MessageBox.Show("Ошибка");

                    Bitmap bitmap = new Bitmap(openFileDialog2.FileNames[i]);
                    sources.Add(new List<double>());

                    for (int w = 0; w < bitmap.Width; w++)
                    {
                        for (int h = 0; h < bitmap.Height; h++)
                        {
                            double val = bitmap.GetPixel(w, h).R +
                                            bitmap.GetPixel(w, h).G +
                                            bitmap.GetPixel(w, h).B;
                            if (val <= 128 + 128 + 128)
                                val = 1;
                            else
                                val = 0;
                            sources[i].Add(val);
                        }
                    }
                }
                hebbNet.Init(sources);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// обучение сети Кохонена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog2.ShowDialog(this) == DialogResult.OK)
            {
                sources = new List<List<double>>();

                kohonenNet = new KohonenNetwork();

                kohonenNet.Alpha = decimal.ToDouble(numericUpDown1.Value);
                kohonenNet.DistanceParam = decimal.ToDouble(numericUpDown2.Value);

                names = new List<string>();
                for (int i = 0; i < openFileDialog2.FileNames.Count(); i++)
                {
                    ImageStringForm gfrom1 = new ImageStringForm(openFileDialog2.FileNames[i],
                                            i.ToString());
                    if (gfrom1.ShowDialog() == DialogResult.OK)
                        names.Add(gfrom1.exampleName);
                    else
                        MessageBox.Show("Ошибка");

                    Bitmap bitmap = new Bitmap(openFileDialog2.FileNames[i]);
                    sources.Add(new List<double>());

                    for (int w = 0; w < bitmap.Width; w++)
                    {
                        for (int h = 0; h < bitmap.Height; h++)
                        {
                            double val = bitmap.GetPixel(w, h).R +
                                            bitmap.GetPixel(w, h).G +
                                            bitmap.GetPixel(w, h).B;
                            if (val <= 128 + 128 + 128)
                                val = 1;
                            else
                                val = 0;
                            sources[i].Add(val);
                        }
                    }
                }
                kohonenNet.Init(sources);
            }
        }


        /// <summary>
        /// распознавание образа сетью Кохонена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                List<double> source = new List<double>();
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName, true);
                Bitmap image = new Bitmap(openFileDialog1.FileName, true);

                for (int x = 0; x < image.Width; x++)
                    for (int y = 0; y < image.Height; y++)
                    {
                        double val = image.GetPixel(x, y).R +
                                            image.GetPixel(x, y).G +
                                            image.GetPixel(x, y).B;
                        if (val <= 128 + 128 + 128)
                            val = 1;
                        else
                            val = 0;
                        source.Add(val);
                    }
                if (source.Count != kohonenNet.VectorSize)
                    MessageBox.Show("Размер картинки не соответствует образцам!");
                else
                    MessageBox.Show("Мы думаем, что это тип " +
                        names[kohonenNet.FindImage(source)]);
            }
        }
    }
}
