using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core;
using Automation.Core.DataAccessAbstractions;
using AutoMapper;
using Microsoft.AspNet.OData;
using Automation.Service.ViewModel;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Automation.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : Controller
    {
        private IGenericRepo<TestData> _genericRepo;
        private ITestDataRepo _testDataRepo;
        private IMapper _mapper;
        private ILogger<TestDataController> _logger;

        public TestDataController(IGenericRepo<TestData> genericRepo,
            ITestDataRepo testDataRepo,
            IMapper mapper,
            ILogger<TestDataController> logger)
        {
            _genericRepo = genericRepo;
            _testDataRepo = testDataRepo;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/values
        [EnableQuery]
        [HttpGet("GetTestAllData")]
        public ActionResult<IList<TestDataVM>> Get()
        {
            var result = new List<TestDataVM>();
            try
            {
                result = _mapper.Map<List<TestDataVM>>(_genericRepo.GetAll().ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }

        // GET api/values/5
        [HttpGet("GetTestData/{id}/{userId}")]
        public TestDataVM Get(int id, int userId)
        {
            var result = new TestDataVM();
            try
            {
                var data = _genericRepo.GetById(id);
                if (data != null)
                {
                    data.IsLocked = true;
                    data.LockedByUser = userId;
                    data.UserId = userId;
                    _testDataRepo.UpdateLockedByFlags(data);
                }
                result = _mapper.Map<TestDataVM>(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }

        [HttpPut("ResetLockedByField/{id}/{userId}")]
        public void ResetLockedByField(int id, int userId)
        {
            try
            {
                var data = _genericRepo.GetById(id);
                if (data != null)
                {
                    data.IsLocked = null;
                    data.LockedByUser = null;
                    data.UserId = null;
                    _testDataRepo.UpdateLockedByFlags(data);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
        }

        // POST api/values
        [HttpPost("AddTestData")]
        public void Post([FromBody]TestDataVM testData)
        {
            try
            {
                var iteration = GetNextIteration(testData.TCID);
                testData.Iterations = iteration;
                if (iteration == 0)
                {
                    PopulateParamsValues(testData);
                }
                var data = _mapper.Map<TestData>(testData);
                _testDataRepo.CreateTestData(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
        }

        private void PopulateParamsValues(TestDataVM testData)
        {
            string moduleType = testData.Module;


            testData.Param1 = $"{moduleType}_Param1";

            testData.Param2 = $"{moduleType}_Param2";

            testData.Param3 = $"{moduleType}_Param3";

            testData.Param4 = $"{moduleType}_Param4";

            testData.Param5 = $"{moduleType}_Param5";

            testData.Param6 = $"{moduleType}_Param6";

            testData.Param7 = $"{moduleType}_Param7";

            testData.Param8 = $"{moduleType}_Param8";

            testData.Param9 = $"{moduleType}_Param9";

            testData.Param10 = $"{moduleType}_Param10";

            testData.Param11 = $"{moduleType}_Param11";

            testData.Param12 = $"{moduleType}_Param12";

            testData.Param13 = $"{moduleType}_Param13";

            testData.Param14 = $"{moduleType}_Param14";

            testData.Param15 = $"{moduleType}_Param15";

            testData.Param16 = $"{moduleType}_Param16";

            testData.Param17 = $"{moduleType}_Param17";

            testData.Param18 = $"{moduleType}_Param18";

            testData.Param19 = $"{moduleType}_Param19";

            testData.Param20 = $"{moduleType}_Param20";

            testData.Param21 = $"{moduleType}_Param21";

            testData.Param22 = $"{moduleType}_Param22";

            testData.Param23 = $"{moduleType}_Param23";

            testData.Param24 = $"{moduleType}_Param24";

            testData.Param25 = $"{moduleType}_Param25";

            testData.Param26 = $"{moduleType}_Param26";

            testData.Param27 = $"{moduleType}_Param27";

            testData.Param28 = $"{moduleType}_Param28";

            testData.Param29 = $"{moduleType}_Param29";

            testData.Param30 = $"{moduleType}_Param30";

            testData.Param31 = $"{moduleType}_Param31";

            testData.Param32 = $"{moduleType}_Param32";

            testData.Param33 = $"{moduleType}_Param33";

            testData.Param34 = $"{moduleType}_Param34";

            testData.Param35 = $"{moduleType}_Param35";

            testData.Param36 = $"{moduleType}_Param36";

            testData.Param37 = $"{moduleType}_Param37";

            testData.Param38 = $"{moduleType}_Param38";

            testData.Param39 = $"{moduleType}_Param39";

            testData.Param40 = $"{moduleType}_Param40";

            testData.Param41 = $"{moduleType}_Param41";

            testData.Param42 = $"{moduleType}_Param42";

            testData.Param43 = $"{moduleType}_Param43";

            testData.Param44 = $"{moduleType}_Param44";

            testData.Param45 = $"{moduleType}_Param45";

            testData.Param46 = $"{moduleType}_Param46";

            testData.Param47 = $"{moduleType}_Param47";

            testData.Param48 = $"{moduleType}_Param48";

            testData.Param49 = $"{moduleType}_Param49";

            testData.Param50 = $"{moduleType}_Param50";

            testData.Param51 = $"{moduleType}_Param51";

            testData.Param52 = $"{moduleType}_Param52";

            testData.Param53 = $"{moduleType}_Param53";

            testData.Param54 = $"{moduleType}_Param54";

            testData.Param55 = $"{moduleType}_Param55";

            testData.Param56 = $"{moduleType}_Param56";

            testData.Param57 = $"{moduleType}_Param57";

            testData.Param58 = $"{moduleType}_Param58";

            testData.Param59 = $"{moduleType}_Param59";

            testData.Param60 = $"{moduleType}_Param60";

            testData.Param61 = $"{moduleType}_Param61";

            testData.Param62 = $"{moduleType}_Param62";

            testData.Param63 = $"{moduleType}_Param63";

            testData.Param64 = $"{moduleType}_Param64";

            testData.Param65 = $"{moduleType}_Param65";

            testData.Param66 = $"{moduleType}_Param66";

            testData.Param67 = $"{moduleType}_Param67";

            testData.Param68 = $"{moduleType}_Param68";

            testData.Param69 = $"{moduleType}_Param69";

            testData.Param70 = $"{moduleType}_Param70";

            testData.Param71 = $"{moduleType}_Param71";

            testData.Param72 = $"{moduleType}_Param72";

            testData.Param73 = $"{moduleType}_Param73";

            testData.Param74 = $"{moduleType}_Param74";

            testData.Param75 = $"{moduleType}_Param75";

            testData.Param76 = $"{moduleType}_Param76";

            testData.Param77 = $"{moduleType}_Param77";

            testData.Param78 = $"{moduleType}_Param78";

            testData.Param79 = $"{moduleType}_Param79";

            testData.Param80 = $"{moduleType}_Param80";

            testData.Param81 = $"{moduleType}_Param81";

            testData.Param82 = $"{moduleType}_Param82";

            testData.Param83 = $"{moduleType}_Param83";

            testData.Param84 = $"{moduleType}_Param84";

            testData.Param85 = $"{moduleType}_Param85";

            testData.Param86 = $"{moduleType}_Param86";

            testData.Param87 = $"{moduleType}_Param87";

            testData.Param88 = $"{moduleType}_Param88";

            testData.Param89 = $"{moduleType}_Param89";

            testData.Param90 = $"{moduleType}_Param90";

            testData.Param91 = $"{moduleType}_Param91";

            testData.Param92 = $"{moduleType}_Param92";

            testData.Param93 = $"{moduleType}_Param93";

            testData.Param94 = $"{moduleType}_Param94";

            testData.Param95 = $"{moduleType}_Param95";

            testData.Param96 = $"{moduleType}_Param96";

            testData.Param97 = $"{moduleType}_Param97";

            testData.Param98 = $"{moduleType}_Param98";

            testData.Param99 = $"{moduleType}_Param99";

            testData.Param100 = $"{moduleType}_Param100";

            testData.Param101 = $"{moduleType}_Param101";

            testData.Param102 = $"{moduleType}_Param102";

            testData.Param103 = $"{moduleType}_Param103";

            testData.Param104 = $"{moduleType}_Param104";

            testData.Param105 = $"{moduleType}_Param105";

            testData.Param106 = $"{moduleType}_Param106";

            testData.Param107 = $"{moduleType}_Param107";

            testData.Param108 = $"{moduleType}_Param108";

            testData.Param109 = $"{moduleType}_Param109";

            testData.Param110 = $"{moduleType}_Param110";

            testData.Param111 = $"{moduleType}_Param111";

            testData.Param112 = $"{moduleType}_Param112";

            testData.Param113 = $"{moduleType}_Param113";

            testData.Param114 = $"{moduleType}_Param114";

            testData.Param115 = $"{moduleType}_Param115";

            testData.Param116 = $"{moduleType}_Param116";

            testData.Param117 = $"{moduleType}_Param117";

            testData.Param118 = $"{moduleType}_Param118";

            testData.Param119 = $"{moduleType}_Param119";

            testData.Param120 = $"{moduleType}_Param120";

            testData.Param121 = $"{moduleType}_Param121";

            testData.Param122 = $"{moduleType}_Param122";

            testData.Param123 = $"{moduleType}_Param123";

            testData.Param124 = $"{moduleType}_Param124";

            testData.Param125 = $"{moduleType}_Param125";

            testData.Param126 = $"{moduleType}_Param126";

            testData.Param127 = $"{moduleType}_Param127";

            testData.Param128 = $"{moduleType}_Param128";

            testData.Param129 = $"{moduleType}_Param129";

            testData.Param130 = $"{moduleType}_Param130";

            testData.Param131 = $"{moduleType}_Param131";

            testData.Param132 = $"{moduleType}_Param132";

            testData.Param133 = $"{moduleType}_Param133";

            testData.Param134 = $"{moduleType}_Param134";

            testData.Param135 = $"{moduleType}_Param135";

            testData.Param136 = $"{moduleType}_Param136";

            testData.Param137 = $"{moduleType}_Param137";

            testData.Param138 = $"{moduleType}_Param138";

            testData.Param139 = $"{moduleType}_Param139";

            testData.Param140 = $"{moduleType}_Param140";

            testData.Param141 = $"{moduleType}_Param141";

            testData.Param142 = $"{moduleType}_Param142";

            testData.Param143 = $"{moduleType}_Param143";

            testData.Param144 = $"{moduleType}_Param144";

            testData.Param145 = $"{moduleType}_Param145";

            testData.Param146 = $"{moduleType}_Param146";

            testData.Param147 = $"{moduleType}_Param147";

            testData.Param148 = $"{moduleType}_Param148";

            testData.Param149 = $"{moduleType}_Param149";

            testData.Param150 = $"{moduleType}_Param150";

            testData.Param151 = $"{moduleType}_Param151";

            testData.Param152 = $"{moduleType}_Param152";

            testData.Param153 = $"{moduleType}_Param153";

            testData.Param154 = $"{moduleType}_Param154";

            testData.Param155 = $"{moduleType}_Param155";

            testData.Param156 = $"{moduleType}_Param156";

            testData.Param157 = $"{moduleType}_Param157";

            testData.Param158 = $"{moduleType}_Param158";

            testData.Param159 = $"{moduleType}_Param159";

            testData.Param160 = $"{moduleType}_Param160";

            testData.Param161 = $"{moduleType}_Param161";

            testData.Param162 = $"{moduleType}_Param162";

            testData.Param163 = $"{moduleType}_Param163";

            testData.Param164 = $"{moduleType}_Param164";

            testData.Param165 = $"{moduleType}_Param165";

            testData.Param166 = $"{moduleType}_Param166";

            testData.Param167 = $"{moduleType}_Param167";

            testData.Param168 = $"{moduleType}_Param168";

            testData.Param169 = $"{moduleType}_Param169";

            testData.Param170 = $"{moduleType}_Param170";

            testData.Param171 = $"{moduleType}_Param171";

            testData.Param172 = $"{moduleType}_Param172";

            testData.Param173 = $"{moduleType}_Param173";

            testData.Param174 = $"{moduleType}_Param174";

            testData.Param175 = $"{moduleType}_Param175";

            testData.Param176 = $"{moduleType}_Param176";

            testData.Param177 = $"{moduleType}_Param177";

            testData.Param178 = $"{moduleType}_Param178";

            testData.Param179 = $"{moduleType}_Param179";

            testData.Param180 = $"{moduleType}_Param180";

            testData.Param181 = $"{moduleType}_Param181";

            testData.Param182 = $"{moduleType}_Param182";

            testData.Param183 = $"{moduleType}_Param183";

            testData.Param184 = $"{moduleType}_Param184";

            testData.Param185 = $"{moduleType}_Param185";

            testData.Param186 = $"{moduleType}_Param186";

            testData.Param187 = $"{moduleType}_Param187";

            testData.Param188 = $"{moduleType}_Param188";

            testData.Param189 = $"{moduleType}_Param189";

            testData.Param190 = $"{moduleType}_Param190";

            testData.Param191 = $"{moduleType}_Param191";

            testData.Param192 = $"{moduleType}_Param192";

            testData.Param193 = $"{moduleType}_Param193";

            testData.Param194 = $"{moduleType}_Param194";

            testData.Param195 = $"{moduleType}_Param195";

            testData.Param196 = $"{moduleType}_Param196";

            testData.Param197 = $"{moduleType}_Param197";

            testData.Param198 = $"{moduleType}_Param198";

            testData.Param199 = $"{moduleType}_Param199";

            testData.Param200 = $"{moduleType}_Param200";

            testData.Param201 = $"{moduleType}_Param201";

            testData.Param202 = $"{moduleType}_Param202";

            testData.Param203 = $"{moduleType}_Param203";

            testData.Param204 = $"{moduleType}_Param204";

            testData.Param205 = $"{moduleType}_Param205";

            testData.Param206 = $"{moduleType}_Param206";

            testData.Param207 = $"{moduleType}_Param207";

            testData.Param208 = $"{moduleType}_Param208";

            testData.Param209 = $"{moduleType}_Param209";

            testData.Param210 = $"{moduleType}_Param210";

            testData.Param211 = $"{moduleType}_Param211";

            testData.Param212 = $"{moduleType}_Param212";

            testData.Param213 = $"{moduleType}_Param213";

            testData.Param214 = $"{moduleType}_Param214";

            testData.Param215 = $"{moduleType}_Param215";

            testData.Param216 = $"{moduleType}_Param216";

            testData.Param217 = $"{moduleType}_Param217";

            testData.Param218 = $"{moduleType}_Param218";

            testData.Param219 = $"{moduleType}_Param219";

            testData.Param220 = $"{moduleType}_Param220";

            testData.Param221 = $"{moduleType}_Param221";

            testData.Param222 = $"{moduleType}_Param222";

            testData.Param223 = $"{moduleType}_Param223";

            testData.Param224 = $"{moduleType}_Param224";

            testData.Param225 = $"{moduleType}_Param225";

            testData.Param226 = $"{moduleType}_Param226";

            testData.Param227 = $"{moduleType}_Param227";

            testData.Param228 = $"{moduleType}_Param228";

            testData.Param229 = $"{moduleType}_Param229";

            testData.Param230 = $"{moduleType}_Param230";

            testData.Param231 = $"{moduleType}_Param231";

            testData.Param232 = $"{moduleType}_Param232";

            testData.Param233 = $"{moduleType}_Param233";

            testData.Param234 = $"{moduleType}_Param234";

            testData.Param235 = $"{moduleType}_Param235";

            testData.Param236 = $"{moduleType}_Param236";

            testData.Param237 = $"{moduleType}_Param237";

            testData.Param238 = $"{moduleType}_Param238";

            testData.Param239 = $"{moduleType}_Param239";

            testData.Param240 = $"{moduleType}_Param240";

            testData.Param241 = $"{moduleType}_Param241";

            testData.Param242 = $"{moduleType}_Param242";

            testData.Param243 = $"{moduleType}_Param243";

            testData.Param244 = $"{moduleType}_Param244";

            testData.Param245 = $"{moduleType}_Param245";

            testData.Param246 = $"{moduleType}_Param246";

            testData.Param247 = $"{moduleType}_Param247";

            testData.Param248 = $"{moduleType}_Param248";

            testData.Param249 = $"{moduleType}_Param249";

            testData.Param250 = $"{moduleType}_Param250";

        }

        // PUT api/values/5
        [HttpPut("UpdateTestData/{id}")]
        public void Put(int id, [FromBody]TestDataVM testData)
        {
            try
            {
                var data = _mapper.Map<TestData>(testData);
                _testDataRepo.UpdateTestData(id, data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
        }

        // DELETE api/values/5
        [HttpDelete("DeleteTestData/{id}/{userId}")]
        public void Delete(int id, int userId)
        {
            try
            {
                _testDataRepo.DeleteTestData(id, userId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
        }

        [HttpGet("GetRecordsCount")]
        public int GetRecordsCount()
        {
            var result = 0;
            try
            {
                result = _genericRepo.GetRecordsCount();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }

        [HttpGet("GetNextIteration/{tcid}")]
        public int GetNextIteration(string tcid)
        {
            int result = 0;
            try
            {
                var iteration = _testDataRepo.GetLastIterationNumber(tcid);
                result = (iteration == null) ? 0 : (int)iteration + 1;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }

        [HttpGet("GetAllModule")]
        public List<string> GetAllModule()
        {
            var result = new List<string>();
            try
            {
                result = _testDataRepo.GetAllModule();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }

        [HttpGet("GetAllTCID")]
        public List<string> GetAllTCID()
        {
            var result = new List<string>();
            try
            {
                result = _testDataRepo.GetAllTCID();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
                result.Add(ex.Message);

            }
            return result;
        }
    }
}
