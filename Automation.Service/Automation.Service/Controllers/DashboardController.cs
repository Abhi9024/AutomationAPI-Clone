﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.DataAccessAbstractions;
using Automation.Core;
using AutoMapper;
using Automation.Service.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Automation.Service.Controllers
{
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private IGenericRepo<UserTable> _genericRepo;
        private IDashboardRepo _dashboardRepo;
        private IMapper _mapper;

        public DashboardController(IGenericRepo<UserTable> genericRepo, IDashboardRepo dashboardRepo,IMapper mapper)
        {
            _genericRepo = genericRepo;
            _dashboardRepo = dashboardRepo;
            _mapper = mapper;
        }
        
        // GET: api/User/GetActiveUsers
        [HttpGet("GetActiveUsers")]
        public int GetActiveUsers()
        {
            return _genericRepo.GetRecordsCount();
        }

        [HttpGet("RecordsModifiedCount")]
        public int RecordsModifiedCount()
        {
            return _dashboardRepo.RecordsModified();
        }

        [HttpGet("GetLatestFeeds")]
        public string[] GetLatestFeeds()
        {
            var feeds = new List<string>();
            var users = _dashboardRepo.ModifiedFeeds();
            foreach (var user in users)
            {
                feeds.Add($"{user.ToUpper()} recently Modified/Added an entry.");
            }

            return feeds.ToArray();
        }

        [HttpGet("GetAllRoles")]
        public List<RoleVM> GetAllRoles()
        {
           var roles = _mapper.Map<List<RoleVM>>(_dashboardRepo.GetAllRoles());
           return roles;
        }
    }
}
