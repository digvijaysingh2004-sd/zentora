using System;

namespace zentoraHRMS.Models
{
    public class EmployeeGoalModel
    {
        public int EmployeeGoalId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string ProfileImage { get; set; }
        public string Email { get; set; }
        public int GoalTypeId { get; set; }
        public string GoalTypeName { get; set; }
        public string GoalTitle { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Target { get; set; }
        public int Progress { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? SystemAddon { get; set; }
    }
}
