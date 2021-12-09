using AutoMapper;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private GerenciarCarrinho _gerenciarCarrinho;
        private IRepositoryProduto _repositoryProduto;
        private IMapper _mapper;

        public CarrinhoCompraController(GerenciarCarrinho gerenciarCarrinho, IRepositoryProduto repositoryProduto, IMapper mapper)
        {
            _gerenciarCarrinho = gerenciarCarrinho;
            _repositoryProduto = repositoryProduto;
            _mapper = mapper;
        }
        public IActionResult Index()
        {       
            List<ProdutoItemCarrinho> ProdutoItemCarrinhoNoCarrinho = _gerenciarCarrinho.Consultar();

            List<ProdutoItemCarrinho> ProdutoItemCarrinhoCompleto = new List<ProdutoItemCarrinho>();

            foreach (var item in ProdutoItemCarrinhoNoCarrinho)
            {
                Produto Produto = _repositoryProduto.ObterPorId(item.Id);

                ProdutoItemCarrinho ProdutoItemCarrinho = _mapper.Map<ProdutoItemCarrinho>(Produto);
                ProdutoItemCarrinho.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;

                ProdutoItemCarrinhoCompleto.Add(ProdutoItemCarrinho);
            }

            return View(ProdutoItemCarrinhoCompleto);
        }        
        public IActionResult Adicionar(int id)
        {
            var Produto = _repositoryProduto.ObterPorId(id);

            if (Produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var Item = new ProdutoItemCarrinho() { Id = id, QuantidadeProdutoCarrinho = 1 };
                _gerenciarCarrinho.Cadastrar(Item);

                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult AterarQuantidade(int id, int quantidade)
        {
            var Item = new ProdutoItemCarrinho() { Id = id, QuantidadeProdutoCarrinho = quantidade};
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
