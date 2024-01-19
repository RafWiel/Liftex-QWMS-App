using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QWMS.Models
{
    public class OrderListModel //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        [JsonPropertyName("order")]        
        public string Order { get; set; } = string.Empty;

        [JsonPropertyName("contractor")]
        public string Contractor { get; set; } = string.Empty;
        
        //public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
