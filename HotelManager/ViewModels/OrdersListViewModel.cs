using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HotelManager.HelpModels;
using HotelManager.Models;

namespace HotelManager.ViewModels
{
    class OrdersListViewModel
    {
        private List<DisplayOrderModel> _ordersList;

        public OrdersListViewModel()
        {
            _ordersList = new List<DisplayOrderModel>();
            GetOrders();
        }

        private void GetOrders()
        {
            using (HotelContext hc = new HotelContext())
            {
                var tempList = hc.Order.Where(x => x.BookIn > DateTime.Now).ToList();
                foreach (var o in tempList)
                {
                    Client c = hc.Client.Where(x => x.Id == o.ClientId).FirstOrDefault();
                    Room r = hc.Room.Where(x => x.Id == o.RoomId).FirstOrDefault();

                    Orders.Add(new DisplayOrderModel
                    {
                        RoomNumber = r.Number,
                        BookIn = o.BookIn,
                        BookOut = o.BookOut,
                        Floor = r.Floor,
                        Type = r.Type,
                        FirstName = c.FirstName,
                        LastName = c.LastName

                    });
                }
            }
        }
        public List<DisplayOrderModel> Orders 
        {
            get { return _ordersList; }
            set { _ordersList = value; }
        }
    }
}
