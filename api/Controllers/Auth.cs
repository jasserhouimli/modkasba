using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Auth : ControllerBase
    {
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            return Ok("Hello Auth");
        }
    }
}