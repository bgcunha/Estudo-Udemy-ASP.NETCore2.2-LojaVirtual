using LojaVirtual.Libraries.Cookie;
using LojaVirtual.Models.ProdutoAgregador;
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
               
        public void Cadastrar(ProdutoItem itemCarrinho)
        {
            List<ProdutoItem> Lista;
            if (_gerenciarCookie.Existe(Key))
            {
                Lista = Consultar();
                var ItemLocalizado = Lista.SingleOrDefault(a => a.Id == itemCarrinho.Id);

                if (ItemLocalizado == null)
                {
                    Lista.Add(itemCarrinho);
                }
                else
                {
                    ItemLocalizado.QuantidadeProdutoCarrinho = ItemLocalizado.QuantidadeProdutoCarrinho + 1;
                }
            }
            else
            {
                Lista = new List<ProdutoItem>();
                Lista.Add(itemCarrinho);
            }

            Salvar(Lista);
        }

        public void Atualizar(ProdutoItem itemCarrinho)
        {
            var Itens = Consultar();

            var ItemLocalizado = Itens.SingleOrDefault(x => x.Id == itemCarrinho.Id);

            if (ItemLocalizado != null)
            {
                ItemLocalizado.QuantidadeProdutoCarrinho = itemCarrinho.QuantidadeProdutoCarrinho;
                Salvar(Itens);
            }
        }

        public void Remover(ProdutoItem itemCarrinho)
        {
            var Itens = Consultar();

            var ItemLocalizado = Itens.SingleOrDefault(x=>x.Id == itemCarrinho.Id);

            if (ItemLocalizado != null)
            {
                Itens.Remove(ItemLocalizado);
                Salvar(Itens);
            }
        }

        public List<ProdutoItem> Consultar()
        {
            if (_gerenciarCookie.Existe(Key))
            {
                string valor = _gerenciarCookie.Consultar(Key);
                return JsonConvert.DeserializeObject<List<ProdutoItem>>(valor);
            }
            
            return new List<ProdutoItem>();            
        }

        public void Salvar(List<ProdutoItem> itens)
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
}
