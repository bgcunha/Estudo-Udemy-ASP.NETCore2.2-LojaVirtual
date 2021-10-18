using LojaVirtual.Controllers.Libraries.Sessao;
using LojaVirtual.Models;
using Newtonsoft.Json;

namespace LojaVirtual.Controllers.Libraries.Login
{
    public class LoginCliente
    {
        private string _key = "LOGIN.CLIENTE";
        private GerenciarSessao _gereciarSessao;

        public LoginCliente(GerenciarSessao sessao)
        {
            _gereciarSessao = sessao;
        }

        public void SetCliente(Cliente cliente)
        {
            var clienteJSONString = JsonConvert.SerializeObject(cliente);
            _gereciarSessao.Cadastrar(_key, clienteJSONString);
        }

        public Cliente GetCliente()
        {
            if (_gereciarSessao.Existe(_key))
            {
                var clienteString = _gereciarSessao.Consultar(_key);

                return JsonConvert.DeserializeObject<Cliente>(clienteString); ;
            }

            return null;
            
        }

        public void Logout()
        {
            _gereciarSessao.RemoverTodos();
        }
    }
}
