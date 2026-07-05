using System;

namespace zentoraHRMS.Models
{
    public class ProjectModel
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectCode { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
    }
}
