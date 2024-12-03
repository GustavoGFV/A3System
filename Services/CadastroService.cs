using A3System.Dbo;
using A3System.Dbo.Dto.User;
using A3System.Dbo.Model;
using A3System.Interface;
using A3System.Resources;
using A3System.Utils.ValidatorHasher;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace UsuariosAPI.Services
{
    public class CadastroService : IRegisterService
    {
        private IMapper _mapper;
        private AppDbContext _context;
        private IValidator<CreateUserDto> _validator;

        public CadastroService(IMapper mapper, AppDbContext context, IValidator<CreateUserDto> validator)
        {
            _mapper = mapper;
            _context = context;
            _validator = validator;
        }

        /// <summary>
        /// Cadastrar Usuario
        /// Ele valida todos os campos no inicio para evitar o processamento com campos falhos
        /// </summary>
        /// <returns>Sucess</returns>
        public async Task CadastrarUsuario(CreateUserDto createDto)
        {
            try
            {
                await _validator.ValidateAsync(createDto, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRuleSets("Senha").IncludeRulesNotInRuleSet();
                });

                var usuarioIndentity = await _context.Users.Where(x => x.Email == createDto.Email!).FirstOrDefaultAsync();
                if (usuarioIndentity != null) throw new Exception(ErrorTranslation.UserExists);

                var createDtoOrigin = _mapper.Map<CreateUserDto>(createDto);


                createDtoOrigin.Password = HashPassword.GerarHash(createDto.Password!);
                UserModel usuario = _mapper.Map<UserModel>(createDtoOrigin);

                _context.Add(usuario);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.UserCreateFailed, e);
            }
        }
    }
}
