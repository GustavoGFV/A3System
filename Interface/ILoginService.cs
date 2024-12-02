using A3System.Dbo.Dto.Login;
using A3System.Dbo.Dto.User;

namespace A3System.Interface
{
    public interface ILoginService
    {
        Task<UserDto> LoginAsync(LoginDto request);
    }
}
