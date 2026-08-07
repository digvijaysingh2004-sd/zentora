using System;
using System.Collections.Generic;

namespace zentoraHRMS.Models
{
    public class EmployeeReviewModel
    {
        public int ReviewId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeeImage { get; set; }
        public int ReviewerId { get; set; }
        public string ReviewerName { get; set; }
        public string ReviewerEmail { get; set; }
        public string ReviewerImage { get; set; }
        public int ReviewCycleId { get; set; }
        public string ReviewCycleName { get; set; }
        public DateTime ReviewDate { get; set; }
        public decimal? Rating { get; set; }
        public string Status { get; set; } // Scheduled, In Progress, Completed
        public string OverallComments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        
        // Helper properties for roles
        public int? EmployeeRoleId { get; set; }
        public string EmployeeRoleName { get; set; }
    }

    public class EmployeeReviewRatingModel
    {
        public int ReviewRatingId { get; set; }
        public int ReviewId { get; set; }
        public int IndicatorId { get; set; }
        public string IndicatorName { get; set; }
        public string IndicatorCategoryName { get; set; }
        public string MeasurementUnit { get; set; }
        public string TargetValue { get; set; }
        public string Description { get; set; }
        public int RatingValue { get; set; }
        public string Comments { get; set; }
    }

    public class ReviewSubmissionModel
    {
        public int ReviewId { get; set; }
        public string OverallComments { get; set; }
        public List<IndicatorRatingSubmission> Ratings { get; set; }
    }

    public class IndicatorRatingSubmission
    {
        public int IndicatorId { get; set; }
        public int RatingValue { get; set; }
        public string Comments { get; set; }
    }
}
