using System;
using System.Collections.Generic;
using System.Text;

namespace HotelManager.HelpModels
{
    class DisplayOrderModel
    {
        public int RoomNumber { get; set; }
        public int Floor { get; set; }
        public string Type { get; set; }
        public DateTime BookIn { get; set; }
        public DateTime BookOut { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
