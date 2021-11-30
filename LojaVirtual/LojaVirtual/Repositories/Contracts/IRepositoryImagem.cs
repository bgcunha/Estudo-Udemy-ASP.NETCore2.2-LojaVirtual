using LojaVirtual.Models;
using System.Collections.Generic;

namespace LojaVirtual.Repositories.Contracts
{
    public interface IRepositoryImagem
    {
        void CadastrarImagens(List<Imagem> imagens);
        void Cadastrar(Imagem model);
        void Excluir(int id);
        void ExcluirImagemsDoProduto(int idProduto);

    }
}
