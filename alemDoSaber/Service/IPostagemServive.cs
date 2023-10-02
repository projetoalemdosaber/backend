namespace RedeSocial.Service
{
    public interface IPostagemServive 
    {
        Task<IEnumerable<Postagem>> GetAll();

        Task<Postagem?> GetById (long id);

        Task<IEnumerable<Postagem>> GetByTitulo (string titulo);

        Task<IEnumerable<Postagem>> GetByData (DateOnly date);

        Task<Postagem?> Create(Postagem postagem);

        Task<Postagem?> Update(Postagem postagem);

        Task Delete(Postagem postagem);

    }
}
