//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LinkManagement
{
    using System;
    using System.Collections.Generic;
    
    public partial class Topic
    {
        public Topic()
        {
            this.Links = new HashSet<Link>();
        }
    
        public int UserID { get; set; }
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public Nullable<int> ParentID { get; set; }
    
        public virtual ICollection<Link> Links { get; set; }
        public virtual User User { get; set; }
    }
}
