using A3System.Dbo;
using A3System.Dbo.Dto.User;
using A3System.Dbo.Model;
using A3System.Interface;
using A3System.Resources;
using A3System.Utils.ValidatorHasher;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace A3System.Services
{
    public class UsuarioService : IUsuarioService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IValidator<UpdateUserDto> _validatorUpdate;

        public UsuarioService(IMapper mapper, AppDbContext context, IValidator<UpdateUserDto> validatorUpdate)
        {
            _mapper = mapper;
            _context = context;
            _validatorUpdate = validatorUpdate;
        }

        #region Retornos de Usuario

        /// <summary>
        /// Seleção de usuario por ID
        /// </summary>
        /// <returns>ReadUserDto</returns>
        public async Task<ReadUserDto> GetUsuario(int id)
        {
            try
            {
                UserModel? usuario = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (usuario != null) return _mapper.Map<ReadUserDto>(usuario);
                throw new Exception(ErrorTranslation.UserNotFound);
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.UserNotFound, e);
            }
        }

        /// <summary>
        /// Seleção de multiplos usuarios
        /// </summary>
        /// <returns>List<ReadUserDto></returns>
        public async Task<List<ReadUserDto>> GetUsuarios()
        {
            try
            {
                var usuarios = new List<UserModel>();
                var mappedUsers = new List<ReadUserDto>();

                using (var context = _context)
                {
                    usuarios = context.Users.ToList();
                    if (usuarios.Count == 0) throw new Exception(ErrorTranslation.UserNotFound);

                    mappedUsers = _mapper.Map<List<ReadUserDto>>(usuarios);
                }
                return mappedUsers;
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.UserNotFound, e);
            }
        }
        #endregion

        #region Endpoints de Atualizar Usuario

        /// <summary>
        /// Atualização da senha do usuario
        /// No validator temos a validação expecifica para a senha assim evitando que tente validar todas as informações
        /// neste caso desnecessarias
        /// </summary>
        public async Task UpdatePassword(UpdateUserDto usuarioDto)
        {
            try
            {
                await _validatorUpdate.ValidateAsync(usuarioDto, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRuleSets("Senhas");
                });

                var info = await _context.Users.AsNoTracking().Where(u => u.Id == usuarioDto.Id).FirstOrDefaultAsync();
                if (info == null) throw new Exception(ErrorTranslation.UserNotFound);
                info.Password = HashPassword.GerarHash(usuarioDto.Password!);

                _context.Users.Update(info);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.UserUpdateFailed, e);
            }
        }

        /// <summary>
        /// Atualização do usuario
        /// No validator temos a validação de todos os campos para verificar se nenhum deles foi excluido acidentalmente
        /// a validação de senhas não esta inclusa pois neste caso é desnecessaria
        /// </summary>
        public async Task UpdateUser(UpdateUserDto usuarioDto)
        {
            try
            {
                await _validatorUpdate.ValidateAsync(usuarioDto, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRulesNotInRuleSet();
                });

                var info = await _context.Users.AsNoTracking().Where(u => u.Id == usuarioDto.Id).FirstOrDefaultAsync();
                if (info == null) throw new Exception(ErrorTranslation.UserNotFound);
                usuarioDto.Password = info.Password;
                info = _mapper.Map<UserModel>(usuarioDto);

                _context.Users.Update(info);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.UserUpdateFailed, e);
            }
        }
        #endregion
    }
}
