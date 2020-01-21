using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IAuthProvider
    {
        int ValidateLogin(string userName, string password);
        void CreateUser(string userName, string password);
        string ComputeHash(string input);
    }
}
