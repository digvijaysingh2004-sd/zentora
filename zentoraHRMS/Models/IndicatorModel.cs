using System;

namespace zentoraHRMS.Models
{
    public class IndicatorModel
    {
        public int IndicatorId { get; set; }
        public int IndicatorCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string IndicatorName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
