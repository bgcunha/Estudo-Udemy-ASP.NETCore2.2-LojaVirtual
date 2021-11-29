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
    public class ProdutoController : Controller
    {
        private IRepositoryProduto _repositoryProduto;
        private IRepositoryCategoria _repositoryCategoria;
        public ProdutoController(IRepositoryProduto repositoryProduto, IRepositoryCategoria repositoryCategoria)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryCategoria = repositoryCategoria;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos = _repositoryProduto.ObterTodos(pagina, pesquisa);
            return View(produtos);
        }

        public  IActionResult Cadastrar()
        {
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a=> new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(Produto model )
        {
            if (ModelState.IsValid)
            {
                _repositoryProduto.Cadastrar(model);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SSALVO;

                return RedirectToAction(nameof(Index));

            }
            
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
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
            if (ModelState.IsValid)
            {
                _repositoryProduto.Atualizar(model);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SALTERADO;

                return RedirectToAction(nameof(Index));

            }

            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }
    }
}
