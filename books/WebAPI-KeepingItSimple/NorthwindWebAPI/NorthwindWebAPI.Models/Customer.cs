using System;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public Customer() { }       //  empty constructor aids serialization

        public Customer(IDataReader reader)
        {
            this.CustomerID = reader["CustomerID"].ToString();
            this.CompanyName = reader["CompanyName"].ToString();
            this.ContactName = reader["ContactName"].ToString();
            this.ContactTitle = reader["ContactTitle"].ToString();
            this.Address = reader["Address"].ToString();
            this.City = reader["City"].ToString();
            this.Region = reader["Region"].ToString();
            this.PostalCode = reader["PostalCode"].ToString();
            this.Country = reader["Country"].ToString();
            this.Phone = reader["Phone"].ToString();
            this.Fax = reader["Fax"] == DBNull.Value ? string.Empty : reader["Fax"].ToString();
        }
    }
}