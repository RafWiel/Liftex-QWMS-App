using QWMS.Helpers;

namespace QWMS.Models.Products
{
    public class ProductDetailsCountModel 
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = string.Empty;
        public decimal SaleCount { get; set; }
        public decimal WarehouseCount { get; set; }
        public decimal ReservationCount { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }

        public string SaleCountStr => Tools.FormatCount(SaleCount, MeasureUnitDecimalPlaces);
        public string WarehouseCountStr => Tools.FormatCount(WarehouseCount, MeasureUnitDecimalPlaces);
        public string ReservationCountStr => Tools.FormatCount(ReservationCount, MeasureUnitDecimalPlaces);
    }
}
