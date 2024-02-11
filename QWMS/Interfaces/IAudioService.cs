using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Interfaces
{
    public interface IAudioService
    {
        void PlayBeepSound();
        void PlayErrorSound();
        void PlayNotificationSound();
        void PlaySuccessSound();
    }
}
