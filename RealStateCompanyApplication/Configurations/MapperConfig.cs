using AutoMapper;
using RealStateCompanyApplication.Data;
using RealStateCompanyApplication.Model;

namespace RealStateCompanyApplication.Configurations
{
    /// <summary>
    /// Class used to control property mapping
    /// </summary>
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            /// Mapping between object CreatePropertyDto and object Property
            CreateMap<CreatePropertyDto, Property>();
        }
    }
}
