using Microsoft.Win32;
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

            if (result.HasValue && result.Value)
            {
                int count = 0;

                MessageBox.Show(csvFielDialog.FileName);
            }
        }
    }
}
