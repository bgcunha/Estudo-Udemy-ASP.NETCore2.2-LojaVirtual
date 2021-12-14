using LojaVirtual.Libraries.Seguranca;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace LojaVirtual.Libraries.Cookie
{
    public class GerenciarCookie
    {
        private IHttpContextAccessor _context; 
        private IConfiguration _configuration;

        public GerenciarCookie(IHttpContextAccessor context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public void Cadastrar(string key, string valor)
        {
            CookieOptions Options = new CookieOptions() { Expires = DateTime.Now.AddDays(7), IsEssential = true};


            var ValorCrypt = StringCipher.Encrypt(valor, _configuration.GetValue<string>("KeyCrypt"));

            _context.HttpContext.Response.Cookies.Append(key, ValorCrypt, Options);
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
            var ValorCrypt = _context.HttpContext.Request.Cookies[key];

            var Valor = StringCipher.Decrypt(ValorCrypt, _configuration.GetValue<string>("KeyCrypt"));

            return Valor;
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
