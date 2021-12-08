using LojaVirtual.Libraries.Lang;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.Models.ProdutoAgregador
{
    public class Produto
    {
        public int Id { get; set; }

        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        public string Nome { get; set; }

        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Preço")] 
        public decimal Valor { get; set; }

        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Range(0, 1000000, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Quantidade { get; set; }

        //Frete Correio
        [JsonIgnore]
        [Range(0.001, 30, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public double Peso { get; set; }

        [JsonIgnore]
        [Range(11, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Largura { get; set; }

        [JsonIgnore]
        [Range(2, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Altura { get; set; }

        [JsonIgnore]
        [Range(16, 105, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E006")]
        public int Comprimento { get; set; }

        //Banco de dados - Relacionamento entre tabelas
        [JsonIgnore]
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E001")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        //POO - Associações entre objetos
        //[JsonIgnore]
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        [JsonIgnore] 
        public virtual ICollection<Imagem> Imagens { get; set; }
    }
}
