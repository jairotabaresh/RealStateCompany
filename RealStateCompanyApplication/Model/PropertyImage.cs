using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealStateCompanyApplication.Model
{
    /// <summary>
    /// Mapping the database PropertyImage table
    /// </summary>
    [Table("PropertyImage")]
    public class PropertyImage
    {
        [Key]
        public int IdPropertyImage { get; set; }
        public byte[]? FileImage { get; set; }
        public bool? Enabled { get; set; }
        public int IdProperty { get; set; }
    }
}
