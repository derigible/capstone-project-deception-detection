using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    public partial class JointValue
    {
        public JointValue()
        {

        }
        public JointValue(long id, float xVal, float yVal, float zVal)
        {
            this.id = id;
            this.xValue = xVal;
            this.yValue = yVal;
            this.zValue = zVal;
        }
    }
}
