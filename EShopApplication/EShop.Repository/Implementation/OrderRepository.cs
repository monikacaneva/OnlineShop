using EShop.Domain.Domain;
using EShop.Domain.Identity;
using EShop.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace EShop.Repository.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly ApplicationDbContext context;
        private DbSet<Order> entities;
        //string errorMessage = string.Empty;

        public OrderRepository(ApplicationDbContext context, IRepository<Order> orderRepository)
        {
            this.context = context;
            entities = context.Set<Order>();
            _orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(u=>u.User)
                .Include(z=>z.ProductInOrders)
                .Include("ProductInOrders.OrderedProduct")
                .ToList();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return entities
                .Include(u => u.User)
                .Include(z => z.ProductInOrders)
                .Include("ProductInOrders.OrderedProduct")
                .SingleOrDefaultAsync(z=>z.Id==model.Id).Result;
        }
    }
}
