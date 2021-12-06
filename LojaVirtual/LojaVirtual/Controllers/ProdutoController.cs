
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
        public  IActionResult ListagemCategoria()
        {
            return View();
        }
        

        /****************************************************/
        public ActionResult Visualizar()
        {
            var produto = GetProduto();

            return View(produto);            
        }

        private Produto GetProduto()
        {
            return new Produto() 
            {
                Id = 1,
                Nome = "Celular Xiaomi",
                Descricao = "4 Câmeras",
                Valor = 1299.90M,
            };

        }
    }
}
