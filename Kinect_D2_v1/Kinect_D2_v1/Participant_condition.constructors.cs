using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Participant_Condition
    {
        public Participant_Condition(long participant_id, long condition_id)
        {
            this.participant_id = participant_id;
            this.condition_id = condition_id;
        }
    }
}
