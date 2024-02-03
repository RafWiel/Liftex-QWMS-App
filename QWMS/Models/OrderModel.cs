using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Models
{
    public class OrderModel //: INotifyPropertyChanged
    {
        //public event PropertyChangedEventHandler? PropertyChanged;

        [JsonProperty("name")]        
        public string Name { get; set; } = string.Empty;

        [JsonProperty("contractor")]
        public string Contractor { get; set; } = string.Empty;
        
        //public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
