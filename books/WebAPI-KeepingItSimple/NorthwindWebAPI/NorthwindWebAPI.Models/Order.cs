using System;
using System.Collections.Generic;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }

        public Customer Customer { get; set; }
        public Employee Employee { get; set; }

        public ICollection<Order_Detail> Order_Details { get; set; }

        public Order() { }              //  the empty constructor aids serialization

        public Order(IDataReader reader)
        {
            this.OrderID = Convert.ToInt32(reader["OrderID"]);
            this.OrderDate = reader["OrderDate"] as DateTime?;
            this.RequiredDate = reader["RequiredDate"] as DateTime?;
            this.ShippedDate = reader["ShippedDate"] as DateTime?;
            this.ShipVia = reader["ShipVia"] as int?;
            this.Freight = reader["Freight"] as decimal?;
            this.ShipName = reader["ShipName"] as string;
            this.ShipAddress = reader["ShipAddress"] as string;
            this.ShipCity = reader["ShipCity"] as string;
            this.ShipRegion = reader["ShipRegion"] as string;
            this.ShipPostalCode = reader["ShipPostalCode"] as string;
            this.ShipCountry = reader["ShipCountry"] as string;


            this.Customer = new Customer();
            this.Customer.CustomerID = reader["CustomerID"] as string;
            this.Customer.CustomerID = this.Customer.CustomerID;
            this.Customer.CompanyName = reader["CompanyName"] as string;
            this.Customer.ContactName = reader["ContactName"] as string;


            this.Employee = new Employee();
            this.Employee.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
            this.Employee.EmployeeID = this.Employee.EmployeeID;
            this.Employee.FirstName = reader["FirstName"] as string;
            this.Employee.LastName = reader["LastName"] as string;


            Order_Details = new HashSet<Order_Detail>();
        }
    }
}