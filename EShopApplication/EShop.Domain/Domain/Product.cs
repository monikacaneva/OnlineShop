﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public string ProductDescription { get; set; }
        public int Price { get; set; }
        public int Rating { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCart { get; set; }

        public ICollection<ProductInOrder>? ProductInOrders { get; set; }
    }
}
