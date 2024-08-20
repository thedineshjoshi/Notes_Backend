using Data.ApplicationDbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Notes.Common;
using Notes.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly ApplicationDbContext db;
        public readonly IConfiguration _config;

        public AuthenticationController(ApplicationDbContext _db, IConfiguration config)
        {
            this.db = _db;
            _config = config;
        }
        // GET: api/<AuthenticationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthenticationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AuthenticationController>
        [Route("Login")]
        [HttpPost]
        public IActionResult Login([FromBody] SignIn LoginRequest)
        {
            var userCredentials = db.Users.FirstOrDefault(u => u.Username == LoginRequest.Username);
            var Username = userCredentials.Username;
            var Password = CommonMethods.ConvertToDecrypt(userCredentials.PasswordHash);

            if (LoginRequest.Username == Username && LoginRequest.Password == Password)
            {
                var userId = userCredentials.Id;
                var authClaims = new List<Claim>
                 {
                     new Claim("UserName",Username),
                     new Claim("UserId",userId.ToString()),
                 };
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));
                var token = new JwtSecurityToken(
                    issuer: _config["Jwt:ValidIssuer"],

                    audience: _config["Jwt:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        // PUT api/<AuthenticationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthenticationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
