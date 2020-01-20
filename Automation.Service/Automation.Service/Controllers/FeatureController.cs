using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.DataAccessAbstractions;
using Automation.Core;
using AutoMapper;
using Automation.Service.ViewModel;
using Microsoft.AspNet.OData;

namespace Automation.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private IGenericRepo<ModuleController> _genericRepo;
        private IGenericRepo<TestController> _genericRepo2;
        private IGenericRepo<BrowserVMExec> _genericRepo3;
        private ITestControllerRepo _testControllerRepo;
        private IGenericRepo<ModuleController_Map> _moduleControllerMapRepo;
        private IGenericRepo<TestController_Map> _testControllerMapRepo;
        private IMapper _mapper;

        public FeatureController(IGenericRepo<ModuleController> genericRepo,
            IGenericRepo<ModuleController_Map> moduleControllerMapRepo,
            IGenericRepo<TestController> genericRepo2,
            IGenericRepo<TestController_Map> testControllerMapRepo,
            IGenericRepo<BrowserVMExec> genericRepo3,
            ITestControllerRepo testControllerRepo,
            IMapper mapper)
        {
            _genericRepo = genericRepo;
            _genericRepo2 = genericRepo2;
            _genericRepo3 = genericRepo3;
            _testControllerRepo = testControllerRepo;
            _moduleControllerMapRepo = moduleControllerMapRepo;
            _testControllerMapRepo = testControllerMapRepo;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("GetAllModuleController/{userId}")]
        public IList<ModuleControllerVM> GetAllModuleController(int userId)
        {
            var mapData = GetAllModuleControllerMap();
            var moduleData = _mapper.Map<List<ModuleControllerVM>>(_genericRepo.GetAll());

            foreach (var item in mapData)
            {
                if (item.UserId == userId)
                {
                    foreach (var value in moduleData.Where(m => m.ModuleID == item.ModuleID))
                    {
                        value.ModuleID = item.ModuleID;
                        value.MachineSequenceID = item.MachineSequenceID;
                        value.Run = item.Run;
                        value.LockedByUser = item.LockedByUser;
                        value.UserId = item.UserId;
                    }
                }
            }

            return moduleData;
        }

        [EnableQuery]
        [HttpGet("GetAllModuleControllerMap")]
        public IList<ModuleController_MapVM> GetAllModuleControllerMap()
        {
            return _mapper.Map<List<ModuleController_MapVM>>(_moduleControllerMapRepo.GetAll());
        }

        [HttpGet("GetModuleRecordsCount")]
        public int GetModuleRecordsCount()
        {
            return _genericRepo.GetRecordsCount();
        }

        [EnableQuery]
        [HttpGet("GetAllTestController/{userId}")]
        public IList<TestControllerVM> GetAllTestController(int userId)
        {
            var mapData = GetAllTestControllerMap();
            var testControllerData = _mapper.Map<List<TestControllerVM>>(_genericRepo2.GetAll());
            foreach (var item in mapData)
            {
                if (item.UserId == userId)
                {
                    foreach (var value in testControllerData.Where(m => m.TestCaseID == item.TestCaseID))
                    {
                        value.TestCaseID = item.TestCaseID;
                        value.TestScriptDescription = item.TestScriptDescription;
                        value.Run = item.Run;
                        value.JIRA_ID = item.JIRA_ID;
                        value.SequenceID = item.SequenceID;
                        value.LockedByUser = item.LockedByUser;
                        value.UserId = item.UserId;
                    }
                }
            }

            return testControllerData;
        }

        [EnableQuery]
        [HttpGet("GetAllTestControllerMap")]
        public IList<TestController_MapVM> GetAllTestControllerMap()
        {
            return _mapper.Map<List<TestController_MapVM>>(_testControllerMapRepo.GetAll());
        }

        [HttpGet("GetTestControllerRecordsCount")]
        public int GetTestControllerRecordsCount()
        {
            return _genericRepo2.GetRecordsCount();
        }


        [EnableQuery]
        [HttpGet("GetAllBrowserController")]
        public IList<BrowserControllerVM> GetAllBrowserVMExec()
        {
            return _mapper.Map<List<BrowserControllerVM>>(_genericRepo3.GetAll());
        }

        [HttpGet("GetBrowserRecordsCount")]
        public int GetBrowserRecordsCount()
        {
            return _genericRepo3.GetRecordsCount();
        }

        [HttpGet("GetModuleControllerById/{id}/{userId}")]
        public ModuleControllerVM GetController1ById(int id,int userId)
        {
            var data = _genericRepo.GetById(id);
            var result = _mapper.Map<ModuleControllerVM>(data);
            var mappedData = _mapper.Map<ModuleController_MapVM>(_testControllerRepo.GetMappedModuleData(userId, data));
            if (mappedData != null)
                result = _mapper.Map<ModuleControllerVM>(mappedData);
           
            return result;
        }

        //[HttpGet("GetModuleControllerMapById/{id}")]
        //public ModuleController_MapVM GetModuleControllerMapById(int id)
        //{
        //    return _mapper.Map<ModuleController_MapVM>(_moduleControllerMapRepo.GetById(id));
        //}

        [HttpGet("GetTestControllerById/{id}/{userId}")]
        public TestControllerVM GetController2ById(int id,int userId)
        {
            var data = _genericRepo2.GetById(id);
            var result = _mapper.Map<TestControllerVM>(data);
            var mappedData = _mapper.Map<TestController_MapVM>(_testControllerRepo.GetMappedTestControllerData(userId, data));
            if (mappedData != null)
                result = _mapper.Map<TestControllerVM>(mappedData);

            return result;
        }

        //[HttpGet("GetTestControllerMapById/{id}")]
        //public TestController_MapVM GetTestControllerMapById(int id)
        //{
        //    return _mapper.Map<TestController_MapVM>(_testControllerMapRepo.GetById(id));
        //}

        [HttpGet("GetBrowserControllerById/{id}")]
        public BrowserControllerVM GetController3ById(int id)
        {
            return _mapper.Map<BrowserControllerVM>(_genericRepo3.GetById(id));
        }

        [HttpPost("AddModuleController")]
        public void AddModuleController([FromBody]ModuleControllerVM controller1)
        {
            var data = _mapper.Map<ModuleController>(controller1);
            _testControllerRepo.CreateController1(data);
        }

        [HttpPost("AddTestController")]
        public void AddTestController([FromBody]TestControllerVM controller2)
        {
            var data = _mapper.Map<TestController>(controller2);
            _testControllerRepo.CreateController2(data);
        }

        [HttpPost("AddBrowserController")]
        public void AddBrowserController([FromBody]BrowserControllerVM controller3)
        {
            var data = _mapper.Map<BrowserVMExec>(controller3);
            _testControllerRepo.CreateController3(data);
        }

        [HttpPut("UpdateModuleController/{id}")]
        public void UpdateModuleController(int id, [FromBody]ModuleControllerVM testController1)
        {
            var data = _mapper.Map<ModuleController>(testController1);
            var mappedData = _testControllerRepo.GetMappedModuleData(testController1.UserId, data);

            if (mappedData != null)
            {
                var mappedUpdate = _mapper.Map<ModuleController_MapVM>(data);
                var mapData = _mapper.Map<ModuleController_Map>(mappedUpdate);
                _testControllerRepo.UpdateController1Map(testController1.UserId, mapData);
            }
            else
            {
                _testControllerRepo.CreateController1Map(testController1.UserId, data);
            }
        }

        [HttpPut("UpdateTestController/{id}")]
        public void UpdateTestController(int id, [FromBody]TestControllerVM testController2)
        {
            var data = _mapper.Map<TestController>(testController2);
            var mappedData = _testControllerRepo.GetMappedTestControllerData(testController2.UserId, data);

            if (mappedData != null)
            {
                var mappedUpdate = _mapper.Map<TestController_MapVM>(data);
                var mapData = _mapper.Map<TestController_Map>(mappedUpdate);
                _testControllerRepo.UpdateController2Map(testController2.UserId, mapData);
            }
            else
            {
                _testControllerRepo.CreateController2Map(testController2.UserId, data);
            }
        }

        [HttpPut("UpdateBrowserController/{id}")]
        public void UpdateBrowserController(int id, [FromBody]BrowserControllerVM testController3)
        {
            var data = _mapper.Map<BrowserVMExec>(testController3);
            _testControllerRepo.UpdateController3(id, data);
        }


        [HttpDelete("DeleteModuleController/{id}")]
        public void DeleteModuleController(int id, [FromBody]ModuleControllerVM testController1)
        {
            var data = _mapper.Map<ModuleController>(testController1);
            var mappedData = _testControllerRepo.GetMappedModuleData(testController1.UserId, data);
            if (mappedData == null)
            {
                _testControllerRepo.DeleteController1(id);
            }
            else
            {
                _testControllerRepo.DeleteController1Map(testController1.UserId, testController1.ModuleID);
            }
        }

        [HttpDelete("DeleteTestController/{id}")]
        public void DeleteTestController(int id, [FromBody]TestControllerVM testController2)
        {
            var data = _mapper.Map<TestController>(testController2);
            var mappedData = _testControllerRepo.GetMappedTestControllerData(testController2.UserId, data);

            if (mappedData == null)
            {
                _testControllerRepo.DeleteController2(id);
            }
            else
            {
                _testControllerRepo.DeleteController2Map(testController2.UserId, testController2.TestCaseID);
            }
        }

        [HttpDelete("DeleteBrowserController/{id}/{userId}")]
        public void DeleteBrowserController(int id,int userId)
        {
            _testControllerRepo.DeleteController3(id,userId);
        }
    }
}
