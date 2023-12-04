using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealStateCompanyApplication.Model
{
    /// <summary>
    /// Mapping the database Property table
    /// </summary>
    [Table("Property")]
    public class Property
    {
        [Key]
        public int IdProperty { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }
    }
}
