//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project.Seed
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProjectFollower
    {
        public int ProfileID { get; set; }
        public int FollowedProjectID { get; set; }
        public int ID { get; set; }
        public System.DateTime FollowDate { get; set; }
    
        public virtual Profile Profile { get; set; }
        public virtual Project Project { get; set; }
    }
}
