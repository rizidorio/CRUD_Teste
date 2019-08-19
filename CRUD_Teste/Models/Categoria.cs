using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_Teste.Models
{
    [Table("Categorias")]
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Display(Name = "Nome da Categoria")]
        public string CategoriaNome { get; set; }

        public List<Produto> produtos { get; set; }
    }
}