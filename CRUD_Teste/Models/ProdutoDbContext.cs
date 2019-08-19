using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CRUD_Teste.Models
{
    /* Esta classe indica ao EF o mapeamento existente e permite
     o acesso ao banco de dados*/
    public class ProdutoDbContext : DbContext
    {
        public DbSet<Produto> produtos { get; set; }
        public DbSet<Categoria> categorias { get; set; }
    }
}