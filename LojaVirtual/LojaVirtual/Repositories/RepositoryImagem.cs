using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class RepositoryImagem : IRepositoryImagem
    {
        private LojaVirtualContext _context;

        public RepositoryImagem(LojaVirtualContext context)
        {
            this._context = context;           
        }

        public void Cadastrar(Imagem model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Excluir(int id)
        {
            var model = _context.Imagens.Find(id);
            _context.Remove(model);
            _context.SaveChanges();
        }

        public void ExcluirImagemsDoProduto(int idProduto)
        {
            var models = _context.Imagens.Where(x=>x.ProdutoId == idProduto).ToArray();

            foreach (var model in models)
            {
                _context.Remove(model);
            }
           
            _context.SaveChanges();
        }
    }
}
