using RedeSocial.Model;

namespace RedeSocial.Service
{
    public interface ITemaService
    {

        Task<IEnumerable<Tema>> GetAll();
        Task<Tema?> GetById(long id);
        Task<IEnumerable<Tema>> GetByAssunto(string descricao);
        Task<Tema?> Create(Tema tema);
        Task<Tema?> Update(Tema tema);
        Task Delete(Tema tema);

    }
}
