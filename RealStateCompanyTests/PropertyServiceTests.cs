using AutoMapper;
using RealStateCompanyApplication.Context;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Model;
using RealStateCompanyApplication.Services;

namespace RealStateCompanyTests
{
    /// <summary>
    /// Tests for the property service implementation using the real database
    /// </summary>
    public class PropertyServiceTests
    {
        private RealStateContext _context;
        private PropertyService _propertyService;
        private IMapper _mapper;

        public PropertyServiceTests()
        {
            _context = new RealStateContext(DatabaseConnectionTest.GetConnection());
            _propertyService = new PropertyService(_mapper, _context);
        }

        /// <summary>
        /// Test to obtain all the properties in the database
        /// </summary>
        [Fact]
        public async void Get_All_Properties()
        {
            LoadPropertyData(_context);
            var result = await _propertyService.GetProperties(null, null);

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Test to obtain all the properties in the database filtering by name
        /// </summary>
        [Fact]
        public async void Get_Properties_By_Name()
        {
            LoadPropertyData(_context);
            var result = await _propertyService.GetProperties(null, "Property 1");

            Assert.NotEmpty(result);
        }

        /// <summary>
        /// Test to obtain zero properties sending an exsiting Id
        /// </summary>
        [Fact]
        public async void Get_Zero_Properties()
        {
            LoadPropertyData(_context);
            var result = await _propertyService.GetProperties(2566, null);

            Assert.Equal(0, result.Count());
        }

        /// <summary>
        /// Test to create a new property
        /// </summary>
        [Fact]
        public async void Create_Property()
        {
                CreatePropertyDto property = new CreatePropertyDto
                {
                    Name = "Property 2500",
                    Address = "Address 2500",
                    Price = 666667,
                    CodeInternal = 69,
                    Year = 2009,
                    IdOwner = 1
                };

            var result = await _propertyService.CreateProperty(property);

            Assert.Equal(1, result.IdOwner);
        }

        /// <summary>
        /// Test to check that a property cannot be created by sending an existing IdOwner
        /// </summary>
        [Fact]
        public async void Create_Property_Fails()
        {
            CreatePropertyDto property = new CreatePropertyDto
            {
                Name = "Property 2500",
                Address = "Address 2500",
                Price = 666667,
                CodeInternal = 69,
                Year = 2009,
                IdOwner = 25
            };

            var result = await _propertyService.CreateProperty(property);

            Assert.Equal(null, result);
        }

        /// <summary>
        /// Test to check that a property cannot be updated by sending an existing IdOwner
        /// </summary>
        [Fact]
        public async void Update_Property_Fails()
        {
            UpdatePropertyDto property = new UpdatePropertyDto
            {
               
                Name = "Property 2600",
                Address = "Address 2600",
                Price = 456465,
                CodeInternal = 78,
                Year = 2019,
                IdOwner = 66
            };

            var result = await _propertyService.UpdateProperty(1, property);

            Assert.Equal(null, result);
        }

        /// <summary>
        /// Method to create two new properties in the database in order to check the Get_All_Properties test
        /// </summary>
        /// <param name="context"></param>
        private void LoadPropertyData(RealStateContext context)
        {
            context.Properties.Add(
                new Property
                {
                    Name = "Property 1",
                    Address = "Address 1",
                    Price = 123456,
                    CodeInternal = 1,
                    Year = 2012,
                    IdOwner = 1
                });

            context.Properties.Add(
                new Property
                {
                    Name = "Property 2",
                    Address = "Address 2",
                    Price = 999999,
                    CodeInternal = 2,
                    Year = 2013,
                    IdOwner = 2
                });

            context.SaveChanges();
        }
    }
}
