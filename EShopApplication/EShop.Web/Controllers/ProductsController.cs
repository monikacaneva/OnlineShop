using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShop.Domain.Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.WebSockets;
using EShop.Domain.DTO;
using EShop.Domain.Identity;
using EShop.Repository;
using EShop.Service.Interface;

namespace EShop.Web.Controllers
{
    
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IShoppingCartService _shoppingCartService;
        public ProductsController(IProductService productService, IShoppingCartService shoppingCartService)
        {
            _productService = productService;
            _shoppingCartService = shoppingCartService;
        }

        // GET: Products
        public IActionResult Index()
        {
            return View(_productService.GetProducts());
        }

        //GET: AddProductToCard
        public IActionResult AddProductToCard(Guid Id)
        {
            var product = _shoppingCartService.getProductInfo(Id);
            if(product!=null)
            {
                return View(product);
            }
            return View();
        }
        //POST: AddProductToCard
        [HttpPost]
        
        public IActionResult AddProductToCard(AddToShoppingCardDto model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result=_shoppingCartService.AddProductToCard(userId, model);
            if(result!=null)
            {
                return RedirectToAction("Index","ShoppingCarts");
            }
            else
            {
                return View(model);
            }
            
        }
        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Id,ProductName,ProductImage,ProductDescription,Price,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                _productService.CreateNewProduct(loggedUser, product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductImage,ProductDescription,Price,Rating")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _productService.UpdateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var product = _productService.GetProductById(id);
            _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _productService.GetProductById(id) != null;
        }
    }
}
