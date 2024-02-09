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
        Task<ProductModel?> GetOne(string ean);
    }
}
