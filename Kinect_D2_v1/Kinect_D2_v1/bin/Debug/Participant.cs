//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kinect_D2_v1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Participant
    {
        public Participant()
        {
            this.Participant_Condition = new HashSet<Participant_Condition>();
        }
    
        public long participant_id { get; set; }
        public string code { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string pnum { get; set; }
        public string add_line_1 { get; set; }
        public string add_line_2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
    
        public virtual ICollection<Participant_Condition> Participant_Condition { get; set; }
    }
}
