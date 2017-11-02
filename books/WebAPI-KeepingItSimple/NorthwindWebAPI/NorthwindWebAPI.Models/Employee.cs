using System;
using System.Data;

namespace NorthwindWebAPI.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string HomePhone { get; set; }
        public string Extension { get; set; }
        public byte[] Photo { get; set; }
        public string Notes { get; set; }
        public string PhotoPath { get; set; }

        public Employee() { }      //  The empty constructor aids serialization

        public Employee(IDataReader reader)
        {
            this.EmployeeID = Convert.ToInt32(reader["EmployeeID"]);
            this.TitleOfCourtesy = reader["TitleOfCourtesy"] as string;
            this.FirstName = reader["FirstName"].ToString();
            this.LastName = reader["LastName"].ToString();
            this.Title = reader["Title"] as string;
            this.BirthDate = reader["BirthDate"] as DateTime?;
            this.HireDate = reader["HireDate"] as DateTime?;
            this.Address = reader["Address"] as string;
            this.City = reader["City"] as string;
            this.Region = reader["Region"] as string;
            this.PostalCode = reader["PostalCode"] as string;
            this.Country = reader["Country"] as string;
            this.HomePhone = reader["HomePhone"] as string;
            this.Extension = reader["Extension"] as string;
            this.Photo = reader["Photo"] as byte[];
            this.Notes = reader["Notes"] as string;
            this.PhotoPath = reader["PhotoPath"] as string;
        }
    }
}