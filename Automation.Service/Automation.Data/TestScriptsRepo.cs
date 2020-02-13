using Automation.Core.DataAccessAbstractions;
using System;
using Automation.Core;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Automation.Data
{
    public class TestScriptsRepo : ITestScriptsRepo
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;

        public TestScriptsRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScript()
        {
            return @"INSERT INTO [dbo].[TestScripts] (TestCaseID,[TCStepID], TestScriptName, FunctionDescription, FunctionName,
                    [Run], Param1, Param2, Param3, Param4, Param5, Param6, Param8, Param7, Param9, Param10,
                    Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20,
                    [Param21],[Param22],[Param23],[Param24],[Param25],[Param26],[Param27],[Param28],[Param29],[Param30],
                    [Param31],[Param32],[Param33],[Param34],[Param35],[Param36],[Param37],[Param38],[Param39],[Param40],
                    [Param41],[Param42],[Param43],[Param44],[Param45],[Param46],[Param47],[Param48],[Param49],[Param50],
                    [Param51],[Param52],[Param53],[Param54],[Param55],[Param56],[Param57],[Param58],[Param59],[Param60],
                    [Param61],[Param62],[Param63],[Param64],[Param65],[Param66],[Param67],[Param68],[Param69],[Param70],
                    [Param71],[Param72],[Param73],[Param74],[Param75],[Param76],[Param77],[Param78],[Param79],[Param80],
                    [Param81],[Param82],[Param83],[Param84],[Param85],[Param86],[Param87],[Param88],[Param89],[Param90],
                    [Param91],[Param92],[Param93],[Param94],[Param95],[Param96],[Param97],[Param98],[Param99],[Param100],
                    [Module],[StatusID],[CUDStatusID],[IsLocked],
                     [LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@TestCaseID,@TCStepID, @TestScriptName, @FunctionDescription, @FunctionName,
                    @Run, @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param8, @Param7, @Param9, @Param10,
                    @Param11, @Param12, @Param13, @Param14, @Param15, @Param16, @Param17, @Param18, @Param19, @Param20,
                    @Param21,@Param22,@Param23,@Param24,@Param25,@Param26,@Param27,@Param28,@Param29,@Param30,
                    @Param31,@Param32,@Param33,@Param34,@Param35,@Param36,@Param37,@Param38,@Param39,@Param40,
                    @Param41,@Param42,@Param43,@Param44,@Param45,@Param46,@Param47,@Param48,@Param49,@Param50,
                    @Param51,@Param52,@Param53,@Param54,@Param55,@Param56,@Param57,@Param58,@Param59,@Param60,
                    @Param61,@Param62,@Param63,@Param64,@Param65,@Param66,@Param67,@Param68,@Param69,@Param70,
                    @Param71,@Param72,@Param73,@Param74,@Param75,@Param76,@Param77,@Param78,@Param79,@Param80,
                    @Param81,@Param82,@Param83,@Param84,@Param85,@Param86,@Param87,@Param88,@Param89,@Param90,
                    @Param91,@Param92,@Param93,@Param94,@Param95,@Param96,@Param97,@Param98,@Param99,@Param100,
                    @Module,@StatusID,@CUDStatusID,
                    @IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn,@UserId )";
        }

        private string GetInsertScriptMap()
        {
            return @"INSERT INTO [dbo].[TestScripts_Map] ([MasterTestScriptID],TestCaseID,[TCStepID], TestScriptName, FunctionDescription, FunctionName,
                    [Run], Param1, Param2, Param3, Param4, Param5, Param6, Param8, Param7, Param9, Param10,
                    Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20,
                    [Param21],[Param22],[Param23],[Param24],[Param25],[Param26],[Param27],[Param28],[Param29],[Param30],
                    [Param31],[Param32],[Param33],[Param34],[Param35],[Param36],[Param37],[Param38],[Param39],[Param40],
                    [Param41],[Param42],[Param43],[Param44],[Param45],[Param46],[Param47],[Param48],[Param49],[Param50],
                    [Param51],[Param52],[Param53],[Param54],[Param55],[Param56],[Param57],[Param58],[Param59],[Param60],
                    [Param61],[Param62],[Param63],[Param64],[Param65],[Param66],[Param67],[Param68],[Param69],[Param70],
                    [Param71],[Param72],[Param73],[Param74],[Param75],[Param76],[Param77],[Param78],[Param79],[Param80],
                    [Param81],[Param82],[Param83],[Param84],[Param85],[Param86],[Param87],[Param88],[Param89],[Param90],
                    [Param91],[Param92],[Param93],[Param94],[Param95],[Param96],[Param97],[Param98],[Param99],[Param100],
                    [Module],[LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@MasterTestScriptID,@TestCaseID,@TCStepID, @TestScriptName, @FunctionDescription, @FunctionName,
                    @Run, @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param8, @Param7, @Param9, @Param10,
                    @Param11, @Param12, @Param13, @Param14, @Param15, @Param16, @Param17, @Param18, @Param19, @Param20,
                    @Param21,@Param22,@Param23,@Param24,@Param25,@Param26,@Param27,@Param28,@Param29,@Param30,
                    @Param31,@Param32,@Param33,@Param34,@Param35,@Param36,@Param37,@Param38,@Param39,@Param40,
                    @Param41,@Param42,@Param43,@Param44,@Param45,@Param46,@Param47,@Param48,@Param49,@Param50,
                    @Param51,@Param52,@Param53,@Param54,@Param55,@Param56,@Param57,@Param58,@Param59,@Param60,
                    @Param61,@Param62,@Param63,@Param64,@Param65,@Param66,@Param67,@Param68,@Param69,@Param70,
                    @Param71,@Param72,@Param73,@Param74,@Param75,@Param76,@Param77,@Param78,@Param79,@Param80,
                    @Param81,@Param82,@Param83,@Param84,@Param85,@Param86,@Param87,@Param88,@Param89,@Param90,
                    @Param91,@Param92,@Param93,@Param94,@Param95,@Param96,@Param97,@Param98,@Param99,@Param100,
                    @Module,@LockedByUser, @CreatedOn, @UpdatedOn,@UserId )";
        }

        private string GetUpdateScript()
        {
            return @"UPDATE [dbo].[TestScripts]
                     SET TestCaseID = @TestCaseID,[TCStepID]=@TCStepID, TestScriptName = @TestScriptName, FunctionDescription = @FunctionDescription, 
                          FunctionName = @FunctionName, [Run] = @Run, Param1 = @Param1, Param2 = @Param2, Param3 = @Param3, 
                          Param4 = @Param4, Param5 = @Param5, Param6 = @Param6, Param8 = @Param8, Param7 = @Param7, Param9 = @Param9,
                          Param10 = @Param10, Param11 = @Param11, Param12 = @Param12, Param13 = @Param13, Param14 = @Param14, Param15 = @Param15, 
                          Param16 = @Param16, Param17 = @Param17, Param18 = @Param18, Param19 = @Param19, Param20 = @Param20, 
                          [Param21]= @Param21, [Param22]= @Param22, [Param23]= @Param23, [Param24]= @Param24, [Param25]= @Param25,
                          [Param26]= @Param26, [Param27]= @Param27, [Param28]= @Param28, [Param29]= @Param29, [Param30]= @Param30, 
                          [Param31]= @Param31, [Param32]= @Param32, [Param33]= @Param33, [Param34]= @Param34, [Param35]= @Param35, [Param36]= @Param36, 
                          [Param37]= @Param37, [Param38]= @Param38, [Param39]= @Param39, [Param40]= @Param40, [Param41]= @Param41, [Param42]= @Param42,
                          [Param43]= @Param43, [Param44]= @Param44, [Param45]= @Param45, [Param46]= @Param46, [Param47]= @Param47, [Param48]= @Param48, 
                        [Param49]= @Param49, [Param50]= @Param50, [Param51]= @Param51, [Param52]= @Param52, [Param53]= @Param53, [Param54]= @Param54,
                        [Param55]= @Param55, [Param56]= @Param56, [Param57]= @Param57, [Param58]= @Param58, [Param59]= @Param59, [Param60]= @Param60, 
                        [Param61]= @Param61, [Param62]= @Param62, [Param63]= @Param63, [Param64]= @Param64, [Param65]= @Param65, [Param66]= @Param66,
                        [Param67]= @Param67, [Param68]= @Param68, [Param69]= @Param69, [Param70]= @Param70, [Param71]= @Param71, [Param72]= @Param72, 
                        [Param73]= @Param73, [Param74]= @Param74, [Param75]= @Param75, [Param76]= @Param76, [Param77]= @Param77, [Param78]= @Param78, 
                        [Param79]= @Param79, [Param80]= @Param80, [Param81]= @Param81, [Param82]= @Param82, [Param83]= @Param83, [Param84]= @Param84, 
                        [Param85]= @Param85, [Param86]= @Param86, [Param87]= @Param87, [Param88]= @Param88, [Param89]= @Param89, [Param90]= @Param90, 
                        [Param91]= @Param91, [Param92]= @Param92, [Param93]= @Param93, [Param94]= @Param94, [Param95]= @Param95, [Param96]= @Param96, 
                        [Param97]= @Param97, [Param98]= @Param98, [Param99]= @Param99, [Param100]= @Param100,
                          [Module] = @Module,[StatusID]=@StatusID,[CUDStatusID]=@CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, 
                          [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetUpdateScriptMap()
        {
            return @"UPDATE [dbo].[TestScripts_Map]
                     SET [MasterTestScriptID]=@MasterTestScriptID, TestCaseID = @TestCaseID,[TCStepID]=@TCStepID, TestScriptName = @TestScriptName, FunctionDescription = @FunctionDescription, 
                          FunctionName = @FunctionName, [Run] = @Run, Param1 = @Param1, Param2 = @Param2, Param3 = @Param3, 
                          Param4 = @Param4, Param5 = @Param5, Param6 = @Param6, Param8 = @Param8, Param7 = @Param7, Param9 = @Param9,
                          Param10 = @Param10, Param11 = @Param11, Param12 = @Param12, Param13 = @Param13, Param14 = @Param14, Param15 = @Param15, 
                          Param16 = @Param16, Param17 = @Param17, Param18 = @Param18, Param19 = @Param19, Param20 = @Param20, 
                          [Param21]= @Param21, [Param22]= @Param22, [Param23]= @Param23, [Param24]= @Param24, [Param25]= @Param25,
                          [Param26]= @Param26, [Param27]= @Param27, [Param28]= @Param28, [Param29]= @Param29, [Param30]= @Param30, 
                          [Param31]= @Param31, [Param32]= @Param32, [Param33]= @Param33, [Param34]= @Param34, [Param35]= @Param35, [Param36]= @Param36, 
                          [Param37]= @Param37, [Param38]= @Param38, [Param39]= @Param39, [Param40]= @Param40, [Param41]= @Param41, [Param42]= @Param42,
                          [Param43]= @Param43, [Param44]= @Param44, [Param45]= @Param45, [Param46]= @Param46, [Param47]= @Param47, [Param48]= @Param48, 
                        [Param49]= @Param49, [Param50]= @Param50, [Param51]= @Param51, [Param52]= @Param52, [Param53]= @Param53, [Param54]= @Param54,
                        [Param55]= @Param55, [Param56]= @Param56, [Param57]= @Param57, [Param58]= @Param58, [Param59]= @Param59, [Param60]= @Param60, 
                        [Param61]= @Param61, [Param62]= @Param62, [Param63]= @Param63, [Param64]= @Param64, [Param65]= @Param65, [Param66]= @Param66,
                        [Param67]= @Param67, [Param68]= @Param68, [Param69]= @Param69, [Param70]= @Param70, [Param71]= @Param71, [Param72]= @Param72, 
                        [Param73]= @Param73, [Param74]= @Param74, [Param75]= @Param75, [Param76]= @Param76, [Param77]= @Param77, [Param78]= @Param78, 
                        [Param79]= @Param79, [Param80]= @Param80, [Param81]= @Param81, [Param82]= @Param82, [Param83]= @Param83, [Param84]= @Param84, 
                        [Param85]= @Param85, [Param86]= @Param86, [Param87]= @Param87, [Param88]= @Param88, [Param89]= @Param89, [Param90]= @Param90, 
                        [Param91]= @Param91, [Param92]= @Param92, [Param93]= @Param93, [Param94]= @Param94, [Param95]= @Param95, [Param96]= @Param96, 
                        [Param97]= @Param97, [Param98]= @Param98, [Param99]= @Param99, [Param100]= @Param100,
                          [Module] = @Module,[LockedByUser]=@LockedByUser, 
                          [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE [MasterTestScriptID]=@MasterTestScriptID and [UserId]= @UserId";
        }

        private string GetUpdateLockedByUserScript()
        {
            return @"UPDATE [dbo].[TestScripts]
                     SET [IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetDeleteScript()
        {
            return @"UPDATE [dbo].[TestScripts]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetDeleteScriptMap()
        {
            return @"DELETE FROM [dbo].[TestScripts_Map]
                     WHERE [MasterTestScriptID]=@MasterTestScriptID and [UserId]= @UserId";
        }

        private string GetFilteredScript()
        {

            return @"SELECT * FROM [dbo].[TestScripts] where [ID] NOT IN @Ids";
        }

        private string GetDataScriptFromTestScriptMap()
        {
            return @"Select * from [dbo].[TestScripts_Map] where [UserId]=@UserId and [MasterTestScriptID]=@MasterTestScriptID";
        }

        private string GetAllTestScriptNameScript()
        {
            return @"SELECT Distinct TestScriptName FROM [dbo].[TestScripts]";
        }

        private string GetAllTestCaseIDScript()
        {
            return @"SELECT Distinct TestCaseID FROM [dbo].[TestScripts]";
        }

        public void CreateScript(TestScripts script)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestCaseID", script.TestCaseID);
                parameters.Add("@TCStepID", script.TCStepID);
                parameters.Add("@TestScriptName", script.TestScriptName);
                parameters.Add("@FunctionDescription", script.FunctionDescription);
                parameters.Add("@FunctionName", script.FunctionName);
                parameters.Add("@Run", script.Run);
                parameters.Add("@Param1", script.Param1);
                parameters.Add("@Param2", script.Param2);
                parameters.Add("@Param3", script.Param3);
                parameters.Add("@Param4", script.Param4);
                parameters.Add("@Param5", script.Param5);
                parameters.Add("@Param6", script.Param6);
                parameters.Add("@Param8", script.Param8);
                parameters.Add("@Param7", script.Param7);
                parameters.Add("@Param9", script.Param9);
                parameters.Add("@Param10", script.Param10);
                parameters.Add("@Param11", script.Param11);
                parameters.Add("@Param12", script.Param12);
                parameters.Add("@Param13", script.Param13);
                parameters.Add("@Param14", script.Param14);
                parameters.Add("@Param15", script.Param15);
                parameters.Add("@Param16", script.Param16);
                parameters.Add("@Param17", script.Param17);
                parameters.Add("@Param18", script.Param18);
                parameters.Add("@Param19", script.Param19);
                parameters.Add("@Param20", script.Param20);
                parameters.Add("@Param21", script.Param21);
                parameters.Add("@Param22", script.Param22);
                parameters.Add("@Param23", script.Param23);
                parameters.Add("@Param24", script.Param24);
                parameters.Add("@Param25", script.Param25);
                parameters.Add("@Param26", script.Param26);
                parameters.Add("@Param27", script.Param27);
                parameters.Add("@Param28", script.Param28);
                parameters.Add("@Param29", script.Param29);
                parameters.Add("@Param30", script.Param30);
                parameters.Add("@Param31", script.Param31);
                parameters.Add("@Param32", script.Param32);
                parameters.Add("@Param33", script.Param33);
                parameters.Add("@Param34", script.Param34);
                parameters.Add("@Param35", script.Param35);
                parameters.Add("@Param36", script.Param36);
                parameters.Add("@Param37", script.Param37);
                parameters.Add("@Param38", script.Param38);
                parameters.Add("@Param39", script.Param39);
                parameters.Add("@Param40", script.Param40);
                parameters.Add("@Param41", script.Param41);
                parameters.Add("@Param42", script.Param42);
                parameters.Add("@Param43", script.Param43);
                parameters.Add("@Param44", script.Param44);
                parameters.Add("@Param45", script.Param45);
                parameters.Add("@Param46", script.Param46);
                parameters.Add("@Param47", script.Param47);
                parameters.Add("@Param48", script.Param48);
                parameters.Add("@Param49", script.Param49);
                parameters.Add("@Param50", script.Param50);
                parameters.Add("@Param51", script.Param51);
                parameters.Add("@Param52", script.Param52);
                parameters.Add("@Param53", script.Param53);
                parameters.Add("@Param54", script.Param54);
                parameters.Add("@Param55", script.Param55);
                parameters.Add("@Param56", script.Param56);
                parameters.Add("@Param57", script.Param57);
                parameters.Add("@Param58", script.Param58);
                parameters.Add("@Param59", script.Param59);
                parameters.Add("@Param60", script.Param60);
                parameters.Add("@Param61", script.Param61);
                parameters.Add("@Param62", script.Param62);
                parameters.Add("@Param63", script.Param63);
                parameters.Add("@Param64", script.Param64);
                parameters.Add("@Param65", script.Param65);
                parameters.Add("@Param66", script.Param66);
                parameters.Add("@Param67", script.Param67);
                parameters.Add("@Param68", script.Param68);
                parameters.Add("@Param69", script.Param69);
                parameters.Add("@Param70", script.Param70);
                parameters.Add("@Param71", script.Param71);
                parameters.Add("@Param72", script.Param72);
                parameters.Add("@Param73", script.Param73);
                parameters.Add("@Param74", script.Param74);
                parameters.Add("@Param75", script.Param75);
                parameters.Add("@Param76", script.Param76);
                parameters.Add("@Param77", script.Param77);
                parameters.Add("@Param78", script.Param78);
                parameters.Add("@Param79", script.Param79);
                parameters.Add("@Param80", script.Param80);
                parameters.Add("@Param81", script.Param81);
                parameters.Add("@Param82", script.Param82);
                parameters.Add("@Param83", script.Param83);
                parameters.Add("@Param84", script.Param84);
                parameters.Add("@Param85", script.Param85);
                parameters.Add("@Param86", script.Param86);
                parameters.Add("@Param87", script.Param87);
                parameters.Add("@Param88", script.Param88);
                parameters.Add("@Param89", script.Param89);
                parameters.Add("@Param90", script.Param90);
                parameters.Add("@Param91", script.Param91);
                parameters.Add("@Param92", script.Param92);
                parameters.Add("@Param93", script.Param93);
                parameters.Add("@Param94", script.Param94);
                parameters.Add("@Param95", script.Param95);
                parameters.Add("@Param96", script.Param96);
                parameters.Add("@Param97", script.Param97);
                parameters.Add("@Param98", script.Param98);
                parameters.Add("@Param99", script.Param99);
                parameters.Add("@Param100", script.Param100);
                parameters.Add("@Module", script.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", script.IsLocked);
                parameters.Add("@LockedByUser", script.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", script.UserId);

                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteScript(int scriptId,int userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", scriptId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateScript(int scriptId, TestScripts script)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", scriptId);
                parameters.Add("@TestCaseID", script.TestCaseID);
                parameters.Add("@TCStepID", script.TCStepID);
                parameters.Add("@TestScriptName", script.TestScriptName);
                parameters.Add("@FunctionDescription", script.FunctionDescription);
                parameters.Add("@FunctionName", script.FunctionName);
                parameters.Add("@Run", script.Run);
                parameters.Add("@Param1", script.Param1);
                parameters.Add("@Param2", script.Param2);
                parameters.Add("@Param3", script.Param3);
                parameters.Add("@Param4", script.Param4);
                parameters.Add("@Param5", script.Param5);
                parameters.Add("@Param6", script.Param6);
                parameters.Add("@Param8", script.Param8);
                parameters.Add("@Param7", script.Param7);
                parameters.Add("@Param9", script.Param9);
                parameters.Add("@Param10", script.Param10);
                parameters.Add("@Param11", script.Param11);
                parameters.Add("@Param12", script.Param12);
                parameters.Add("@Param13", script.Param13);
                parameters.Add("@Param14", script.Param14);
                parameters.Add("@Param15", script.Param15);
                parameters.Add("@Param16", script.Param16);
                parameters.Add("@Param17", script.Param17);
                parameters.Add("@Param18", script.Param18);
                parameters.Add("@Param19", script.Param19);
                parameters.Add("@Param20", script.Param20);
                parameters.Add("@Param21", script.Param21);
                parameters.Add("@Param22", script.Param22);
                parameters.Add("@Param23", script.Param23);
                parameters.Add("@Param24", script.Param24);
                parameters.Add("@Param25", script.Param25);
                parameters.Add("@Param26", script.Param26);
                parameters.Add("@Param27", script.Param27);
                parameters.Add("@Param28", script.Param28);
                parameters.Add("@Param29", script.Param29);
                parameters.Add("@Param30", script.Param30);
                parameters.Add("@Param31", script.Param31);
                parameters.Add("@Param32", script.Param32);
                parameters.Add("@Param33", script.Param33);
                parameters.Add("@Param34", script.Param34);
                parameters.Add("@Param35", script.Param35);
                parameters.Add("@Param36", script.Param36);
                parameters.Add("@Param37", script.Param37);
                parameters.Add("@Param38", script.Param38);
                parameters.Add("@Param39", script.Param39);
                parameters.Add("@Param40", script.Param40);
                parameters.Add("@Param41", script.Param41);
                parameters.Add("@Param42", script.Param42);
                parameters.Add("@Param43", script.Param43);
                parameters.Add("@Param44", script.Param44);
                parameters.Add("@Param45", script.Param45);
                parameters.Add("@Param46", script.Param46);
                parameters.Add("@Param47", script.Param47);
                parameters.Add("@Param48", script.Param48);
                parameters.Add("@Param49", script.Param49);
                parameters.Add("@Param50", script.Param50);
                parameters.Add("@Param51", script.Param51);
                parameters.Add("@Param52", script.Param52);
                parameters.Add("@Param53", script.Param53);
                parameters.Add("@Param54", script.Param54);
                parameters.Add("@Param55", script.Param55);
                parameters.Add("@Param56", script.Param56);
                parameters.Add("@Param57", script.Param57);
                parameters.Add("@Param58", script.Param58);
                parameters.Add("@Param59", script.Param59);
                parameters.Add("@Param60", script.Param60);
                parameters.Add("@Param61", script.Param61);
                parameters.Add("@Param62", script.Param62);
                parameters.Add("@Param63", script.Param63);
                parameters.Add("@Param64", script.Param64);
                parameters.Add("@Param65", script.Param65);
                parameters.Add("@Param66", script.Param66);
                parameters.Add("@Param67", script.Param67);
                parameters.Add("@Param68", script.Param68);
                parameters.Add("@Param69", script.Param69);
                parameters.Add("@Param70", script.Param70);
                parameters.Add("@Param71", script.Param71);
                parameters.Add("@Param72", script.Param72);
                parameters.Add("@Param73", script.Param73);
                parameters.Add("@Param74", script.Param74);
                parameters.Add("@Param75", script.Param75);
                parameters.Add("@Param76", script.Param76);
                parameters.Add("@Param77", script.Param77);
                parameters.Add("@Param78", script.Param78);
                parameters.Add("@Param79", script.Param79);
                parameters.Add("@Param80", script.Param80);
                parameters.Add("@Param81", script.Param81);
                parameters.Add("@Param82", script.Param82);
                parameters.Add("@Param83", script.Param83);
                parameters.Add("@Param84", script.Param84);
                parameters.Add("@Param85", script.Param85);
                parameters.Add("@Param86", script.Param86);
                parameters.Add("@Param87", script.Param87);
                parameters.Add("@Param88", script.Param88);
                parameters.Add("@Param89", script.Param89);
                parameters.Add("@Param90", script.Param90);
                parameters.Add("@Param91", script.Param91);
                parameters.Add("@Param92", script.Param92);
                parameters.Add("@Param93", script.Param93);
                parameters.Add("@Param94", script.Param94);
                parameters.Add("@Param95", script.Param95);
                parameters.Add("@Param96", script.Param96);
                parameters.Add("@Param97", script.Param97);
                parameters.Add("@Param98", script.Param98);
                parameters.Add("@Param99", script.Param99);
                parameters.Add("@Param100", script.Param100);
                parameters.Add("@Module", script.Module);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", null);
                parameters.Add("@LockedByUser", null);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", script.UserId);

                con.Query($"{GetUpdateScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateLockedByFlags(TestScripts testScript)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", testScript.ID);
                parameters.Add("@IsLocked", testScript.IsLocked);
                parameters.Add("@LockedByUser", testScript.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);

                con.Query($"{GetUpdateLockedByUserScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void CreateScriptMap(TestScripts_Map script)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestCaseID", script.TestCaseID);
                parameters.Add("@TCStepID", script.TCStepID);
                parameters.Add("@TestScriptName", script.TestScriptName);
                parameters.Add("@FunctionDescription", script.FunctionDescription);
                parameters.Add("@FunctionName", script.FunctionName);
                parameters.Add("@Run", script.Run);
                parameters.Add("@Param1", script.Param1);
                parameters.Add("@Param2", script.Param2);
                parameters.Add("@Param3", script.Param3);
                parameters.Add("@Param4", script.Param4);
                parameters.Add("@Param5", script.Param5);
                parameters.Add("@Param6", script.Param6);
                parameters.Add("@Param8", script.Param8);
                parameters.Add("@Param7", script.Param7);
                parameters.Add("@Param9", script.Param9);
                parameters.Add("@Param10", script.Param10);
                parameters.Add("@Param11", script.Param11);
                parameters.Add("@Param12", script.Param12);
                parameters.Add("@Param13", script.Param13);
                parameters.Add("@Param14", script.Param14);
                parameters.Add("@Param15", script.Param15);
                parameters.Add("@Param16", script.Param16);
                parameters.Add("@Param17", script.Param17);
                parameters.Add("@Param18", script.Param18);
                parameters.Add("@Param19", script.Param19);
                parameters.Add("@Param20", script.Param20);
                parameters.Add("@Param21", script.Param21);
                parameters.Add("@Param22", script.Param22);
                parameters.Add("@Param23", script.Param23);
                parameters.Add("@Param24", script.Param24);
                parameters.Add("@Param25", script.Param25);
                parameters.Add("@Param26", script.Param26);
                parameters.Add("@Param27", script.Param27);
                parameters.Add("@Param28", script.Param28);
                parameters.Add("@Param29", script.Param29);
                parameters.Add("@Param30", script.Param30);
                parameters.Add("@Param31", script.Param31);
                parameters.Add("@Param32", script.Param32);
                parameters.Add("@Param33", script.Param33);
                parameters.Add("@Param34", script.Param34);
                parameters.Add("@Param35", script.Param35);
                parameters.Add("@Param36", script.Param36);
                parameters.Add("@Param37", script.Param37);
                parameters.Add("@Param38", script.Param38);
                parameters.Add("@Param39", script.Param39);
                parameters.Add("@Param40", script.Param40);
                parameters.Add("@Param41", script.Param41);
                parameters.Add("@Param42", script.Param42);
                parameters.Add("@Param43", script.Param43);
                parameters.Add("@Param44", script.Param44);
                parameters.Add("@Param45", script.Param45);
                parameters.Add("@Param46", script.Param46);
                parameters.Add("@Param47", script.Param47);
                parameters.Add("@Param48", script.Param48);
                parameters.Add("@Param49", script.Param49);
                parameters.Add("@Param50", script.Param50);
                parameters.Add("@Param51", script.Param51);
                parameters.Add("@Param52", script.Param52);
                parameters.Add("@Param53", script.Param53);
                parameters.Add("@Param54", script.Param54);
                parameters.Add("@Param55", script.Param55);
                parameters.Add("@Param56", script.Param56);
                parameters.Add("@Param57", script.Param57);
                parameters.Add("@Param58", script.Param58);
                parameters.Add("@Param59", script.Param59);
                parameters.Add("@Param60", script.Param60);
                parameters.Add("@Param61", script.Param61);
                parameters.Add("@Param62", script.Param62);
                parameters.Add("@Param63", script.Param63);
                parameters.Add("@Param64", script.Param64);
                parameters.Add("@Param65", script.Param65);
                parameters.Add("@Param66", script.Param66);
                parameters.Add("@Param67", script.Param67);
                parameters.Add("@Param68", script.Param68);
                parameters.Add("@Param69", script.Param69);
                parameters.Add("@Param70", script.Param70);
                parameters.Add("@Param71", script.Param71);
                parameters.Add("@Param72", script.Param72);
                parameters.Add("@Param73", script.Param73);
                parameters.Add("@Param74", script.Param74);
                parameters.Add("@Param75", script.Param75);
                parameters.Add("@Param76", script.Param76);
                parameters.Add("@Param77", script.Param77);
                parameters.Add("@Param78", script.Param78);
                parameters.Add("@Param79", script.Param79);
                parameters.Add("@Param80", script.Param80);
                parameters.Add("@Param81", script.Param81);
                parameters.Add("@Param82", script.Param82);
                parameters.Add("@Param83", script.Param83);
                parameters.Add("@Param84", script.Param84);
                parameters.Add("@Param85", script.Param85);
                parameters.Add("@Param86", script.Param86);
                parameters.Add("@Param87", script.Param87);
                parameters.Add("@Param88", script.Param88);
                parameters.Add("@Param89", script.Param89);
                parameters.Add("@Param90", script.Param90);
                parameters.Add("@Param91", script.Param91);
                parameters.Add("@Param92", script.Param92);
                parameters.Add("@Param93", script.Param93);
                parameters.Add("@Param94", script.Param94);
                parameters.Add("@Param95", script.Param95);
                parameters.Add("@Param96", script.Param96);
                parameters.Add("@Param97", script.Param97);
                parameters.Add("@Param98", script.Param98);
                parameters.Add("@Param99", script.Param99);
                parameters.Add("@Param100", script.Param100);
                parameters.Add("@Module", script.Module);
                parameters.Add("@LockedByUser", script.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", script.UserId);
                parameters.Add("@MasterTestScriptID", script.MasterTestScriptID);

                con.Query($"{GetInsertScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateScriptMap(int? userId, TestScripts_Map script)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TestCaseID", script.TestCaseID);
                parameters.Add("@TCStepID", script.TCStepID);
                parameters.Add("@TestScriptName", script.TestScriptName);
                parameters.Add("@FunctionDescription", script.FunctionDescription);
                parameters.Add("@FunctionName", script.FunctionName);
                parameters.Add("@Run", script.Run);
                parameters.Add("@Param1", script.Param1);
                parameters.Add("@Param2", script.Param2);
                parameters.Add("@Param3", script.Param3);
                parameters.Add("@Param4", script.Param4);
                parameters.Add("@Param5", script.Param5);
                parameters.Add("@Param6", script.Param6);
                parameters.Add("@Param8", script.Param8);
                parameters.Add("@Param7", script.Param7);
                parameters.Add("@Param9", script.Param9);
                parameters.Add("@Param10", script.Param10);
                parameters.Add("@Param11", script.Param11);
                parameters.Add("@Param12", script.Param12);
                parameters.Add("@Param13", script.Param13);
                parameters.Add("@Param14", script.Param14);
                parameters.Add("@Param15", script.Param15);
                parameters.Add("@Param16", script.Param16);
                parameters.Add("@Param17", script.Param17);
                parameters.Add("@Param18", script.Param18);
                parameters.Add("@Param19", script.Param19);
                parameters.Add("@Param20", script.Param20);
                parameters.Add("@Param21", script.Param21);
                parameters.Add("@Param22", script.Param22);
                parameters.Add("@Param23", script.Param23);
                parameters.Add("@Param24", script.Param24);
                parameters.Add("@Param25", script.Param25);
                parameters.Add("@Param26", script.Param26);
                parameters.Add("@Param27", script.Param27);
                parameters.Add("@Param28", script.Param28);
                parameters.Add("@Param29", script.Param29);
                parameters.Add("@Param30", script.Param30);
                parameters.Add("@Param31", script.Param31);
                parameters.Add("@Param32", script.Param32);
                parameters.Add("@Param33", script.Param33);
                parameters.Add("@Param34", script.Param34);
                parameters.Add("@Param35", script.Param35);
                parameters.Add("@Param36", script.Param36);
                parameters.Add("@Param37", script.Param37);
                parameters.Add("@Param38", script.Param38);
                parameters.Add("@Param39", script.Param39);
                parameters.Add("@Param40", script.Param40);
                parameters.Add("@Param41", script.Param41);
                parameters.Add("@Param42", script.Param42);
                parameters.Add("@Param43", script.Param43);
                parameters.Add("@Param44", script.Param44);
                parameters.Add("@Param45", script.Param45);
                parameters.Add("@Param46", script.Param46);
                parameters.Add("@Param47", script.Param47);
                parameters.Add("@Param48", script.Param48);
                parameters.Add("@Param49", script.Param49);
                parameters.Add("@Param50", script.Param50);
                parameters.Add("@Param51", script.Param51);
                parameters.Add("@Param52", script.Param52);
                parameters.Add("@Param53", script.Param53);
                parameters.Add("@Param54", script.Param54);
                parameters.Add("@Param55", script.Param55);
                parameters.Add("@Param56", script.Param56);
                parameters.Add("@Param57", script.Param57);
                parameters.Add("@Param58", script.Param58);
                parameters.Add("@Param59", script.Param59);
                parameters.Add("@Param60", script.Param60);
                parameters.Add("@Param61", script.Param61);
                parameters.Add("@Param62", script.Param62);
                parameters.Add("@Param63", script.Param63);
                parameters.Add("@Param64", script.Param64);
                parameters.Add("@Param65", script.Param65);
                parameters.Add("@Param66", script.Param66);
                parameters.Add("@Param67", script.Param67);
                parameters.Add("@Param68", script.Param68);
                parameters.Add("@Param69", script.Param69);
                parameters.Add("@Param70", script.Param70);
                parameters.Add("@Param71", script.Param71);
                parameters.Add("@Param72", script.Param72);
                parameters.Add("@Param73", script.Param73);
                parameters.Add("@Param74", script.Param74);
                parameters.Add("@Param75", script.Param75);
                parameters.Add("@Param76", script.Param76);
                parameters.Add("@Param77", script.Param77);
                parameters.Add("@Param78", script.Param78);
                parameters.Add("@Param79", script.Param79);
                parameters.Add("@Param80", script.Param80);
                parameters.Add("@Param81", script.Param81);
                parameters.Add("@Param82", script.Param82);
                parameters.Add("@Param83", script.Param83);
                parameters.Add("@Param84", script.Param84);
                parameters.Add("@Param85", script.Param85);
                parameters.Add("@Param86", script.Param86);
                parameters.Add("@Param87", script.Param87);
                parameters.Add("@Param88", script.Param88);
                parameters.Add("@Param89", script.Param89);
                parameters.Add("@Param90", script.Param90);
                parameters.Add("@Param91", script.Param91);
                parameters.Add("@Param92", script.Param92);
                parameters.Add("@Param93", script.Param93);
                parameters.Add("@Param94", script.Param94);
                parameters.Add("@Param95", script.Param95);
                parameters.Add("@Param96", script.Param96);
                parameters.Add("@Param97", script.Param97);
                parameters.Add("@Param98", script.Param98);
                parameters.Add("@Param99", script.Param99);
                parameters.Add("@Param100", script.Param100);
                parameters.Add("@Module", script.Module);
                parameters.Add("@LockedByUser", script.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", userId);
                parameters.Add("@MasterTestScriptID", script.MasterTestScriptID);

                con.Query($"{GetUpdateScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteScriptMap(int? userId, int masterTestScriptId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MasterTestScriptID", masterTestScriptId);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScriptMap()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public List<TestScripts> GetFilteredTestScripts(List<int> Ids)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new { Ids = Ids };
                var result = (List<TestScripts>)con.Query<TestScripts>($"{GetFilteredScript()}",
                   parameters,
                    commandType: CommandType.Text);
                return result;
            }
        }

        public TestScripts_Map GetMappedTestScript(int id, int? userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@MasterTestScriptID", id);
                parameters.Add("@UserId", userId);

                var result = con.Query<TestScripts_Map>($"{GetDataScriptFromTestScriptMap()}",
                    parameters,
                    commandType: CommandType.Text).FirstOrDefault();
                return result;
            }
        }

        public List<string> GetAllTestScriptName()
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var result = (List<string>)con.Query<string>($"{GetAllTestScriptNameScript()}",
                    commandType: CommandType.Text);

                return result;
            }
        }

        public List<string> GetAllTestCaseID()
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var result = (List<string>)con.Query<string>($"{GetAllTestCaseIDScript()}",
                    commandType: CommandType.Text);

                return result;
            }
        }
    }
}
