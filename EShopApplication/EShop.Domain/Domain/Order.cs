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
    public class Order : BaseEntity
    {
        public string? UserId { get; set; }
        public EShopApplicationUser? User { get; set; }

        public IEnumerable<ProductInOrder> ProductInOrders { get; set; }
    }
}
