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

        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IConfiguration configuration, ILogger<AuthenticationController> logger)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        /// <summary>
        ///Validates user credentials and returns a JWT token if valid.
        /// </summary>
        /// <param name="authenticationRequestBody">The user's login details</param>
        /// <returns>A JWT token if credentials are valid; otherwise, Unauthorized.</returns>
        [HttpPost("Authenticate")]
        public ActionResult<string> Authenticate(AuthenticationRequestBody authenticationRequestBody)
        {
            // Log the start of the authentication process
            _logger.LogInformation("Starting authentication for user: {UserName}", authenticationRequestBody.UserName);

            // Validate user credentials
            bool isValidUser = ValidateUserCredentials(authenticationRequestBody.UserName, authenticationRequestBody.Password);

            if (!isValidUser)
            {
                _logger.LogWarning("Authentication failed for user: {UserName}. Invalid credentials provided.", authenticationRequestBody.UserName);
                return Unauthorized("Invalid username or password.");
            }

            // Log the creation of the JWT token
            _logger.LogInformation("Creating JWT token for user: {UserName}", authenticationRequestBody.UserName);

            // Create token
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("user", authenticationRequestBody.UserName),
                new Claim("Application_name", "NovelNook")
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                expires: DateTime.UtcNow.AddHours(4),
                signingCredentials: signingCredentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            _logger.LogInformation("Successfully created JWT token for user: {UserName}", authenticationRequestBody.UserName);

            return Ok(new { Token = tokenToReturn, Message = "Authentication successful" });
        }

        private bool ValidateUserCredentials(string? userName, string? password)
        {
            // Log the credentials validation process
            _logger.LogInformation("Validating credentials for user: {UserName}", userName);

            if (userName == "karpagam" && password == "karpagam")
            {
                _logger.LogInformation("Credentials validated successfully for user: {UserName}", userName);
                return true;
            }

            _logger.LogWarning("Failed to validate credentials for user: {UserName}", userName);
            return false;
        }
    }
}
