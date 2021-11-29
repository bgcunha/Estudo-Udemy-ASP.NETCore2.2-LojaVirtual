using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace LojaVirtual.Controllers.Libraries.Middleware
{
    public class ValidateAntiForgeryTokenMiddleware
    {
        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

        public ValidateAntiForgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            var Cabcalho = context.Request.Headers["x-requested-with"];
            var Ajax = (Cabcalho == "XMLHttpRequest");
            if (HttpMethods.IsPost(context.Request.Method) && !(context.Request.Form.Files.Count > 0 && Ajax))
            {                
                await _antiforgery.ValidateRequestAsync(context);
            }

            await _next(context);
        }
    }
}
