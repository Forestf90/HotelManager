using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int ClientId { get; set; }
        public DateTime BookIn { get; set; }
        public DateTime BookOut { get; set; }
        public int GuestsNumber { get; set; }
        public decimal Price { get; set; }

        public virtual Client Client { get; set; }
        public virtual Room Room { get; set; }
    }
}
