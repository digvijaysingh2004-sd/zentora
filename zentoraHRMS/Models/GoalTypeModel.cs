using System;

namespace zentoraHRMS.Models
{
    public class GoalTypeModel
    {
        public int GoalTypeId { get; set; }
        public string GoalTypeName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool SystemAddon { get; set; }
    }
}
