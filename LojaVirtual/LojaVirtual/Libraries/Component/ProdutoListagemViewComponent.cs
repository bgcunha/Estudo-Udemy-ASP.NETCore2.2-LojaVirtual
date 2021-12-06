using LojaVirtual.Models;
using LojaVirtual.Models.ViewModels;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class ProdutoListagemViewComponent : ViewComponent
    {
        private IRepositoryProduto _repositoryProduto;
        private IRepositoryCategoria _repositoryCategoria;

        public ProdutoListagemViewComponent(IRepositoryProduto repositoryProduto, IRepositoryCategoria repositoryCategoria)
        {
            _repositoryProduto = repositoryProduto;
            _repositoryCategoria = repositoryCategoria;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int? Pagina = 1;
            string Pesquisa = "";
            string Ondem = "A";
            IEnumerable<Categoria> Categorias = null;
            
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
            if (ViewContext.RouteData.Values.ContainsKey("slug"))
            {
                var slug = ViewContext.RouteData.Values["slug"].ToString();
                var CategoriaPrincipal = _repositoryCategoria.ObterCategoria(slug);
                if (CategoriaPrincipal != null)
                {
                    Categorias = _repositoryCategoria.ObterCategoriasRecursivas(CategoriaPrincipal).ToList();
                }


            }

            var produtos = _repositoryProduto.ObterTodos(Pagina, Pesquisa, Ondem, Categorias);

            var ViewModel = new VMProdutoListagem { Lista = produtos };

            return View(ViewModel);
        }
    }
}
