using LojaVirtual.Libraries.Cookie;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace LojaVirtual.Libraries.CarrinhoCompra
{
    public class GerenciarCarrinho
    {
        private string Key = "CARRINHO.COMPRA";

        private GerenciarCookie _gerenciarCookie;

        public GerenciarCarrinho(GerenciarCookie gerenciarCookie)
        {
            _gerenciarCookie = gerenciarCookie;
        }

        public void Cadastriar(ItemCarrinho itemCarrinho)
        {
            var Itens = new List<ItemCarrinho>();

            if (_gerenciarCookie.Existe(Key))
            {
                Itens = Consultar();

                var ItemLocalizado = Itens.SingleOrDefault(x => x.Id == itemCarrinho.Id);

                if (ItemLocalizado != null)
                {
                    Itens.Add(itemCarrinho);
                }
                else 
                {
                    itemCarrinho.Quantidade += itemCarrinho.Quantidade;
                }                   
            }
            else
            {
               
                Itens.Add(itemCarrinho);                
            }

            Salvar(Itens);
        }

        public void Atualizar(ItemCarrinho itemCarrinho)
        {
            var Itens = Consultar();

            var ItemLocalizado = Itens.SingleOrDefault(x => x.Id == itemCarrinho.Id);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.Quantidade = itemCarrinho.Quantidade;
                Salvar(Itens);
            }
        }

        public void Remover(ItemCarrinho itemCarrinho)
        {
            var Itens = Consultar();

            var ItemLocalizado = Itens.SingleOrDefault(x=>x.Id == itemCarrinho.Id);

            if (ItemLocalizado != null) 
            {
                Itens.Remove(ItemLocalizado);
                Salvar(Itens);
            }                
        }

        public List<ItemCarrinho> Consultar()
        {
            if (_gerenciarCookie.Existe(Key))
            {
                var Cookies = _gerenciarCookie.Consultar(Key);

                return JsonConvert.DeserializeObject<List<ItemCarrinho>>(Cookies);
            }

            return new List<ItemCarrinho>();
        }

        public void Salvar(List<ItemCarrinho> itens)
        {
            var Cookie = JsonConvert.SerializeObject(itens);

            _gerenciarCookie.Cadastrar(Key, Cookie);
        }

        public bool Existe()
        {
            return _gerenciarCookie.Consultar(Key) != null;
        }

        public void RemoverTodos()
        {
            _gerenciarCookie.Remover(Key);
        }
    }

    public class ItemCarrinho
    {
        public int? Id { get; set; }
        public int Quantidade { get; set; }
    }
}
