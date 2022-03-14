using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using JAparicio.Ecommerce.Api.Models;

#nullable disable

namespace JAparicio.Ecommerce.Api.Data
{
    public partial class EcommerceDb : DbContext
    {

        public DbSet<Cart> Carrito { get; set; }

        public DbSet<Category> Categoria { get; set; }

        public DbSet<Product> Producto { get; set; }

        public EcommerceDb()
        {

        }

        public EcommerceDb(DbContextOptions<EcommerceDb> options)
            : base(options)
        {

        }
    }
}
