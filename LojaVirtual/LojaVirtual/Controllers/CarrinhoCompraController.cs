using AutoMapper;
using LojaVirtual.Libraries.CarrinhoCompra;
using LojaVirtual.Libraries.Gerenciador.Frete;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Models.Constants;
using LojaVirtual.Models.ProdutoAgregador;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private GerenciarCarrinho _gerenciarCarrinho;
        private IRepositoryProduto _repositoryProduto;
        private IMapper _mapper;
        private WSCorreiosCalcularFrete _wscorreios;
        private CalcularPacote _calcularPacote;

        public CarrinhoCompraController(GerenciarCarrinho gerenciarCarrinho, IRepositoryProduto repositoryProduto, IMapper mapper, WSCorreiosCalcularFrete wscorreios, CalcularPacote calcularPacote)
        {
            _gerenciarCarrinho = gerenciarCarrinho;
            _repositoryProduto = repositoryProduto;
            _mapper = mapper;
            _wscorreios = wscorreios;
            _calcularPacote = calcularPacote;
        }
        public IActionResult Index()
        {
            var produtoItemCompleto = CarregarProdutoDB();

            return View(produtoItemCompleto);
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

        public async Task<IActionResult> CalcularFrete(int cepDestino)
        {
            try
            {
                List<ProdutoItem> produtos = CarregarProdutoDB();

                List<Pacote> pacotes = _calcularPacote.CalcularPacotesDeProdutos(produtos);

                ValorPrazoFrete valorPAC = await _wscorreios.CalcularFrete( cepDestino.ToString(), TipoFreteConstant.PAC, pacotes);
                ValorPrazoFrete valorSEDEX = await _wscorreios.CalcularFrete(cepDestino.ToString(), TipoFreteConstant.SEDEX, pacotes);
                ValorPrazoFrete valorSEDEX10 = await _wscorreios.CalcularFrete(cepDestino.ToString(), TipoFreteConstant.SEDEX10, pacotes);

                List<ValorPrazoFrete> lista = new List<ValorPrazoFrete>();
                if (valorPAC != null) lista.Add(valorPAC);
                if (valorSEDEX != null)  lista.Add(valorSEDEX);
                if (valorSEDEX10 != null)  lista.Add(valorSEDEX10);

                return Ok(lista);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        private List<ProdutoItem> CarregarProdutoDB()
        {
            List<ProdutoItem> produtoItemNoCarrinho = _gerenciarCarrinho.Consultar();

            List<ProdutoItem> produtoItemCompleto = new List<ProdutoItem>();

            foreach (var item in produtoItemNoCarrinho)
            {
                Produto produto = _repositoryProduto.ObterPorId(item.Id);

                ProdutoItem produtoItem = _mapper.Map<ProdutoItem>(produto);
                produtoItem.QuantidadeProdutoCarrinho = item.QuantidadeProdutoCarrinho;

                produtoItemCompleto.Add(produtoItem);
            }

            return produtoItemCompleto;
        }
    }
}
