
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        private IRepositoryProduto _repositoryProduto;
        private IRepositoryCategoria _repositoryCategoria;

        public ProdutoController(IRepositoryProduto repositoryProduto, IRepositoryCategoria repositoryCategoria)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryCategoria = repositoryCategoria;
        }

        [HttpGet]
        [Route("/Produto/Categoria/{slug}")]
        public  IActionResult ListagemCategoria(string slug)
        {
            var categoria = _repositoryCategoria.ObterCategoria(slug);
            return View(categoria);
        }
                
        public ActionResult Visualizar(int id)
        {
            return View(_repositoryProduto.ObterPorId(id));
        }
    }
}
