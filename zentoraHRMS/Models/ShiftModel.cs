using System;

namespace zentoraHRMS.Models
{
    public class ShiftModel
    {
        public int ShiftId { get; set; }
        public string ShiftName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
