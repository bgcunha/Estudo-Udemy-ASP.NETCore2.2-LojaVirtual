
using LojaVirtual.Libraries.Login;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Libraries.Filtros
{
    /*
     Tipo de filtro
     - Autorização 
     - Recurso
     - Ação
     - Execução
     - Resultado
     */
    public class ClienteAutorizacaoAttribute : Attribute, IAuthorizationFilter  
    {
        LoginCliente _loginCliente;
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginCliente = (LoginCliente)context.HttpContext.RequestServices.GetService(typeof(LoginCliente));

            Cliente cliente = _loginCliente.GetCliente(); 

            if (cliente == null)
            {
                context.Result = context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            
        }
    }
}
