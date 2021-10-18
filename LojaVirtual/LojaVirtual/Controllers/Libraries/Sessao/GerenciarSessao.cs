﻿using Microsoft.AspNetCore.Http;

namespace LojaVirtual.Controllers.Libraries.Sessao
{
    public class GerenciarSessao
    {
        private IHttpContextAccessor _context;
        public GerenciarSessao(IHttpContextAccessor context)
        {
            _context = context;
        }

        public void Cadastrar(string key, string valor)
        {
            _context.HttpContext.Session.SetString(key, valor);
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
                _context.HttpContext.Session.Remove(key);
        }

        public string Consultar(string key) 
        {
            return _context.HttpContext.Session.GetString(key);
        }

        public bool Existe(string key)
        {
            return _context.HttpContext.Session.GetString(key) != null;
            
        }

        public void RemoverTodos() 
        {
            _context.HttpContext.Session.Clear();
        }
    }
}
