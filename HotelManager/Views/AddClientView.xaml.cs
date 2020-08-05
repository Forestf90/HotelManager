using HotelManager.Models;
using HotelManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelManager.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddClientView.xaml
    /// </summary>
    public partial class AddClientView : UserControl
    {
        public AddClientView()
        {
            InitializeComponent();
            DataContext = new ClientAddViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(this))
            {
                string firstName = FirstNameText.Text;
                string lastName = LastNameText.Text;
                string email = EmailText.Text;
                string phone = PhoneText.Text;

                if (!String.IsNullOrEmpty(firstName) && !String.IsNullOrEmpty(lastName) &&
                    !String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(phone))
                {
                    using (HotelContext hc = new HotelContext())
                    {
                        hc.Client.Add(new Client
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Email = email,
                            Phone = phone
                        });

                        hc.SaveChanges();
                    }
                    MessageBox.Show("Client added");

                }
            }

        }

        public static bool IsValid(DependencyObject parent)
        {
            if (Validation.GetHasError(parent))
                return false;

            for (int i = 0; i != VisualTreeHelper.GetChildrenCount(parent); ++i)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (!IsValid(child)) { return false; }
            }

            return true;
        }
    }
}
