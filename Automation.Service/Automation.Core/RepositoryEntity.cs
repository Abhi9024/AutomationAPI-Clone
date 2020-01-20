using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core
{
    public class Repository : BaseEntity
    {
        public string LogicalName { get; set; }
        public string FindMethod { get; set; }
        public string XpathQueryPropertyName { get; set; }
        public string PropertyValue { get; set; }
        public string TagName { get; set; }
        public string Module { get; set; }
        public int StatusID { get; set; }
        public int CUDStatusID { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UserId { get; set; }
    }
}
