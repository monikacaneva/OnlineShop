using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Identity;
using EShop.Domain;
namespace EShop.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public EShopApplicationUser? Owner { get; set; }
        public string? OwnerId { get; set; }
    }
}
