using Microsoft.AspNetCore.JsonPatch;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Model;

namespace RealStateCompanyApplication.Services.Contracts
{
    /// <summary>
    /// Signatures of the methods used by PropertyService class
    /// </summary>
    public interface IPropertyService
    {
        public Task<Property?> CreateProperty(CreatePropertyDto createPropertyDto);
        public Task<Property?> UpdateProperty(int idProperty, UpdatePropertyDto updatePropertyDto);
        public Task<IEnumerable<Property>?> GetProperties(int? idOwner, string? propertyName);
        public Task<Property?> UpdatePropertyPrice(int idProperty, JsonPatchDocument propertyPrice);
        public Task<PropertyImage?> AddPropertyImage(IFormFile file, int idProperty, bool enabled);
    }
}
