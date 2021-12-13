using AutoMapper;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Libraries.Lang;
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
            List<Models.ProdutoAgregador.ProdutoItem> ProdutoItemCarrinhoNoCarrinho = _gerenciarCarrinho.Consultar();

            List<Models.ProdutoAgregador.ProdutoItem> ProdutoItemCarrinhoCompleto = new List<Models.ProdutoAgregador.ProdutoItem>();

            foreach (var item in ProdutoItemCarrinhoNoCarrinho)
            {
                Produto Produto = _repositoryProduto.ObterPorId(item.Id);

                Models.ProdutoAgregador.ProdutoItem ProdutoItemCarrinho = _mapper.Map<Models.ProdutoAgregador.ProdutoItem>(Produto);
                ProdutoItemCarrinho.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;

                ProdutoItemCarrinhoCompleto.Add(ProdutoItemCarrinho);
            }

            return View(ProdutoItemCarrinhoCompleto);
        }        
        public IActionResult AdicionarItem(int id)
        {
            var Produto = _repositoryProduto.ObterPorId(id);

            if (Produto == null)
            {
                return View("NaoExisteItem");
            }
            else
            {
                var Item = new Models.ProdutoAgregador.ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = 1 };
                _gerenciarCarrinho.Cadastrar(Item);

                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult AlterarQuantidade(int id, int quantidade)
        {
            var Produto =_repositoryProduto.ObterPorId(id);

            if (quantidade < 1)
            {
                return BadRequest(new { mensagem = Mensagem.MSG_E007 });
            }
            else if (quantidade > Produto.Quantidade)
            {
                return BadRequest(new { mensagem = Mensagem.MSG_E008 });
            }

            var Item = new Models.ProdutoAgregador.ProdutoItem() { Id = id, QuantidadeProdutoCarrinho = quantidade};
            _gerenciarCarrinho.Atualizar(Item);
            return Ok(new { mensagem = Mensagem.MSG_SSALVO });
        }
        public IActionResult RemoverItem(int id)
        {
            _gerenciarCarrinho.Remover(new Models.ProdutoAgregador.ProdutoItem() { Id = id });
            return RedirectToAction(nameof(Index));
        }
    }
}
