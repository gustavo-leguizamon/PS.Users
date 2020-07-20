using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PS.Users.Domain.Interfaces.Services;
using PS.Users.Domain.Models;
using System;

namespace PS.Users.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = _service.Create(model);
                return new JsonResult(user) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            try
            {
                var user = _service.Authenticate(model.Username, model.Password);
                var login = _service.GenerateToken(user);
                return Ok(login);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet()]
        public IActionResult get()
        {
            var us = User.Identity;
            return Ok();
        }
    }
}
