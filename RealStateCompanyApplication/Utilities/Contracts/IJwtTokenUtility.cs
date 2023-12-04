using RealStateCompanyApplication.Data;

namespace RealStateCompanyApplication.Utilities.Contracts
{
    /// <summary>
    /// Signatures of the methods used by JwtTokenUtility class
    /// </summary>
    public interface IJwtTokenUtility
    {
        public string GetTokenForUser(UserDto userDto);
    }
}
