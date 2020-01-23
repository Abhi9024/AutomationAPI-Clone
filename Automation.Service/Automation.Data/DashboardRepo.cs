using Automation.Core.DataAccessAbstractions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Automation.Core;

namespace Automation.Data
{
    public class DashboardRepo : IDashboardRepo
    {
        private IConfiguration _config;
        private string strConnectionString;

        public DashboardRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        public string[] ModifiedFeeds()
        {
            var result = new List<string>();
            
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "Repository", "ModuleController", "TestController", "BrowserVMExec", "TestData", "TestScripts" };
                foreach (var item in typeNames)
                {
                   var  allUsers = con.Query<string>($"{GetUserModifiedScript(item)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }
            
            return result.Distinct().ToArray() ;
        }

        private string GetUserModifiedScript(string tableName)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId from [dbo].[{tableName}] where UpdatedOn IS NOT NULL)";
        }

        public int RecordsModified()
        {
            var result = 0;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "Repository", "ModuleController", "TestController", "BrowserVMExec", "TestData", "TestScripts" };
                foreach (var item in typeNames)
                {
                    result += (int)(con.Query<int>($"Select COUNT(*) from  [dbo].[{item}] WHERE UpdatedOn IS NOT NULL", commandType: CommandType.Text)).FirstOrDefault();
                }
            }
            return result;
        }

        private string GetAllRolesScript()
        {
            return @"select * from [dbo].[UserRole]";
        }

        public List<UserRole> GetAllRoles()
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                 var result = (con.Query<UserRole>($"{GetAllRolesScript()}", commandType: CommandType.Text)).ToList();
                return result;
            }
        }
    }
}
