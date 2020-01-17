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
    public class RepositoryController : Controller
    {
        private IGenericRepo<Repository> _genericRepo;
        private IRepositoryEntityRepo _repositoryEntityRepo;
        private IMapper _mapper;

        public RepositoryController(IGenericRepo<Repository> genericRepo, IRepositoryEntityRepo repositoryEntityRepo,IMapper mapper)
        {
            _genericRepo = genericRepo;
            _repositoryEntityRepo = repositoryEntityRepo;
            _mapper = mapper;
        }

        [EnableQuery]
        [HttpGet("GetAllRepository")]
        public IList<RepositoryEntityVM> GetAllRepository()
        {
            var data = _mapper.Map<List<RepositoryEntityVM>>(_genericRepo.GetAll());
            return data;
        }

        [HttpGet("GetRepositoryById/{id}/{userId}")]
        public RepositoryEntityVM GetRepositoryById(int id, int userId)
        {
            var data = _genericRepo.GetById(id);
            data.IsLocked = true;
            data.LockedByUser = userId;
            _repositoryEntityRepo.UpdateLockedByFlags(data);
            return _mapper.Map<RepositoryEntityVM>(data);
        }

        [HttpPut("ResetLockedByField/{id}/{userId}")]
        public void ResetLockedByField(int id, int userId)
        {
            var data = _genericRepo.GetById(id);
            data.IsLocked = null;
            data.LockedByUser = null;
            _repositoryEntityRepo.UpdateLockedByFlags(data);
        }

        [HttpPost("AddRepository")]
        public void AddRepository([FromBody]RepositoryEntityVM repository)
        {
            var data = _mapper.Map<Repository>(repository);
            _repositoryEntityRepo.CreateRepository(data);
        }

        [HttpPut("UpdateRepository/{id}")]
        public void UpdateRepository(int id, [FromBody]RepositoryEntityVM repository)
        {
            var data = _mapper.Map<Repository>(repository);
            _repositoryEntityRepo.UpdateRepository(id, data);
        }

        [HttpDelete("DeleteRepository/{id}/{userId}")]
        public void DeleteRepository(int id,int userId)
        {
            _repositoryEntityRepo.DeleteRepository(id,userId);
        }
    }
}
