using Microsoft.EntityFrameworkCore;
using RedeSocial.Data;
using RedeSocial.Model;

namespace RedeSocial.Service.Implements
{
    public class PostagemService : IPostagemService
    {
        private readonly AppDbContext _context;

        public PostagemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _context.Postagens
                .Include(u => u.User)
                .Include(p => p.Tema)
                .ToListAsync();
        }

        public async Task<Postagem?> GetById(long id)
        {
            try
            {
                var Postagem = await _context.Postagens
                    .Include(p => p.Tema)
                    .Include(p => p.User)
                    .FirstAsync(i => i.Id == id);

                return Postagem;
            }
            catch
            {
                return null;
            }

        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            var Postagem = await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.User)
                .Where(p => p.Titulo.Contains(titulo))
                .ToListAsync();

            return Postagem;
        }

        public async Task<IEnumerable<Postagem>> GetByData (DateTimeOffset dataInicial, DateTimeOffset dataFinal)
        {
            var Data = await _context.Postagens
                 .Include(p => p.Tema)
                 .Include(p => p.User)
                 .Where(p => DateTimeOffset.Compare(p.DataLancamento, dataFinal) <= 0 && DateTimeOffset.Compare(p.DataLancamento, dataInicial) >= 0)
                .ToListAsync();

            return Data;
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            if (postagem.Tema is not null)
            {
                var BuscaTema = await _context.Temas.FindAsync(postagem.Tema.Id);

                if (BuscaTema is null )
                {
                    return null;
                }
                
                postagem.Tema = BuscaTema;
               
            }
            
            if(postagem.User is not null)
            {
                var BuscaUser = _context.Users.FirstOrDefault(p => p.Id == postagem.User.Id);
                
                if (BuscaUser is null)
                {
                    return null;
                }
                
                postagem.User = BuscaUser;
            }
            
            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync();

            return postagem;
        }

        public async Task<Postagem?> Curtir(long id)
        {
            var Postagem = await _context.Postagens
             .Include(p => p.Tema)
             .FirstOrDefaultAsync(p => p.Id == id);

            if (Postagem is null)
                return null;

            Postagem.Curtir += 1;

            _context.Update(Postagem);
            await _context.SaveChangesAsync();

            return Postagem;

        }

        public async Task<Postagem?> Amei(long id)
        {
            var Postagem = await _context.Postagens
             .Include(p => p.Tema)
             .FirstOrDefaultAsync(p => p.Id == id);

            if (Postagem is null)
                return null;

            Postagem.Amei += 1;

            _context.Update(Postagem);
            await _context.SaveChangesAsync();

            return Postagem;

        }

        public async Task<Postagem?> Indico(long id)
        {
            var Postagem = await _context.Postagens
             .Include(p => p.Tema)
             .FirstOrDefaultAsync(p => p.Id == id);

            if (Postagem is null)
                return null;

            Postagem.Indico += 1;

            _context.Update(Postagem);
            await _context.SaveChangesAsync();

            return Postagem;

        }

        public async Task<Postagem?> Update(Postagem postagem)
        {

            var PostagemUpdate = await _context.Postagens.FindAsync(postagem.Id);

            if (PostagemUpdate is null)
                return null;

            postagem.Tema = postagem.Tema is not null ? _context.Temas.FirstOrDefault(p => p.Id == postagem.Tema.Id) : null;

            _context.Entry(PostagemUpdate).State = EntityState.Detached;
            _context.Entry(postagem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return postagem;

        }

        public async Task Delete(Postagem postagem)
        {

            _context.Postagens.Remove(postagem);
            await _context.SaveChangesAsync();

        }
    }
}
