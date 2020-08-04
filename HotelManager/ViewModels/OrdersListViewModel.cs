using System;
using System.Collections.Generic;
using System.Text;
using HotelManager.HelpModels;

namespace HotelManager.ViewModels
{
    class OrdersListViewModel
    {
        private IList<DisplayOrderModel> _ordersList;

        public OrdersListViewModel()
        {
            _ordersList = new List<DisplayOrderModel>
            {
                new DisplayOrderModel
                {
                    RoomNumber = 3, BookIn = DateTime.Now, BookOut = DateTime.Now, Floor=5, Type="with single bathroom", FirstName="Michal", LastName="Sliwa"
                },
                new DisplayOrderModel
                {
                    RoomNumber = 35, BookIn = DateTime.Now, BookOut = DateTime.Now, Floor=5, Type="with singthroom", FirstName="lukasz", LastName="Slsdfgiwa"
                },
                new DisplayOrderModel
                {
                    RoomNumber = 5, BookIn = DateTime.Now, BookOut = DateTime.Now, Floor=2, Type="we bathroit", FirstName="Kamil", LastName="dsfg"
                },
                new DisplayOrderModel
                {
                    RoomNumber = 43, BookIn = DateTime.Now, BookOut = DateTime.Now, Floor=5, Type="no bathroom", FirstName="gty", LastName="sfd"
                }
            };
        }
        public IList<DisplayOrderModel> Orders 
        {
            get { return _ordersList; }
            set { _ordersList = value; }
        }
    }
}
