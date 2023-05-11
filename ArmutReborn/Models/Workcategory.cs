using System;
using System.Collections.Generic;

namespace ArmutReborn.Models
{
    public partial class Workcategory
    {
        public Workcategory()
        {
            InverseParent = new HashSet<Workcategory>();
            WorkListings = new HashSet<WorkListing>();
        }

        public uint Id { get; set; }
        public string Name { get; set; } = null!;
        public uint? ParentId { get; set; }
        public string RuleTemplate { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;

        public virtual Workcategory? Parent { get; set; }
        public virtual ICollection<Workcategory> InverseParent { get; set; }
        public virtual ICollection<WorkListing> WorkListings { get; set; }
    }
}
