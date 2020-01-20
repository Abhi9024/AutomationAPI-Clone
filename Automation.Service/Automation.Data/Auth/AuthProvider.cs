using Automation.Core.DataAccessAbstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using System.Linq;

namespace Automation.Data.Auth
{
    public class AuthProvider : IAuthProvider
    {
        private IConfiguration _config;
        private string strConnectionString;

        public AuthProvider(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScript()
        {
            return @"INSERT INTO [dbo].[UserTable] ([UserName],[Password])
                    VALUES (@UserName, @Password)";
        }

        private string GetUserValidateScript()
        {
            return @"Select Count(*) from [dbo].[UserTable] where [UserName] = @UserName and [Password] = @Password";
        }

        public string ComputeHash(string input)
        {
            var data = Encoding.ASCII.GetBytes(input);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);
            var hashedPassword = Encoding.ASCII.GetString(sha1data);
            return hashedPassword;
        }

        public void CreateUser(string userName, string password)
        {
            var hashedPassword = ComputeHash(password);
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);
                parameters.Add("@Password", hashedPassword);

                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public bool ValidateLogin(string userName, string password)
        {
            var hashedPassword = ComputeHash(password);
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);
                parameters.Add("@Password", hashedPassword);

               var count =  con.Query<int>($"{GetUserValidateScript()}",
                    parameters,
                    commandType: CommandType.Text);
                return count.FirstOrDefault() > 0;
            }
        }
    }
}
