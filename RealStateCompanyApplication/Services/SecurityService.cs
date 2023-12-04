using Microsoft.EntityFrameworkCore;
using RealStateCompanyApplication.Context;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Services.Contracts;
using RealStateCompanyApplication.Utilities.Contracts;

namespace RealStateCompanyApplication.Services
{
    /// <summary>
    /// Service that implements the logic to generate an access token per user
    /// </summary>
    public class SecurityService: ISecurityService
    {
        private readonly RealStateContext _realStateContext;
        private readonly IJwtTokenUtility _jwtTokenUtility;

        public SecurityService(RealStateContext realStateContext, IJwtTokenUtility jwtTokenUtility)
        {
            _realStateContext = realStateContext;
            _jwtTokenUtility = jwtTokenUtility; 
        }

        /// <summary>
        /// Validates that the user requesting the access token exists and returns the token
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        public async Task<UserTokenDto?> GetJwtToken(UserDto userDto)
        {
            var user = await _realStateContext.Users.Where(x => x.UserName == userDto.UserName && x.Password == userDto.Password)
                                                    .FirstOrDefaultAsync();

            if (user != null) 
            {
                var token = _jwtTokenUtility.GetTokenForUser(userDto);
                var userToken = new UserTokenDto()
                {
                    UserName = userDto.UserName,
                    Password = userDto.Password,
                    AccessJwtToken = token
                };
                return userToken;
            }

            return null;
        }
    }
}
