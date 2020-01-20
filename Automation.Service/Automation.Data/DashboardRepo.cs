using Automation.Core.DataAccessAbstractions;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

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
            return new string[] {"Feeds to be implemented"} ;
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
    }
}
