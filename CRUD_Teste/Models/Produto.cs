﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Teste.Models
{
    [Table("Produtos")]
    public class Produto
    {
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.", AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A descrição do produto é obrigatória.", AllowEmptyStrings = false)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Informe o preço do produto.")]
        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
        public decimal Preco { get; set; }

        public string Imagem { get; set; }

        public int CategoriaId { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}