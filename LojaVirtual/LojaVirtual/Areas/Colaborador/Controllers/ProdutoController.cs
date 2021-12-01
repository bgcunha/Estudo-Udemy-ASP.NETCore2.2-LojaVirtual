using LojaVirtual.Controllers.Libraries.Arquivo;
using LojaVirtual.Controllers.Libraries.Filtros;
using LojaVirtual.Controllers.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ProdutoController : Controller
    {
        private IRepositoryProduto _repositoryProduto;
        private IRepositoryCategoria _repositoryCategoria;
        private IRepositoryImagem _repositoryImagem;
        public ProdutoController(IRepositoryProduto repositoryProduto, IRepositoryCategoria repositoryCategoria, IRepositoryImagem repositoryImagem)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryCategoria = repositoryCategoria;
            _repositoryImagem = repositoryImagem;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos = _repositoryProduto.ObterTodos(pagina, pesquisa);
            return View(produtos);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto model)
        {
            var CaminhosTemp = new List<string> { Request.Form["Imagem"] };
            if (ModelState.IsValid)
            {
                _repositoryProduto.Cadastrar(model);
                
                var imagensDefinitivas = GerenciadorArquivo.MoverImagensProduto(CaminhosTemp.Where(x => x.Length > 0).ToList(), model.Id);

                _repositoryImagem.CadastrarImagens(imagensDefinitivas);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SSALVO;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            model.Imagens = CaminhosTemp.Select(x => new Imagem { Caminho = x }).ToList();
            return View(model);
        }

        public IActionResult Atualizar(int id)
        {
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            var produto = _repositoryProduto.ObterPorId(id);
            return View(produto);
        }

        [HttpPost]
        public IActionResult Atualizar(Produto model)
        {
            var CaminhosTemp = new List<string> { Request.Form["Imagem"] };
            if (ModelState.IsValid)
            {
                _repositoryProduto.Atualizar(model);

                var imagensDefinitivas = GerenciadorArquivo.MoverImagensProduto(CaminhosTemp.Where(x => x.Length > 0).ToList(), model.Id);

                _repositoryImagem.ExcluirImagemsDoProduto(model.Id);

                _repositoryImagem.CadastrarImagens(imagensDefinitivas);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SSALVO;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            model.Imagens = CaminhosTemp.Select(x => new Imagem { Caminho = x }).ToList();
            return View(model);
        }

       [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            var Modelo = _repositoryProduto.ObterPorId(id);

            GerenciadorArquivo.ExcluirImagemsProduto(Modelo.Imagens.ToList());
            _repositoryImagem.ExcluirImagemsDoProduto(id);
            _repositoryProduto.Excluir(id);

            TempData["MSG_S"] = Mensagem.MSG_SEXCLUIDO;

            return RedirectToAction(nameof(Index));
        }

    }
}
