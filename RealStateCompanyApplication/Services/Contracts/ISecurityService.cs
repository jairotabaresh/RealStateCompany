using RealStateCompanyApplication.Data;

namespace RealStateCompanyApplication.Services.Contracts
{
    /// <summary>
    /// Signatures of the methods used by SecurityService class
    /// </summary>
    public interface ISecurityService
    {
        public Task<UserTokenDto?> GetJwtToken(UserDto userDto);
    }
}
