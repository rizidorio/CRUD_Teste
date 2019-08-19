using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Teste.Models
{
    /* Classe utilizada para inclusão de produtos, será usada apenas
     na view Create*/
    public class ProdutoViewModel
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória.", AllowEmptyStrings = false)]
        [Display(Name = "Descrição do Produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto.", AllowEmptyStrings = false)]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "Selecione uma categoria", AllowEmptyStrings = false)]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Imagem")]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}