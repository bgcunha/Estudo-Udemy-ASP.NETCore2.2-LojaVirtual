
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Controllers
{
    public class ProdutoController : Controller
    {
        public ProdutoController()
        {
        }

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
