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

        public TestDataController(IGenericRepo<TestData> genericRepo, ITestDataRepo testDataRepo, IMapper mapper)
        {
            _genericRepo = genericRepo;
            _testDataRepo = testDataRepo;
            _mapper = mapper;
        }
        // GET: api/values
        [EnableQuery]
        [HttpGet("GetTestAllData")]
        public ActionResult<IList<TestDataVM>> Get()
        {
            return _mapper.Map<List<TestDataVM>>(_genericRepo.GetAll().ToList());
        }

        // GET api/values/5
        [HttpGet("GetTestData/{id}/{userId}")]
        public TestDataVM Get(int id, int userId)
        {
            var data = _genericRepo.GetById(id);
            if (data != null)
            {
                data.IsLocked = true;
                data.LockedByUser = userId;
                data.UserId = userId;
                _testDataRepo.UpdateLockedByFlags(data);
            }
            return _mapper.Map<TestDataVM>(data);
        }

        [HttpPut("ResetLockedByField/{id}/{userId}")]
        public void ResetLockedByField(int id, int userId)
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

        // POST api/values
        [HttpPost("AddTestData")]
        public void Post([FromBody]TestDataVM testData)
        {
            var data = _mapper.Map<TestData>(testData);
            _testDataRepo.CreateTestData(data);
        }

        // PUT api/values/5
        [HttpPut("UpdateTestData/{id}")]
        public void Put(int id, [FromBody]TestDataVM testData)
        {
            var data = _mapper.Map<TestData>(testData);
            _testDataRepo.UpdateTestData(id, data);
        }

        // DELETE api/values/5
        [HttpDelete("DeleteTestData/{id}/{userId}")]
        public void Delete(int id, int userId)
        {
            _testDataRepo.DeleteTestData(id, userId);
        }

        [HttpGet("GetRecordsCount")]
        public int GetRecordsCount()
        {
            return _genericRepo.GetRecordsCount();
        }
    }
}
