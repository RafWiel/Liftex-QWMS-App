using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.ViewModels.Products
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        private string _title = "Kod towaru";
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _code = string.Empty;
        public string Code
        {
            get => _code;
            set => Set(ref _code, value);
        }

        private string _ean = string.Empty;
        public string Ean
        {
            get => _ean;
            set => Set(ref _ean, value);
        }

        private decimal _price;
        public decimal Price
        {
            get => _price;
            set 
            {
                Set(ref _price, value);
                NotifyPropertyChanged(nameof(PriceStr));
            }
        }

        public string PriceStr => _price > 0 ? $"{Price:0.00}PLN" : string.Empty;

        private decimal _count;
        public decimal Count
        {
            get => _count;
            set
            {
                Set(ref _count, value);
                NotifyPropertyChanged(nameof(CountStr));
            }
        }

        public string CountStr => _count > 0 ? _count.ToString(CultureInfo.InvariantCulture) : string.Empty;

    }
}
