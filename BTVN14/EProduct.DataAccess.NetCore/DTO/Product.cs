using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EProduct.DataAccess.NetCore.DTO
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Product() { }
        public Product(int productid, string productname, int quantity,DateTime expirationdate) 
        { 
            ProductID = productid;
            ProductName = productname;
            Quantity = quantity;
            ExpirationDate = expirationdate;
        }
        ~Product() { }
        public override string ToString()
        {
            return $"ID : {ProductID}, Name: {ProductName}, SL : {Quantity}, Day : {ExpirationDate}";
        }
    }
}
