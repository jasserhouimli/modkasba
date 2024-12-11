using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly dbContext _context;

        public UserController(dbContext context)
        {
            _context = context;
        }


        [HttpGet]

        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.users.ToListAsync();
            return Ok(users);
        }


        [HttpPost]

        public async Task<IActionResult> AddUser(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }


    }
}
