using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Automation.Core.DataAccessAbstractions;
using Automation.Service.ViewModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Automation.Service.Controllers.Authentication
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthProvider _authProvider;

        public AuthController(IAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }
        
        // POST api/values
        [HttpPost("Login")]
        public bool Login([FromBody]UserVM user)
        {
            var isSuccess = _authProvider.ValidateLogin(user.UserName,user.Password);
            return isSuccess;
        }

        // POST api/values
        [HttpPost("CreateUser")]
        public string CreateUser([FromBody]UserVM user)
        {
            _authProvider.CreateUser(user.UserName, user.Password);
            return @"User Created Succesfully!";
        }
        
    }
}
