using System.Collections.Generic;
using System.Threading.Tasks;
using IvarsSykkelsjappe.Models.Products;

namespace IvarsSykkelsjappe.Services.Products
{
    public interface IProductService
    {
        Task AddAsync(ProductFormModel product);

        IEnumerable<ProductCategoryViewModel> GetProductCategories();

        IEnumerable<ProductViewModel> GetAll();

        Task DeleteAsync(int id);
    }
}
