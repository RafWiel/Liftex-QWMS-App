using QWMS.Helpers;
using System.Globalization;

namespace QWMS.Models.Reservations
{
    public class ReservationListModel
    {
        public int Id { get; set; }
        public string Contractor { get; set; } = string.Empty;
        public string OrderName { get; set; } = string.Empty;
        public decimal Count { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }

        public string CountStr => Tools.FormatCount(Count, MeasureUnitDecimalPlaces);
    }
}
