using LojaVirtual.Models;
using System.Collections.Generic;

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

        IEnumerable<Cliente> ObterTodos();

    }
}
