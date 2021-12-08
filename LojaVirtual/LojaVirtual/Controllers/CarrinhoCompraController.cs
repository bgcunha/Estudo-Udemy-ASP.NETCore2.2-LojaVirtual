using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private GerenciarCarrinho _gerenciarCarrinho;
        private IRepositoryProduto _repositoryProduto;

        public CarrinhoCompraController(GerenciarCarrinho gerenciarCarrinho, IRepositoryProduto repositoryProduto)
        {
            _gerenciarCarrinho = gerenciarCarrinho;
            _repositoryProduto = repositoryProduto;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Adicionar(int id)
        {
            var Produto = _repositoryProduto.ObterPorId(id);

            if (Produto != null)
            {
                var Item = new ProdutoItemCarrinho() { Id = id, QuantidadeProutoCarrinho = 1 };

                return RedirectToAction(nameof(Index));
            }

            return View("NaoExisteItem");
        }
        public IActionResult AterarQuantidade(int id, int quantidade)
        {
            var Item = new ProdutoItemCarrinho() { Id = id, QuantidadeProutoCarrinho = quantidade};

            _gerenciarCarrinho.Atualizar(Item);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remover(int id)
        {
            _gerenciarCarrinho.Remover(new ProdutoItemCarrinho() { Id = id });

            return RedirectToAction(nameof(Index));
        }
    }
}
