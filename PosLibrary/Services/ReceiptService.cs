using PosLibrary.Models;
using PosLibrary.Data;
using System.Text;

namespace PosLibrary.Services
{
    /// <summary>
    /// POS системд баримт үүсгэх болон хэвлэх үйлчилгээ үзүүлдэг.
    /// </summary>
    public class ReceiptService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// ReceiptService классын шинэ жишээг эхлүүлнэ.
        /// </summary>
        /// <param name="context">Баримтын үйлдлийн өгөгдлийн сангийн контекст.</param>
        public ReceiptService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Борлуулалтын гүйлгээний баримтын форматыг үүсгэнэ.
        /// </summary>
        /// <param name="sale">Баримт үүсгэх борлуулалтын гүйлгээ.</param>
        /// <returns>Баримтын дэлгэрэнгүйг агуулсан форматтай мөрийг буцаана.</returns>
        public string GenerateReceipt(Sale sale)
        {
            var receipt = new StringBuilder();
            
            // Header
            receipt.AppendLine("=====================================");
            receipt.AppendLine("           POS SYSTEM RECEIPT        ");
            receipt.AppendLine("=====================================");
            receipt.AppendLine($"Date: {sale.Date:yyyy-MM-dd HH:mm:ss}");
            receipt.AppendLine($"Receipt #: {sale.Id}");
            receipt.AppendLine($"Cashier: {sale.User?.Username}");
            receipt.AppendLine("-------------------------------------");
            receipt.AppendLine("Item                Qty    Price    Total");
            receipt.AppendLine("-------------------------------------");
            
            // Items
            foreach (var item in sale.Items)
            {
                string itemName = item.Product?.Name ?? "Unknown";
                string itemCode = item.Product?.Code ?? "";
                receipt.AppendLine($"{itemName} ({itemCode})");
                receipt.AppendLine($"         {item.Quantity,3} x {item.UnitPrice,8:C} = {item.TotalPrice,8:C}");
            }
            
            // Totals
            receipt.AppendLine("-------------------------------------");
            receipt.AppendLine($"Subtotal:                    {sale.TotalAmount,8:C}");
            receipt.AppendLine("=====================================");
            receipt.AppendLine("           Thank you!               ");
            receipt.AppendLine("=====================================");
            
            return receipt.ToString();
        }
        
        /// <summary>
        /// Баримтыг файлд хадгална.
        /// </summary>
        /// <param name="receipt">Хадгалах баримтын мөр.</param>
        /// <returns>Хадгалагдсан баримтын файлын замыг буцаана.</returns>
        public string SaveReceipt(string receipt)
        {
            string fileName = $"receipt_{DateTime.Now:yyyyMMddHHmmss}.txt";
            File.WriteAllText(fileName, receipt);
            return fileName;
        }
    }
} 