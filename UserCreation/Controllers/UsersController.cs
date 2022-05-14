using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserCreation.Data;
using UserCreation.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserCreation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private ApiDbContext _dbContext;

        public UsersController(ApiDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<IActionResult> Get(int? pageNumber, int? pageSize)
        {
            try
            {
                int currentPageNumber = pageNumber ?? 1;
                int currentPageSize = pageSize ?? 5;

                var users = await (from user in _dbContext.Users
                                   select new
                                   {
                                       Id = user.Id,
                                       firstName = user.firstName,
                                       lastName = user.lastName,
                                       dob = user.dob,
                                       city = user.dob,
                                       phone = user.phone,
                                       email = user.email,
                                       gender = user.gender,
                                   }
                            ).ToListAsync();
                return Ok(users.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex);
            }
            
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
                return NotFound("No record found against this ID.");
            else
                return Ok(user) ;
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest,ex);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
