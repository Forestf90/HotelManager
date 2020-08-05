using HotelManager.HelpModels;
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
    /// Logika interakcji dla klasy OrdersListView.xaml
    /// </summary>
    public partial class OrdersListView : UserControl
    {
        public OrdersListView()
        {
            InitializeComponent();
            DataContext = new OrdersListViewModel();
        }

        private void orderDelete_Click(object sender, RoutedEventArgs e)
        {
            DisplayOrderModel selected = (DisplayOrderModel)ordersDataGrid.SelectedItem;

            using (HotelContext hc = new HotelContext())
            {
                Room r = hc.Room.Where(x => x.Number == selected.RoomNumber).FirstOrDefault();


                var delete = hc.Order.Where(x => x.BookIn == selected.BookIn)
                                    .Where(z => z.RoomId == r.Id).FirstOrDefault();
                hc.Order.Remove(delete);
                hc.SaveChanges();
                DataContext = new OrdersListViewModel();
            }

        }

        private void orderUpdate_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
