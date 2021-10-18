using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class RepositoryCategoria : IRepositoryCategoria
    {
        private LojaVirtualContext _context;
        private IConfiguration _config;

        public RepositoryCategoria(LojaVirtualContext context, IConfiguration configuration)
        {
            _context = context;
            _config = configuration;
        }        

        public void Cadastrar(Categoria model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }


        public void Atualizar(Categoria model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

        
        public void Excluir(int id)
        {
            var Categoria = ObterPorId(id);
            _context.Remove(Categoria);
            _context.SaveChanges();
        }

       
        public Categoria ObterPorId(int id)
        {
            return _context.Categorias.Find(id);
        }

        public IEnumerable<Categoria> ObterTodos()
        {
            return _context.Categorias.ToList();
        }
        
        public IPagedList<Categoria> ObterTodos(int? pagina)
        {
            var numeroPagina = pagina ?? 1;

            return _context.Categorias.Include(x=>x.CategoriaPai).ToPagedList<Categoria> (numeroPagina, _config.GetValue<int>("RegistrosPorPagina") );
        }
    }
}
