using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreDapperCrudDemo.Domain;
using AspNetCoreDapperCrudDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreDapperCrudDemo.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productService.GetAllProducts());
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _productService.GetProductById(id));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _productService.CreateProductAsync(product);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            return View(await _productService.GetProductById(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dbProduct = await _productService.GetProductById(id);
                    if (await TryUpdateModelAsync<Product>(dbProduct))
                    {
                        await _productService.UpdateProductAsync(dbProduct);
                        return RedirectToAction(nameof(Index));
                    } 
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dbProduct = await _productService.GetProductById(id);
                if (dbProduct != null)
                {
                    await _productService.DeleteProductAsync(dbProduct); 
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to delete. " +
                                             "Try again, and if the problem persists " +
                                             "see your system administrator.");
            }

            return RedirectToAction(nameof(Index));
        }
    } 
}
