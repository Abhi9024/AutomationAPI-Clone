using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface ITestControllerRepo
    {
        void CreateController1(ModuleController controller1);
        void CreateController1Map(int? userId, ModuleController controller1);
        void UpdateController1(int controllerId, ModuleController controller1);
        void UpdateController1Map(int? userId, ModuleController_Map controller1);
        void DeleteController1(int controllerId);
        void DeleteController1Map(int? userId, string moduleId);
        ModuleController_Map GetMappedModuleData(int? userId, ModuleController controller1);
        List<string> GetAllModuleID();

        TestController_Map GetMappedTestControllerData(int? userId, TestController controller2);
        void CreateController2(TestController controller2);
        void CreateController2Map(int? userId,TestController controller2);
        void UpdateController2(int controllerId, TestController controller2);
        void UpdateController2Map(int? userId, TestController_Map controller2);
        void DeleteController2(int controllerId);
        void DeleteController2Map(int? userId, string testCaseID);

        void CreateController3(BrowserVMExec controller3);
        void UpdateController3(int controllerId, BrowserVMExec controller3);
        void DeleteController3(int controllerId, int userId);
    }

}
