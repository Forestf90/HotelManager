using HotelManager.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logika interakcji dla klasy ImportCsvView.xaml
    /// </summary>
    public partial class ImportCsvView : UserControl
    {
        public ImportCsvView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog csvFielDialog = new OpenFileDialog();
            csvFielDialog.Filter = "CSF files(*.csv)|*.csv";

            bool? result = csvFielDialog.ShowDialog();
            int count = 0;

            if (result.HasValue && result.Value)
            {

                using (var reader = new StreamReader(csvFielDialog.FileName))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(';');

                        try
                        {


                            String email = values[11];
                            int number = Convert.ToInt32(values[0]);
                            int guest = Convert.ToInt32(values[8]);
                            DateTime dateIn = DateTime.Parse(values[6]);
                            DateTime dateOut = DateTime.Parse(values[7]);
                            float price = float.Parse(values[5]);
                            using (HotelContext hc = new HotelContext())
                            {
                                Client client = hc.Client.Where(x => x.Email == email).FirstOrDefault();
                                Room r = hc.Room.Where(x => x.Number == number).FirstOrDefault();

                                if (client == null || r == null) continue;


                                if (r.Beds < guest) continue;

                                int isFree = hc.Order.Where(x => x.RoomId == r.Id)
                                                    .Where(z =>
                                                    (
                                                    (z.BookIn >= dateIn && z.BookIn < dateOut) ||
                                                    (z.BookOut <= dateOut && z.BookOut > dateIn) ||
                                                    (z.BookIn <= dateIn && z.BookOut > dateOut)
                                                    ))
                                                    .ToList().Count();
                                if (isFree > 0) continue;

                                Order temp = new Order
                                {
                                    RoomId = r.Id,
                                    ClientId = client.Id,
                                    GuestsNumber = guest,
                                    BookIn = dateIn,
                                    BookOut = dateOut,
                                    Price = (decimal)price
                                };

                                hc.Order.Add(temp);

                                count++;
                                hc.SaveChanges();
                            };

                           
                        }
                        catch
                        {
                            continue;
                        }
                    }

                }
            }

            MessageBox.Show($"{count} orders was added");
        }
    }
}

