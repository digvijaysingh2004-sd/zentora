using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zentoraHRMS.Models
{
    // Wraps everything the Roles page needs
    public class RolePageViewModel
    {
        public List<RoleModel> RolesList { get; set; }
        public SidebarViewModel Modules { get; set; }
    }

    public class RoleModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; } // The Role Code (e.g. SYS-ADM)
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public bool IsActive { get; set; }
    }

    public class RoleSaveModel
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public List<RolePermissionDto> Permissions { get; set; }
    }

    public class RolePermissionDto
    {
        public string ModuleLevel { get; set; } // "p", "c", or "s"
        public int ModuleId { get; set; }
        public string ActionCode { get; set; }
    }
}