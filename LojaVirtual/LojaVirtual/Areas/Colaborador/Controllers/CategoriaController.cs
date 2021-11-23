using LojaVirtual.Controllers.Libraries.Filtros;
using LojaVirtual.Controllers.Libraries.Lang;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class CategoriaController : Controller
    {
        private  IRepositoryCategoria _repositoryCategoria;

        public CategoriaController(IRepositoryCategoria repositoryCategoria)
        {
            _repositoryCategoria = repositoryCategoria;
        }

        public IActionResult Index(int? pagina)
        {
            var categorias = _repositoryCategoria.ObterTodos(pagina);
            return View(categorias);
        }

        public IActionResult Cadastrar()
        {
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a=> new SelectListItem($"{a.Id} - {a.Nome}", a.Id.ToString()));
            
            return View();
        }

        [HttpPost]        
        public IActionResult Cadastrar([FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repositoryCategoria.Cadastrar(categoria);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SSALVO;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            return View();
        }

        public IActionResult Atualizar(int id)
        {
            var categoria = _repositoryCategoria.ObterPorId(id);
            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Where(x=>x.Id != id).Select(a => new SelectListItem($"{a.Id} - {a.Nome}", a.Id.ToString()));

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _repositoryCategoria.Atualizar(categoria);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SALTERADO;

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CATEGORIAS = _repositoryCategoria.ObterTodos().Where(x => x.Id != categoria.Id).Select(a => new SelectListItem($"{a.Id} - {a.Nome}", a.Id.ToString()));
            return View();
        }

        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _repositoryCategoria.Excluir(id);

            TempData["MSG_SUCESSO"] = Mensagem.MSG_SEXCLUIDO;

            return RedirectToAction(nameof(Index));
        }
    }
}
