using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        private IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ??
                throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            //validating user
            bool user = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (!user)
            {
                return Unauthorized();
            }

            //creating token
            var securityKey = new SymmetricSecurityKey(
              Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();

            claimsForToken.Add(new Claim("user", authenticationRequestBody.UserName));
            claimsForToken.Add(new Claim("Application_name", "NovelNook"));
          


            var jwtSecurityToken = new JwtSecurityToken(
               _configuration["Authentication:Issuer"],
               _configuration["Authentication:Audience"],
               claimsForToken,
               DateTime.UtcNow,
               DateTime.UtcNow.AddHours(1),
               signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);

        }

        private bool ValidateUserCredentials(string? userName, string? password)
        {
            if(userName=="karpagam" && password =="karpagam")
            {
                return true;
            }
            return false;
        }

    }
}
