using QWMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IOrdersService
    {
        Task<List<OrderModel>?> GetOrders();
    }
}
