using EShop.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Domain;
using EShop.Repository.Interface;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
        }
        public Product CreateNewProduct(string userId, Product product)
        {            
            return _productRepository.Insert(product);
        }

        public Product DeleteProduct(Guid id)
        {
            var product=this.GetProductById(id);
            return _productRepository.Delete(product);
        }

        public Product GetProductById(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetAll().ToList();
        }

        public Product UpdateProduct(Product product)
        {
            return _productRepository.Update(product);
        }
    }
}
