using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryColaborador
    {

        Colaborador Login(string email, string senha);

        //CRUD
        void Cadastrar(Colaborador model);

        void Atualizar(Colaborador model);
        void AtualizarSenha(Colaborador model);
        void Excluir(int id);

        Colaborador ObterPorId(int id);

        //IEnumerable<Colaborador> ObterTodos();

        List<Colaborador> ObterPorEmail(string email);

        IPagedList<Colaborador> ObterTodos(int? pagina);

    }
}
