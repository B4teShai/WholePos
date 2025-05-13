using Microsoft.EntityFrameworkCore;
using PosLibrary.Data;
using PosLibrary.Models;

namespace PosLibrary.Services
{
    /// <summary>
    /// POS системд бүтээгдэхүүн болон ангиллыг удирдах үйлчилгээ үзүүлдэг.
    /// </summary>
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// ProductService классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        /// <param name="context">Бүтээгдэхүүний үйлдлийн өгөгдлийн сангийн контекст.</param>
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Бүтээгдэхүүнийг түүний кодоор татаж авна.
        /// </summary>
        /// <param name="code">Хайх бүтээгдэхүүний код.</param>
        /// <returns>Бүтээгдэхүүн олдвол буцаана; эсвэл, null.</returns>
        public async Task<Product?> GetProductByCode(string code)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Code == code);
        }

        /// <summary>
        /// Системээс бүх бүтээгдэхүүнийг татаж авна.
        /// </summary>
        /// <returns>Систем дэх бүх бүтээгдэхүүний жагсаалт.</returns>
        public async Task<List<Product>> GetAllProducts()
        {
     
            return await _context.Products
                .Include(p => p.Category)
                .ToListAsync();
        }

        /// <summary>
        /// Системээс бүх бүтээгдэхүүний ангиллыг татаж авна.
        /// </summary>
        /// <returns>Систем дэх бүх ангиллын жагсаалт.</returns>
        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Системд шинэ бүтээгдэхүүн нэмнэ.
        /// </summary>
        /// <param name="product">Нэмэх бүтээгдэхүүн.</param>
        /// <returns>Бүтээгдэхүүн амжилттай нэмэгдсэн бол true; эсвэл, false.</returns>
        public async Task<bool> AddProduct(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Систем дэх байгаа бүтээгдэхүүнийг шинэчилнэ.
        /// </summary>
        /// <param name="product">Шинэчлэх бүтээгдэхүүн.</param>
        /// <returns>Бүтээгдэхүүн амжилттай шинэчлэгдсэн бол true; эсвэл, false.</returns>
        public async Task<bool> UpdateProduct(Product product)
        {
            try
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Системээс бүтээгдэхүүнийг устгана.
        /// </summary>
        /// <param name="productId">Устгах бүтээгдэхүүний ID.</param>
        /// <returns>Бүтээгдэхүүн амжилттай устгагдсан бол true; эсвэл, false.</returns>
        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Систем дэх бүтээгдэхүүний нөөцийн хэмжээг шинэчилнэ.
        /// </summary>
        /// <param name="productId">Шинэчлэх бүтээгдэхүүний ID.</param>
        /// <param name="stockQuantity">Бүтээгдэхүүний шинэ нөөцийн хэмжээ.</param>
        /// <returns>Нөөцийн хэмжээ амжилттай шинэчлэгдсэн бол true; эсвэл, false.</returns>
        public async Task<bool> UpdateProductStockQuantity(int productId, int stockQuantity)
        {
            try
            {
                var product = await _context.Products.FindAsync(productId);
                if (product != null)
                {
                    product.StockQuantity = stockQuantity;
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
} 