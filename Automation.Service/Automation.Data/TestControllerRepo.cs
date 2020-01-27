using Automation.Core.DataAccessAbstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Automation.Core;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace Automation.Data
{
    public class TestControllerRepo : ITestControllerRepo
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;

        public TestControllerRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScriptForTestController1()
        {
            return @"INSERT INTO [dbo].[ModuleController] ([ModuleID],[ModuleSeqID],[MachineID],[MachineSequenceID],[Run],
                     [StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn])
                    VALUES (@ModuleID, @ModuleSeqID, @MachineID, @MachineSequenceID, @Run, 
                            @StatusID, @CUDStatusID,@IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn )";
        }

        private string GetInsertScriptForTestController1Map()
        {
            return @"INSERT INTO [dbo].[ModuleController_Map] ([ModuleID],[ModuleSeqID],[MachineID],[MachineSequenceID],[Run],
                     [LockedByUser], [CreatedOn], [UpdatedOn], [UserId],[RefId])
                    VALUES (@ModuleID, @ModuleSeqID, @MachineID, @MachineSequenceID, @Run, 
                            @LockedByUser, @CreatedOn, @UpdatedOn, @UserId,@RefId)";
        }

        private string GetUpdateScriptForTestController1()
        {
            return @"UPDATE [dbo].[ModuleController]
                     SET  [ModuleID] =  @ModuleID,[ModuleSeqID] = @ModuleSeqID,
                          [MachineID] = @MachineID,[MachineSequenceID] = @MachineSequenceID,[Run] = @Run,
                          [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, 
                          [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetUpdateScriptForTestController1Map()
        {
            return @"UPDATE [dbo].[ModuleController_Map]
                     SET  [ModuleID] =  @ModuleID,[ModuleSeqID] = @ModuleSeqID,
                          [MachineID] = @MachineID,[MachineSequenceID] = @MachineSequenceID,[Run] = @Run,
                          [LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn,[UserId]=@UserId,[RefId]=@RefId
                     WHERE [ModuleID]=@ModuleID and [UserId]=@UserId";
        }

        private string GetDeleteScriptForTestController1()
        {
            return @"UPDATE [dbo].[ModuleController]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID
                     WHERE ID=@Id";
        }

        private string GetDeleteScriptForTestController1Map()
        {
            return @"DELETE FROM [dbo].[ModuleController_Map]
                     WHERE [ModuleID]=@ModuleID and [UserId]=@UserId and [RefId]=@RefId";
        }

        private string GetInsertScriptForTestController2()
        {
            return @"INSERT INTO [dbo].[TestController] ([FeatureID],[TestCaseID],[Run],[Iterations],
                                                      [Browsers],[SequenceID],[TestType],[JiraID],[StepsCount],[TestScriptName],[TestScriptDescription],
                                                      [StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn])
                    VALUES (@FeatureID,@TestCaseID,@Run,@Iterations,@Browsers,@SequenceID,@TestType,@JIRA_ID,@StepsCount,@TestScriptName,
                            @TestScriptDescription,@StatusID, @CUDStatusID, @IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn)";
        }

        private string GetInsertScriptForTestController2Map()
        {
            return @"INSERT INTO [dbo].[TestController_Map] ([FeatureID],[TestCaseID],[Run],[Iterations],
                                                      [Browsers],[SequenceID],[TestType],[JiraID],[StepsCount],[TestScriptName],[TestScriptDescription],
                     [LockedByUser], [CreatedOn], [UpdatedOn], [UserId],[RefId])
                    VALUES (@FeatureID,@TestCaseID,@Run,@Iterations,@Browsers,@SequenceID,@TestType,@JIRA_ID,@StepsCount,@TestScriptName,
                            @TestScriptDescription, @LockedByUser, @CreatedOn, @UpdatedOn,@UserId,@RefId)";
        }

        private string GetUpdateScriptForTestController2()
        {
            return @"UPDATE [dbo].[TestController]
                     SET  [FeatureID] = @FeatureID,[TestCaseID] =@TestCaseID ,[Run] =@Run,[Iterations] = @Iterations,
                          [Browsers] =@Browsers,[SequenceID] =@SequenceID,[TestType]=@TestType,[JiraID]=@JIRA_ID,
                          [StepsCount]=@StepsCount,[TestScriptName]=@TestScriptName,
                          [TestScriptDescription]=@TestScriptDescription,[StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,
                          [IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetUpdateScriptForTestController2Map()
        {
            return @"UPDATE [dbo].[TestController_Map]
                     SET  [FeatureID] = @FeatureID,[TestCaseID] =@TestCaseID ,[Run] =@Run,[Iterations] = @Iterations,
                          [Browsers] =@Browsers,[SequenceID] =@SequenceID,[TestType]=@TestType,[JiraID]=@JIRA_ID,
                          [StepsCount]=@StepsCount,[TestScriptName]=@TestScriptName,
                          [TestScriptDescription]=@TestScriptDescription,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn,
                          [UserId]=@UserId,[RefId]=@RefId
                     WHERE [TestCaseID]=@TestCaseID and [UserId]=@UserId";
        }

        private string GetDeleteScriptForTestController2()
        {
            return @"UPDATE [dbo].[TestController]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID
                     WHERE ID=@Id";
        }

        private string GetDeleteScriptForTestController2Map()
        {
            return @"DELETE FROM [dbo].[TestController_Map]
                     WHERE [TestCaseID]=@TestCaseID and [UserId]=@UserId and [RefId]=@RefId";
        }

        private string GetInsertScriptForTestController3()
        {
            return @"INSERT INTO [dbo].[BrowserVMExec] ([VMID],[Browser],[Run],[StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@VMID, @Browser, @Run,@StatusID, @CUDStatusID, @IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn,@UserId )";
        }

        private string GetUpdateScriptForTestController3()
        {
            return @"UPDATE [dbo].[BrowserVMExec]
                     SET [VMID] = @VMID,[Browser]=@Browser,[Run]=@Run,[StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser,[UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetDeleteScriptForTestController3()
        {
            return @"UPDATE [dbo].[BrowserVMExec]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[UserId]=@UserId
                     WHERE ID=@Id";
        }


        private string GetDataScriptFromModuleControllerMap()
        {
            return @"Select * from [dbo].[ModuleController_Map] where [UserId]=@UserId and [ModuleID]=@ModuleID and [RefId]=@RefId";
        }

        private string GetDataScriptFromTestControllerMap()
        {
            return @"Select * from [dbo].[TestController_Map] where [UserId]=@UserId and [TestCaseID]=@TestCaseID and [RefId]=@RefId";
        }

        private string GetAllModuleIDScript()
        {
            return @"SELECT DISTINCT [ModuleID] FROM [dbo].[ModuleController]";
        }

        public void CreateController1(ModuleController controller1)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", controller1.ModuleID);
                parameters.Add("@ModuleSeqID", controller1.ModuleSeqID);
                parameters.Add("@MachineID", controller1.MachineID);
                parameters.Add("@MachineSequenceID", controller1.MachineSequenceID);
                parameters.Add("@Run", controller1.Run);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", controller1.IsLocked);
                parameters.Add("@LockedByUser", controller1.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);


                con.Query($"{GetInsertScriptForTestController1()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateController1Map(int? userId, ModuleController controller1, int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", controller1.ModuleID);
                parameters.Add("@ModuleSeqID", controller1.ModuleSeqID);
                parameters.Add("@MachineID", controller1.MachineID);
                parameters.Add("@MachineSequenceID", controller1.MachineSequenceID);
                parameters.Add("@Run", controller1.Run);
                parameters.Add("@LockedByUser", controller1.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                con.Query($"{GetInsertScriptForTestController1Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateController2(TestController controller2)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeatureID", controller2.FeatureID);
                parameters.Add("@TestCaseID", controller2.TestCaseID);
                parameters.Add("@Run", controller2.Run);
                parameters.Add("@Iterations", controller2.Iterations);
                parameters.Add("@Browsers", controller2.Browsers);
                parameters.Add("@SequenceID", controller2.SequenceID);
                parameters.Add("@TestType", controller2.TestType);
                parameters.Add("@JIRA_ID", controller2.JiraID);
                parameters.Add("@StepsCount", controller2.StepsCount);
                parameters.Add("@TestScriptName", controller2.TestScriptName);
                parameters.Add("@TestScriptDescription", controller2.TestScriptDescription);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", controller2.IsLocked);
                parameters.Add("@LockedByUser", controller2.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);


                con.Query($"{GetInsertScriptForTestController2()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateController2Map(int? userId, TestController controller2,int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeatureID", controller2.FeatureID);
                parameters.Add("@TestCaseID", controller2.TestCaseID);
                parameters.Add("@Run", controller2.Run);
                parameters.Add("@Iterations", controller2.Iterations);
                parameters.Add("@Browsers", controller2.Browsers);
                parameters.Add("@SequenceID", controller2.SequenceID);
                parameters.Add("@TestType", controller2.TestType);
                parameters.Add("@JIRA_ID", controller2.JiraID ?? " ");
                parameters.Add("@StepsCount", controller2.StepsCount);
                parameters.Add("@TestScriptName", controller2.TestScriptName);
                parameters.Add("@TestScriptDescription", controller2.TestScriptDescription);
                parameters.Add("@LockedByUser", controller2.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                con.Query($"{GetInsertScriptForTestController2Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateController3(BrowserVMExec controller3)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@VMID", controller3.VMID);
                parameters.Add("@Browser", controller3.Browser);
                parameters.Add("@Run", controller3.Run);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", controller3.IsLocked);
                parameters.Add("@LockedByUser", controller3.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", controller3.UserId);

                con.Query($"{GetInsertScriptForTestController3()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteController1(int controllerId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", controllerId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);

                con.Query($"{GetDeleteScriptForTestController1()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteController1Map(int? userId, string moduleId,int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", moduleId);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                con.Query($"{GetDeleteScriptForTestController1Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteController2(int controllerId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", controllerId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);

                con.Query($"{GetDeleteScriptForTestController2()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteController2Map(int? userId, string testCaseID,int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestCaseID", testCaseID);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                con.Query($"{GetDeleteScriptForTestController2Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteController3(int controllerId,int userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", controllerId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScriptForTestController3()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateController1(int controllerId, ModuleController controller1)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", controller1.ModuleID);
                parameters.Add("@ModuleSeqID", controller1.ModuleSeqID);
                parameters.Add("@MachineID", controller1.MachineID);
                parameters.Add("@MachineSequenceID", controller1.MachineSequenceID);
                parameters.Add("@Run", controller1.Run);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", controller1.IsLocked);
                parameters.Add("@LockedByUser", controller1.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@Id", controllerId);

                con.Query($"{GetUpdateScriptForTestController1()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public ModuleController_Map GetMappedModuleData(int? userId, ModuleController controller1,int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", controller1.ModuleID);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                var result = con.Query<ModuleController_Map>($"{GetDataScriptFromModuleControllerMap()}",
                     parameters,
                     commandType: CommandType.Text).FirstOrDefault();

                return result;
            }
        }


        public void UpdateController1Map(int? userId, ModuleController_Map controller1)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@ModuleID", controller1.ModuleID);
                parameters.Add("@ModuleSeqID", controller1.ModuleSeqID);
                parameters.Add("@MachineID", controller1.MachineID);
                parameters.Add("@MachineSequenceID", controller1.MachineSequenceID);
                parameters.Add("@Run", controller1.Run);
                parameters.Add("@LockedByUser", controller1.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", controller1.RefId);

                con.Query($"{GetUpdateScriptForTestController1Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public TestController_Map GetMappedTestControllerData(int? userId, TestController controller2,int refId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestCaseID", controller2.TestCaseID);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", refId);

                var result = con.Query<TestController_Map>($"{GetDataScriptFromTestControllerMap()}",
                     parameters,
                     commandType: CommandType.Text).FirstOrDefault();

                return result;
            }
        }

        public void UpdateController2(int controllerId, TestController controller2)
        {

            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeatureID", controller2.FeatureID);
                parameters.Add("@TestCaseID", controller2.TestCaseID);
                parameters.Add("@Run", controller2.Run);
                parameters.Add("@Iterations", controller2.Iterations);
                parameters.Add("@Browsers", controller2.Browsers);
                parameters.Add("@SequenceID", controller2.SequenceID);
                parameters.Add("@TestType", controller2.TestType);
                parameters.Add("@JIRA_ID", controller2.JiraID);
                parameters.Add("@StepsCount", controller2.StepsCount);
                parameters.Add("@TestScriptName", controller2.TestScriptName);
                parameters.Add("@TestScriptDescription", controller2.TestScriptDescription);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", controller2.IsLocked);
                parameters.Add("@LockedByUser", controller2.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@Id", controllerId);

                con.Query($"{GetUpdateScriptForTestController2()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateController2Map(int? userId, TestController_Map controller2)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@FeatureID", controller2.FeatureID);
                parameters.Add("@TestCaseID", controller2.TestCaseID);
                parameters.Add("@Run", controller2.Run);
                parameters.Add("@Iterations", controller2.Iterations);
                parameters.Add("@Browsers", controller2.Browsers);
                parameters.Add("@SequenceID", controller2.SequenceID);
                parameters.Add("@TestType", controller2.TestType);
                parameters.Add("@JIRA_ID", controller2.JiraID);
                parameters.Add("@StepsCount", controller2.StepsCount);
                parameters.Add("@TestScriptName", controller2.TestScriptName);
                parameters.Add("@TestScriptDescription", controller2.TestScriptDescription);
                parameters.Add("@LockedByUser", controller2.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);
                parameters.Add("@RefId", controller2.RefId);

                con.Query($"{GetUpdateScriptForTestController2Map()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateController3(int controllerId, BrowserVMExec controller3)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", controllerId);
                parameters.Add("@VMID", controller3.VMID);
                parameters.Add("@Browser", controller3.Browser);
                parameters.Add("@Run", controller3.Run);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", controller3.IsLocked);
                parameters.Add("@LockedByUser", controller3.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", controller3.UserId);

                con.Query($"{GetUpdateScriptForTestController3()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public List<string> GetAllModuleID()
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var result = (List<string>) con.Query<string>($"{GetAllModuleIDScript()}",
                    commandType: CommandType.Text);

                return result;
            }
        }
    }
}
