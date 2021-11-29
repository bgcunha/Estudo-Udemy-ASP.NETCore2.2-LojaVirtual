using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LojaVirtual.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        [Display(Name = "Preço")] 
        public decimal Valor { get; set; }
        public int Quantidade { get; set; }

        //Frete
        public double Peso { get; set; }
        public int largura { get; set; }
        public int Altura { get; set; }
        public int Profundidade { get; set; }

        //Banco de dados - Relacionamento entre tabelas
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        //POO - Associações entre objetos
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        public virtual ICollection<Imagem> Imagens { get; set; }
    }
}
