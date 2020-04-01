using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockHistory.Data;
using StockHistory.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Util;
using EntityState = Microsoft.EntityFrameworkCore.EntityState;

namespace StockHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StockHistoryAPIContext _context;

        public UserController(StockHistoryAPIContext context)
        {
            _context = context;
        }       

        // POST: api/User
        /// <summary>
        /// User register.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /purchasesHistoric
        ///     {
        ///       "Name": 'Diego',
        ///       "CPF": "35925713893",
        ///       "Password": "123qwe!@"
        ///     }
        ///
        /// </remarks>
        /// <param name="userData"></param>
        /// <returns>A User as created</returns>
        /// <response code="200">Returns the User created item</response>
        /// <response code="400">This CPF as been registered</response>
        /// <response code="500">Server Error</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> PostUser(User userData)
        {
            try
            {
                userData.Password = HashMd5Generator.Generate(userData.Password);

                if (UserExistCPF(userData.CPF))
                {
                    return BadRequest("This CPF as been registered!");
                }

                _context.User.Add(userData);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.InnerException);
            }

            return Ok(userData.idUser);
        }

        // PUT: api/User/1
        /// <summary>
        /// Update a password an specific register.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /purchasesHistoric
        ///     {
        ///       "idUser" : 1
        ///       "Password": "123qwe!@"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="userPass"></param>
        /// <response code="204">Returns the newly created item</response>
        /// <response code="400">User not fond</response> 
        /// <response code="401">Unauthorized</response> 
        /// <response code="500">Server error</response> 
        [Authorize("Bearer")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutUser(int id, UserPassword userPass)
        {
            if (id != userPass.idUser)
            {
                return BadRequest("User changed is not the same as the user logged in!");
            }

            userPass.Password = HashMd5Generator.Generate(userPass.Password);

            var user = new User() { idUser = userPass.idUser, Password = userPass.Password };

            _context.Entry(user).Property(x => x.Password).IsModified = true;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UserExists(id))
                {
                    return NotFound("User not found!");
                }
                else
                {
                    return StatusCode(500, ex.Message);
                }
            }

            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        private bool UserExists(decimal id)
        {
            return _context.User.Any(e => e.idUser == id);
        }

        private bool UserExistCPF(decimal CPF)
        {
            return _context.User.Any(e => e.CPF == CPF);
        }
    }
}
