using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Models.Orders
{
    public class OrderModel 
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;        
        public string Contractor { get; set; } = string.Empty;
    }
}
