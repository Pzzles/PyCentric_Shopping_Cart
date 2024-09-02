using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using PyCentric_Shopping_Cart.Models;
using PyCentric_Shopping_Cart.Service;

namespace PyCentric_Shopping_Cart.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShoppingCartService _cartService;

        public AccountController(ShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        public IActionResult Home()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = FetchProducts();

   

            // Return the HTML content as a JSON object
            return PartialView("Products", products);
        }

        private static List<Product> FetchProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name = "Apple", Price = 1.99m, Quantity = 10, Priority = 1 },
                new Product { Id = 3, Name = "Kota", Price = 8.99m, Quantity = 5, Priority = 2 },
                new Product { Id = 4, Name = "Mince Meat", Price = 6.99m, Quantity = 20, Priority = 2 },
                new Product { Id = 5, Name = "Sardines", Price = 12.99m, Quantity = 12, Priority = 2 },
                new Product { Id = 6, Name = "Bread", Price = 2.99m, Quantity = 8, Priority = 1 },
                new Product { Id = 7, Name = "Eggs", Price = 1.99m, Quantity = 25, Priority = 1 },
                new Product { Id = 9, Name = "Steak", Price = 19.99m, Quantity = 30, Priority = 3 },
                new Product { Id = 10, Name = "Prawns", Price = 4.99m, Quantity = 7, Priority = 3 },
                new Product { Id = 11, Name = "Wors", Price = 4.99m, Quantity = 3, Priority = 2 },
                new Product { Id = 13, Name = "Chicken feet", Price = 5.99m, Quantity = 11, Priority = 2 },
                new Product { Id = 14, Name = "Tin fish", Price = 7.99m, Quantity = 14, Priority = 1 },
                new Product { Id = 15, Name = "Cabbage", Price = 99.99m, Quantity = 9, Priority = 3 }
            };
        }

        [HttpGet]
        public IActionResult AddToCart(Product product)
        {
      
            if (product != null)
            {
                _cartService.AddItem(product);
                return RedirectToAction("GetProducts");
            }
            return Json(new { success = false, message = "Failed to add this item to cart." });
        }

        [HttpGet]
        public IActionResult ViewCart()
        {
            var items = _cartService.Items;
            return PartialView("Cart", items);
        }

        [HttpGet]
        public IActionResult SortCartItems(string sortOrder)
        {
            var items = _cartService.Items;
            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder == "asc")
                {
                    items = items.OrderBy(item => item.Id).ToList();
                }
                else if (sortOrder == "desc")
                {
                    items = items.OrderByDescending(item => item.Id).ToList();
                }

                return PartialView("Cart", items);
            }
                return PartialView("Cart", items);
        }
    }
}
