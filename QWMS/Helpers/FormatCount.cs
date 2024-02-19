using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Helpers
{
    public class FormatCount
    {
        public static string ToString(decimal count, int decimalPlaces)
        {
            switch (decimalPlaces)
            {
                case 1:
                    return string.Create(CultureInfo.InvariantCulture, $"{count:0.0}");
                case 2:                    
                    return string.Create(CultureInfo.InvariantCulture, $"{count:0.00}");                    
                case 3:
                    return string.Create(CultureInfo.InvariantCulture, $"{count:0.000}");
                case 4:
                    return string.Create(CultureInfo.InvariantCulture, $"{count:0.0000}");
                default:
                    return $"{count:0}";                    
            }
        }
    }
}
