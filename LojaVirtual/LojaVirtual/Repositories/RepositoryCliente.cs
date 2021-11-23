using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private LojaVirtualContext _context;
        private IConfiguration _config;

        public RepositoryCliente(LojaVirtualContext context, IConfiguration configuration)
        {
            this._context = context;
            this._config = configuration;
        }

        public Cliente Login(string email, string senha)
        {
            var cliente = _context.Clientes.Where(c => c.Email == email && c.Senha == senha).FirstOrDefault();

            return cliente;
        }

        public void Cadastrar(Cliente model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Atualizar(Cliente model)
        {
            _context.Update(model);
            _context.SaveChanges();
        }
        
        public void Excluir(int id)
        {
            var cliente = ObterPorId(id);
            _context.Remove(cliente);
            _context.SaveChanges();
        }
       
        public Cliente ObterPorId(int id)
        {
            return _context.Clientes.Find(id);
        }
        public IPagedList<Cliente> ObterTodos(int? pagina, string pesquisa)
        {
            var numeroPagina = pagina ?? 1;

            
            if (string.IsNullOrEmpty(pesquisa))
            {
                return _context.Clientes.ToPagedList<Cliente>(numeroPagina, _config.GetValue<int>("RegistrosPorPagina"));
            }

            pesquisa = pesquisa.Trim();
            return _context.Clientes.Where(x => x.Nome.Contains(pesquisa) || x.Email.Contains(pesquisa)).ToPagedList<Cliente>(numeroPagina, _config.GetValue<int>("RegistrosPorPagina"));
            
        }
    }
}
