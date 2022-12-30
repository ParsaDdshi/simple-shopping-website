using ESHOP.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace ESHOP.Services.Interfaces;

public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product GetProductIncludeItem(int id);
    List<Category> GetProductCategories(int id);
    List<Product> GetCategoryProducts(int id);
    List<Category> GetCategories();
    void InsertItem(Item item);
    void InsertProduct(Product product);
    void Save();
    void InsertCategoryToProduct(CategoryToProduct categoryToProduct);
    Product GetProductById(int id);
    Item GetItemById(int id);
    void DeleteProduct(Product product);
    void DeleteItem(Item item);
    ProductViewModel GetProduct(int id);
    List<int> GetProductGroups(int id);
    void DeleteCategoryToProducts(int id);
    void EditCategoryToProducts(List<int> selectedGroups, int productId);
    IEnumerable<Category> GetAllCategories();
    IEnumerable<ShowProductViewModel> GetCategoryForSidebar();
    void InsertCategory(Category category);
    Category GetCategoryById(int id);
    void UpdateCategory(Category category);
    bool IsCategoryExists(int id);
    void RemoveCategoryRelations(int categoryId);
    void RemoveCategory(Category category);
    IIncludableQueryable<Product, Item> GetProductsIncludeItems();
}