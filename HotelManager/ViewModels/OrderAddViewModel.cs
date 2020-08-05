using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HotelManager.ViewModels
{
    class OrderAddViewModel
    {

        public IEnumerable<Room> RoomsList { get; set; }

        public IEnumerable<Client> Clients { get; set; }

        public OrderAddViewModel()
        {
            GetClients();
            GetRooms();
        }

        [Required(ErrorMessage = "Field cannot be empty")]
        public Room Room { get; set; }

        private DateTime _bookIn;

        [Required(ErrorMessage = "Field cannot be empty")]
        public DateTime BookIn;


        private DateTime _bookOut;

        [Required(ErrorMessage = "Field cannot be empty")]
        public DateTime BookOut;

        private void GetRooms()
        {
            using (HotelContext hc = new HotelContext())
            {
                RoomsList = hc.Room.ToList();
            }
        }

        private void GetClients()
        {
            using(HotelContext hc = new HotelContext())
            {
                Clients = hc.Client.ToList();
            }
        }

    }
}
