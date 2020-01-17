using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.DataAccessAbstractions;
using Automation.Core;
using Microsoft.AspNetCore.DataProtection;
using AutoMapper;
using Automation.Service.ViewModel;
using Microsoft.AspNet.OData;

namespace Automation.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestScriptsController : ControllerBase
    {
        private IGenericRepo<TestScripts> _genericRepo;
        private ITestScriptsRepo _testScriptsRepo;
        private IMapper _mapper;

        public TestScriptsController(IGenericRepo<TestScripts> genericRepo, ITestScriptsRepo testScriptsRepo,IMapper mapper)
        {
            _genericRepo = genericRepo;
            _testScriptsRepo = testScriptsRepo;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("GetScripts")]
        public ActionResult<IList<TestScriptVM>> GetScripts()
        {
            var result = _mapper.Map<List<TestScriptVM>>(_genericRepo.GetAll().ToList());
            return result;
        }

        [HttpGet("GetScript/{id}/{userId}")]
        public TestScriptVM Get(int id,int userId)
        {
            var data = _genericRepo.GetById(id);
            data.IsLocked = true;
            data.LockedByUser = userId;
            _testScriptsRepo.UpdateLockedByFlags(data);
            return _mapper.Map<TestScriptVM>(_genericRepo.GetById(id));
        }

        [HttpPut("ResetLockedByField/{id}/{userId}")]
        public void ResetLockedByField(int id, int userId)
        {
            var data = _genericRepo.GetById(id);
            data.IsLocked = null;
            data.LockedByUser = null;
            _testScriptsRepo.UpdateLockedByFlags(data);
        }

        [HttpPost("AddScript")]
        public void Post([FromBody]TestScriptVM scripts)
        {
            var data = _mapper.Map<TestScripts>(scripts);
            _testScriptsRepo.CreateScript(data);
        }

        [HttpPut("UpdateScript/{id}")]
        public void Put(int id, [FromBody]TestScriptVM scripts)
        {
            var data = _mapper.Map<TestScripts>(scripts);
            _testScriptsRepo.UpdateScript(id, data);
        }

        [HttpPatch("UpdateScript/{id}")]
        public void Patch(int id, [FromBody]TestScriptVM scripts)
        {
            var data = _mapper.Map<TestScripts>(scripts);
            _testScriptsRepo.UpdateScript(id, data);
        }

        [HttpDelete("DeleteScript/{id}/{userId}")]
        public void Delete(int id,int userId)
        {
            _testScriptsRepo.DeleteScript(id,userId);
        }
    }
}
