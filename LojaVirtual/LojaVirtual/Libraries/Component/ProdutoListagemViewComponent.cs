using LojaVirtual.Models.ViewModels;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class ProdutoListagemViewComponent : ViewComponent
    {
        private IRepositoryProduto _repositoryProduto;

        public ProdutoListagemViewComponent(IRepositoryProduto repositoryProduto)
        {
            _repositoryProduto = repositoryProduto;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? Pagina = 1;
            string Pesquisa = "";
            string Ondem = "A";
            
            if (HttpContext.Request.Query.ContainsKey("pagina"))
            {
                Pagina = int.Parse( HttpContext.Request.Query["pagina"]);
            }
            if (HttpContext.Request.Query.ContainsKey("pesquisa"))
            {
                Pesquisa = HttpContext.Request.Query["pesquisa"].ToString();
            }
            if (HttpContext.Request.Query.ContainsKey("ordenacao"))
            {
                Ondem = HttpContext.Request.Query["Ordenacao"].ToString();
            }

            var produtos = _repositoryProduto.ObterTodos(Pagina, Pesquisa, Ondem);

            var ViewModel = new VMProdutoListagem { Lista = produtos };

            return View(ViewModel);
        }
    }
}
