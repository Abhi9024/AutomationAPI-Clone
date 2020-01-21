using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core
{
    public class ModuleController : BaseEntity
    {
        public string ModuleID { get; set; }
        public int ModuleSeqID { get; set; }
        public string MachineID { get; set; }
        public int MachineSequenceID { get; set; }
        public string Run { get; set; }
        public int StatusID { get; set; }
        public int CUDStatusID { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class ModuleController_Map : BaseEntity
    {
        public string ModuleID { get; set; }
        public int ModuleSeqID { get; set; }
        public string MachineID { get; set; }
        public int MachineSequenceID { get; set; }
        public string Run { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UserId { get; set; }
    }

    public class TestController : BaseEntity
    {
        public string FeatureID { get; set; }
        public string TestCaseID { get; set; }
        public string Run { get; set; }
        public int Iterations { get; set; }
        public string Browsers { get; set; }
        public int SequenceID { get; set; }
        public string TestType { get; set; }
        public string JiraID { get; set; }
        public int StepsCount { get; set; }
        public string TestScriptName { get; set; }
        public string TestScriptDescription { get; set; }
        public int StatusID { get; set; }
        public int CUDStatusID { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }

    public class TestController_Map : BaseEntity
    {
        public string FeatureID { get; set; }
        public string TestCaseID { get; set; }
        public string Run { get; set; }
        public int Iterations { get; set; }
        public string Browsers { get; set; }
        public int SequenceID { get; set; }
        public string TestType { get; set; }
        public string JiraID { get; set; }
        public int StepsCount { get; set; }
        public string TestScriptName { get; set; }
        public string TestScriptDescription { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UserId { get; set; }
    }

    public class BrowserVMExec : BaseEntity
    {
        public string VMID { get; set; }
        public string Browser { get; set; }
        public string Run { get; set; }
        public int StatusID { get; set; }
        public int CUDStatusID { get; set; }
        public bool? IsLocked { get; set; }
        public int? LockedByUser { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UserId { get; set; }
    }
}
