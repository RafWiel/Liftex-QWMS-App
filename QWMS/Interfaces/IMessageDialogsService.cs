using QWMS.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IMessageDialogsService
    {
        void ShowNotification(string title, string message, int delay);
        void ShowError(string title, string message, int delay);
    }
}
