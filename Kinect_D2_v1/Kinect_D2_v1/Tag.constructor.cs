using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Tag
    {
        public Tag(string description)
        {
            this.description = description;
        }

        public override string ToString()
        {
            return this.description;
        }
    }
}
