using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Participant
    {
        public Participant(long id, string code, string fName, string lName)
        {
            this.participant_id = id;
            this.code = code;
            this.first_name = fName;
            this.last_name = lName;
        }


        override public string ToString()
        {
            return this.first_name + " " + this.last_name + participant_id;
        }
    }
}
