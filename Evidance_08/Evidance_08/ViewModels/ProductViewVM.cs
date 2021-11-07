using Evidance_08.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evidance_08.ViewModels
{
    public class ProductViewVM
    {
            public int ProductId { get; set; }
            [Required, StringLength(50), Display(Name = "Product Name")]
            public string ProductName { get; set; }
            [Required, Display(Name = "Stock")]
            public int Stock { get; set; }
            public HttpPostedFileBase Picture { get; set; }
            [Required]
            public int SaleId { get; set; }
            public virtual Sale Sale { get; set; }
     }
}
