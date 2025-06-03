using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Domain;

namespace EShop.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<ProductInShoppingCart>? AllProducts { get; set; }
        public int TotalPrice { get; set; }
    }
}
