using HotelManager.Models;
using HotelManager.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Logika interakcji dla klasy AddOrderView.xaml
    /// </summary>
    public partial class AddOrderView : UserControl
    {
        public AddOrderView()
        {
            InitializeComponent();  
            DataContext = new OrderAddViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(this))
            {
                Room tempRoom = (Room)RoomCombo.SelectedItem;
                string clientEmail = ClientCombo.Text;
                int guest = Convert.ToInt32(GuestText.Text);
                float price = float.Parse(PriceText.Text);
                DateTime dateIn = (DateTime)DateIn.SelectedDate;
                DateTime dateOut = (DateTime)DateOut.SelectedDate;

                if (dateIn >= dateOut || dateIn < DateTime.Today)
                {
                    MessageBox.Show("Incorrect dates", "Error");
                    return;
                }


                using (HotelContext hc = new HotelContext())
                {


                    if (tempRoom.Beds < guest)
                    {
                        MessageBox.Show($"There is only {tempRoom.Beds} places in the room", "Error");
                        return;
                    }

                    int isFree = hc.Order.Where(x => x.RoomId == tempRoom.Id)
                                        .Where(z => 
                                        (
                                        (z.BookIn >= dateIn && z.BookIn < dateOut) ||
                                        (z.BookOut <= dateOut && z.BookOut > dateIn) ||
                                        (z.BookIn <= dateIn && z.BookOut > dateOut)
                                        ))
                                        .ToList().Count();
                    if (isFree > 0)
                    {
                        MessageBox.Show($"This room is already booked", "Error");
                        return;
                    }

                    Client tempClient = hc.Client.Where(x => x.Email == clientEmail).FirstOrDefault();

                    

                    Order temp = new Order
                    {
                        RoomId = tempRoom.Id,
                        ClientId = tempClient.Id,
                        GuestsNumber = guest,
                        BookIn = dateIn,
                        BookOut = dateOut,
                        Price = (decimal)price
                    };

                    hc.Order.Add(temp);
                   

                    hc.SaveChanges();
                }
                MessageBox.Show("Order added");


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
