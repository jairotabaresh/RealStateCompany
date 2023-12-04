using Moq;
using RealStateCompanyApplication.Controllers;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Model;
using RealStateCompanyApplication.Services.Contracts;

namespace RealStateCompanyTests
{
    /// <summary>
    /// Tests for the property controller implementation using mocked data
    /// </summary>
    public class PropertyControllerTests
    {
        private readonly Mock<IPropertyService> _property_service;
        private readonly PropertyController _propertyController;

        public PropertyControllerTests()
        {
            _property_service = new Mock<IPropertyService>();
            _propertyController = new PropertyController(_property_service.Object);
        }

        /// <summary>
        /// Test to obtain two mocked properties
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_Mocked_Properties()
        {
            List<Property> properties = new List<Property>()
            {
                new Property()
                {
                    Name = "Property 1",
                    Address = "Address 1",
                    Price = 123456,
                    CodeInternal = 1,
                    Year = 2012,
                    IdOwner = 1
                },
                new Property()
                {
                    Name = "Property 2",
                    Address = "Address 2",
                    Price = 999999,
                    CodeInternal = 2,
                    Year = 2013,
                    IdOwner = 2
                }
            };

            _property_service.Setup(obj => obj.GetProperties(null, null)).ReturnsAsync(properties);

            var result = await _propertyController.GetProperties(null, null);

            Assert.NotNull(result);
        }

        /// <summary>
        /// Test to create a new mocked property
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Create_Mocked_Property()
        {
            Property property = new Property()
            {
                IdProperty = 45,
                Name = "Property 1000",
                Address = "Address 19",
                Price = 1245465,
                CodeInternal = 701,
                Year = 2004,
                IdOwner = 1
            };

            Property mockedProperty = new Property()
            {
                IdProperty = 55,
                Name = "Property 1000",
                Address = "Address 19",
                Price = 1245465,
                CodeInternal = 701,
                Year = 2004,
                IdOwner = 1
            };


            _property_service.Setup(obj => obj.CreateProperty(It.IsAny<CreatePropertyDto>())).ReturnsAsync(mockedProperty);

            var result = await _propertyController.CreateProperty(It.IsAny<CreatePropertyDto>());

            Assert.NotNull(result);
        }

        /// <summary>
        /// Test to update a mocked property
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task UpdateMockedProperty()
        {
            Property property = new Property()
            {
                IdProperty = 12,
                Name = "Property 4",
                Address = "Address 17",
                Price = 1452000,
                CodeInternal = 125,
                Year = 1975,
                IdOwner = 1
            };

            _property_service.Setup(obj => obj.UpdateProperty(1,It.IsAny<UpdatePropertyDto>())).ReturnsAsync(property);

            var result = await _propertyController.UpdateProperty(1, It.IsAny<UpdatePropertyDto>());

            Assert.NotNull(result);
        }
    }
}
