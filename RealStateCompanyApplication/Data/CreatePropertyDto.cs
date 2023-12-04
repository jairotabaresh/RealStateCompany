namespace RealStateCompanyApplication.Data
{
    /// <summary>
    /// Information used to create a brand new property
    /// </summary>
    public class CreatePropertyDto
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Price { get; set; }
        public int? CodeInternal { get; set; }
        public int? Year { get; set; }
        public int IdOwner { get; set; }
    }
}
