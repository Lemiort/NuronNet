using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NeuronNet
{
    /// <summary>
    /// интерфейс работы со всеми нейронными сетями
    /// </summary>
    interface INeuronNetwork
    {
        void Init(List<List<double>> sources);
        int FindImage(List<double> x);

        /// <summary>
        /// число нейронов в сети
        /// </summary>
        int NeuronCount
        {
            get;
        }

        int VectorSize
        {
            get;
        }
    }
}
