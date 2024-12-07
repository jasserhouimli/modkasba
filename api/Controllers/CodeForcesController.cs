using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http;
using api.Models;
using System.Net.Http;
using System.Text.Json;
namespace api.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class CodeForcesController : ControllerBase
    {

        private readonly HttpClient _client;

        public CodeForcesController(HttpClient client)
        {
            _client = client;
        }

        [HttpGet("{handle}")]
        public async Task<ActionResult<ApiResponse>> Get(string handle)
        {
            var response = await _client.GetStreamAsync($"https://codeforces.com/api/user.status?handle={handle}");
            var result = await JsonSerializer.DeserializeAsync<ApiResponse>(response);
            return Ok(result);
        }


    }
}