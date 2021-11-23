using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryCliente
    {

        Cliente Login(string email, string senha);

        //CRUD
        void Cadastrar(Cliente model);

        void Atualizar(Cliente model);

        void Excluir(int id);

        Cliente ObterPorId(int id);

        IPagedList<Cliente> ObterTodos(int? pagina, string pesquisa);

    }
}
