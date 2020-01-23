using Automation.Core.DataAccessAbstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Automation.Core;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Automation.Data
{
    public class KeywordEntityRepo : IKeywordEntityRepo
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;

        public KeywordEntityRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScript()
        {
            return @"INSERT INTO [dbo].[KeywordLibrary] ([Module],[FunctionName],[StepDescription],[ActionOrKeyword],[ObjectLogicalName],[Run],
                     Param1, Param2, Param3, Param4, Param5, Param6, Param8, Param7, Param9, Param10,
                     Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20, [StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@Module, @FunctionName, @StepDescription, @ActionOrKeyword, @ObjectLogicalName, @Run, 
                            @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param8, @Param7, @Param9, @Param10,
                            @Param11, @Param12, @Param13, @Param14, @Param15, @Param16, @Param17, @Param18, @Param19, @Param20,
                            @StatusID, @CUDStatusID, @IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn, @UserId)";
        }

        private string GetInsertScriptMap()
        {
            return @"INSERT INTO [dbo].[KeywordLibrary_Map] ([Module],[FunctionName],[StepDescription],[ActionOrKeyword],[ObjectLogicalName],[Run],
                     Param1, Param2, Param3, Param4, Param5, Param6, Param8, Param7, Param9, Param10,
                     Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20, [LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@Module, @FunctionName, @StepDescription, @ActionOrKeyword, @ObjectLogicalName, @Run, 
                            @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param8, @Param7, @Param9, @Param10,
                            @Param11, @Param12, @Param13, @Param14, @Param15, @Param16, @Param17, @Param18, @Param19, @Param20,
                            @LockedByUser, @CreatedOn, @UpdatedOn, @UserId)";
        }

        private string GetUpdateScript()
        {
            return @"UPDATE [dbo].[KeywordLibrary]
                     SET [FunctionName]=@FunctionName,[StepDescription]=@StepDescription,[ActionOrKeyword]= @ActionOrKeyword,[ObjectLogicalName]=@ObjectLogicalName,[Run]=@Run,
                          Param1 = @Param1, Param2 = @Param2, Param3 = @Param3, Param4 = @Param4, Param5 = @Param5, Param6 = @Param6, Param8 = @Param8, 
                          Param7 = @Param7, Param9 = @Param9, Param10 = @Param10,Param11 = @Param11, Param12 = @Param12, Param13 = @Param13, Param14 = @Param14, Param15 = @Param15, 
                          Param16 = @Param16, Param17 = @Param17, Param18 = @Param18, Param19 = @Param19, Param20 = @Param20,[Module]=@Module,
                          [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetUpdateScriptMap()
        {
            return @"UPDATE [dbo].[KeywordLibrary_Map]
                     SET [FunctionName]=@FunctionName,[StepDescription]=@StepDescription,[ActionOrKeyword]= @ActionOrKeyword,[ObjectLogicalName]=@ObjectLogicalName,[Run]=@Run,
                          Param1 = @Param1, Param2 = @Param2, Param3 = @Param3, Param4 = @Param4, Param5 = @Param5, Param6 = @Param6, Param8 = @Param8, 
                          Param7 = @Param7, Param9 = @Param9, Param10 = @Param10,Param11 = @Param11, Param12 = @Param12, Param13 = @Param13, Param14 = @Param14, Param15 = @Param15, 
                          Param16 = @Param16, Param17 = @Param17, Param18 = @Param18, Param19 = @Param19, Param20 = @Param20,[Module]=@Module,
                          [LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE [FunctionName]=@FunctionName and [UserId]=@UserId";
        }

        private string GetUpdateLockedByUserScript()
        {
            return @"UPDATE [dbo].[KeywordLibrary]
                     SET [IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetDeleteScript()
        {
            return @"UPDATE [dbo].[KeywordLibrary]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetDeleteScriptMap()
        {
            return @"DELETE FROM [dbo].[KeywordLibrary_Map]
                     WHERE [FunctionName]=@FunctionName and [UserId]=@UserId";
        }

        public void CreateKeyword(KeywordLibrary keyword)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FunctionName", keyword.FunctionName);
                parameters.Add("@StepDescription", keyword.StepDescription);
                parameters.Add("@ActionOrKeyword", keyword.ActionOrKeyword);
                parameters.Add("@ObjectLogicalName", keyword.ObjectLogicalName);
                parameters.Add("@Run", keyword.Run);
                parameters.Add("@Param1", keyword.Param1);
                parameters.Add("@Param2", keyword.Param2);
                parameters.Add("@Param3", keyword.Param3);
                parameters.Add("@Param4", keyword.Param4);
                parameters.Add("@Param5", keyword.Param5);
                parameters.Add("@Param6", keyword.Param6);
                parameters.Add("@Param8", keyword.Param8);
                parameters.Add("@Param7", keyword.Param7);
                parameters.Add("@Param9", keyword.Param9);
                parameters.Add("@Param10", keyword.Param10);
                parameters.Add("@Param11", keyword.Param11);
                parameters.Add("@Param12", keyword.Param12);
                parameters.Add("@Param13", keyword.Param13);
                parameters.Add("@Param14", keyword.Param14);
                parameters.Add("@Param15", keyword.Param15);
                parameters.Add("@Param16", keyword.Param16);
                parameters.Add("@Param17", keyword.Param17);
                parameters.Add("@Param18", keyword.Param18);
                parameters.Add("@Param19", keyword.Param19);
                parameters.Add("@Param20", keyword.Param20);
                parameters.Add("@Module", keyword.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", keyword.IsLocked);
                parameters.Add("@LockedByUser", keyword.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", keyword.UserId);

                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteKeyword(int keywordId,int userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", keywordId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateKeyword(int keywordId, KeywordLibrary keyword)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", keywordId);
                parameters.Add("@FunctionName", keyword.FunctionName);
                parameters.Add("@StepDescription", keyword.StepDescription);
                parameters.Add("@ActionOrKeyword", keyword.ActionOrKeyword);
                parameters.Add("@ObjectLogicalName", keyword.ObjectLogicalName);
                parameters.Add("@Run", keyword.Run);
                parameters.Add("@Param1", keyword.Param1);
                parameters.Add("@Param2", keyword.Param2);
                parameters.Add("@Param3", keyword.Param3);
                parameters.Add("@Param4", keyword.Param4);
                parameters.Add("@Param5", keyword.Param5);
                parameters.Add("@Param6", keyword.Param6);
                parameters.Add("@Param8", keyword.Param8);
                parameters.Add("@Param7", keyword.Param7);
                parameters.Add("@Param9", keyword.Param9);
                parameters.Add("@Param10", keyword.Param10);
                parameters.Add("@Param11", keyword.Param11);
                parameters.Add("@Param12", keyword.Param12);
                parameters.Add("@Param13", keyword.Param13);
                parameters.Add("@Param14", keyword.Param14);
                parameters.Add("@Param15", keyword.Param15);
                parameters.Add("@Param16", keyword.Param16);
                parameters.Add("@Param17", keyword.Param17);
                parameters.Add("@Param18", keyword.Param18);
                parameters.Add("@Param19", keyword.Param19);
                parameters.Add("@Param20", keyword.Param20);
                parameters.Add("@Module", keyword.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", null);
                parameters.Add("@LockedByUser", null);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", keyword.UserId);

                con.Query($"{GetUpdateScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateLockedByFlags(KeywordLibrary keyword)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", keyword.ID);
                parameters.Add("@IsLocked", keyword.IsLocked);
                parameters.Add("@LockedByUser", keyword.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);

                con.Query($"{GetUpdateLockedByUserScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateKeyword_Map(KeywordLibrary_Map keyword)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FunctionName", keyword.FunctionName);
                parameters.Add("@StepDescription", keyword.StepDescription);
                parameters.Add("@ActionOrKeyword", keyword.ActionOrKeyword);
                parameters.Add("@ObjectLogicalName", keyword.ObjectLogicalName);
                parameters.Add("@Run", keyword.Run);
                parameters.Add("@Param1", keyword.Param1);
                parameters.Add("@Param2", keyword.Param2);
                parameters.Add("@Param3", keyword.Param3);
                parameters.Add("@Param4", keyword.Param4);
                parameters.Add("@Param5", keyword.Param5);
                parameters.Add("@Param6", keyword.Param6);
                parameters.Add("@Param8", keyword.Param8);
                parameters.Add("@Param7", keyword.Param7);
                parameters.Add("@Param9", keyword.Param9);
                parameters.Add("@Param10", keyword.Param10);
                parameters.Add("@Param11", keyword.Param11);
                parameters.Add("@Param12", keyword.Param12);
                parameters.Add("@Param13", keyword.Param13);
                parameters.Add("@Param14", keyword.Param14);
                parameters.Add("@Param15", keyword.Param15);
                parameters.Add("@Param16", keyword.Param16);
                parameters.Add("@Param17", keyword.Param17);
                parameters.Add("@Param18", keyword.Param18);
                parameters.Add("@Param19", keyword.Param19);
                parameters.Add("@Param20", keyword.Param20);
                parameters.Add("@Module", keyword.Module);
                parameters.Add("@LockedByUser", keyword.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", keyword.UserId);

                con.Query($"{GetInsertScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateKeywordMap(int? userId, KeywordLibrary_Map keyword)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FunctionName", keyword.FunctionName);
                parameters.Add("@StepDescription", keyword.StepDescription);
                parameters.Add("@ActionOrKeyword", keyword.ActionOrKeyword);
                parameters.Add("@ObjectLogicalName", keyword.ObjectLogicalName);
                parameters.Add("@Run", keyword.Run);
                parameters.Add("@Param1", keyword.Param1);
                parameters.Add("@Param2", keyword.Param2);
                parameters.Add("@Param3", keyword.Param3);
                parameters.Add("@Param4", keyword.Param4);
                parameters.Add("@Param5", keyword.Param5);
                parameters.Add("@Param6", keyword.Param6);
                parameters.Add("@Param8", keyword.Param8);
                parameters.Add("@Param7", keyword.Param7);
                parameters.Add("@Param9", keyword.Param9);
                parameters.Add("@Param10", keyword.Param10);
                parameters.Add("@Param11", keyword.Param11);
                parameters.Add("@Param12", keyword.Param12);
                parameters.Add("@Param13", keyword.Param13);
                parameters.Add("@Param14", keyword.Param14);
                parameters.Add("@Param15", keyword.Param15);
                parameters.Add("@Param16", keyword.Param16);
                parameters.Add("@Param17", keyword.Param17);
                parameters.Add("@Param18", keyword.Param18);
                parameters.Add("@Param19", keyword.Param19);
                parameters.Add("@Param20", keyword.Param20);
                parameters.Add("@Module", keyword.Module);
                parameters.Add("@LockedByUser", null);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);

                con.Query($"{GetUpdateScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteKeywordMap(int? userId, string functionName)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FunctionName", functionName);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }
    }
}
