using AutoMapper;
using Urly.Domain;
using Urly.Dto;

namespace Urly.WebApi
{
    public class DtoMappingProfile : Profile
    {
        public DtoMappingProfile()
        {
            CreateMap<Link, LinkDto>()
                .ForMember(x => x.ShortCode, op => op.Ignore());
        }
    }
}
