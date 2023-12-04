using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Services.Contracts;

namespace RealStateCompanyApplication.Controllers
{
    /// <summary>
    /// Controller or entry point for application CRUD methods
    /// The messages returned by the Endpoints should not be handled here, but for a better understanding of the exercise I include them here
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        /// <summary>
        /// Determines whether a property could be created or not
        /// </summary>
        /// <param name="createPropertyDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateProperty")]
        public async Task<IActionResult> CreateProperty(CreatePropertyDto createPropertyDto)
        {
            var response = await _propertyService.CreateProperty(createPropertyDto);
            return response != null ? Ok(response) : NotFound("Owner not found");
        }

        /// <summary>
        /// Determines whether a property could be updated or not
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="updatePropertyDto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateProperty")]
        public async Task<IActionResult> UpdateProperty(int idProperty, UpdatePropertyDto updatePropertyDto)
        {
            var response = await _propertyService.UpdateProperty(idProperty, updatePropertyDto);
            return response != null ? Ok(response) : NotFound("Owner or Property not found");
        }

        /// <summary>
        /// Determines whether a property could be listed or not
        /// </summary>
        /// <param name="idOwner"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("GetProperties")]
        public async Task<IActionResult> GetProperties(int? idOwner, string? propertyName)
        {
            var response = await _propertyService.GetProperties(idOwner, propertyName);
            return response != null ? Ok(response) : NotFound("Property not found");
        }

        /// <summary>
        /// Determines if the price of a property can be changed
        /// </summary>
        /// <param name="idProperty"></param>
        /// <param name="propertyPrice"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("UpdatePropertyPrice")]
        public async Task<IActionResult> UpdatePropertyPrice(int idProperty, JsonPatchDocument propertyPrice)
        {
            var response = await _propertyService.UpdatePropertyPrice(idProperty, propertyPrice);
            return response != null ? Ok(response) : NotFound("Property not found");
        }

        /// <summary>
        /// Determines if an image can be added to a property
        /// </summary>
        /// <param name="file"></param>
        /// <param name="idProperty"></param>
        /// <param name="enabled"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddPropertyImage")]
        public async Task<IActionResult> AddPropertyImage(IFormFile file, int idProperty, bool enabled)
        {
            var response = await _propertyService.AddPropertyImage(file, idProperty, enabled);
            return response != null ? Ok(response) : NotFound("Property not found");
        }
    }
}