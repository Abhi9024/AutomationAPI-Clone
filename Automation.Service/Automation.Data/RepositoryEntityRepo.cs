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
    public class RepositoryEntityRepo : IRepositoryEntityRepo
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;

        public RepositoryEntityRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScript()
        {
            return @"INSERT INTO [dbo].[Repository] ([LogicalName],[FindMethod],[XpathQueryPropertyName],[PropertyValue],[TagName],[Module],
                     [StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@LogicalName, @FindMethod, @XpathQueryPropertyName, @PropertyValue, @TagName, @Module,
                            @StatusID, @CUDStatusID,@IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn,@UserId)";
        }

        private string GetUpdateScript()
        {
            return @"UPDATE [dbo].[Repository]
                     SET [LogicalName]=@LogicalName,[FindMethod]=@FindMethod,[XpathQueryPropertyName]=@XpathQueryPropertyName,
                          [PropertyValue]=@PropertyValue,[TagName]=@TagName,[Module]=@Module,
                          [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, 
                          [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetUpdateLockedByUserScript()
        {
            return @"UPDATE [dbo].[Repository]
                     SET [IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetDeleteScript()
        {
            return @"UPDATE [dbo].[Repository]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        public void CreateRepository(Repository repositoryEntity)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@LogicalName", repositoryEntity.LogicalName);
                parameters.Add("@FindMethod", repositoryEntity.FindMethod);
                parameters.Add("@XpathQueryPropertyName", repositoryEntity.XpathQueryPropertyName);
                parameters.Add("@PropertyValue", repositoryEntity.PropertyValue);
                parameters.Add("@TagName", repositoryEntity.TagName);
                parameters.Add("@Module", repositoryEntity.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", repositoryEntity.IsLocked);
                parameters.Add("@LockedByUser", repositoryEntity.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", repositoryEntity.UserId);


                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteRepository(int repositoryId,int userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", repositoryId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateRepository(int repositoryId, Repository repositoryEntity)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", repositoryId);
                parameters.Add("@LogicalName", repositoryEntity.LogicalName);
                parameters.Add("@FindMethod", repositoryEntity.FindMethod);
                parameters.Add("@XpathQueryPropertyName", repositoryEntity.XpathQueryPropertyName);
                parameters.Add("@PropertyValue", repositoryEntity.PropertyValue);
                parameters.Add("@TagName", repositoryEntity.TagName);
                parameters.Add("@Module", repositoryEntity.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", null);
                parameters.Add("@LockedByUser", null);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", repositoryEntity.UserId);

                con.Query($"{GetUpdateScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateLockedByFlags(Repository repository)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", repository.ID);
                parameters.Add("@IsLocked", repository.IsLocked);
                parameters.Add("@LockedByUser", repository.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);

                con.Query($"{GetUpdateLockedByUserScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }
    }
}
