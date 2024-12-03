using A3System.Dbo;
using A3System.Dbo.Dto.Setor;
using A3System.Dbo.Model;
using A3System.Interface;
using A3System.Resources;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace A3System.Services
{
    public class SetorService : ISetorService
    {
        private AppDbContext _context;
        private IMapper _mapper;
        private IValidator<CreateSetorDto> _validatorCreate;
        private IValidator<UpdateSetorDto> _validatorUpdate;

        public SetorService(AppDbContext context, IMapper mapper, ITokenService tokenService, IValidator<CreateSetorDto> validatorCreate, IValidator<UpdateSetorDto> validatorUpdate)
        {
            _context = context;
            _mapper = mapper;
            _validatorCreate = validatorCreate;
            _validatorUpdate = validatorUpdate;
        }

        /// <summary>
        /// Create Setor, ele valida todos os campos se estão preenchidos corretamente antes de iniciar o processo
        /// </summary>
        /// <returns>Sucess</returns>
        public async Task CreateSetor(CreateSetorDto setor)
        {
            try
            {
                await _validatorCreate.ValidateAsync(setor, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRulesNotInRuleSet();
                });
                var mappedSetor = _mapper.Map<SetorModel>(setor);
                _context.Setor.Add(mappedSetor);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.SetorCreateFailed, e);
            }
        }

        /// <summary>
        /// Busca o Setor pelo ID do Setor
        /// </summary>
        /// <returns>ReadSetorDto</returns>
        public async Task<ReadSetorDto> GetSetorById(int id)
        {
            try
            {
                var setor = await _context.Setor.Where(x => x.Id == id).FirstOrDefaultAsync();
                if (setor == null) throw new Exception(string.Format(ErrorTranslation.SetorNotFoundID0, id));
                return _mapper.Map<ReadSetorDto>(setor);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Busca todos os Setores
        /// </summary>
        /// <returns>List<ReadSetorDto></returns>
        public async Task<List<ReadSetorDto>> GetAll()
        {
            try
            {
                var setorList = await _context.Setor.ToListAsync()!;
                if (!setorList.Any()) throw new Exception(ErrorTranslation.SetorNotFound);
                return _mapper.Map<List<ReadSetorDto>>(setorList);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        /// <summary>
        /// Atualiza o setor selecionado,
        /// Ele valida todos os campos no inicio para evitar o processamento com campos falhos
        /// Ele mantem a data do CreatedAt pois ela nao deve ser atualizada
        /// </summary>
        /// <returns>Sucess</returns>
        public async Task UpdateSetor(UpdateSetorDto setor)
        {
            try
            {
                await _validatorUpdate.ValidateAsync(setor, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRulesNotInRuleSet();
                });

                var setorDb = _context.Setor.AsNoTracking().Where(x => x.Id == setor.Id).FirstOrDefault();
                if (setorDb == null) throw new Exception(ErrorTranslation.InvalidSetor);
                setor.CreatedAt = setorDb.CreatedAt;
                setorDb = _mapper.Map<SetorModel>(setor);
                _context.Setor.Update(setorDb);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.SetorFailedUpdate, e);
            }
        }
    }
}
