using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
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
        public ProdutoController(IRepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var produtos = _repositoryProduto.ObterTodos(pagina, pesquisa);
            return View(produtos);
        }
    }
}
