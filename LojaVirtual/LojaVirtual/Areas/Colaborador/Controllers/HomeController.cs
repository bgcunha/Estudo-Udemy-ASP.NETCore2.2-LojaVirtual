using LojaVirtual.Controllers.Libraries.Filtros;
using LojaVirtual.Controllers.Libraries.Login;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    public class HomeController : Controller
    {
        private IRepositoryColaborador _repositoryColaborador;

        private LoginColaborador _loginColaborador;

        public HomeController(IRepositoryColaborador repositoryColaborador, LoginColaborador loginColaborador)
        {
            _repositoryColaborador = repositoryColaborador;
            _loginColaborador = loginColaborador;
        }
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] Models.Colaborador colaborador)
        {
            var colaboradorLogado = _repositoryColaborador.Login(colaborador.Email, colaborador.Senha);

            if (colaboradorLogado != null)
            {
                _loginColaborador.SetColaborador(colaboradorLogado);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                @ViewData["MSG_ERRO"] = "Dados de login inválidos!";

                return View();
            }
        }

        [ColaboradorAutorizacao]
        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginColaborador.Logout();
            return RedirectToAction(nameof(Login), nameof(HomeController));
        }

        [HttpGet]
        [ColaboradorAutorizacaoAttribute]
        public IActionResult Painel()
        {
            //var clienteLogado = _loginColaborador.GetColaborador();

            //return new ContentResult() { Content = $"Logado - Id: {clienteLogado.Id } E-mail: {clienteLogado.Email} logado!" };

            return View();
        }

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult CadastrarNovaSenha()
        {
            return View();
        }

    }
}
