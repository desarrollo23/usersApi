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
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserEngine _userEngine;

        public UserController(IUserEngine userEngine)
        {
            _userEngine = userEngine;
        }

        [HttpPost]
        public ActionResult<EntityResponse> Create([FromBody]UserEntityDTO userEntityDTO)
        {
           var response = _userEngine.Create(userEntityDTO);

            return response;
        }

        [HttpGet]
        public ActionResult<EntityResponse> Get()
        {
            var response = _userEngine.GetUsers();
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
