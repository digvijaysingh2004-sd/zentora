using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zentoraHRMS.Models
{
    public class SubDepartmentModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int SubDepartmentId { get; set; }
        public string SubDepartmentName { get; set; }
        public string SubDepartmentCode { get; set; }
        public string HeadEmail { get; set; }
        public string HeadPhone { get; set; }
        public string Description { get; set; }
        public string HeadOfSubDepartment { get; set; }
        public int NumberOfEmployees { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}