using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager) : BaseController
    {

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            var response = await serviceManager.AuthenticationService.RegisterAsync(request);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var response = await serviceManager.AuthenticationService.LoginAsync(request);
            return Ok(response);
        }
    }
}
