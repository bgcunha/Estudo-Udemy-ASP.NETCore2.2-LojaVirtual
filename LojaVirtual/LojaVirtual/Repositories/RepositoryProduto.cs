
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
        public void Atualizar(Produto produto)
        {
            _context.Update(produto);
            _context.SaveChanges();
        }

        public void Cadastrar(Produto produto)
        {
            _context.Add(produto);
            _context.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Produto produto = ObterPorId(Id);
            _context.Remove(produto);
            _context.SaveChanges();
        }

        public Produto ObterPorId(int Id)
        {
            return _context.Produtos.Include(a => a.Imagens).OrderBy(a => a.Nome).Where(a => a.Id == Id).FirstOrDefault();
        }

        public IPagedList<Produto> ObterTodos(int? pagina, string pesquisa)
        {
            return ObterTodos(pagina, pesquisa, "A");
        }

        public IPagedList<Produto> ObterTodos(int? pagina, string pesquisa, string ordenacao)
        {
            int RegistroPorPagina = _config.GetValue<int>("RegistrosPorPagina");

            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _context.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
            {
                bancoProduto = bancoProduto.Where(a => a.Nome.Contains(pesquisa.Trim()));
            }
            if (ordenacao == "A")
            {
                bancoProduto = bancoProduto.OrderBy(a => a.Nome);
            }
            if (ordenacao == "ME")
            {
                bancoProduto = bancoProduto.OrderBy(a => a.Valor);
            }
            if (ordenacao == "MA")
            {
                bancoProduto = bancoProduto.OrderByDescending(a => a.Valor);
            }

            return bancoProduto.Include(a => a.Imagens).ToPagedList<Produto>(NumeroPagina, RegistroPorPagina);
        }
    }
}
