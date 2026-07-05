using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zentoraHRMS.Models
{
    public class SidebarViewModel
    {
        public List<ParentModuleVM> ParentModules { get; set; }

        public SidebarViewModel()
        {
            ParentModules = new List<ParentModuleVM>();
        }
    }

    public class ParentModuleVM
    {
        public int ParentModuleId { get; set; }
        public string ModuleName { get; set; }
        public string IconClass { get; set; }
        public string RouteUrl { get; set; }
        public int SortOrder { get; set; }
        public List<ChildModuleVM> Children { get; set; }

        public ParentModuleVM()
        {
            Children = new List<ChildModuleVM>();
        }
    }

    public class ChildModuleVM
    {
        public int ChildModuleId { get; set; }
        public int ParentModuleId { get; set; }
        public string ModuleName { get; set; }
        public string RouteUrl { get; set; }
        public int SortOrder { get; set; }
        public List<SubChildModuleVM> SubChildren { get; set; }

        public ChildModuleVM()
        {
            SubChildren = new List<SubChildModuleVM>();
        }
    }

    public class SubChildModuleVM
    {
        public int SubChildModuleId { get; set; }
        public int ChildModuleId { get; set; }
        public string ModuleName { get; set; }
        public string RouteUrl { get; set; }
        public int SortOrder { get; set; }
    }
}