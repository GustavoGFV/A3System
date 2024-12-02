using A3System.Dbo.Dto.Setor;
using A3System.Dbo.Model;
using AutoMapper;

namespace A3System.Profiles
{
    public class SetorProfile : Profile
    {
        public SetorProfile()
        {
            CreateMap<CreateSetorDto, SetorModel>();
            CreateMap<SetorModel, UpdateSetorDto>();
            CreateMap<UpdateSetorDto, SetorModel>();
            CreateMap<SetorModel, ReadSetorDto>();
        }
    }
}
