using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StockHistory.Data;
using StockHistory.Models;
using Util;

namespace StockHistory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly StockHistoryAPIContext _context;

        public LoginController(StockHistoryAPIContext context)
        {
            _context = context;
        }

        // POST: api/User/Login/35925713893/456@Oi
        /// <summary>
        /// Get a user to login.
        /// </summary>
        /// <param name="CPF"></param>
        /// <param name="Pass"></param>
        /// <returns>Return login validation</returns>
        /// <response code="200">Returns a list of Ticker</response>
        /// <response code="400">Invalid Password</response> 
        /// <response code="404">User not found</response> 
        /// <response code="500">Internal Error</response> 
        [AllowAnonymous]
        [HttpGet("{CPF}/{Pass}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LoginModel> LoginPost( decimal CPF, string Pass,
            [FromServices]SigningConfigurations signingConfigurations,
            [FromServices]TokenConfigurations tokenConfigurations)
        {

            User user = _context.User.Where(x => x.CPF.Equals(CPF)).FirstOrDefault();
            LoginModel loginModel = new LoginModel();

            if (user == null)
            {
                return NotFound("User not found with this CPF!");
            }
            try
            {
                var passCript = HashMd5Generator.Generate(Pass);

                if (user.Password != passCript)
                {
                    return BadRequest("Passoword is invalid!");
                }


                bool validCredentials = false;

                validCredentials = (user != null);

                if (validCredentials)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(user.CPF.ToString(), "Login"),
                        new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.CPF.ToString())
                        }
                    );

                    DateTime creationDate = DateTime.Now;
                    DateTime expirationDate = creationDate + TimeSpan.FromSeconds(tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();

                    var securityToken = handler.CreateToken(new SecurityTokenDescriptor
                    {
                        Issuer = tokenConfigurations.Issuer,
                        Audience = tokenConfigurations.Audience,
                        SigningCredentials = signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = creationDate,
                        Expires = expirationDate
                    });

                    var token = handler.WriteToken(securityToken);


                    loginModel.authenticated = true;
                    loginModel.created = creationDate.ToString("yyyy-MM-dd HH:mm:ss");
                    loginModel.expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss");
                    loginModel.accessToken = token;
                    loginModel.message = "OK";

                    return Ok(loginModel);
                }
                else
                {
                    loginModel.authenticated = false;
                    loginModel.message = "Failed to authenticate!";

                    return BadRequest(loginModel);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}