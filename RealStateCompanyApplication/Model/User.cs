using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealStateCompanyApplication.Model
{
    /// <summary>
    /// Mapping the database User table
    /// </summary>
    [Table("User")]
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
