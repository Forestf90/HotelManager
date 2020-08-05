using HotelManager.HelpModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelManager.ViewModels
{
    class ClientAddViewModel : ObservableObject
    {
        private string _firstName;

        [Required(ErrorMessage ="Field cannot be empty")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                ValidationProperty(value, "FirstName");
                OnPropertyChanged(ref _firstName, value);
            }
        }

        private string _lastName;

        [Required(ErrorMessage = "Field cannot be empty")]
        public string LastName
        {
            get { return _lastName; }
            set
            {
                ValidationProperty(value, "LastName");
                OnPropertyChanged(ref _lastName, value);
            }
        }

        private string _email;

        [Required(ErrorMessage = "Field cannot be empty")]
        [EmailAddress(ErrorMessage = "Add a valid email")]
        public string Email
        {
            get { return _email; }
            set
            {
                ValidationProperty(value, "Email");
                OnPropertyChanged(ref _email, value);
            }
        }

        private string _phone;

        [Required(ErrorMessage = "Field cannot be empty")]
        [StringLength(maximumLength: 11, MinimumLength = 7,ErrorMessage = "Phone number must contain 7-11 digits")]
        [RegularExpression("[0-9]+", ErrorMessage ="Only digits")]
        public string Phone
        {
            get { return _phone; }
            set
            {
                ValidationProperty(value, "Phone");
                OnPropertyChanged(ref _phone, value);
            }
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
