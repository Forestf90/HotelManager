using HotelManager.HelpModels;
using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace HotelManager.ViewModels
{
    class OrderAddViewModel : ObservableObject
    {

        public IEnumerable<Room> RoomsList { get; private set; }

        public IEnumerable<Client> Clients { get; private set; }

        public OrderAddViewModel()
        {
            GetClients();
            GetRooms();
            _bookIn = DateTime.Today;
        }

        public OrderAddViewModel(Order or, String orEmail, Room orNumber)
        {
            GetClients();
            GetRooms();
            FillUpdate(or, orEmail, orNumber);
        }

        private int _room;

        [Required(ErrorMessage = "Field cannot be empty")]
        public int Room 
        {
            get { return _room; }
            set
            {
                ValidationProperty(value, "Room");
                OnPropertyChanged(ref _room, value);
            }
        }

        private string _client;

        [Required(ErrorMessage = "Field cannot be empty")]
        public string Client
        {
            get { return _client; }
            set
            {
                ValidationProperty(value, "Client");
                OnPropertyChanged(ref _client, value);
            }
        }

        private int _guests;

        [Required(ErrorMessage = "Field cannot be empty")]
        [RegularExpression("[0-9]+", ErrorMessage = "Only digits")]
        public int Guests
        {
            get { return _guests; }
            set
            {
                ValidationProperty(value, "Guests");
                OnPropertyChanged(ref _guests, value);
            }
        }

        private float _price;

        [Required(ErrorMessage = "Field cannot be empty")]
        [Range(1, float.MaxValue, ErrorMessage = "Enter positive value")]
        public float Price
        {
            get { return _price; }
            set
            {
                ValidationProperty(value, "Price");
                OnPropertyChanged(ref _price, value);
            }
        }


        private DateTime _bookIn;

        [Required(ErrorMessage = "Field cannot be empty")]
        public DateTime BookIn 
        {
            get { return _bookIn; }
            set
            {
                ValidationProperty(value, "BookIn");
                OnPropertyChanged(ref _bookIn, value);
            }
        }


        private DateTime _bookOut = DateTime.Today.AddDays(1);

        [Required(ErrorMessage = "Field cannot be empty")]
        public DateTime BookOut
        {
            get { return _bookOut; }
            set
            {
                ValidationProperty(value, "BookOut");
                OnPropertyChanged(ref _bookOut, value);
            }
        }

        private void GetRooms()
        {
            using (HotelContext hc = new HotelContext())
            {
                RoomsList = hc.Room.ToList();
            }
        }

        private void GetClients()
        {
            using (HotelContext hc = new HotelContext())
            {
                Clients = hc.Client.ToList();
            }
        }

        private void FillUpdate(Order or, String orEmail, Room orNumber)
        {
            //_bookOut = or.BookOut;
            //BookIn = or.BookIn;
            Guests = or.GuestsNumber;
            Price = (float) or.Price;
            Client = orEmail;
            _room = orNumber.Number;

        } 

        private void ValidationProperty<T>(T value, string name)
        {
            Validator.ValidateProperty(value, new ValidationContext(this)
            {
                MemberName = name
            });
        }


    }
}
