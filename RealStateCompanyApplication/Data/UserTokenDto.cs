namespace RealStateCompanyApplication.Data
{
    /// <summary>
    /// Information that is returned when generating an access token for a specific user
    /// </summary>
    public class UserTokenDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? AccessJwtToken { get; set; }
    }
}
