using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Data;
using Microsoft.EntityFrameworkCore;
using DatingApp.API.Models;
using DatingApp.API.Dtos;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers 
{
    
     [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
     private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo ,IConfiguration config)
        {
          _repo= repo;
            _config = config;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody]UserForRegister userforregistration)
        {
            //validate request

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
          userforregistration.username=userforregistration.username.ToLower();
          if(await _repo.UserExist(userforregistration.username))
          return BadRequest("User Already Exist");

          var userToCreate= new User() {
              Username=userforregistration.username
          };
          var createdUser =await _repo.Register(userToCreate,userforregistration.password);

          return StatusCode(201);

        }


        [HttpPost("login")]

        public async Task<IActionResult> Login ([FromBody]UserForLogin userforlogin)
        {

            var userfromrepo = await _repo.Login(userforlogin.Username.ToLower(), userforlogin.Password);
            if (userfromrepo == null)
                return Unauthorized();

            var claims = new[]
            {
               new Claim(ClaimTypes.NameIdentifier,userfromrepo.Id.ToString()),
               new Claim(ClaimTypes.Name,userfromrepo.Username.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokendescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenhandler = new JwtSecurityTokenHandler();
            var token = tokenhandler.CreateToken(tokendescriptor);

            return Ok(new
            {

                token = tokenhandler.WriteToken(token)
            });

        }

    }
}