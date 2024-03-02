 using QWMS.Models.Barcodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IBarcodesService
    {
        Task<List<BarcodeListModel>?> Get(int productId, int? page);        
    }
}
