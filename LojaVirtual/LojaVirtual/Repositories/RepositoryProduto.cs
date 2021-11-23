
using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class RepositoryProduto : IRepositoryProduto
    {
        private LojaVirtualContext _context;
        private IConfiguration _config;

        public RepositoryProduto(LojaVirtualContext context, IConfiguration configuration)
        {
            this._context = context;
            this._config = configuration;
        }
        public void Atualizar(Produto model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }

        public void Cadastrar(Produto model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var produto = ObterPorId(id);
            _context.Remove(produto);
            _context.SaveChanges();
        }

        public Produto ObterPorId(int id)
        {
            return _context.Produtos.Include(x => x.Imagens).Where(x => x.Id == id).FirstOrDefault();
        }

        public IPagedList<Produto> ObterTodos(int? pagina, string pesquisa)
        {
            var numeroPagina = pagina ?? 1;


            if (string.IsNullOrEmpty(pesquisa))
            {
                return _context.Produtos.Include(x => x.Imagens).ToPagedList<Produto>(numeroPagina, _config.GetValue<int>("RegistrosPorPagina"));
            }

            pesquisa = pesquisa.Trim();
            return _context.Produtos.Include(x => x.Imagens).Where(x => x.Nome.Contains(pesquisa)).ToPagedList<Produto>(numeroPagina, _config.GetValue<int>("RegistrosPorPagina"));

        }
    }
}
