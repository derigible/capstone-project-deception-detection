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
    
    public partial class Participant_Condition
    {
        public Participant_Condition()
        {
            this.Features = new HashSet<Feature>();
            this.Raw_Data = new HashSet<Raw_Data>();
        }
    
        public long pc_id { get; set; }
        public long condition_id { get; set; }
        public long participant_id { get; set; }
    
        public virtual Condition Condition { get; set; }
        public virtual ICollection<Feature> Features { get; set; }
        public virtual Participant Participant { get; set; }
        public virtual ICollection<Raw_Data> Raw_Data { get; set; }
    }
}