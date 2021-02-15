using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dde.api.Helpers;
using dde.api.Models;
using dde.api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dde.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IUserService _userService;

        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }
    }
}