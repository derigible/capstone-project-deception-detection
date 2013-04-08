using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Condition
    {
        public Condition(long experiment_id)
        {
            this.experiment_id = experiment_id;
        }

        public override string ToString()
        {
            return this.code;
        }
    }
}
