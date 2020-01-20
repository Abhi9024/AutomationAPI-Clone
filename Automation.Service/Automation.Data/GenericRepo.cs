using Automation.Core;
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
    public class GenericRepo<T> : IGenericRepo<T>
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;

        public GenericRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        public IList<T> GetAll()
        {
            var result = new List<T>();
            var typeName = typeof(T).Name;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                result = (List<T>) con.Query<T>($"Select * from  [dbo].[{typeName}]", commandType: CommandType.Text);
            }
            return result;
        }

        public T GetById(int id)
        {
            T result;
            var typeName = typeof(T).Name;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                result = con.Query<T>($"Select * from  [dbo].[{typeName}] where ID = {id}", commandType: CommandType.Text).FirstOrDefault();
            }
            return result;
        }

        public int GetRecordsCount()
        {
            var result = 0;
            var typeName = typeof(T).Name;
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                result = (int)(con.Query<int>($"Select COUNT(*) from  [dbo].[{typeName}]", commandType: CommandType.Text)).FirstOrDefault();
            }
            return result;
        }
    }
}
