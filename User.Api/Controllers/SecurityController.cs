using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Common.Response.Security;
using User.Model.Interfaces.Engine;
using User.Model.Requests;

namespace User.Api.Controllers
{
    [Route("api/security")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityEngine _securityEngine;

        public SecurityController(ISecurityEngine securityEngine)
        {
            _securityEngine = securityEngine;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(UserCredentials userCredentials)
        {
            var response = await _securityEngine.RegisterUser(userCredentials);

            if (string.IsNullOrEmpty(response.ErrorMessage))
                return response;

            return BadRequest(response.ErrorMessage);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login(UserCredentials userCredentials)
        {
            var response = await _securityEngine.Login(userCredentials);

            if (string.IsNullOrEmpty(response.ErrorMessage))
                return response;

            return BadRequest(response.ErrorMessage);
        }
    }
}
