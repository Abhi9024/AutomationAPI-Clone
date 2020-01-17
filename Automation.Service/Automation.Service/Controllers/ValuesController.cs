using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core;
using Automation.Core.DataAccessAbstractions;

namespace Automation.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IGenericRepo<TestScripts> _testScriptsRepo;
        private IGenericRepo<ModuleController> _testTc1Repo;
        private IGenericRepo<TestController> _testTc2Repo;
        private IGenericRepo<BrowserVMExec> _testTc3Repo;
        private IGenericRepo<KeywordLibrary> _keywordRepo;
        private IGenericRepo<Repository> _repositoryRepo;

        public ValuesController(IGenericRepo<TestScripts> testScriptsRepo, 
            IGenericRepo<KeywordLibrary> keywordRepo,
            IGenericRepo<Repository> repositoryRepo,
            IGenericRepo<ModuleController> testTc1Repo,
            IGenericRepo<TestController> testTc2Repo,
            IGenericRepo<BrowserVMExec> testTc3Repo)
        {
            _testScriptsRepo = testScriptsRepo;
            _testTc1Repo = testTc1Repo;
            _testTc2Repo = testTc2Repo;
            _testTc3Repo = testTc3Repo;
            _keywordRepo = keywordRepo;
            _repositoryRepo = repositoryRepo;
        }
        // GET api/values
        [HttpGet]
        [Route("GetScripts")]
        public ActionResult<IList<TestScripts>> GetScripts()
        {
            var result = _testScriptsRepo.GetAll().ToList();
            return result;
        }

        [HttpGet]
        [Route("GetTC1")]
        public ActionResult<IList<ModuleController>> GetTC1()
        {
            var result = _testTc1Repo.GetAll().ToList();
            return result;
        }

        [HttpGet]
        [Route("GetTC2")]
        public ActionResult<IList<TestController>> GetTC2()
        {
            var result = _testTc2Repo.GetAll().ToList();
            return result;
        }

        [HttpGet]
        [Route("GetTC3")]
        public ActionResult<IList<BrowserVMExec>> GetTC3()
        {
            var result = _testTc3Repo.GetAll().ToList();
            return result;
        }

        [HttpGet]
        [Route("GetKeywords")]
        public ActionResult<IList<KeywordLibrary>> GetKeywords()
        {
            var result = _keywordRepo.GetAll().ToList();
            return result;
        }

        [HttpGet]
        [Route("GetRepository")]
        public ActionResult<IList<Repository>> GetRepository()
        {
            var result = _repositoryRepo.GetAll().ToList();
            return result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
