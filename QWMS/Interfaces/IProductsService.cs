 using QWMS.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IProductsService
    {
        Task<List<ProductListModel>?> Get(string? search, int? page);
        Task<ProductDetailsModel?> GetSingle(int id);
        Task<ProductDetailsModel?> GetSingle(string ean);        
    }
}
