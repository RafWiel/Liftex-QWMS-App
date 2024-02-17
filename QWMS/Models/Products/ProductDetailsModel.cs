using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Models.Products
{
    public class ProductDetailsModel 
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Ean { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Count { get; set; }
        public List<ProductDetailsCountModel> Items { get; set; } = new();
    }
}
