using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNet
{
    class HebbNetwork
    {
        /// <summary>
        /// связи нейронов первого уровня
        /// </summary>
        List<List<double>> weights1;

        /// <summary>
        /// аксоны нейронов первого уровня
        /// </summary>
        List<double> y1;

        /// <summary>
        /// число нейронов в сети
        /// </summary>
        public int NeuronCount
        {
            get;
            private set;
        }

        public int VectorSize
        {
            get;
            private set;
        }

        private void React(List<double> x)
        {
            double max = double.MinValue;
            int maxi = 0;
            for(int i = 0; i< NeuronCount; i++)
            {
                y1[i] = 0;
                for(int j=0; j< VectorSize; j++)
                {
                    y1[i] += weights1[i][j] * x[j];
                }

                if (y1[i] >= max)
                {
                    maxi = i;
                    max = y1[maxi];
                }
                /*if (y1[i] > 1)
                    y1[i] = 1;
                else 
                    y1[i] = 0;*/
            }

            for (int i = 0; i < NeuronCount; i++)
            {
                y1[i] = 0;
            }
            y1[maxi] = max;
        }

        public void Init(List<List<double>> sources)
        {
            NeuronCount = sources.Count;
            VectorSize = sources[0].Count;


            ///инициализация
            y1 = new List<double>();
            weights1 = new List<List<double>>();
            for (int i = 0; i < NeuronCount; i++)
            {
                weights1.Add(new List<double>());
                y1.Add(0.0);
                for (int j = 0; j < VectorSize; j++)
                    weights1[i].Add(0.0);
            }

            //обучение
            bool allRight = true;
            do
            {
                allRight = true;
                for (int i = 0; i < NeuronCount; i++)
                {
                    //просчитать реакцию на этот сигнал
                    React(sources[i]);

                    if (y1[i] < 1)
                    {

                        allRight = false;
                        //сorrect weights
                        for(int j=0; j< NeuronCount; j++)
                        {
                                y1[j] = 0;
                        }
                        y1[i] = 1;
                        for (int u =0; u< NeuronCount; u++)
                        {
                            for (int j = 0; j < VectorSize; j++)
                            {
                                    weights1[u][j] +=sources[i][j] * y1[u];
                                //weights1[u][j] += sources[i][j];
                            }
                           // weights1[u][i]
                        }
                    }
                }
            } while (allRight == false);
        }

        public int FindImage(List<double> x)
        {
            React(x);

            for(int i=0; i< NeuronCount; i++)
            {
                if (y1[i] >= 1)
                    return i;
            }
            return 0;
        }

    }
}
