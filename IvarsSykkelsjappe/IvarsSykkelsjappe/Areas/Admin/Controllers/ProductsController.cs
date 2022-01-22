using System;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Products;
using IvarsSykkelsjappe.Services.Products;
using Microsoft.AspNetCore.Mvc;

namespace IvarsSykkelsjappe.Areas.Admin.Controllers
{
    using static WebConstants;

    public class ProductsController : AdminController
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        public IActionResult Create()
        {
            return View(new ProductFormModel
            {
                Categories = this.productService.GetProductCategories()
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductFormModel productForm)
        {
            if (!ModelState.IsValid)
            {
                productForm.Categories = this.productService.GetProductCategories();
                return View(productForm);
            }

            await this.productService.AddAsync(productForm);
            TempData[GlobalMessageKey] = "The product was added successfully.";
            return RedirectToAction(nameof(AllProducts));
        }

        public IActionResult AllProducts()
        {
            var products = this.productService.GetAll();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.productService.DeleteAsync(id);
            TempData[GlobalMessageKey] = "The product was deleted successfully.";
            return RedirectToAction(nameof(AllProducts));
        }
    }
}
