using System;
using System.Collections.Generic;

namespace HotelManager.Models
{
    public partial class Client
    {
        public Client()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
