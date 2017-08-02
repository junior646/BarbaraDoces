using System;
using System.ComponentModel.DataAnnotations;

namespace BarbaraDoces.Models
{

    public class TipoProdutoViewModel
    {
        [Key]
        public int Id_TipoProduto { get; set; }

        [Required]
        [Display(Name = "Tipo de Produto: ")]
        [StringLength(75, ErrorMessage = "O nome Tipo do Produto deve conter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Desc_TipoProduto { get; set; }

        [Display(Name = "Disponibilidade do Produto: ")]
        public bool Ativo { get; set; }
    }

    public class ProdutoViewModel
    {
        [Key]
        public int Id { get; set; }
        public int Id_Produto { get; set; }

        [Required]
        [Display(Name = "Produto: ")]
        [StringLength(75, ErrorMessage = "O nome do Produto deve conter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Nome_Produto { get; set; }

        [Display(Name = "Descrição: ")]
        [StringLength(75, ErrorMessage = "A descrição do Produto deve conter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string Desc_Produto { get; set; }

        [Display(Name = "Validade: ")]
        public string Vali_Produto { get; set; }

        [Display(Name = "Disponibilidade do Produto: ")]
        public bool Ativo { get; set; }

        public byte[] Imagem_Produto { get; set; }
        public PrecoViewModel Preco { get; set; }
        public SaborViewModel Sabor { get; set; }
        public TipoProdutoViewModel TipoProduto { get; set; }
    }
    public class PrecoViewModel
    {
        [Key]
        public int Id_Preco { get; set; }

        [Required]
        [Display(Name = "Valor do Produto: ")]
        public double Valor_Produto { get; set; }
    }
    public class SaborViewModel
    {
        [Key]
        public int Id_Sabor { get; set; }
        [Required]
        [Display(Name = "Sabor: ")]
        public string Desc_Sabor { get; set; }

        [Display(Name = "Disponibilidade do sabor: ")]
        public bool Ativo { get; set; }
    }

    public class NovidadeViewModel
    {
        [Key]
        public int IdNovidade { get; set; }

        [Required]
        [Display(Name = "Titulo: ")]
        [StringLength(75, ErrorMessage = "O título deve conter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string TituloNovidade { get; set; }

        [Required]
        [Display(Name = "Descrição: ")]
        [StringLength(75, ErrorMessage = "A descrição deve ter no mínimo {2} caracteres.", MinimumLength = 2)]
        public string DescNovidade { get; set; }

        [Required(ErrorMessage = "Preencha a Data de cadastro desta novidade.")]
        [Display(Name = "Data de Lançamento: ")]
        [DataType(DataType.DateTime, ErrorMessage = "Data incorreta")]
        public DateTime DtNovidade { get; set; }

        public ProdutoViewModel Produto { get; set; }
    }
}