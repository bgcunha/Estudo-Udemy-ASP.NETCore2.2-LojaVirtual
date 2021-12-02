using LojaVirtual.Libraries;
using LojaVirtual.Libraries.Filtros;
using LojaVirtual.Libraries.Lang;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;


namespace LojaVirtual.Areas.Colaborador.Controllers
{
    [Area("Colaborador")]
    [ColaboradorAutorizacao(ColaboradorTipoConstant.Gerente)]
    public class ColaboradorController : Controller
    {
        private IRepositoryColaborador _repositoryColaborador;

        private GerenciarEmail _gerenciarEmail;

        public ColaboradorController(IRepositoryColaborador repositoryColaborador, GerenciarEmail gerenciarEmail)
        {
            _repositoryColaborador = repositoryColaborador;
            _gerenciarEmail = gerenciarEmail;
        }

        public ActionResult Index(int? pagina)
        {
            return View(_repositoryColaborador.ObterTodos(pagina));
        }
        
        
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar([FromForm] Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                colaborador.Tipo = ColaboradorTipoConstant.Comum;
                colaborador.Senha = KeyGenerator.GetUniqueKey(8);
                _repositoryColaborador.Cadastrar(colaborador);

                _gerenciarEmail.EnviarSenhaColaborador(colaborador);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SSALVO;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [ValidateHttpReferer]
        public IActionResult GerarSenha(int id)
        {
            var colaborador = _repositoryColaborador.ObterPorId(id);

            colaborador.Senha = KeyGenerator.GetUniqueKey(8);

            _repositoryColaborador.AtualizarSenha(colaborador);

            _gerenciarEmail.EnviarSenhaColaborador(colaborador);

            TempData["MSG_SUCESSO"] = Mensagem.MSG_SEMAIL;

            return RedirectToAction(nameof(Index));

        }

        public ActionResult Atualizar(int id)
        {
            var colaborador = _repositoryColaborador.ObterPorId(id);
            return View(colaborador);
        }

        [HttpPost]
        public ActionResult Atualizar([FromForm] Models.Colaborador colaborador)
        {
            ModelState.Remove("Senha");
            if (ModelState.IsValid)
            {
                _repositoryColaborador.Atualizar(colaborador);

                TempData["MSG_SUCESSO"] = Mensagem.MSG_SALTERADO;

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [ValidateHttpReferer]      
        public ActionResult Excluir(int id)
        {
            _repositoryColaborador.Excluir(id);

            TempData["MSG_SUCESSO"] = Mensagem.MSG_SEXCLUIDO;

            return RedirectToAction(nameof(Index));
        }

    }
}
