using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Core.DataAccessAbstractions
{
    public interface ITestScriptsRepo
    {
        void CreateScript(TestScripts script);
        void UpdateScript(int scriptId, TestScripts script);
        void DeleteScript(int scriptId, int userId);
        void UpdateLockedByFlags(TestScripts testScript);
    }
}
