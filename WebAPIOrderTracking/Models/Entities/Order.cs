using System;
using System.Collections.Generic;

namespace WebAPIOrderTracking.Models.Entities
{
    public partial class Order
    {
        public int Orderid { get; set; }
        public string Ordername { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Visitdate { get; set; }
        public DateTime Updatedate { get; set; }
    }
}
