using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNet
{
    class HammingNetwork
    {
        /// <summary>
        /// аксоны нейронов первого уровня
        /// </summary>
        List<double> y1;

        /// <summary>
        /// аксоны нейронов второго уровня
        /// </summary>
        List<double> y2;

        /// <summary>
        /// синапсы нейронов второго уроня
        /// </summary>
        List<double> s2;

        /// <summary>
        /// связи нейронов первого уровня
        /// </summary>
        List<List<double>> weights1;

        /// <summary>
        /// связи нейронов второго уровня
        /// </summary>
        List<List<double>> weights2;

        /// <summary>
        /// число нейронов в сети
        /// </summary>
        public int NeuronCount
        {
            get;
            set;
        }

        public int VectorSize
        {
            get;
            set;
        }


        public void Init(List<List<double>> samples)
        {
            Random rand = new Random();
            NeuronCount = samples.Count;
            VectorSize = samples[0].Count;
            y1 = new List<double>(NeuronCount);
            y2 = new List<double>(NeuronCount);
            s2 = new List<double>(NeuronCount);

            weights1 = new List<List<double>>();
            weights2 = new List<List<double>>();
            for(int i=0; i< NeuronCount; i++)
            {
                y1.Add(0.0);
                y2.Add(0.0);
                s2.Add(0.0);

                weights1.Add( new List<double>());
                weights2.Add(new List<double>());
                for(int j=0; j<VectorSize; j++)
                {
                    weights1[i].Add(samples[i][j] / 2);
                    weights2[i].Add(Eps);
                    if (i == j)
                        weights2[i][j] = 1.0;
                }
            }
        }

        public double MaxValue
        {
            get;
            set;
        }

        public double K
        {
            get;
            set;
        }

        public double Eps
        {
            get;
            set;
        }

        private double function(double v)
        {
            if (v <= 0)
                return 0;
            else if (v <= MaxValue)
            {
                return K * v;
            }
            else
                return MaxValue;
        }

        public int FindImage(List<double> x)
        {
            for (int k = 0; k < 500; k++)
            {
                List<double> prevY2 = new List<double>();
                for (int i = 0; i < NeuronCount; i++)
                {
                    y1[i] = 0;
                    //копирование для сравнения потом
                    prevY2.Add(y2[i]);

                    for (int j = 0; j < VectorSize; j++)
                    {
                        y1[i] += weights1[i][j] * x[j];
                    }
                    y1[i] += VectorSize / 2.0;
                    y2[i] = y1[i];
                }

                for (int i = 0; i < NeuronCount; i++)
                {
                    s2[i] = y2[i];
                    for (int j = 0; j < NeuronCount; j++)
                    {
                        s2[i] -= Eps* y2[j];
                    }
                }

                for (int i = 0; i < NeuronCount; i++)
                {
                    y2[i] = function(s2[i]);
                }

                int numOfMatches = 0;
                for (int i = 0; i < NeuronCount; i++)
                {
                    if (y2[i] == prevY2[i])
                        numOfMatches++;
                }
                if (numOfMatches == y2.Count)
                    break;
            }


            double max = double.MinValue;
            int maxi = 0;
            for (int i = 0; i < NeuronCount; i++)
            {
                if (y2[i] >= max)
                {
                    max = y2[i];
                    maxi = i;
                }
            }

            return maxi;
        }
    }
}
