using LojaVirtual.Controllers.Libraries.Sessao;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Controllers.Libraries.Login
{
    public class LoginColaborador
    {
        private string _key = "LOGIN.COLABORADOR";
        private GerenciarSessao _gereciarSessao;

        public LoginColaborador(GerenciarSessao sessao)
        {
            _gereciarSessao = sessao;
        }

        public void SetColaborador(Colaborador Colaborador)
        {
            var ColaboradorJSONString = JsonConvert.SerializeObject(Colaborador);
            _gereciarSessao.Cadastrar(_key, ColaboradorJSONString);
        }

        public Colaborador GetColaborador()
        {
            if (_gereciarSessao.Existe(_key))
            {
                var ColaboradorString = _gereciarSessao.Consultar(_key);

                return JsonConvert.DeserializeObject<Colaborador>(ColaboradorString); ;
            }

            return null;
            
        }

        public void Logout()
        {
            _gereciarSessao.RemoverTodos();
        }
    }
}
