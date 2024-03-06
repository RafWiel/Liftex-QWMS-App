 using QWMS.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IReservationsService
    {
        Task<List<ReservationListModel>?> Get(int productId, int? page);        
    }
}
