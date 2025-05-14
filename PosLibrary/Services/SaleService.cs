using Microsoft.EntityFrameworkCore;
using PosLibrary.Data;
using PosLibrary.Models;

namespace PosLibrary.Services
{
    /// <summary>
    /// POS системд борлуулалтын гүйлгээг удирдах үйлчилгээ үзүүлдэг.
    /// </summary>
    public class SaleService
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;

        /// <summary>
        /// SaleService классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        /// <param name="context">Борлуулалтын үйлдлийн өгөгдлийн сангийн контекст.</param>
        /// <param name="productService">Бүтээгдэхүүнийг удирдах үйлчилгээ.</param>
        public SaleService(ApplicationDbContext context, ProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        /// <summary>
        /// SaleService классын шинэ жишээг эхлүүлнэ, ProductService-ийг дотооддоо үүсгэнэ.
        /// </summary>
        /// <param name="context">Борлуулалтын үйлдлийн өгөгдлийн сангийн контекст.</param>
        public SaleService(ApplicationDbContext context)
        {
            _context = context;
            _productService = new ProductService(context);
        }

        /// <summary>
        /// Шинэ борлуулалтын гүйлгээ үүсгэж, бүтээгдэхүүний тоо хэмжээг шинэчилнэ.
        /// </summary>
        /// <param name="sale">Үүсгэх борлуулалтын гүйлгээ.</param>
        /// <returns>Үүсгэсэн борлуулалтын гүйлгээг буцаана.</returns>
        public async Task<Sale> CreateSale(Sale sale)
        {
            foreach (var item in sale.Items)
            {
                var product = await _context.Products.FindAsync(item.ProductId);
                if (product != null)
                {
                    product.StockQuantity -= item.Quantity;
                    await _productService.UpdateProductStockQuantity(product.Id, product.StockQuantity);
                }
            }

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();
            return sale;
        }

        /// <summary>
        /// Тодорхой өдрийн бүх борлуулалтыг татаж авна.
        /// </summary>
        /// <param name="date">Борлуулалтыг татаж авах өдөр.</param>
        /// <returns>Тухайн өдрийн борлуулалтын жагсаалт.</returns>
        public async Task<List<Sale>> GetSalesByDate(DateTime date)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Include(s => s.User)
                .Where(s => s.CreatedAt.Date == date.Date)
                .ToListAsync();
        }

        /// <summary>
        /// Тодорхой ID-тай борлуулалтыг татаж авна.
        /// </summary>
        /// <param name="saleId">Татаж авах борлуулалтын ID.</param>
        /// <returns>Борлуулалт олдвол буцаана; эсвэл, null.</returns>
        public async Task<Sale?> GetSaleById(int saleId)
        {
            return await _context.Sales
                .Include(s => s.Items)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == saleId);
        }

        /// <summary>
        /// Борлуулалтын нийт дүнг тооцоолно.
        /// </summary>
        /// <param name="sale">Нийт дүнг тооцоолох борлуулалт.</param>
        /// <returns>Борлуулалтын нийт дүнг буцаана.</returns>
        public decimal CalculateTotal(Sale sale)
        {
            decimal total = 0;
            foreach (var item in sale.Items)
            {
                total += item.UnitPrice * item.Quantity;
            }
            return total;
        }

        /// <summary>
        /// Хэрэглэгчид өгөх хариултыг тооцоолно.
        /// </summary>
        /// <param name="sale">Хариултыг тооцоолох борлуулалт.</param>
        /// <returns>Өгөх хариултын дүнг буцаана.</returns>
        public decimal CalculateChange(Sale sale)
        {
            return sale.AmountPaid - sale.Total;
        }
    }
} 