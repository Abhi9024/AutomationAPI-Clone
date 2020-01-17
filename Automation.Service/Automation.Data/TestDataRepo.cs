using Automation.Core.DataAccessAbstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Automation.Core;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace Automation.Data
{
    public class TestDataRepo : ITestDataRepo
    {
        private string strConnectionString { get; set; }
        private IConfiguration _config;
        public TestDataRepo(IConfiguration config)
        {
            _config = config;
            strConnectionString = _config.GetSection("ConnectionString").Value;
        }

        private string GetInsertScript()
        {
            return @"INSERT INTO [dbo].[TestData] ([Module], [TCID], [Iterations], Param1, Param2, Param3, Param4, Param5, Param6, Param8, Param7, Param9, Param10,
                    Param11, Param12, Param13, Param14, Param15, Param16, Param17, Param18, Param19, Param20,
                    [Param21],[Param22],[Param23],[Param24],[Param25],[Param26],[Param27],[Param28],[Param29],[Param30],
                    [Param31],[Param32],[Param33],[Param34],[Param35],[Param36],[Param37],[Param38],[Param39],[Param40],
                    [Param41],[Param42],[Param43],[Param44],[Param45],[Param46],[Param47],[Param48],[Param49],[Param50],
                    [Param51],[Param52],[Param53],[Param54],[Param55],[Param56],[Param57],[Param58],[Param59],[Param60],
                    [Param61],[Param62],[Param63],[Param64],[Param65],[Param66],[Param67],[Param68],[Param69],[Param70],
                    [Param71],[Param72],[Param73],[Param74],[Param75],[Param76],[Param77],[Param78],[Param79],[Param80],
                    [Param81],[Param82],[Param83],[Param84],[Param85],[Param86],[Param87],[Param88],[Param89],[Param90],
                    [Param91],[Param92],[Param93],[Param94],[Param95],[Param96],[Param97],[Param98],[Param99],[Param100],
                    [Param101],[Param102],[Param103],[Param104],[Param105],[Param106],[Param107],[Param108],[Param109],[Param110],
                    [Param111],[Param112],[Param113],[Param114],[Param115],[Param116],[Param117],[Param118],[Param119],[Param120],
                    [Param121],[Param122],[Param123],[Param124],[Param125],[Param126],[Param127],[Param128],[Param129],[Param130],
                    [Param131],[Param132],[Param133],[Param134],[Param135],[Param136],[Param137],[Param138],[Param139],[Param140],
                    [Param141],[Param142],[Param143],[Param144],[Param145],[Param146],[Param147],[Param148],[Param149],[Param150],
                    [Param151],[Param152],[Param153],[Param154],[Param155],[Param156],[Param157],[Param158],[Param159],[Param160],
                    [Param161],[Param162],[Param163],[Param164],[Param165],[Param166],[Param167],[Param168],[Param169],[Param170],
                    [Param171],[Param172],[Param173],[Param174],[Param175],[Param176],[Param177],[Param178],[Param179],[Param180],
                    [Param181],[Param182],[Param183],[Param184],[Param185],[Param186],[Param187],[Param188],[Param189],[Param190],
                    [Param191],[Param192],[Param193],[Param194],[Param195],[Param196],[Param197],[Param198],[Param199],[Param200],
                    [Param201],[Param202],[Param203],[Param204],[Param205],[Param206],[Param207],[Param208],[Param209],[Param210],
                    [Param211],[Param212],[Param213],[Param214],[Param215],[Param216],[Param217],[Param218],[Param219],[Param220],
                    [Param221],[Param222],[Param223],[Param224],[Param225],[Param226],[Param227],[Param228],[Param229],[Param230],
                    [Param231],[Param232],[Param233],[Param234],[Param235],[Param236],[Param237],[Param238],[Param239],[Param240],
                    [Param241],[Param242],[Param243],[Param244],[Param245],[Param246],[Param247],[Param248],[Param249],[Param250],
                    [StatusID],[CUDStatusID],[IsLocked],[LockedByUser], [CreatedOn], [UpdatedOn],[UserId])
                    VALUES (@Module, @TCID, @Iterations, 
                    @Param1, @Param2, @Param3, @Param4, @Param5, @Param6, @Param8, @Param7, @Param9, @Param10,
                    @Param11, @Param12, @Param13, @Param14, @Param15, @Param16, @Param17, @Param18, @Param19, @Param20, 
                    @Param21,@Param22,@Param23,@Param24,@Param25,@Param26,@Param27,@Param28,@Param29,@Param30,
                    @Param31,@Param32,@Param33,@Param34,@Param35,@Param36,@Param37,@Param38,@Param39,@Param40,
                    @Param41,@Param42,@Param43,@Param44,@Param45,@Param46,@Param47,@Param48,@Param49,@Param50,
                    @Param51,@Param52,@Param53,@Param54,@Param55,@Param56,@Param57,@Param58,@Param59,@Param60,
                    @Param61,@Param62,@Param63,@Param64,@Param65,@Param66,@Param67,@Param68,@Param69,@Param70,
                    @Param71,@Param72,@Param73,@Param74,@Param75,@Param76,@Param77,@Param78,@Param79,@Param80,
                    @Param81,@Param82,@Param83,@Param84,@Param85,@Param86,@Param87,@Param88,@Param89,@Param90,
                    @Param91,@Param92,@Param93,@Param94,@Param95,@Param96,@Param97,@Param98,@Param99,@Param100,
                    @Param101,@Param102,@Param103,@Param104,@Param105,@Param106,@Param107,@Param108,@Param109,@Param110,
                    @Param111,@Param112,@Param113,@Param114,@Param115,@Param116,@Param117,@Param118,@Param119,@Param120,
                    @Param121,@Param122,@Param123,@Param124,@Param125,@Param126,@Param127,@Param128,@Param129,@Param130,
                    @Param131,@Param132,@Param133,@Param134,@Param135,@Param136,@Param137,@Param138,@Param139,@Param140,
                    @Param141,@Param142,@Param143,@Param144,@Param145,@Param146,@Param147,@Param148,@Param149,@Param150,
                    @Param151,@Param152,@Param153,@Param154,@Param155,@Param156,@Param157,@Param158,@Param159,@Param160,
                    @Param161,@Param162,@Param163,@Param164,@Param165,@Param166,@Param167,@Param168,@Param169,@Param170,
                    @Param171,@Param172,@Param173,@Param174,@Param175,@Param176,@Param177,@Param178,@Param179,@Param180,
                    @Param181,@Param182,@Param183,@Param184,@Param185,@Param186,@Param187,@Param188,@Param189,@Param190,
                    @Param191,@Param192,@Param193,@Param194,@Param195,@Param196,@Param197,@Param198,@Param199,@Param200,
                    @Param201,@Param202,@Param203,@Param204,@Param205,@Param206,@Param207,@Param208,@Param209,@Param210,
                    @Param211,@Param212,@Param213,@Param214,@Param215,@Param216,@Param217,@Param218,@Param219,@Param220,
                    @Param221,@Param222,@Param223,@Param224,@Param225,@Param226,@Param227,@Param228,@Param229,@Param230,
                    @Param231,@Param232,@Param233,@Param234,@Param235,@Param236,@Param237,@Param238,@Param239,@Param240,
                    @Param241,@Param242,@Param243,@Param244,@Param245,@Param246,@Param247,@Param248,@Param249,@Param250,
                    @StatusID, @CUDStatusID, @IsLocked, @LockedByUser, @CreatedOn, @UpdatedOn,@UserId )";
        }

        private string GetUpdateScript()
        {
            return @"UPDATE [dbo].[TestData]
                     SET [Module] = @Module, [TCID] = @TCID, [Iterations] = @Iterations, 
                          Param1 = @Param1, Param2 = @Param2, Param3 = @Param3, 
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
                        [Param97]= @Param97, [Param98]= @Param98, [Param99]= @Param99, [Param100]= @Param100, [Param101]= @Param101, [Param102]= @Param102, 
                        [Param103]= @Param103, [Param104]= @Param104, [Param105]= @Param105, [Param106]= @Param106, [Param107]= @Param107, [Param108]= @Param108,
                        [Param109]= @Param109, [Param110]= @Param110, [Param111]= @Param111, [Param112]= @Param112, [Param113]= @Param113, [Param114]= @Param114,
                        [Param115]= @Param115, [Param116]= @Param116, [Param117]= @Param117, [Param118]= @Param118, [Param119]= @Param119, [Param120]= @Param120, 
                        [Param121]= @Param121, [Param122]= @Param122, [Param123]= @Param123, [Param124]= @Param124, [Param125]= @Param125, [Param126]= @Param126, 
                        [Param127]= @Param127, [Param128]= @Param128, [Param129]= @Param129, [Param130]= @Param130, [Param131]= @Param131, [Param132]= @Param132, 
                        [Param133]= @Param133, [Param134]= @Param134, [Param135]= @Param135, [Param136]= @Param136, [Param137]= @Param137, [Param138]= @Param138,
                        [Param139]= @Param139, [Param140]= @Param140, [Param141]= @Param141, [Param142]= @Param142, [Param143]= @Param143, [Param144]= @Param144,
                        [Param145]= @Param145, [Param146]= @Param146, [Param147]= @Param147, [Param148]= @Param148, [Param149]= @Param149, [Param150]= @Param150, 
                        [Param151]= @Param151, [Param152]= @Param152, [Param153]= @Param153, [Param154]= @Param154, [Param155]= @Param155, [Param156]= @Param156,
                        [Param157]= @Param157, [Param158]= @Param158, [Param159]= @Param159, [Param160]= @Param160, [Param161]= @Param161, [Param162]= @Param162, 
                        [Param163]= @Param163, [Param164]= @Param164, [Param165]= @Param165, [Param166]= @Param166, [Param167]= @Param167, [Param168]= @Param168, 
                        [Param169]= @Param169, [Param170]= @Param170, [Param171]= @Param171, [Param172]= @Param172, [Param173]= @Param173, [Param174]= @Param174,
                        [Param175]= @Param175, [Param176]= @Param176, [Param177]= @Param177, [Param178]= @Param178, [Param179]= @Param179, [Param180]= @Param180, 
                        [Param181]= @Param181, [Param182]= @Param182, [Param183]= @Param183, [Param184]= @Param184, [Param185]= @Param185, [Param186]= @Param186, 
                        [Param187]= @Param187, [Param188]= @Param188, [Param189]= @Param189, [Param190]= @Param190, [Param191]= @Param191, [Param192]= @Param192, 
                        [Param193]= @Param193, [Param194]= @Param194, [Param195]= @Param195, [Param196]= @Param196, [Param197]= @Param197, [Param198]= @Param198, 
                        [Param199]= @Param199, [Param200]= @Param200, [Param201]= @Param201, [Param202]= @Param202, [Param203]= @Param203, [Param204]= @Param204, 
                        [Param205]= @Param205, [Param206]= @Param206, [Param207]= @Param207, [Param208]= @Param208, [Param209]= @Param209, [Param210]= @Param210, 
                        [Param211]= @Param211, [Param212]= @Param212, [Param213]= @Param213, [Param214]= @Param214, [Param215]= @Param215, [Param216]= @Param216,
                        [Param217]= @Param217, [Param218]= @Param218, [Param219]= @Param219, [Param220]= @Param220, [Param221]= @Param221, [Param222]= @Param222, 
                        [Param223]= @Param223, [Param224]= @Param224, [Param225]= @Param225, [Param226]= @Param226, [Param227]= @Param227, [Param228]= @Param228, 
                        [Param229]= @Param229, [Param230]= @Param230, [Param231]= @Param231, [Param232]= @Param232, [Param233]= @Param233, [Param234]= @Param234,
                        [Param235]= @Param235, [Param236]= @Param236, [Param237]= @Param237, [Param238]= @Param238, [Param239]= @Param239, [Param240]= @Param240, 
                        [Param241]= @Param241, [Param242]= @Param242, [Param243]= @Param243, [Param244]= @Param244, [Param245]= @Param245, [Param246]= @Param246, 
                        [Param247]= @Param247, [Param248]= @Param248, [Param249]= @Param249, [Param250]= @Param250,
                         [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        private string GetUpdateLockedByUserScript()
        {
            return @"UPDATE [dbo].[TestData]
                     SET [IsLocked]= @IsLocked,[LockedByUser]=@LockedByUser, [UpdatedOn]=@UpdatedOn
                     WHERE ID=@Id";
        }

        private string GetDeleteScript()
        {
            return @"UPDATE [dbo].[TestData]
                     SET [StatusID]= @StatusID,[CUDStatusID]= @CUDStatusID,[UserId]=@UserId
                     WHERE ID=@Id";
        }

        public void CreateTestData(TestData testData)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Module", testData.Module);
                parameters.Add("@TCID", testData.TCID);
                parameters.Add("@Iterations", testData.Iterations);
                parameters.Add("@Param1", testData.Param1);
                parameters.Add("@Param2", testData.Param2);
                parameters.Add("@Param3", testData.Param3);
                parameters.Add("@Param4", testData.Param4);
                parameters.Add("@Param5", testData.Param5);
                parameters.Add("@Param6", testData.Param6);
                parameters.Add("@Param8", testData.Param8);
                parameters.Add("@Param7", testData.Param7);
                parameters.Add("@Param9", testData.Param9);
                parameters.Add("@Param10", testData.Param10);
                parameters.Add("@Param11", testData.Param11);
                parameters.Add("@Param12", testData.Param12);
                parameters.Add("@Param13", testData.Param13);
                parameters.Add("@Param14", testData.Param14);
                parameters.Add("@Param15", testData.Param15);
                parameters.Add("@Param16", testData.Param16);
                parameters.Add("@Param17", testData.Param17);
                parameters.Add("@Param18", testData.Param18);
                parameters.Add("@Param19", testData.Param19);
                parameters.Add("@Param20", testData.Param20);
                parameters.Add("@Param20", testData.Param20);
                parameters.Add("@Param21", testData.Param21);
                parameters.Add("@Param22", testData.Param22);
                parameters.Add("@Param23", testData.Param23);
                parameters.Add("@Param24", testData.Param24);
                parameters.Add("@Param25", testData.Param25);
                parameters.Add("@Param26", testData.Param26);
                parameters.Add("@Param27", testData.Param27);
                parameters.Add("@Param28", testData.Param28);
                parameters.Add("@Param29", testData.Param29);
                parameters.Add("@Param30", testData.Param30);
                parameters.Add("@Param31", testData.Param31);
                parameters.Add("@Param32", testData.Param32);
                parameters.Add("@Param33", testData.Param33);
                parameters.Add("@Param34", testData.Param34);
                parameters.Add("@Param35", testData.Param35);
                parameters.Add("@Param36", testData.Param36);
                parameters.Add("@Param37", testData.Param37);
                parameters.Add("@Param38", testData.Param38);
                parameters.Add("@Param39", testData.Param39);
                parameters.Add("@Param40", testData.Param40);
                parameters.Add("@Param41", testData.Param41);
                parameters.Add("@Param42", testData.Param42);
                parameters.Add("@Param43", testData.Param43);
                parameters.Add("@Param44", testData.Param44);
                parameters.Add("@Param45", testData.Param45);
                parameters.Add("@Param46", testData.Param46);
                parameters.Add("@Param47", testData.Param47);
                parameters.Add("@Param48", testData.Param48);
                parameters.Add("@Param49", testData.Param49);
                parameters.Add("@Param50", testData.Param50);
                parameters.Add("@Param51", testData.Param51);
                parameters.Add("@Param52", testData.Param52);
                parameters.Add("@Param53", testData.Param53);
                parameters.Add("@Param54", testData.Param54);
                parameters.Add("@Param55", testData.Param55);
                parameters.Add("@Param56", testData.Param56);
                parameters.Add("@Param57", testData.Param57);
                parameters.Add("@Param58", testData.Param58);
                parameters.Add("@Param59", testData.Param59);
                parameters.Add("@Param60", testData.Param60);
                parameters.Add("@Param61", testData.Param61);
                parameters.Add("@Param62", testData.Param62);
                parameters.Add("@Param63", testData.Param63);
                parameters.Add("@Param64", testData.Param64);
                parameters.Add("@Param65", testData.Param65);
                parameters.Add("@Param66", testData.Param66);
                parameters.Add("@Param67", testData.Param67);
                parameters.Add("@Param68", testData.Param68);
                parameters.Add("@Param69", testData.Param69);
                parameters.Add("@Param70", testData.Param70);
                parameters.Add("@Param71", testData.Param71);
                parameters.Add("@Param72", testData.Param72);
                parameters.Add("@Param73", testData.Param73);
                parameters.Add("@Param74", testData.Param74);
                parameters.Add("@Param75", testData.Param75);
                parameters.Add("@Param76", testData.Param76);
                parameters.Add("@Param77", testData.Param77);
                parameters.Add("@Param78", testData.Param78);
                parameters.Add("@Param79", testData.Param79);
                parameters.Add("@Param80", testData.Param80);
                parameters.Add("@Param81", testData.Param81);
                parameters.Add("@Param82", testData.Param82);
                parameters.Add("@Param83", testData.Param83);
                parameters.Add("@Param84", testData.Param84);
                parameters.Add("@Param85", testData.Param85);
                parameters.Add("@Param86", testData.Param86);
                parameters.Add("@Param87", testData.Param87);
                parameters.Add("@Param88", testData.Param88);
                parameters.Add("@Param89", testData.Param89);
                parameters.Add("@Param90", testData.Param90);
                parameters.Add("@Param91", testData.Param91);
                parameters.Add("@Param92", testData.Param92);
                parameters.Add("@Param93", testData.Param93);
                parameters.Add("@Param94", testData.Param94);
                parameters.Add("@Param95", testData.Param95);
                parameters.Add("@Param96", testData.Param96);
                parameters.Add("@Param97", testData.Param97);
                parameters.Add("@Param98", testData.Param98);
                parameters.Add("@Param99", testData.Param99);
                parameters.Add("@Param100", testData.Param100);
                parameters.Add("@Param101", testData.Param101);
                parameters.Add("@Param102", testData.Param102);
                parameters.Add("@Param103", testData.Param103);
                parameters.Add("@Param104", testData.Param104);
                parameters.Add("@Param105", testData.Param105);
                parameters.Add("@Param106", testData.Param106);
                parameters.Add("@Param107", testData.Param107);
                parameters.Add("@Param108", testData.Param108);
                parameters.Add("@Param109", testData.Param109);
                parameters.Add("@Param110", testData.Param110);
                parameters.Add("@Param111", testData.Param111);
                parameters.Add("@Param112", testData.Param112);
                parameters.Add("@Param113", testData.Param113);
                parameters.Add("@Param114", testData.Param114);
                parameters.Add("@Param115", testData.Param115);
                parameters.Add("@Param116", testData.Param116);
                parameters.Add("@Param117", testData.Param117);
                parameters.Add("@Param118", testData.Param118);
                parameters.Add("@Param119", testData.Param119);
                parameters.Add("@Param120", testData.Param120);
                parameters.Add("@Param121", testData.Param121);
                parameters.Add("@Param122", testData.Param122);
                parameters.Add("@Param123", testData.Param123);
                parameters.Add("@Param124", testData.Param124);
                parameters.Add("@Param125", testData.Param125);
                parameters.Add("@Param126", testData.Param126);
                parameters.Add("@Param127", testData.Param127);
                parameters.Add("@Param128", testData.Param128);
                parameters.Add("@Param129", testData.Param129);
                parameters.Add("@Param130", testData.Param130);
                parameters.Add("@Param131", testData.Param131);
                parameters.Add("@Param132", testData.Param132);
                parameters.Add("@Param133", testData.Param133);
                parameters.Add("@Param134", testData.Param134);
                parameters.Add("@Param135", testData.Param135);
                parameters.Add("@Param136", testData.Param136);
                parameters.Add("@Param137", testData.Param137);
                parameters.Add("@Param138", testData.Param138);
                parameters.Add("@Param139", testData.Param139);
                parameters.Add("@Param140", testData.Param140);
                parameters.Add("@Param141", testData.Param141);
                parameters.Add("@Param142", testData.Param142);
                parameters.Add("@Param143", testData.Param143);
                parameters.Add("@Param144", testData.Param144);
                parameters.Add("@Param145", testData.Param145);
                parameters.Add("@Param146", testData.Param146);
                parameters.Add("@Param147", testData.Param147);
                parameters.Add("@Param148", testData.Param148);
                parameters.Add("@Param149", testData.Param149);
                parameters.Add("@Param150", testData.Param150);
                parameters.Add("@Param151", testData.Param151);
                parameters.Add("@Param152", testData.Param152);
                parameters.Add("@Param153", testData.Param153);
                parameters.Add("@Param154", testData.Param154);
                parameters.Add("@Param155", testData.Param155);
                parameters.Add("@Param156", testData.Param156);
                parameters.Add("@Param157", testData.Param157);
                parameters.Add("@Param158", testData.Param158);
                parameters.Add("@Param159", testData.Param159);
                parameters.Add("@Param160", testData.Param160);
                parameters.Add("@Param161", testData.Param161);
                parameters.Add("@Param162", testData.Param162);
                parameters.Add("@Param163", testData.Param163);
                parameters.Add("@Param164", testData.Param164);
                parameters.Add("@Param165", testData.Param165);
                parameters.Add("@Param166", testData.Param166);
                parameters.Add("@Param167", testData.Param167);
                parameters.Add("@Param168", testData.Param168);
                parameters.Add("@Param169", testData.Param169);
                parameters.Add("@Param170", testData.Param170);
                parameters.Add("@Param171", testData.Param171);
                parameters.Add("@Param172", testData.Param172);
                parameters.Add("@Param173", testData.Param173);
                parameters.Add("@Param174", testData.Param174);
                parameters.Add("@Param175", testData.Param175);
                parameters.Add("@Param176", testData.Param176);
                parameters.Add("@Param177", testData.Param177);
                parameters.Add("@Param178", testData.Param178);
                parameters.Add("@Param179", testData.Param179);
                parameters.Add("@Param180", testData.Param180);
                parameters.Add("@Param181", testData.Param181);
                parameters.Add("@Param182", testData.Param182);
                parameters.Add("@Param183", testData.Param183);
                parameters.Add("@Param184", testData.Param184);
                parameters.Add("@Param185", testData.Param185);
                parameters.Add("@Param186", testData.Param186);
                parameters.Add("@Param187", testData.Param187);
                parameters.Add("@Param188", testData.Param188);
                parameters.Add("@Param189", testData.Param189);
                parameters.Add("@Param190", testData.Param190);
                parameters.Add("@Param191", testData.Param191);
                parameters.Add("@Param192", testData.Param192);
                parameters.Add("@Param193", testData.Param193);
                parameters.Add("@Param194", testData.Param194);
                parameters.Add("@Param195", testData.Param195);
                parameters.Add("@Param196", testData.Param196);
                parameters.Add("@Param197", testData.Param197);
                parameters.Add("@Param198", testData.Param198);
                parameters.Add("@Param199", testData.Param199);
                parameters.Add("@Param200", testData.Param200);
                parameters.Add("@Param201", testData.Param201);
                parameters.Add("@Param202", testData.Param202);
                parameters.Add("@Param203", testData.Param203);
                parameters.Add("@Param204", testData.Param204);
                parameters.Add("@Param205", testData.Param205);
                parameters.Add("@Param206", testData.Param206);
                parameters.Add("@Param207", testData.Param207);
                parameters.Add("@Param208", testData.Param208);
                parameters.Add("@Param209", testData.Param209);
                parameters.Add("@Param210", testData.Param210);
                parameters.Add("@Param211", testData.Param211);
                parameters.Add("@Param212", testData.Param212);
                parameters.Add("@Param213", testData.Param213);
                parameters.Add("@Param214", testData.Param214);
                parameters.Add("@Param215", testData.Param215);
                parameters.Add("@Param216", testData.Param216);
                parameters.Add("@Param217", testData.Param217);
                parameters.Add("@Param218", testData.Param218);
                parameters.Add("@Param219", testData.Param219);
                parameters.Add("@Param220", testData.Param220);
                parameters.Add("@Param221", testData.Param221);
                parameters.Add("@Param222", testData.Param222);
                parameters.Add("@Param223", testData.Param223);
                parameters.Add("@Param224", testData.Param224);
                parameters.Add("@Param225", testData.Param225);
                parameters.Add("@Param226", testData.Param226);
                parameters.Add("@Param227", testData.Param227);
                parameters.Add("@Param228", testData.Param228);
                parameters.Add("@Param229", testData.Param229);
                parameters.Add("@Param230", testData.Param230);
                parameters.Add("@Param231", testData.Param231);
                parameters.Add("@Param232", testData.Param232);
                parameters.Add("@Param233", testData.Param233);
                parameters.Add("@Param234", testData.Param234);
                parameters.Add("@Param235", testData.Param235);
                parameters.Add("@Param236", testData.Param236);
                parameters.Add("@Param237", testData.Param237);
                parameters.Add("@Param238", testData.Param238);
                parameters.Add("@Param239", testData.Param239);
                parameters.Add("@Param240", testData.Param240);
                parameters.Add("@Param241", testData.Param241);
                parameters.Add("@Param242", testData.Param242);
                parameters.Add("@Param243", testData.Param243);
                parameters.Add("@Param244", testData.Param244);
                parameters.Add("@Param245", testData.Param245);
                parameters.Add("@Param246", testData.Param246);
                parameters.Add("@Param247", testData.Param247);
                parameters.Add("@Param248", testData.Param248);
                parameters.Add("@Param249", testData.Param249);
                parameters.Add("@Param250", testData.Param250);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Created);
                parameters.Add("@IsLocked", testData.IsLocked);
                parameters.Add("@LockedByUser", testData.LockedByUser);
                parameters.Add("@CreatedOn", DateTime.UtcNow);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", testData.UserId);

                con.Query($"{GetInsertScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void DeleteTestData(int testDataId,int userId)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", testDataId);
                parameters.Add("@StatusID", (int)Status.InActive);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Deleted);
                parameters.Add("@UserId", userId);

                con.Query($"{GetDeleteScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateTestData(int testDataId, TestData testData)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", testDataId);
                parameters.Add("@Module", testData.Module);
                parameters.Add("@TCID", testData.TCID);
                parameters.Add("@Iterations", testData.Iterations);
                parameters.Add("@Param1", testData.Param1);
                parameters.Add("@Param2", testData.Param2);
                parameters.Add("@Param3", testData.Param3);
                parameters.Add("@Param4", testData.Param4);
                parameters.Add("@Param5", testData.Param5);
                parameters.Add("@Param6", testData.Param6);
                parameters.Add("@Param8", testData.Param8);
                parameters.Add("@Param7", testData.Param7);
                parameters.Add("@Param9", testData.Param9);
                parameters.Add("@Param10", testData.Param10);
                parameters.Add("@Param11", testData.Param11);
                parameters.Add("@Param12", testData.Param12);
                parameters.Add("@Param13", testData.Param13);
                parameters.Add("@Param14", testData.Param14);
                parameters.Add("@Param15", testData.Param15);
                parameters.Add("@Param16", testData.Param16);
                parameters.Add("@Param17", testData.Param17);
                parameters.Add("@Param18", testData.Param18);
                parameters.Add("@Param19", testData.Param19);
                parameters.Add("@Param20", testData.Param20);
                parameters.Add("@Param20", testData.Param20);
                parameters.Add("@Param21", testData.Param21);
                parameters.Add("@Param22", testData.Param22);
                parameters.Add("@Param23", testData.Param23);
                parameters.Add("@Param24", testData.Param24);
                parameters.Add("@Param25", testData.Param25);
                parameters.Add("@Param26", testData.Param26);
                parameters.Add("@Param27", testData.Param27);
                parameters.Add("@Param28", testData.Param28);
                parameters.Add("@Param29", testData.Param29);
                parameters.Add("@Param30", testData.Param30);
                parameters.Add("@Param31", testData.Param31);
                parameters.Add("@Param32", testData.Param32);
                parameters.Add("@Param33", testData.Param33);
                parameters.Add("@Param34", testData.Param34);
                parameters.Add("@Param35", testData.Param35);
                parameters.Add("@Param36", testData.Param36);
                parameters.Add("@Param37", testData.Param37);
                parameters.Add("@Param38", testData.Param38);
                parameters.Add("@Param39", testData.Param39);
                parameters.Add("@Param40", testData.Param40);
                parameters.Add("@Param41", testData.Param41);
                parameters.Add("@Param42", testData.Param42);
                parameters.Add("@Param43", testData.Param43);
                parameters.Add("@Param44", testData.Param44);
                parameters.Add("@Param45", testData.Param45);
                parameters.Add("@Param46", testData.Param46);
                parameters.Add("@Param47", testData.Param47);
                parameters.Add("@Param48", testData.Param48);
                parameters.Add("@Param49", testData.Param49);
                parameters.Add("@Param50", testData.Param50);
                parameters.Add("@Param51", testData.Param51);
                parameters.Add("@Param52", testData.Param52);
                parameters.Add("@Param53", testData.Param53);
                parameters.Add("@Param54", testData.Param54);
                parameters.Add("@Param55", testData.Param55);
                parameters.Add("@Param56", testData.Param56);
                parameters.Add("@Param57", testData.Param57);
                parameters.Add("@Param58", testData.Param58);
                parameters.Add("@Param59", testData.Param59);
                parameters.Add("@Param60", testData.Param60);
                parameters.Add("@Param61", testData.Param61);
                parameters.Add("@Param62", testData.Param62);
                parameters.Add("@Param63", testData.Param63);
                parameters.Add("@Param64", testData.Param64);
                parameters.Add("@Param65", testData.Param65);
                parameters.Add("@Param66", testData.Param66);
                parameters.Add("@Param67", testData.Param67);
                parameters.Add("@Param68", testData.Param68);
                parameters.Add("@Param69", testData.Param69);
                parameters.Add("@Param70", testData.Param70);
                parameters.Add("@Param71", testData.Param71);
                parameters.Add("@Param72", testData.Param72);
                parameters.Add("@Param73", testData.Param73);
                parameters.Add("@Param74", testData.Param74);
                parameters.Add("@Param75", testData.Param75);
                parameters.Add("@Param76", testData.Param76);
                parameters.Add("@Param77", testData.Param77);
                parameters.Add("@Param78", testData.Param78);
                parameters.Add("@Param79", testData.Param79);
                parameters.Add("@Param80", testData.Param80);
                parameters.Add("@Param81", testData.Param81);
                parameters.Add("@Param82", testData.Param82);
                parameters.Add("@Param83", testData.Param83);
                parameters.Add("@Param84", testData.Param84);
                parameters.Add("@Param85", testData.Param85);
                parameters.Add("@Param86", testData.Param86);
                parameters.Add("@Param87", testData.Param87);
                parameters.Add("@Param88", testData.Param88);
                parameters.Add("@Param89", testData.Param89);
                parameters.Add("@Param90", testData.Param90);
                parameters.Add("@Param91", testData.Param91);
                parameters.Add("@Param92", testData.Param92);
                parameters.Add("@Param93", testData.Param93);
                parameters.Add("@Param94", testData.Param94);
                parameters.Add("@Param95", testData.Param95);
                parameters.Add("@Param96", testData.Param96);
                parameters.Add("@Param97", testData.Param97);
                parameters.Add("@Param98", testData.Param98);
                parameters.Add("@Param99", testData.Param99);
                parameters.Add("@Param100", testData.Param100);
                parameters.Add("@Param101", testData.Param101);
                parameters.Add("@Param102", testData.Param102);
                parameters.Add("@Param103", testData.Param103);
                parameters.Add("@Param104", testData.Param104);
                parameters.Add("@Param105", testData.Param105);
                parameters.Add("@Param106", testData.Param106);
                parameters.Add("@Param107", testData.Param107);
                parameters.Add("@Param108", testData.Param108);
                parameters.Add("@Param109", testData.Param109);
                parameters.Add("@Param110", testData.Param110);
                parameters.Add("@Param111", testData.Param111);
                parameters.Add("@Param112", testData.Param112);
                parameters.Add("@Param113", testData.Param113);
                parameters.Add("@Param114", testData.Param114);
                parameters.Add("@Param115", testData.Param115);
                parameters.Add("@Param116", testData.Param116);
                parameters.Add("@Param117", testData.Param117);
                parameters.Add("@Param118", testData.Param118);
                parameters.Add("@Param119", testData.Param119);
                parameters.Add("@Param120", testData.Param120);
                parameters.Add("@Param121", testData.Param121);
                parameters.Add("@Param122", testData.Param122);
                parameters.Add("@Param123", testData.Param123);
                parameters.Add("@Param124", testData.Param124);
                parameters.Add("@Param125", testData.Param125);
                parameters.Add("@Param126", testData.Param126);
                parameters.Add("@Param127", testData.Param127);
                parameters.Add("@Param128", testData.Param128);
                parameters.Add("@Param129", testData.Param129);
                parameters.Add("@Param130", testData.Param130);
                parameters.Add("@Param131", testData.Param131);
                parameters.Add("@Param132", testData.Param132);
                parameters.Add("@Param133", testData.Param133);
                parameters.Add("@Param134", testData.Param134);
                parameters.Add("@Param135", testData.Param135);
                parameters.Add("@Param136", testData.Param136);
                parameters.Add("@Param137", testData.Param137);
                parameters.Add("@Param138", testData.Param138);
                parameters.Add("@Param139", testData.Param139);
                parameters.Add("@Param140", testData.Param140);
                parameters.Add("@Param141", testData.Param141);
                parameters.Add("@Param142", testData.Param142);
                parameters.Add("@Param143", testData.Param143);
                parameters.Add("@Param144", testData.Param144);
                parameters.Add("@Param145", testData.Param145);
                parameters.Add("@Param146", testData.Param146);
                parameters.Add("@Param147", testData.Param147);
                parameters.Add("@Param148", testData.Param148);
                parameters.Add("@Param149", testData.Param149);
                parameters.Add("@Param150", testData.Param150);
                parameters.Add("@Param151", testData.Param151);
                parameters.Add("@Param152", testData.Param152);
                parameters.Add("@Param153", testData.Param153);
                parameters.Add("@Param154", testData.Param154);
                parameters.Add("@Param155", testData.Param155);
                parameters.Add("@Param156", testData.Param156);
                parameters.Add("@Param157", testData.Param157);
                parameters.Add("@Param158", testData.Param158);
                parameters.Add("@Param159", testData.Param159);
                parameters.Add("@Param160", testData.Param160);
                parameters.Add("@Param161", testData.Param161);
                parameters.Add("@Param162", testData.Param162);
                parameters.Add("@Param163", testData.Param163);
                parameters.Add("@Param164", testData.Param164);
                parameters.Add("@Param165", testData.Param165);
                parameters.Add("@Param166", testData.Param166);
                parameters.Add("@Param167", testData.Param167);
                parameters.Add("@Param168", testData.Param168);
                parameters.Add("@Param169", testData.Param169);
                parameters.Add("@Param170", testData.Param170);
                parameters.Add("@Param171", testData.Param171);
                parameters.Add("@Param172", testData.Param172);
                parameters.Add("@Param173", testData.Param173);
                parameters.Add("@Param174", testData.Param174);
                parameters.Add("@Param175", testData.Param175);
                parameters.Add("@Param176", testData.Param176);
                parameters.Add("@Param177", testData.Param177);
                parameters.Add("@Param178", testData.Param178);
                parameters.Add("@Param179", testData.Param179);
                parameters.Add("@Param180", testData.Param180);
                parameters.Add("@Param181", testData.Param181);
                parameters.Add("@Param182", testData.Param182);
                parameters.Add("@Param183", testData.Param183);
                parameters.Add("@Param184", testData.Param184);
                parameters.Add("@Param185", testData.Param185);
                parameters.Add("@Param186", testData.Param186);
                parameters.Add("@Param187", testData.Param187);
                parameters.Add("@Param188", testData.Param188);
                parameters.Add("@Param189", testData.Param189);
                parameters.Add("@Param190", testData.Param190);
                parameters.Add("@Param191", testData.Param191);
                parameters.Add("@Param192", testData.Param192);
                parameters.Add("@Param193", testData.Param193);
                parameters.Add("@Param194", testData.Param194);
                parameters.Add("@Param195", testData.Param195);
                parameters.Add("@Param196", testData.Param196);
                parameters.Add("@Param197", testData.Param197);
                parameters.Add("@Param198", testData.Param198);
                parameters.Add("@Param199", testData.Param199);
                parameters.Add("@Param200", testData.Param200);
                parameters.Add("@Param201", testData.Param201);
                parameters.Add("@Param202", testData.Param202);
                parameters.Add("@Param203", testData.Param203);
                parameters.Add("@Param204", testData.Param204);
                parameters.Add("@Param205", testData.Param205);
                parameters.Add("@Param206", testData.Param206);
                parameters.Add("@Param207", testData.Param207);
                parameters.Add("@Param208", testData.Param208);
                parameters.Add("@Param209", testData.Param209);
                parameters.Add("@Param210", testData.Param210);
                parameters.Add("@Param211", testData.Param211);
                parameters.Add("@Param212", testData.Param212);
                parameters.Add("@Param213", testData.Param213);
                parameters.Add("@Param214", testData.Param214);
                parameters.Add("@Param215", testData.Param215);
                parameters.Add("@Param216", testData.Param216);
                parameters.Add("@Param217", testData.Param217);
                parameters.Add("@Param218", testData.Param218);
                parameters.Add("@Param219", testData.Param219);
                parameters.Add("@Param220", testData.Param220);
                parameters.Add("@Param221", testData.Param221);
                parameters.Add("@Param222", testData.Param222);
                parameters.Add("@Param223", testData.Param223);
                parameters.Add("@Param224", testData.Param224);
                parameters.Add("@Param225", testData.Param225);
                parameters.Add("@Param226", testData.Param226);
                parameters.Add("@Param227", testData.Param227);
                parameters.Add("@Param228", testData.Param228);
                parameters.Add("@Param229", testData.Param229);
                parameters.Add("@Param230", testData.Param230);
                parameters.Add("@Param231", testData.Param231);
                parameters.Add("@Param232", testData.Param232);
                parameters.Add("@Param233", testData.Param233);
                parameters.Add("@Param234", testData.Param234);
                parameters.Add("@Param235", testData.Param235);
                parameters.Add("@Param236", testData.Param236);
                parameters.Add("@Param237", testData.Param237);
                parameters.Add("@Param238", testData.Param238);
                parameters.Add("@Param239", testData.Param239);
                parameters.Add("@Param240", testData.Param240);
                parameters.Add("@Param241", testData.Param241);
                parameters.Add("@Param242", testData.Param242);
                parameters.Add("@Param243", testData.Param243);
                parameters.Add("@Param244", testData.Param244);
                parameters.Add("@Param245", testData.Param245);
                parameters.Add("@Param246", testData.Param246);
                parameters.Add("@Param247", testData.Param247);
                parameters.Add("@Param248", testData.Param248);
                parameters.Add("@Param249", testData.Param249);
                parameters.Add("@Param250", testData.Param250);
                parameters.Add("@StatusID", (int)Status.Active);
                parameters.Add("@CUDStatusID", (int)CUDStatus.Updated);
                parameters.Add("@IsLocked", null);
                parameters.Add("@LockedByUser", null);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);
                parameters.Add("@UserId", testData.UserId);

                con.Query($"{GetUpdateScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }

        public void UpdateLockedByFlags(TestData testData)
        {
            using (IDbConnection con = new SqlConnection(strConnectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", testData.ID);
                parameters.Add("@IsLocked", testData.IsLocked);
                parameters.Add("@LockedByUser", testData.LockedByUser);
                parameters.Add("@UpdatedOn", DateTime.UtcNow);

                con.Query($"{GetUpdateLockedByUserScript()}",
                    parameters,
                    commandType: CommandType.Text);
            }
        }
    }
}
