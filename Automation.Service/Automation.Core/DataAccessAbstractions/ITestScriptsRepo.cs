using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.DataAccessAbstractions
{
    public interface ITestScriptsRepo
    {
        void CreateScript(TestScripts script);
        void CreateScriptMap(TestScripts_Map scriptMap);
        void UpdateScript(int scriptId, TestScripts script);
        void UpdateScriptMap(int? userId, TestScripts_Map scriptMap);
        void DeleteScript(int scriptId, int userId);
        void DeleteScriptMap(int? userId, int masterTestScriptId);
        void UpdateLockedByFlags(TestScripts testScript);
        List<TestScripts> GetFilteredTestScripts(List<int> Ids);
        TestScripts_Map GetMappedTestScript(int id, int? userId);
    }
}
