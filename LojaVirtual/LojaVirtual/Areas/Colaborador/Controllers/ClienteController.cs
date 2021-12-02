using LojaVirtual.Libraries.Filtros;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao]
    public class ClienteController : Controller
    {
        private IRepositoryCliente _RepositoryCliente;

        public ClienteController(IRepositoryCliente repositoryCliente)
        {
            _RepositoryCliente = repositoryCliente;
        }

        public IActionResult Index(int? pagina, string pesquisa)
        {
            var clientes = _RepositoryCliente.ObterTodos(pagina, pesquisa);
            return View(clientes);
        }

        [ValidateHttpReferer]
        public IActionResult AtivarDesativar(int id)
        {
            var cliente =_RepositoryCliente.ObterPorId(id);
            cliente.Situacao = (cliente.Situacao == SituacaoConstant.Ativo) ? "I" : "A";
            _RepositoryCliente.Atualizar(cliente);

            TempData["MSG_SUCESSO"] = Mensagem.MSG_SALTERADO;

            return RedirectToAction(nameof(Index));

           
        }
    }
}
