using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Experiment
    {
        public Experiment(long id, string name)
        {
            this.name = name;
            this.experiment_id = id;
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}
