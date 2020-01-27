using System;
using System.Collections.Generic;
using System.Text;

namespace Automation.Core.DataAccessAbstractions
{
    public interface IAuthProvider
    {
        UserTable ValidateLogin(string userName, string password);
        void CreateUser(string userName, string password,int? roleId);
        string ComputeHash(string input);
    }
}
