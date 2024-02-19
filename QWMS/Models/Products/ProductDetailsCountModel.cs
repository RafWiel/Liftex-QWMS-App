﻿using Newtonsoft.Json;
using QWMS.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QWMS.Models.Products
{
    public class ProductDetailsCountModel 
    {
        public int WarehouseId { get; set; }
        public string WarehouseCode { get; set; } = string.Empty;
        public decimal SaleCount { get; set; }
        public decimal WarehouseCount { get; set; }
        public decimal ReservationCount { get; set; }
        public int MeasureUnitDecimalPlaces { get; set; }

        public string SaleCountStr => FormatCount.ToString(SaleCount, MeasureUnitDecimalPlaces);
        public string WarehouseCountStr => FormatCount.ToString(WarehouseCount, MeasureUnitDecimalPlaces);
        public string ReservationCountStr => FormatCount.ToString(ReservationCount, MeasureUnitDecimalPlaces);
    }
}
