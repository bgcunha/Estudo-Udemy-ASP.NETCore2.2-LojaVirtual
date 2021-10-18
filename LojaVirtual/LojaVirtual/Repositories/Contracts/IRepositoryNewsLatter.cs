using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryNewsLatterEmail
    {
       
        //CRUD
        void Cadastrar(NewsLatterEmail model);

        void Atualizar(NewsLatterEmail model);

        void Excluir(int id);

        NewsLatterEmail ObterPorId(int id);

        IEnumerable<NewsLatterEmail> ObterTodos();

    }
}
