using EShop.Domain.Domain;
using EShop.Domain.DTO;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Product> productRepository, IRepository<Order> orderRepository, IRepository<ProductInOrder> productInOrderRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
        }
        
        public ShoppingCart AddProductToCard(string userId, AddToShoppingCardDto model)
        {
            if (userId != null)
            {
                var loggedUser = _userRepository.Get(userId);
                var userCart = loggedUser.UserCart;
                var selectedProduct = _productRepository.Get(model.SelectedProductId);
                if (selectedProduct != null && userCart!=null)
                {
                    userCart.ProductInShoppingCarts.Add(new ProductInShoppingCart
                    {
                        Product=selectedProduct,
                        ProductId=selectedProduct.Id,
                        ShoppingCart=userCart,
                        ShoppingCartId=userCart.Id,
                        Quantity=model.Quantity
                    });
                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid Id)
        {
            if(userId!=null)
            {
                var loggedUser = _userRepository.Get(userId);
                var productToDelete = loggedUser.UserCart.ProductInShoppingCarts.First(p => p.ProductId.Equals(Id));
                loggedUser.UserCart.ProductInShoppingCarts.Remove(productToDelete);
                _shoppingCartRepository.Update(loggedUser.UserCart);
                return true;
            }
            return false;
        }

        public AddToShoppingCardDto getProductInfo(Guid id)
        {
            var selectedproduct=_productRepository.Get(id);
            if (selectedproduct != null)
            {
                var model=new AddToShoppingCardDto
                {
                    SelectedProductId=selectedproduct.Id,
                    SelectedProductName=selectedproduct.ProductName,
                    Quantity=1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO GetShoppingCartDetails(string userId)
        {
            if(userId!=null && !userId.IsNullOrEmpty())
            {
                var loggedUser = _userRepository.Get(userId);
                var allProducts=loggedUser.UserCart.ProductInShoppingCarts.ToList();
                var totalPrice = 0;
                foreach(var item in allProducts)
                {
                    totalPrice += item.Quantity * item.Product.Price;
                }
                var model = new ShoppingCartDTO
                {
                    AllProducts=allProducts,
                    TotalPrice=totalPrice
                };
                return model;
            }
            return new ShoppingCartDTO
            {
                AllProducts=new List<ProductInShoppingCart>(),
                TotalPrice=0
            };
        }

        public bool orderProducts(string userId)
        {
            if(userId!=null && !userId.IsNullOrEmpty())
            {
                var loggedUser=_userRepository.Get(userId);
                var usercart=loggedUser.UserCart;
                var userorder = new Order
                {
                    Id=Guid.NewGuid(),
                    UserId=userId,
                    User=loggedUser
                };
                _orderRepository.Insert(userorder);

                var productInOrder=usercart.ProductInShoppingCarts
                    .Select(z=> new ProductInOrder
                    {
                        Order=userorder,
                        ProductId=z.ProductId,
                        OrderId=userorder.Id,
                        OrderedProduct=z.Product,
                        Quantity=z.Quantity
                    }).ToList();
                _productInOrderRepository.InsertMany(productInOrder);
                usercart.ProductInShoppingCarts.Clear();
                _shoppingCartRepository.Update(usercart);
                return true;
            }
            return false;
        }
    }
}
