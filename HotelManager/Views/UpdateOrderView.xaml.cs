using HotelManager.Models;
using HotelManager.ViewModels;
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
    /// Logika interakcji dla klasy OrderUpdateView.xaml
    /// </summary>
    public partial class UpdateOrderView : UserControl
    {
        private Order update;

 
        public UpdateOrderView(Order or)
        {
            InitializeComponent();
            DataContext = new OrderUpdateViewModel(or);
            update = or;
            DateInPicU.SelectedDate = or.BookIn;
            DateOutPicU.SelectedDate = or.BookOut;
        }

        private void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid(this))
            {
                int guest = Convert.ToInt32(GuestTextU.Text);
                float price = float.Parse(PriceTextU.Text);
                DateTime dateIn = (DateTime)DateInPicU.SelectedDate;
                DateTime dateOut = (DateTime)DateOutPicU.SelectedDate;

                if (dateIn >= dateOut || dateIn < DateTime.Today)
                {
                    MessageBox.Show("Incorrect dates", "Error");
                    return;
                }


                using (HotelContext hc = new HotelContext())
                {

                    //Room tempRoom = hc.Room.Where(x => x.Order.Select(y => y.Id == update.Id).FirstOrDefault()).FirstOrDefault();

                    Room tempRoom = update.Room;
                    if (tempRoom.Beds < guest)
                    {
                        MessageBox.Show($"There is only {tempRoom.Beds} places in the room", "Error");
                        return;
                    }

                    int isFree = 0;
                    if (update != null)
                    {
                        isFree = hc.Order.Where(x => x.RoomId == tempRoom.Id && x.Id != update.Id)
                                            .Where(z =>
                                            (
                                            (z.BookIn >= dateIn && z.BookIn < dateOut) ||
                                            (z.BookOut <= dateOut && z.BookOut > dateIn) ||
                                            (z.BookIn <= dateIn && z.BookOut > dateOut)
                                            ))
                                            .ToList().Count();
                    }
                    else
                    {
                        isFree = hc.Order.Where(x => x.RoomId == tempRoom.Id)
                                            .Where(z =>
                                            (
                                            (z.BookIn >= dateIn && z.BookIn < dateOut) ||
                                            (z.BookOut <= dateOut && z.BookOut > dateIn) ||
                                            (z.BookIn <= dateIn && z.BookOut > dateOut)
                                            ))
                                            .ToList().Count();
                    }

                    if (isFree > 0)
                    {
                        MessageBox.Show($"This room is already booked", "Error");
                        return;
                    }

                    Client tempClient = hc.Client.Where(x => x.Order.Select(y => y.Id == update.Id).FirstOrDefault()).FirstOrDefault();


                    update.GuestsNumber = guest;
                    update.BookIn = dateIn;
                    update.BookOut = dateOut;
                    update.Price = (decimal)price;
                    hc.Order.Update(update);
                    


                    hc.SaveChanges();
                }
                MessageBox.Show("Order updated");


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
