using System;
using System.Data;
using System.Data.SqlClient;
using OfficeOpenXml;
using EShop.Common; 
using EProduct.DataAccess.NetCore.DTO; 

namespace EProduct.DataAccess.NetCore.Repositories
{
    public class ProductRepository
    {
        public void AddProduct(Product product)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetSqlConnection())
                {
                    SqlCommand cmd = new SqlCommand("AddProduct", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);
                    cmd.Parameters.AddWithValue("@ExpirationDate", product.ExpirationDate);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm sản phẩm vào cơ sở dữ liệu: {ex.Message}");
                // Xử lý ngoại lệ theo ý của bạn
            }
        }

        public void CheckInventory(string productName)
        {
            try
            {
                using (SqlConnection conn = DbHelper.GetSqlConnection())
                {
                    
                    SqlCommand cmd = new SqlCommand("CheckInventory", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ProductName", productName);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Inventory.xlsx");
                        using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
                        {
                            ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Inventory");
                            worksheet.Cells[1, 1].Value = "Product Name";
                            worksheet.Cells[1, 2].Value = "Quantity";
                            worksheet.Cells[1, 3].Value = "Expiration Date";

                            int row = 2;
                            while (reader.Read())
                            {
                                var product = new Product
                                {
                                    ProductName = reader["ProductName"].ToString(),
                                    Quantity = Convert.ToInt32(reader["Quantity"]),
                                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"])
                                };

                                worksheet.Cells[row, 1].Value = product.ProductName;
                                worksheet.Cells[row, 2].Value = product.Quantity;
                                worksheet.Cells[row, 3].Value = product.ExpirationDate.ToString("yyyy-MM-dd");
                                row++;
                            }

                            var file = new System.IO.FileInfo("Inventory.xlsx");
                            package.SaveAs(file);
                        }
                    }
                }

                Console.WriteLine("Thông tin tồn kho đã được xuất ra file Inventory.xlsx.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi kiểm tra tồn kho: {ex.Message}");
                // Xử lý ngoại lệ theo ý của bạn
            }
        }
       
    }
}

