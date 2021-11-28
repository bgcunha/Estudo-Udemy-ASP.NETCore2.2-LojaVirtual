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
    }
}
