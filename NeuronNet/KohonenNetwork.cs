using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNet
{
    class KohonenNetwork : INeuronNetwork
    {
        /// <summary>
        /// связи нейронов первого уровня
        /// </summary>
        List<List<double>> weights1;

        /// <summary>
        /// коэффициент скорости обучения
        /// </summary>
        double alpha = 0.7;

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

        public int FindImage(List<double> x)
        {
            double minDistance = 1e6;
            //индекс нейроаня с минимальным расстоянием
            int mink = 0;

            double distance = 0.0;

            //вычисление расстояния до всех входов
            for (int j = 0; j < NeuronCount; j++)
            {
                distance = Distance(x, j);
                if (minDistance > distance)
                {
                    minDistance = distance;
                    mink = j;
                }
            }
            return mink;
        }

        /// <summary>
        /// на вход подаются значения от -1, до 1
        /// </summary>
        /// <param name="sources"></param>
        public void Init(List<List<double>> sources)
        {
            Random rand = new Random();
            // задание структуры сети
            NeuronCount = sources.Count;
            VectorSize = sources[0].Count;

            weights1 = new List<List<double>>();
            for (int i = 0; i < VectorSize; i++)
            {
                weights1.Add(new List<double>());
                for (int j = 0; j < NeuronCount; j++)
                {
                    weights1[i].Add(rand.NextDouble()*0.1 / Math.Sqrt(VectorSize));
                }
            }
            int counter = 0;
            //максимальное значение изменения весов
            double maxDelta = 0.0;
            do
            {
                double minDistance = 1e6;
                //индекс нейрона с минимальным расстоянием
                int mink = 0;

                double distance = 0.0;
                //случайный вход
                int n = rand.Next(sources.Count);

                //вычисление расстояния до всех входов
                for (int j = 0; j < NeuronCount; j++)
                {
                    distance = Distance(sources[n], j);
                    if (minDistance > distance)
                    {
                        //выбор нейрона k с минимальным расстоянием
                        minDistance = distance;
                        mink = j;
                    }
                }

                maxDelta = 0;
                //коррекция для нейрона-победителя
                for (int i = 0; i < VectorSize; i++)
                {
                    double delta = weights1[i][mink];

                    //сама коррекция веса
                    weights1[i][mink] = weights1[i][mink] + alpha * (sources[n][i] - weights1[i][mink]);
                    ///if (weights1[i][mink] == -1.0)
                    //    return;

                    delta = Math.Abs(weights1[i][mink] - delta);
                    if (maxDelta < delta)
                        maxDelta = delta;
                    
                }
                alpha *= (1-1e-3);
                counter++;
            } while (maxDelta > 1e-6 && alpha > 0);
            //throw new Exception(counter.ToString());
            return;
        }

        private double Distance(List<double> x, int j)
        {
            double sum = 0;
            for( int i=0; i< VectorSize; i++)
            {
                sum += Math.Pow(x[i] - weights1[i][j], 2);
            }
            return sum;
        }
    }
}
