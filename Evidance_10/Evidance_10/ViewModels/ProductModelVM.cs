using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Evidance_10.ViewModels
{
    public class ProductInputModel
    {
        public int ProductId { get; set; }
        [Required, StringLength(40), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Sales Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SalesDate { get; set; }
        [Required]
        public HttpPostedFileBase Picture { get; set; }
        public bool Discontinued { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
    }
    public class ProductEditModel
    {
        public int ProductId { get; set; }
        [Required, StringLength(40), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Sales Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime SalesDate { get; set; }
        public HttpPostedFileBase Picture { get; set; }
        public bool Discontinued { get; set; }
        [Required, ForeignKey("Customer")]
        public int CustomerId { get; set; }
    }
}