
using EShop.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain;

namespace EShop.Repository
{
    public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }
        public virtual DbSet<EShop.Domain.Domain.Product> Products { get; set; }
        public virtual DbSet<EShop.Domain.Domain.ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<EShop.Domain.Domain.ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public virtual DbSet<EShop.Domain.Domain.ProductInOrder> ProductInOrders { get; set; }
        public virtual DbSet<EShop.Domain.Domain.Order> Orders { get; set; }
    }
}

