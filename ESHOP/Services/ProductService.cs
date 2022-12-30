using ESHOP.Data;
using ESHOP.Models;
using ESHOP.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace ESHOP.Services;

public class ProductService : IProductService
{
    private readonly EShopContext _context;

    public ProductService(EShopContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts() => _context.Products.ToList();

    public List<Category> GetCategories() => _context.Categories.ToList();

    public List<Product> GetCategoryProducts(int id)
    {
        return _context.CategoryToProducts
            .Where(p => p.CategoryId == id)
            .Include(p => p.Product)
            .Select(p => p.Product)
            .ToList();
    }

    public List<Category> GetProductCategories(int id)
    {
        return _context.Products
            .Where(p => p.Id == id)
            .SelectMany(c => c.CategoryToProducts)
            .Select(ca => ca.Category)
            .ToList();
    }

    public Product GetProductIncludeItem(int id)
    {
        return _context.Products
            .Include(p => p.Item)
            .SingleOrDefault(p => p.Id == id);
    }

    public void InsertItem(Item item) => _context.Items.Add(item);

    public void InsertProduct(Product product) => _context.Products.Add(product);

    public void InsertCategoryToProduct(CategoryToProduct categoryToProduct) =>
        _context.CategoryToProducts.Add(categoryToProduct);

    public void Save() => _context.SaveChanges();

    public Product GetProductById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);


    public Item GetItemById(int id) => _context.Items.First(i => i.Id == id);

    public void DeleteProduct(Product product) => _context.Products.Remove(product);

    public void DeleteItem(Item item) => _context.Items.Remove(item);

    public ProductViewModel GetProduct(int id)
    {
        return _context.Products
            .Include(p => p.Item)
            .Where(p => p.Id == id)
            .Select(s => new ProductViewModel()
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Item.Price,
                Description = s.Description,
                QuantityInStock = s.Item.QuantityInStock,
            }).FirstOrDefault();
    }

    public List<int> GetProductGroups(int id)
    {
        return _context.CategoryToProducts.Where(p => p.ProductId == id)
            .Select(c => c.CategoryId).ToList();
    }

    public void DeleteCategoryToProducts(int id)
    {
        _context.CategoryToProducts.Where(p => p.ProductId == id).ToList()
            .ForEach(p => _context.CategoryToProducts.Remove(p));
    }

    public void EditCategoryToProducts(List<int> selectedGroups, int productId)
    {
        if (selectedGroups.Any() && selectedGroups.Count > 0)
        {
            foreach (var gr in selectedGroups)
            {
                _context.CategoryToProducts.Add(new CategoryToProduct()
                {
                    CategoryId = gr,
                    ProductId = productId,
                });
                ;
            }

            Save();
        }
    }

    public IEnumerable<Category> GetAllCategories() => _context.Categories.ToList();

    public IEnumerable<ShowProductViewModel> GetCategoryForSidebar()
    {
        return _context.Categories.Select(c => new ShowProductViewModel()
        {
            Name = c.Name,
            CategoryId = c.Id,
            ProductCount = c.CategoryToProducts.Count(p => p.CategoryId == c.Id)
        }).ToList();
    }

    public void InsertCategory(Category category) => _context.Categories.Add(category);

    public Category GetCategoryById(int id) => _context.Categories.FirstOrDefault(m => m.Id == id);

    public void UpdateCategory(Category category) => _context.Attach(category).State = EntityState.Modified;

    public bool IsCategoryExists(int id) => _context.Categories.Any(e => e.Id == id);

    public void RemoveCategoryRelations(int categoryId)
    {
        _context.CategoryToProducts.Where(p => p.CategoryId == categoryId).ToList()
            .ForEach(p => _context.CategoryToProducts.Remove(p));
    }

    public void RemoveCategory(Category category) => _context.Categories.Remove(category);

    public IIncludableQueryable<Product, Item> GetProductsIncludeItems() => _context.Products.Include(p => p.Item);
}
