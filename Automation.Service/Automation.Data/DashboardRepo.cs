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
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
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

        private string GetUserModifiedByMonthScript(string tableName,int fromMonth)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId  from {tableName} WHERE UpdatedOn >= dateadd(MONTH, datediff(MONTH, 0, GetDate()) - {fromMonth}, 0))";
        }

        public string[] GetModifiedFeedsByMonth(int fromLastMonthCount)
        {
            var result = new List<string>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
                foreach (var item in typeNames)
                {
                    var allUsers = con.Query<string>($"{GetUserModifiedByMonthScript(item,fromLastMonthCount)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }

            return result.Distinct().ToArray();
        }
        private string GetUserModifiedByWeekScript(string tableName, int fromWeek)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId  from {tableName} WHERE UpdatedOn >= dateadd(DAY, datediff(DAY, 0, GetDate()) - {fromWeek}, 0))";
        }

        public string[] GetModifiedFeedsByDay(int fromLastDayCount)
        {
            var result = new List<string>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
                foreach (var item in typeNames)
                {
                    var allUsers = con.Query<string>($"{GetUserModifiedByWeekScript(item, fromLastDayCount)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }

            return result.Distinct().ToArray();
        }

        private string GetUserModifiedByMinuteScript(string tableName, int fromMinute)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId  from {tableName} WHERE UpdatedOn >= dateadd(MINUTE, datediff(MINUTE, 0, GetDate()) - {fromMinute}, 0))";
        }

        public string[] GetModifiedFeedsByMinute(int fromLastMinuteCount)
        {
            var result = new List<string>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
                foreach (var item in typeNames)
                {
                    var allUsers = con.Query<string>($"{GetUserModifiedByMinuteScript(item, fromLastMinuteCount)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }

            return result.Distinct().ToArray();
        }

        private string GetUserModifiedByHourScript(string tableName, int fromHour)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId from {tableName} WHERE UpdatedOn >= dateadd(HOUR, datediff(HOUR, 0, GetDate()) - {fromHour}, 0))";
        }

        public string[] GetModifiedFeedsByHours(int fromLastHourCount)
        {
            var result = new List<string>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
                foreach (var item in typeNames)
                {
                    var allUsers = con.Query<string>($"{GetUserModifiedByHourScript(item, fromLastHourCount)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }

            return result.Distinct().ToArray();
        }

        private string GetUserModifiedBySecondScript(string tableName, int fromSecond)
        {
            return $"select UserName from UserTable where UserId IN (Select UserId  from {tableName} WHERE UpdatedOn >= dateadd(SECOND, datediff(MINUTE, 0, GetDate()) - {fromSecond}, 0))";
        }


        public string[] GetModifiedFeedsBySecond(int fromLastSecondCount)
        {
            var result = new List<string>();

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var typeNames = new List<string>() { "KeywordLibrary", "KeywordLibrary_Map", "Repository", "ModuleController", "ModuleController_Map", "TestController", "TestController_Map", "BrowserVMExec", "TestData", "TestScripts", "TestScripts_Map" };
                foreach (var item in typeNames)
                {
                    var allUsers = con.Query<string>($"{GetUserModifiedBySecondScript(item, fromLastSecondCount)}", commandType: CommandType.Text).ToList();
                    result.AddRange(allUsers);
                }
            }

            return result.Distinct().ToArray();
        }
    }
}
