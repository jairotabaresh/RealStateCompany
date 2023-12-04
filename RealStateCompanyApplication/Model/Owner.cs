using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealStateCompanyApplication.Model
{
    /// <summary>
    /// Mapping the database Owner table
    /// </summary>
    [Table("Owner")]
    public class Owner
    {
        [Key]
        public int IdOwner { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public  DateTime? Birthday { get; set; }
    }
}
