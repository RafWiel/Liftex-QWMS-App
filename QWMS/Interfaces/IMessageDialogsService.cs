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
        bool? IsActionStopped { get; }
        void ShowNotification(string title, string message, int delay = 0);
        void ShowError(string title, string message, int delay = 0);
        void ShowActionNotification(string title, string message);
        void UpdateActionNotification(string message);
        void CloseActionNotification();
    }
}
