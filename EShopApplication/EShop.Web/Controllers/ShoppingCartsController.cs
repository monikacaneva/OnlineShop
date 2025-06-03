using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain;
using System.Security.Claims;
using EShop.Domain.DTO;
using Microsoft.CodeAnalysis;
using EShop.Domain.Identity;
using EShop.Repository;
using EShop.Service.Interface;

namespace EShop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            return View(_shoppingCartService.GetShoppingCartDetails(userId));
        }
          
        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteProductFromCart(Guid productId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            var result = _shoppingCartService.deleteFromShoppingCart(userId, productId);

            return RedirectToAction("Index", "ShoppingCarts");
        }

        
        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=_shoppingCartService.orderProducts(userId);

                return RedirectToAction("Index", "ShoppingCarts");
        }
    }
}
