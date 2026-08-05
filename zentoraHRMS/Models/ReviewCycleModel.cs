using System;

namespace zentoraHRMS.Models
{
    public class ReviewCycleModel
    {
        public int ReviewCycleId { get; set; }
        public string CycleName { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? SystemAddon { get; set; }
    }
}
