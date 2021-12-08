using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Cookie
{
    public class GerenciarCookie
    {
        private IHttpContextAccessor _context;
        public GerenciarCookie(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string key, string valor)
        {
            CookieOptions Options = new CookieOptions() { Expires = DateTime.Now.AddDays(7)};
            
            _context.HttpContext.Response.Cookies.Append(key, valor, Options);
        }

        public void Atualizar(string key, string valor)
        {
            if (Existe(key))
                Remover(key);

            Cadastrar(key, valor);
        }

        public void Remover(string key)
        {
            if (Existe(key))
                _context.HttpContext.Response.Cookies.Delete(key);
        }

        public string Consultar(string key)
        {
            return _context.HttpContext.Request.Cookies[key];
        }

        public bool Existe(string key)
        {
            return _context.HttpContext.Request.Cookies[key] != null;

        }

        public void RemoverTodos()
        {
            var Cookies = _context.HttpContext.Request.Cookies.ToList();
            foreach (var item in Cookies)
            {
                Remover(item.Key);
            }
           
        }
    }
}
