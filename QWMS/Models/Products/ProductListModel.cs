using QWMS.Helpers;
using System.Globalization;

namespace QWMS.Models.Products
{
    public class ProductListModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public decimal Count { get; set; }
        public decimal Price { get; set; }        
        public int MeasureUnitDecimalPlaces { get; set; }

        public string PriceStr => Price.ToString("0.00", CultureInfo.InvariantCulture);        
        public string CountStr => Tools.FormatCount(Count, MeasureUnitDecimalPlaces);
    }
}
