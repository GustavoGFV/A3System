using A3System.Dbo.Dto.User;

namespace A3System.Interface
{
    public interface IUsuarioService
    {
        Task<ReadUserDto> GetUsuario(int id);
        Task<List<ReadUserDto>> GetUsuarios();
        Task UpdateUser(UpdateUserDto usuarioDto);
        Task UpdatePassword(UpdateUserDto usuarioDto);
    }
}
