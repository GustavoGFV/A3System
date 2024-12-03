using A3System.Dbo;
using A3System.Dbo.Dto.Login;
using A3System.Dbo.Dto.User;
using A3System.Interface;
using A3System.Utils.ValidatorHasher;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace A3System.Services
{
    public class LoginService : ILoginService
    {
        private ITokenService _tokenService;
        private AppDbContext _context;
        private IMapper _mapper;

        public LoginService(ITokenService tokenService, AppDbContext context, IMapper mapper)
        {
            _tokenService = tokenService;
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>UserDto</returns>
        public async Task<UserDto> LoginAsync(LoginDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                {
                    throw new Exception("Invalid Information");
                }
                var users = await _context.Users.Where(u => u.Email == request.Email && u.Password == HashPassword.GerarHash(request.Password)).FirstOrDefaultAsync();
                if (users != null)
                {
                    var user = _mapper.Map<UserDto>(users);
                    var token = _tokenService.GenerateToken(request.Email, users.Role);
                    user.Token = token;
                    return user;
                }

                throw new Exception("No User Found");
            }
            catch (Exception e)
            {
                throw new Exception("Login Failed", e);
            }
        }
    }
}