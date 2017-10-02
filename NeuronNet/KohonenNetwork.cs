using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNet
{
    class KohonenNetwork : INeuronNetwork
    {
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

        int INeuronNetwork.FindImage(List<double> x)
        {
            throw new NotImplementedException();
        }

        void INeuronNetwork.Init(List<List<double>> sources)
        {
            throw new NotImplementedException();
        }
    }
}
