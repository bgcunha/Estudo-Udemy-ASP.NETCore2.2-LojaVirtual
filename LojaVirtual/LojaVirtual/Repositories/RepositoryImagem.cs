using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
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

        public void CadastrarImagens(List<Imagem> ListaImagens, int ProdutoId)
        {
            if (ListaImagens != null && ListaImagens.Count > 0)
            {
                foreach (var Imagem in ListaImagens)
                {
                    Cadastrar(Imagem);
                }
            }
        }
        public void Cadastrar(Imagem imagem)
        {
            _context.Add(imagem);
            _context.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Imagem imagem = _context.Imagens.Find(Id);
            _context.Remove(imagem);
            _context.SaveChanges();
        }

        public void ExcluirImagensDoProduto(int ProdutoId)
        {
            List<Imagem> imagens = _context.Imagens.Where(a => a.ProdutoId == ProdutoId).ToList();

            foreach (Imagem imagem in imagens)
            {
                _context.Remove(imagem);
            }
            _context.SaveChanges();
        }
    }
}
