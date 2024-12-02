
using A3System.Dbo.Dto.Setor;

namespace A3System.Interface
{
    public interface ISetorService
    {
        Task CreateSetor(CreateSetorDto setor);
        Task<ReadSetorDto> GetSetorById(int id);
        Task<List<ReadSetorDto>> GetAll();
        Task UpdateSetor(UpdateSetorDto setor);
    }
}
