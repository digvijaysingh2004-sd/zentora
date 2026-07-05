using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zentoraHRMS.Models
{
    public class BranchModel
    {
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string FaxNumber { get; set; }
        public string Website { get; set; }
    }
}