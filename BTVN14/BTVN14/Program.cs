// See https://aka.ms/new-console-template for more information
using EProduct.DataAccess.NetCore.DTO;
using EProduct.DataAccess.NetCore.Repositories;
using OfficeOpenXml;
using System.Data;

Console.OutputEncoding = System.Text.Encoding.UTF8;

bool continueProgram = true;
ProductRepository productRepo = new ProductRepository();
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

while (continueProgram)
{
    Console.WriteLine("Chọn hành động:");
    Console.WriteLine("1. Thêm sản phẩm vào kho");
    Console.WriteLine("2. Kiểm tra thông tin tồn kho");
    Console.WriteLine("3. Thoát");

    Console.Write("Nhập lựa chọn của bạn: ");
    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.WriteLine("Nhập thông tin sản phẩm mới:");
            Console.Write("Tên sản phẩm: ");
            string productName = Console.ReadLine();
            Console.Write("Số lượng: ");
            int quantity = int.Parse(Console.ReadLine());
            Console.Write("Ngày hết hạn (yyyy-MM-dd): ");
            string inputDate = Console.ReadLine();

            DateTime expirationDate;
            bool isValidDate = DateTime.TryParseExact(inputDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out expirationDate);

            if (isValidDate)
            {
                // Nếu định dạng ngày tháng hợp lệ, bạn có thể sử dụng biến expirationDate ở đây
                Product newProduct = new Product
                {
                    ProductName = productName,
                    Quantity = quantity,
                    ExpirationDate = expirationDate
                };

                productRepo.AddProduct(newProduct);
                Console.WriteLine("Đã thêm sản phẩm vào kho.");
                
            }
            else
            {
                Console.WriteLine("Định dạng ngày không hợp lệ. Vui lòng nhập theo định dạng yyyy-MM-dd.");
            }
            break;
            
            

        case "2":
            Console.Write("Nhập tên sản phẩm cần kiểm tra tồn kho: ");
            string productNameToCheck = Console.ReadLine();
            productRepo.CheckInventory(productNameToCheck);
            Console.WriteLine("Thông tin tồn kho đã được xuất ra file Inventory.xlsx.");
            break;

        case "3":
            continueProgram = false;
            Console.WriteLine("Đã thoát chương trình.");
            break;

        default:
            Console.WriteLine("Lựa chọn không hợp lệ. Vui lòng chọn lại.");
            break;
    }

    Console.WriteLine(); // Xuống dòng để dễ đọc trong lần lặp tiếp theo
}
