using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EShopAdminApplication.Models
{
    public class ShoppingCart 
    {
        public virtual ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; }
        public EShopApplicationUser Owner { get; set; }
        public string OwnerId { get; set; }
    }
}
