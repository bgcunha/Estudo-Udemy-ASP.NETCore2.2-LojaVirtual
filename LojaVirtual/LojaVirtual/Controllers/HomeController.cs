using LojaVirtual.Libraries;
using LojaVirtual.Libraries.Filtros;
using LojaVirtual.Libraries.Login;
using LojaVirtual.Libraries.Sessao;
using LojaVirtual.Models;
using LojaVirtual.Models.ViewModels;
using LojaVirtual.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryCliente _repositoryCliente;
        private IRepositoryNewsLatterEmail _repositoryNewsLatter;
        private IRepositoryProduto _repositoryProduto;

        private LoginCliente _loginCliente;
        private GerenciarEmail _gerenciarEmail;

        public HomeController(IRepositoryCliente repositoryCliente, IRepositoryNewsLatterEmail repositoryNewsLatter, IRepositoryProduto repositoryProduto, LoginCliente loginCliente, GerenciarEmail gerenciarEmail)
        {
            _repositoryCliente = repositoryCliente;
            _repositoryNewsLatter = repositoryNewsLatter;
            _repositoryProduto = repositoryProduto;

            _loginCliente = loginCliente;
            _gerenciarEmail = gerenciarEmail;
        }

        
        public IActionResult Index()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult Index(int? pagina, string pesquisa, string ordenacao, [FromForm] NewsLatterEmail newsLatterEmail)
        {
            if (ModelState.IsValid)
            {
                _repositoryNewsLatter.Cadastrar(newsLatterEmail);

                TempData["MSG_SUCESSO"] = $"Seu E-mail {newsLatterEmail.Email} foi cadastrado, agora você receberá as nossas promoções.";

                return RedirectToAction(nameof(Index));
            }
            else
            {                
                return View();
            }
        }

        public IActionResult Categoria()
        {
            return View();
        }

        public IActionResult Contato()
        {
            return View();
        }

        public IActionResult ContatoAcao()
        {
            try
            {
                var nome = HttpContext.Request.Form["nome"];
                var email = HttpContext.Request.Form["email"];
                var texto = HttpContext.Request.Form["texto"];

                var contato = new Contato() { Nome = nome, Email = email, Texto = texto };

                var listaMensagem = new List<ValidationResult>();
                var contexto = new ValidationContext(contato);
                var isValid = Validator.TryValidateObject(contato, contexto, listaMensagem, true);

                if (isValid)
                {
                    _gerenciarEmail.EnviarContatoPorEmail(contato);

                    ViewData["MSG_SUCESSO"] = $"Email enviado para: {contato.Email.ToUpper()}";
                }
                else
                {
                    StringBuilder sb = new StringBuilder("Erros:");
                    foreach (var msg in listaMensagem)
                    {
                        sb.Append(msg.ErrorMessage + "<br/>");
                    }

                    ViewData["MSG_ERRO"] = sb.ToString();
                    ViewData["CONTATO"] = contato;

                }

            }
            catch (System.Exception e)
            {
                ViewData["MSG_ERRO"] = $"Opps! Tivemos um erro, tente mais tarde. Erro {e.Message}";
                //TODO - Fazer o log               
            }

            return View("Contato");

        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm]Cliente cliente)
        {
            var clienteLogado =  _repositoryCliente.Login(cliente.Email, cliente.Senha);

            if (clienteLogado != null)
            {
                _loginCliente.SetCliente(clienteLogado);

                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else 
            {
                @ViewData["MSG_ERRO"] = "Dados de login inválidos!";

                return View();
            }
        }

        [HttpGet]
        [ClienteAutorizacaoAttribute]
        public IActionResult Painel()
        {
            var clienteLogado = _loginCliente.GetCliente();
            
             return new ContentResult() { Content = $"Logado - Id: {clienteLogado.Id } E-mail: {clienteLogado.Email} logado!" };
        }


        public IActionResult CadastroCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CadastroCliente([FromForm] Cliente cliente)
        {
            if (ModelState.IsValid)
            {               
                _repositoryCliente.Cadastrar(cliente);                

                TempData["MSG_SUCESSO"] = $"Cadastro do cliente {cliente.Nome} realizado com sucesso!";

                //TODO - Implementar redirecionamentos diferentes (Painel, Carrinho de conpras etc).


                return RedirectToAction(nameof(CadastroCliente));
            }

            return View();
        }

        public IActionResult CarrinhoCompras()
        {
            return View();
        }
    }
}
