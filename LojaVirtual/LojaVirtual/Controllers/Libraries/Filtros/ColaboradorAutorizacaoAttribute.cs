
using LojaVirtual.Controllers.Libraries.Login;
using LojaVirtual.Models;
using LojaVirtual.Models.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LojaVirtual.Controllers.Libraries.Filtros
{
    /*
     Tipo de filtro
     - Autorização 
     - Recurso
     - Ação
     - Execução
     - Resultado
     */


    public class ColaboradorAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {

        private string _TipoColaborador;

        public ColaboradorAutorizacaoAttribute(string TipoColaborador = ColaboradorTipoConstant.Comum) 
        {
            this._TipoColaborador = TipoColaborador;
        }

        LoginColaborador _loginColaborador;

        

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _loginColaborador = (LoginColaborador)context.HttpContext.RequestServices.GetService(typeof(LoginColaborador));

            var colaborador = _loginColaborador.GetColaborador(); 

            if (colaborador == null)
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
            }
            else
            {
                if (colaborador.Tipo.Equals(ColaboradorTipoConstant.Comum) && _TipoColaborador.Equals(ColaboradorTipoConstant.Gerente))
                {
                    context.Result = new ForbidResult();
                }

            }
            
        }
    }
}
