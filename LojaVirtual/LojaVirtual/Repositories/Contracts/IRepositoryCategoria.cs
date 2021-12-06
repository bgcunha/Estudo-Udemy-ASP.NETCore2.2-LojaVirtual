using LojaVirtual.Models;
using System.Collections.Generic;
using X.PagedList;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryCategoria
    {       
        //CRUD
        void Cadastrar(Categoria model);

        void Atualizar(Categoria model);

        void Excluir(int id);

        Categoria ObterPorId(int id);
        Categoria ObterCategoria(string slug);
        IEnumerable<Categoria> ObterCategoriasRecursivas(Categoria categoriaPai);


        IEnumerable<Categoria> ObterTodos(); 
        IPagedList<Categoria> ObterTodos(int? pagina);
        
    }
}
