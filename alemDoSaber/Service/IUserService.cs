using RedeSocial.Model;

namespace RedeSocial.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(long id);

        Task<String?> GetByEmail(string usuario);

        Task<User?> GetByUsuario (string usuario);
        
        Task<User?> Create(User usuario);
        
        Task<User?> Update(User usuario);
    }
}
