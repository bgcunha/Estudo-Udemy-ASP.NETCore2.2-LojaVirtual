using LojaVirtual.Models;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryImagem
    {
        void Cadastrar(Imagem model);
        void Excluir(int id);
        void ExcluirImagemsDoProduto(int idProduto);

    }
}
