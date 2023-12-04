namespace RealStateCompanyApplication.Data
{
    /// <summary>
    /// Information used to add an image to the property
    /// </summary>
    public class AddPropertyImageDto
    {
        public bool? Enabled { get; set; }
        public int IdProperty { get; set; }
    }
}
