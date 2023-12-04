namespace RealStateCompanyApplication.Data
{
    /// <summary>
    /// Information used to validate if the user can be generated an access token to the Endpoints
    /// </summary>
    public class UserDto
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
