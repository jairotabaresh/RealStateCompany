using Microsoft.IdentityModel.Tokens;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Utilities.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RealStateCompanyApplication.Utilities
{
    /// <summary>
    /// Utility that implements the logic to obtain an access token
    /// </summary>
    public class JwtTokenUtility: IJwtTokenUtility
    {
        private readonly IConfiguration _configuration;

        public JwtTokenUtility(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generate the access token for the required user using the Jwt Token approach
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public string GetTokenForUser(UserDto userDto)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserName", userDto.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signIn
                );

            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);

            return generatedToken;
        }
    }
}
