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
    public class KeywordsController : ControllerBase
    {
        private IGenericRepo<KeywordLibrary> _genericRepo;
        private IKeywordEntityRepo _keywordRepo;
        private IMapper _mapper;

        public KeywordsController(IGenericRepo<KeywordLibrary> genericRepo, IKeywordEntityRepo keywordRepo,IMapper mapper)
        {
            _genericRepo = genericRepo;
            _keywordRepo = keywordRepo;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("GetAllKeywords")]
        public IList<KeywordEntityVM> GetAllKeywords()
        {
            return _mapper.Map<List<KeywordEntityVM>>(_genericRepo.GetAll());
        }

        [HttpGet("GetKeywordById/{id}/{userId}")]
        public KeywordEntityVM GetKeywordById(int id,int userId)
        {
            var data =_genericRepo.GetById(id);
            data.IsLocked = true;
            data.LockedByUser = userId;
            _keywordRepo.UpdateLockedByFlags(data);
            return _mapper.Map<KeywordEntityVM>(data);
        }

        [HttpPut("ResetLockedByField/{id}/{userId}")]
        public void ResetLockedByField(int id, int userId)
        {
            var data = _genericRepo.GetById(id);
            data.IsLocked = null;
            data.LockedByUser = null;
            _keywordRepo.UpdateLockedByFlags(data);
        }

        [HttpPost("AddKeyword")]
        public void AddKeyword([FromBody]KeywordEntityVM keyword)
        {
            var data = _mapper.Map<KeywordLibrary>(keyword);
            _keywordRepo.CreateKeyword(data);
        }

        [HttpPut("UpdateKeyword/{id}")]
        public void UpdateKeyword(int id, [FromBody]KeywordEntityVM keyword)
        {
           
            var data = _mapper.Map<KeywordLibrary>(keyword);
            

            _keywordRepo.UpdateKeyword(id, data);
        }

        [HttpDelete("DeleteKeyword/{id}/{userId}")]
        public void DeleteKeyword(int id,int userId)
        {
            _keywordRepo.DeleteKeyword(id, userId);
        }
    }
}
