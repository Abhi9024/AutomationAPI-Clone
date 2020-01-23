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
        private IGenericRepo<KeywordLibrary_Map> _genericRepo2;
        private IKeywordEntityRepo _keywordRepo;
        private IMapper _mapper;

        public KeywordsController(IGenericRepo<KeywordLibrary> genericRepo,
            IGenericRepo<KeywordLibrary_Map> genericRepo2,
            IKeywordEntityRepo keywordRepo,
            IMapper mapper)
        {
            _genericRepo = genericRepo;
            _genericRepo2 = genericRepo2;
            _keywordRepo = keywordRepo;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("GetAllKeywords/{userId}")]
        public IList<KeywordEntityVM> GetAllKeywords(int userId)
        {
            var keywordsMapData = GetAllKeywordsMap();
            var mappedIds = keywordsMapData.Where(k => k.UserId == userId).Select(k => k.MasterKeywordID).ToList();
            var result = _mapper.Map<List<KeywordEntityVM>>(_keywordRepo.GetFilteredKeywords(mappedIds));
            var keywordsMappedList = _mapper.Map<List<KeywordEntityVM>>(keywordsMapData);
            result.AddRange(keywordsMappedList);

            return result;
        }

        [EnableQuery]
        [HttpGet("GetAllKeywordsMap")]
        public IList<KeywordEntity_MapVM> GetAllKeywordsMap()
        {
            return _mapper.Map<List<KeywordEntity_MapVM>>(_genericRepo2.GetAll());
        }

        [HttpGet("GetKeywordById/{id}/{userId}")]
        public KeywordEntityVM GetKeywordById(int id, int userId)
        {
            var result = _mapper.Map<KeywordEntityVM>(_genericRepo.GetById(id));
            var mappedData = _mapper.Map<KeywordEntity_MapVM>(_keywordRepo.GetMappedKeywordLibrary(id, userId));
            if (mappedData != null)
                result = _mapper.Map<KeywordEntityVM>(mappedData);

            return result;
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
                _keywordRepo.UpdateLockedByFlags(data);
            }
        }

        [HttpPost("AddKeyword")]
        public void AddKeyword([FromBody]KeywordEntityVM keyword)
        {
            var data = _mapper.Map<KeywordLibrary>(keyword);
            _keywordRepo.CreateKeyword(data);
        }

        [HttpPut("UpdateKeywordAdmin/{id}")]
        public void UpdateKeywordAdmin(int id, [FromBody]KeywordEntityVM keyword)
        {
            var data = _mapper.Map<KeywordLibrary>(keyword);
            _keywordRepo.UpdateKeyword(id, data);
        }


        [HttpPut("UpdateKeyword/{id}")]
        public void UpdateKeyword(int id, [FromBody]KeywordEntityVM keyword)
        {
            var mapVM = _mapper.Map<KeywordEntity_MapVM>(keyword);
            var keywordMapEntity = _mapper.Map<KeywordLibrary_Map>(mapVM);
            keywordMapEntity.MasterKeywordID = id;

            var mappedData = _keywordRepo.GetMappedKeywordLibrary(id, keyword.UserId);

            if (mappedData != null)
            {
                _keywordRepo.UpdateKeywordMap(keyword.UserId, keywordMapEntity);
            }
            else
            {
                _keywordRepo.CreateKeyword_Map(keywordMapEntity);
            }
        }

        [HttpDelete("DeleteKeyword/{id}/{userId}")]
        public void DeleteKeyword(int id, int userId)
        {
            var mappedData = _keywordRepo.GetMappedKeywordLibrary(id, userId);
            if (mappedData == null)
            {
                _keywordRepo.DeleteKeyword(id, userId);
            }
            else
            {
                _keywordRepo.DeleteKeywordMap(userId, id);
            }
        }

        [HttpGet("GetRecordsCount")]
        public int GetRecordsCount()
        {
            return _genericRepo.GetRecordsCount();
        }

        [HttpGet("GetAllFunctionNames")]
        public List<string> GetAllFunctionNames()
        {
            return _keywordRepo.GetAllFunctionNames();
        }
    }
}
