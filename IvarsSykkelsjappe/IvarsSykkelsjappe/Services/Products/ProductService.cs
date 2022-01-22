using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Data;
using IvarsSykkelsjappe.Data.Models;
using IvarsSykkelsjappe.Models.Products;

namespace IvarsSykkelsjappe.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddAsync(ProductFormModel product)
        {
            var productData = new Product
            {
                Brand = product.Brand,
                Model = product.Model,
                Price = product.Price,
                ProductNumber = product.ProductNumber,
                Description = product.Description,
                Quantity = product.Quantity,
                ImageUrl = product.ImageUrl,
                ProductCategoryId = product.ProductCategoryId
            };

            await this.dbContext.Products.AddAsync(productData);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = this.dbContext.Products.Find(id);
            this.dbContext.Products.Remove(product);
            await this.dbContext.SaveChangesAsync();
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            var products = this.dbContext
                .Products
                .Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    ProductCategory = x.ProductCategory.Name,
                    Brand = x.Brand,
                    Model = x.Model,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity
                })
                .OrderByDescending(x => x.ProductCategory)
                .ToList();

            return products;
        }

        public IEnumerable<ProductCategoryViewModel> GetProductCategories()
            => this.dbContext
                .ProductsCategories
                .Select(x => new ProductCategoryViewModel
                {
                    ProductCategoryId = x.Id,
                    Name = x.Name
                })
                .ToList();
    }
}
