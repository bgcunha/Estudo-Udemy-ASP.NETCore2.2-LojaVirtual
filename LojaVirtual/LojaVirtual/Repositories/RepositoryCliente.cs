using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Repositories
{
    public class RepositoryCliente : IRepositoryCliente
    {
        private LojaVirtualContext _context;
        public RepositoryCliente(LojaVirtualContext context)
        {
            this._context = context;
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
        public IEnumerable<Cliente> ObterTodos()
        {
            return _context.Clientes.ToList();
        }
    }
}
