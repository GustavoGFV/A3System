using A3System.Dbo.Dto.User;
using A3System.Dbo.Model;
using AutoMapper;

namespace A3System.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<ReadUserDto, UserModel>();
            CreateMap<UserModel, ReadUserDto>();

            CreateMap<CreateUserDto, UserModel>();
            CreateMap<UserModel, CreateUserDto>();

            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();

            CreateMap<UpdateUserDto, UserModel>();
            CreateMap<UserModel, UpdateUserDto>();
        }
    }
}
