using System;
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
        public IActionResult Create(ProductFormModel productForm)
        {
            if (!ModelState.IsValid)
            {
                productForm.Categories = this.productService.GetProductCategories();
                return View(productForm);
            }

            this.productService.Add(productForm);
            TempData[GlobalMessageKey] = "The product was added successfully.";
            return RedirectToAction("Index", "Home");
        }
    }
}
