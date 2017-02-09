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

namespace ParkingBon
{
    /// <summary>
    /// Interaction logic for ParkingBonWindow.xaml
    /// </summary>
    public partial class ParkingBonWindow : Window
    {
        public ParkingBonWindow()
        {
            InitializeComponent();
            Nieuw();
        }


        private void Nieuw()
        { 
            DatumBon.SelectedDate = DateTime.Now;
            AankomstLabelTijd.Content = DateTime.Now.ToLongTimeString();
            TeBetalenLabel.Content = "0 €";
            VertrekLabelTijd.Content = AankomstLabelTijd.Content;        
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Programma afsluiten ?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No)
                e.Cancel = true;
        }

        private void minder_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            if (bedrag > 0)
                bedrag -= 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }

        private void meer_Click(object sender, RoutedEventArgs e)
        {
            int bedrag = Convert.ToInt32(TeBetalenLabel.Content.ToString().Replace(" €", ""));
            DateTime vertrekuur = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag);
            if (vertrekuur.Hour < 22)
                bedrag += 1;
            TeBetalenLabel.Content = bedrag.ToString() + " €";
            VertrekLabelTijd.Content = Convert.ToDateTime(AankomstLabelTijd.Content).AddHours(0.5 * bedrag).ToLongTimeString();
        }
    }
}
