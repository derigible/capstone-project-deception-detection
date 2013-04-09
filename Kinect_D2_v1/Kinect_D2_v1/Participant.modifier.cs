using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinect_D2_v1
{
    partial class Participant
    {
        public Participant(string code, string fName, string lName)
        {
            this.code = code;
            this.fname = fName;
            this.lname = lName;
        }


        override public string ToString()
        {
            return this.fname + " " + this.lname + participant_id;
        }
    }
}
