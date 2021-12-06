using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Component
{
    public class MenuViewComponent : ViewComponent
    {
        private IRepositoryCategoria _repositoryCategoria;

        public MenuViewComponent(IRepositoryCategoria repositoryCategoria)
        {
            _repositoryCategoria = repositoryCategoria;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Categorias = _repositoryCategoria.ObterTodos().ToList();
            return View(Categorias);
        }
    }
}
