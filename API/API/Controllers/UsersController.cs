using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using API.Repo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserDb _user;

        public UsersController(IUserDb user)
        {
            _user = user;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] Users model)
        {
            var user = await _user.Authenticate(model.Email, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Users>> GetUser()
        {
            var u = _user.GetUser();
            if (u != null)
                return Ok(u);
            else
                return NotFound();
        }

        [HttpGet("{id}")]
        public ActionResult<Users> GetbyId(int id)
        {
            var u = _user.GetById(id);
            if (u != null)
                return Ok(u);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult<Users> AddUser(Users users)
        {
            var u = _user.AddUser(users);
            if (u != null)
                return Ok(u);
            else
                return NotFound();
        }

        [HttpPut("{id}")]
        public ActionResult<Users> UpdateUser(int id, Users users)
        {
            var u = _user.GetById(id);
            if (u != null)
            {
                return Ok(_user.UpdateUser(id, users));
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var u = _user.GetById(id);
            if (u != null)
            {
                _user.DeleteUser(id);
                return Ok();
            }
            else
                return NotFound();
            
        }

    }
}
