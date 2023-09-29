﻿using Microsoft.EntityFrameworkCore;
using RedeSocial.Data;
using RedeSocial.Model;

namespace RedeSocial.Service.Implements
{
    public class TemaService : ITemaService
    {
        private readonly AppDbContext _context;

        public TemaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tema?> Create(Tema tema)
        {
            await _context.Temas.AddAsync(tema);
            await _context.SaveChangesAsync();

            return tema;
        }

        public async Task Delete(Tema tema)
        {
            _context.Remove(tema);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tema>> GetAll()
        {
            return await _context.Temas
               .ToListAsync();
        }

        public async Task<IEnumerable<Tema>> GetByAssunto(string assunto)
        {
            var Tema = await _context.Temas
                         .Where(d => d.Assunto.Contains(assunto))
                         .ToListAsync();
            return Tema;
        }

        public async Task<Tema?> GetById(long id)
        {
            try
            {
                var Tema = await _context.Temas
                    .FirstAsync(t => t.Id == id);

                return Tema;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Tema?> Update(Tema tema)
        {
            var TemaUpdate = await _context.Temas.FindAsync(tema.Id);

            if (TemaUpdate is null)
            {
                return null;
            }

            _context.Entry(TemaUpdate).State = EntityState.Detached;
            _context.Entry(tema).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return tema;
        }
    }
}