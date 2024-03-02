using QWMS.Helpers;
using System.Globalization;

namespace QWMS.Models.Barcodes
{
    public class BarcodeListModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string MeasureUnit { get; set; } = string.Empty;
    }
}
