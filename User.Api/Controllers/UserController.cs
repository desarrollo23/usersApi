using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Common.Response;
using User.Model.DTOs;
using User.Model.Interfaces.Engine;

namespace User.Api.Controllers
{
    /// <summary>
    /// Controller to manage users
    /// </summary>
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserEngine _userEngine;

        public UserController(IUserEngine userEngine)
        {
            _userEngine = userEngine;
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="userEntityDTO">User request</param>
        /// <returns>Response of type EntityResponse</returns>
        [HttpPost]
        public ActionResult<EntityResponse> Create([FromBody]UserEntityDTO userEntityDTO)
        {
           var response = _userEngine.Create(userEntityDTO);

            return response;
        }

        [HttpGet("list/{pageNumber}")]
        public ActionResult<EntityResponse> Get(int pageNumber = 1)
        {
            var response = _userEngine.GetUsers(pageNumber);
            return response;
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<EntityResponse> GetById(int id)
        {
            var response = _userEngine.GetUserById(id);
            return response;
        }

        [HttpPut("{id}")]
        public ActionResult<EntityResponse> Update([FromBody] UserEntityDTO userEntityDTO, int id)
        {
            var response = _userEngine.UpdateUser(userEntityDTO, id);
            return response;
        }
    }
}
