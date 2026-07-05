using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zentoraHRMS.Models
{
    public class CompanyModel
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string CompanyType { get; set; }
        public string IndustryType { get; set; }
        public string TaxId { get; set; }
        public string PrimaryEmail { get; set; }
        public string PrimaryPhone { get; set; }
        public string SecondaryPhone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int WorkingDaysPerWeek { get; set; }
        public string PrimaryHRName { get; set; }
        public string PrimaryHREmail { get; set; }
        public string PrimaryHRPhone { get; set; }
        public string DefaultCurrency { get; set; }
        public string DefaultTimeZone { get; set; }
        public string FinancialYearStart { get; set; }
        public string FinancialYearEnd { get; set; }
        public string PayrollCycle { get; set; }
        public string DefaultLeavePolicy { get; set; }
        public string ShiftTimings { get; set; }
        public string LogoUrl { get; set; }
        public string TagLine { get; set; }
        public string BrandingColor { get; set; }
        public string Website { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public int ParentCompanyId { get; set; }
        public int NumberOfEmployees { get; set; }
        public string Certifications { get; set; }
        public string InsuranceDetails { get; set; }
        public string Note { get; set; }
    }
}