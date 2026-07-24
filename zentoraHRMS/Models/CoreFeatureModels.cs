using System;

namespace zentoraHRMS.Models
{
    public class DesignationModel
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class AwardModel
    {
        public int AwardId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string AwardType { get; set; }
        public string Gift { get; set; }
        public decimal CashPrice { get; set; }
        public DateTime AwardDate { get; set; }
        public string Description { get; set; }
    }

    public class PromotionModel
    {
        public int PromotionId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public DateTime PromotionDate { get; set; }
        public string Description { get; set; }
    }

    public class AnnouncementModel
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CreatedBy { get; set; }
    }

    public class AssetModel
    {
        public int AssetId { get; set; }
        public string AssetName { get; set; }
        public string AssetCode { get; set; }
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Status { get; set; }
    }

    public class JobPostingModel
    {
        public int JobId { get; set; }
        public string Title { get; set; }
        public string JobType { get; set; }
        public string Location { get; set; }
        public int NoOfVacancies { get; set; }
        public string ExperienceRequired { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
    }

    public class CandidateModel
    {
        public int CandidateId { get; set; }
        public int JobId { get; set; }
        public string JobTitle { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ResumeUrl { get; set; }
        public string ApplicationStatus { get; set; }
        public DateTime AppliedDate { get; set; }
    }

    public class LeaveApplicationModel
    {
        public int LeaveApplicationId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public int LeaveCategoryId { get; set; }
        public string LeaveCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string Status { get; set; }
        public DateTime AppliedDate { get; set; }
    }

    public class AttendanceRecordModel
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public DateTime AttendanceDate { get; set; }
        public TimeSpan ClockIn { get; set; }
        public TimeSpan? ClockOut { get; set; }
        public string Status { get; set; }
    }

    public class SalaryComponentModel
    {
        public int ComponentId { get; set; }
        public string ComponentName { get; set; }
        public string ComponentType { get; set; }
        public string Description { get; set; }
    }

    public class EmployeeSalaryModel
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
    }

    public class PayrollRunModel
    {
        public int PayrollRunId { get; set; }
        public string MonthYear { get; set; }
        public DateTime ProcessedDate { get; set; }
        public string Status { get; set; }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public int EmpId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleType { get; set; }
        public bool IsActive { get; set; }
    }

    public class EmployeeModel
    {
        public int Id { get; set; }
        public string EmpCode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string PhoneCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Designation { get; set; }
        public string Company { get; set; }
        public string Branch { get; set; }
        public string Department { get; set; }
        public string SubDepartment { get; set; }
        public string OfficeShift { get; set; }
        public string OfficeLocation { get; set; }
        public DateTime? DOJ { get; set; }
        public DateTime? DOL { get; set; }
        public DateTime? DOB { get; set; }
        public string ProfileImage { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int? RoleType { get; set; }
        public string LeaveCategory { get; set; }
        public string HolidayCategory { get; set; }
        public string ProjectRole { get; set; }
        public int? ReportingManager { get; set; }
        public int? AssociateReportingManager { get; set; }
        public decimal BasicSalary { get; set; }
        public bool IsActive { get; set; }
    }
}
