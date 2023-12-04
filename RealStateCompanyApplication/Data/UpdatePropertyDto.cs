namespace RealStateCompanyApplication.Data
{
    /// <summary>
    /// Information used to upddate an existing property
    /// </summary>
    public class UpdatePropertyDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }
    }
}
