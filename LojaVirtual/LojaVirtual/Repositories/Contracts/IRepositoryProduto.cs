
using LojaVirtual.Models;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    interface IRepositoryProduto
    {
        void Cadastrar(Produto model);
        void Atualizar(Produto model);        
        void Excluir(int id);

        Produto ObterPorId(int id);
        IPagedList<Produto> ObterTodos(int? pagina, string pesquisa);
    }
}
