﻿using RedeSocial.Model;

namespace RedeSocial.Service
{
    public interface IPostagemService 
    {
        Task<IEnumerable<Postagem>> GetAll();

        Task<Postagem?> GetById (long id);

        Task<IEnumerable<Postagem>> GetByTitulo (string titulo);

        Task<IEnumerable<Postagem>> GetByData (DateTimeOffset dataInicial, DateTimeOffset dataFinal);

        Task<Postagem?> Create(Postagem postagem);

        Task<Postagem?> Curtir(long id);

        Task<Postagem?> Amei(long id);

        Task<Postagem?> Indico(long id);

        Task<Postagem?> Update(Postagem postagem);

        Task Delete(Postagem postagem);

    }
}
