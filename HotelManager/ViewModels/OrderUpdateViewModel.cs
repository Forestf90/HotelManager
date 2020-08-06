using HotelManager.HelpModels;
using HotelManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManager.ViewModels
{
    class OrderUpdateViewModel : ObservableObject
    {

     
        public OrderUpdateViewModel(Order update)
        {
            FillUpdate(update);
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


        private DateTime _bookOut;

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

        private void FillUpdate(Order or)
        {
            _bookOut = or.BookOut;
            BookIn = or.BookIn;
            Guests = or.GuestsNumber;
            Price = (float)or.Price;

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
