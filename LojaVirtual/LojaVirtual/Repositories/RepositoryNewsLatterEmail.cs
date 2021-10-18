using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class NewsLatterRepository : IRepositoryNewsLatterEmail
    {
        private LojaVirtualContext _context;
        public NewsLatterRepository(LojaVirtualContext context)
        {
            this._context = context;
        }
        public void Cadastrar(NewsLatterEmail model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Atualizar(NewsLatterEmail model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }


        public void Excluir(int id)
        {
            var cliente = ObterPorId(id);
            _context.Remove(cliente);
            _context.SaveChanges();
        }    
        
        public NewsLatterEmail ObterPorId(int id)
        {
            return _context.NewsLatterEmails.Find(id);
        }

        public IEnumerable<NewsLatterEmail> ObterTodos()
        {
            return _context.NewsLatterEmails.ToList();
        }        
    }
}
