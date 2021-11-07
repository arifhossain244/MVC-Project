using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Evidance_10.Models
{
    public class Customer
    {
        public Customer()
        {
            this.Products = new List<Product>();
        }
        public int CustomerId { get; set; }
        [Required, StringLength(40), Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
        [Required, StringLength(30), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Field can't be empty"), EmailAddress(ErrorMessage = "E-mail is not valid"), Display(Name = "Email address")]
        public string Email { get; set; }
        [Required, StringLength(30)]
        public string City { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
    public class Product
    {
        public int ProductId { get; set; }
        [Required, StringLength(40), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Sales Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SalesDate { get; set; }
        [Required, StringLength(150)]
        public string Picture { get; set; }
        public bool Discontinued { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
    public class ProductDbContext : DbContext
    {
        public ProductDbContext()
        {
            Database.SetInitializer(new Seeder());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }

    }
    public class Seeder : DropCreateDatabaseIfModelChanges<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {
            Customer c = new Customer { CustomerName = "Md Arif Hossain", PhoneNumber = "01619502018", Email = "arifhossaineng244@gmail.com", City = "Dhaka" };
            c.Products.Add(new Product { ProductName = "G-SHOCK", Price = 3200.00M, SalesDate = new DateTime(2021, 02, 16), Discontinued = false, Picture = "no-pic.jpg" });
            context.Customers.Add(c);
            context.SaveChanges();
        }
    }
}