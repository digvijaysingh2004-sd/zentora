using System;

namespace zentoraHRMS.Models
{
    public class LeaveCategoryModel
    {
        public int LeaveCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int TotalDays { get; set; }
        public bool IsActive { get; set; }
    }
}
