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
using Automation.Core;

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
            return @"INSERT INTO [dbo].[UserTable] ([UserName],[Password],[RoleId])
                    VALUES (@UserName, @Password,@RoleId)";
        }

        private string GetUserValidateScript()
        {
            return @"Select * from [dbo].[UserTable] where [UserName] = @UserName and [Password] = @Password";
        }

        public string ComputeHash(string input)
        {
            var data = Encoding.ASCII.GetBytes(input);
            var sha1 = new SHA1CryptoServiceProvider();
            var sha1data = sha1.ComputeHash(data);
            var hashedPassword = Encoding.ASCII.GetString(sha1data);
            return hashedPassword;
        }

        public void CreateUser(string userName, string password,int? roleId)
        {
            var hashedPassword = ComputeHash(password);
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);
                parameters.Add("@Password", hashedPassword);
                parameters.Add("@RoleId", roleId);

                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public UserTable ValidateLogin(string userName, string password)
        {
            var hashedPassword = ComputeHash(password);
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", userName);
                parameters.Add("@Password", hashedPassword);

               var user =  con.Query<UserTable>($"{GetUserValidateScript()}",
                    parameters,
                    commandType: CommandType.Text).FirstOrDefault();
                return user;
            }
        }
    }
}
