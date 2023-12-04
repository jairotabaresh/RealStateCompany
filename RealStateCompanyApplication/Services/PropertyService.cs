using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RealStateCompanyApplication.Context;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Model;
using RealStateCompanyApplication.Services.Contracts;

namespace RealStateCompanyApplication.Services
{
    /// <summary>
    /// Core service that implements the logic for application CRUDS
    /// </summary>
    public class PropertyService: IPropertyService
    {
        private readonly IMapper _mapper;
        private readonly RealStateContext _realStateContext;

        public PropertyService(IMapper mapper, RealStateContext realStateContext)
        {
            _mapper = mapper;
            _realStateContext = realStateContext;
        }

        /// <summary>
        /// Implementation that creates a new property
        /// </summary>
        /// <param name="createPropertyDto"></param>
        /// <returns></returns>
        public async Task<Property?> CreateProperty(CreatePropertyDto createPropertyDto)
        {
            var owner = await _realStateContext.Owners.FindAsync(createPropertyDto.IdOwner);

            if (owner != null) 
            {
                //This serves to directly map the createPropertyDto property to the Property type, avoiding defining field by field,
                //but I disabled it due to problems with unit tests
                //var property = _mapper.Map<Property>(createPropertyDto);
                var property = new Property();
                property.Name = createPropertyDto.Name;
                property.Address = createPropertyDto.Address;
                property.Price = createPropertyDto.Price;
                property.CodeInternal = createPropertyDto.CodeInternal;
                property.Year = createPropertyDto.Year;
                property.IdOwner = createPropertyDto.IdOwner;
                await _realStateContext.Properties.AddAsync(property);
                await _realStateContext.SaveChangesAsync();
                return property;
            }

            return null;
        }

        /// <summary>
        /// Implementation that updates a property if the Owner exists
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="updatePropertyDto"></param>
        /// <returns></returns>
        public async Task<Property?> UpdateProperty(int idProperty, UpdatePropertyDto updatePropertyDto)
        {
            var property = await _realStateContext.Properties.FindAsync(idProperty);
            var owner = await _realStateContext.Owners.FindAsync(updatePropertyDto.IdOwner);

            if (property != null && owner != null)
            {
                property.Name = updatePropertyDto.Name;
                property.Address = updatePropertyDto.Address;
                property.Price = updatePropertyDto.Price;
                property.CodeInternal = updatePropertyDto.CodeInternal;
                property.Year = updatePropertyDto.Year;
                property.IdOwner = updatePropertyDto.IdOwner;
                await _realStateContext.SaveChangesAsync();
                return property;
            }

            return null;
        }

        /// <summary>
        /// Implementation that lists properties depending on two filters: IdOwner and PropertyName
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Property>?> GetProperties(int? idOwner, string? propertyName)
        {
            var properties = await _realStateContext.Properties.ToListAsync();

            if (idOwner != null)
            {
                properties = properties.Where(x => x.IdOwner == idOwner).ToList();
            }

            if (propertyName != null)
            {
                properties = properties.Where(x => x.Name.Contains(propertyName)).ToList();
            }

            if (properties != null)
            {
                return properties;
            }

            return null;
        }

        /// <summary>
        /// Implementation that updates the property price, but since it is implemented using the PATCH verb and JsonPatchDocument 
        /// you can actually update only the required field, sending the request like this: [
        ///{
        ///"path": "Price",
        ///"op": "Replace",
        ///"value": 250000
        ///}
        ///]
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="propertyPrice"></param>
        /// <returns></returns>
        public async Task<Property?> UpdatePropertyPrice(int idProperty, JsonPatchDocument propertyPrice)
        {
            var property = await _realStateContext.Properties.FindAsync(idProperty);
            
            if (property != null)
            {
                propertyPrice.ApplyTo(property);
                await _realStateContext.SaveChangesAsync();
                return property;
            }

            return null;
        }

        /// <summary>
        /// Implementation that adds an image of the property depending on the file chosen
        /// </summary>
        /// <param name="file"></param>
        /// <param name="idProperty"></param>
        /// <param name="enabledo"></param>
        /// <returns></returns>
        public async Task<PropertyImage?> AddPropertyImage(IFormFile file, int idProperty, bool enabledo)
        {
            var property = await _realStateContext.Properties.FindAsync(idProperty);

            if (property != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var propertyImage = new PropertyImage()
                    {
                        FileImage = stream.ToArray(),
                        Enabled = true,
                        IdProperty = idProperty

                    };
                    await _realStateContext.PropertyImages.AddAsync(propertyImage);
                    await _realStateContext.SaveChangesAsync();
                    return propertyImage;
                }
            }
           
            return null;
        }

    }
}
