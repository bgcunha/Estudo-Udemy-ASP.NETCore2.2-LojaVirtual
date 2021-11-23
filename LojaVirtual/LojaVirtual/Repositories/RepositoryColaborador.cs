
using LojaVirtual.Database;
using LojaVirtual.Models;
using LojaVirtual.Models.Constants;
using LojaVirtual.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace LojaVirtual.Repositories
{
    public class RepositoryColaborador : IRepositoryColaborador
    {
        private LojaVirtualContext _context;
        private IConfiguration _config;


        public RepositoryColaborador(LojaVirtualContext context, IConfiguration configuration)
        {
            this._context = context;
            this._config = configuration;
        }

        public Colaborador Login(string email, string senha)
        {
            var colaborador = _context.Colaboradores.Where(c => c.Email == email && c.Senha == senha).FirstOrDefault();

            return colaborador;
        }


        public void Cadastrar(Colaborador model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }


        public void Atualizar(Colaborador model)
        {
            _context.Update(model);
            _context.Entry(model).Property(x => x.Senha).IsModified = false;
            _context.SaveChanges();
        }

        public void AtualizarSenha(Colaborador model)
        {
            _context.Update(model);
            _context.Entry(model).Property(x => x.Nome).IsModified = false;
            _context.Entry(model).Property(x => x.Email).IsModified = false;
            _context.Entry(model).Property(x => x.Tipo).IsModified = false;
            _context.SaveChanges();
        }


        public void Excluir(int id)
        {
            var colaborador = ObterPorId(id);
            _context.Remove(colaborador);
            _context.SaveChanges();
        }


        public Colaborador ObterPorId(int id)
        {
            return _context.Colaboradores.Find(id);
        }

        //public IEnumerable<Colaborador> ObterTodos()
        //{
        //    return _context.Colaboradores.ToList();
        //}

        public List<Colaborador> ObterPorEmail(string email)
        {
            return _context.Colaboradores.Where(X => X.Email.Equals(email)).AsNoTracking().ToList();
        }

        public IPagedList<Colaborador> ObterTodos(int? pagina)
        {
            var numeroPagina = pagina ?? 1;

            return _context.Colaboradores.Where(x => x.Tipo != ColaboradorTipoConstant.Gerente).ToPagedList<Colaborador>(numeroPagina, _config.GetValue<int>("RegistrosPorPagina"));

        }
    }
}
