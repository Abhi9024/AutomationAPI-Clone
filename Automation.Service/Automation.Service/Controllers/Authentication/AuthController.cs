using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.DataAccessAbstractions;
using Automation.Service.ViewModel;
using AutoMapper;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Automation.Service.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthProvider _authProvider;
        private IMapper _mapper;
        private ILogger<AuthController> _logger;

        public AuthController(IAuthProvider authProvider, IMapper mapper, ILogger<AuthController> logger)
        {
            _authProvider = authProvider;
            _mapper = mapper;
            _logger = logger;
        }

        // POST api/values
        [HttpPost("Login")]
        public UserVM Login([FromBody]UserVM user)
        {
            var result = new UserVM();
            try
            {
                var userData = _authProvider.ValidateLogin(user.UserName, user.Password);
                result = _mapper.Map<UserVM>(userData);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }


        // POST api/values
        [HttpPost("CreateUser")]
        public string CreateUser([FromBody]UserVM user)
        {
            var result = string.Empty;
            try
            {
                _authProvider.CreateUser(user.UserName, user.Password, user.RoleId);
                result = @"User Created Succesfully!";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Source: {ex.Source}, StackTrace: {ex.StackTrace} ,  Message: {ex.Message}");
            }
            return result;
        }
    }
}
