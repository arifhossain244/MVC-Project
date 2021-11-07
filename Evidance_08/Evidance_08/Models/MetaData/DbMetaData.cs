using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Evidance_08.Models.MetaData
{
    //[MetadataType(typeof(SaleType))]
    public partial class ProductType
    {
        public int ProductId { get; set; }
        [Required, StringLength(50), Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Required, Display(Name = "Stock")]
        public int Stock { get; set; }
        [Required, StringLength(150), Display(Name = "Photo")]
        public string Picture { get; set; }
        [Required]
        public int SaleId { get; set; }
        
    }
    //[MetadataType(typeof(ProductType))]
    public class SaleType
    {
        public int SaleId { get; set; }
        [Required, Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required, DataType(DataType.Date), Display(Name = "Sales Date"),
            DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.DateTime SalesDate { get; set; }
    }
}