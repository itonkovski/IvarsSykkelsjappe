using System;
using System.Collections.Generic;
using IvarsSykkelsjappe.Models.Products;

namespace IvarsSykkelsjappe.Services.Products
{
    public interface IProductService
    {
        void Add(ProductFormModel product);

        IEnumerable<ProductCategoryViewModel> GetProductCategories();

        IEnumerable<ProductViewModel> GetAll();

        void Delete(int id);
    }
}
