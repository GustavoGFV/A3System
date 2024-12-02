using A3System.Dbo.Dto.User;

namespace A3System.Interface
{
    public interface IRegisterService
    {
        Task CadastrarUsuario(CreateUserDto request);
    }
}
