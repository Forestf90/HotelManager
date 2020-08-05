using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Room
    {
        public Room()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; }
        public double Size { get; set; }
        public int Beds { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
