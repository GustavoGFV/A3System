using A3System.Dbo;
using A3System.Dbo.Dto.Setor;
using A3System.Dbo.Dto.User;
using A3System.Dbo.Model;
using A3System.Interface;
using A3System.Resources;
using A3System.Validation.UserValidations;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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

        public async Task CreateSetor(CreateSetorDto setor)
        {
            try
            {
                await _validatorCreate.ValidateAsync(setor, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRulesNotInRuleSet();
                });

                _context.Setor.AddRange(_mapper.Map<SetorModel>(setor));
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(ErrorTranslation.SetorCreateFailed, e);
            }
        }

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

        public async Task UpdateSetor(UpdateSetorDto setor)
        {
            try
            {
                await _validatorUpdate.ValidateAsync(setor, options =>
                {
                    options.ThrowOnFailures();
                    options.IncludeRulesNotInRuleSet();
                });

                var setorDb = _context.Setor.Where(x => x.Id == setor.Id).FirstOrDefault();
                if (setorDb == null) throw new Exception(ErrorTranslation.InvalidSetor);
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
