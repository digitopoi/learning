using System;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public class Order_Detail
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }

        public Order_Detail() { }           //  the empty constructor aids serialization

        public Order_Detail(IDataReader reader)
        {
            this.OrderID = Convert.ToInt32(reader["OrderID"]);
            this.ProductID = Convert.ToInt32(reader["ProductID"]);
            this.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
            this.Quantity = Convert.ToInt16(reader["Quantity"]);
            this.Discount = Convert.ToInt32(reader["Discount"]);


            this.Product = new Product();
            this.Product.ProductID = this.ProductID;
            this.Product.ProductName = reader["ProductName"].ToString();
        }
    }
}