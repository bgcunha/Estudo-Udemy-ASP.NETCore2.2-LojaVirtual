
using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryProduto
    {
        //CRUD
        void Cadastrar(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(int Id);

        Produto ObterPorId(int Id);
        IPagedList<Produto> ObterTodos(int? pagina, string pesquisa);
        IPagedList<Produto> ObterTodos(int? pagina, string pesquisa, string ordenacao, IEnumerable<Categoria> categorias);
    }
}
