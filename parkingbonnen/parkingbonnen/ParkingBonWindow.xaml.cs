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
using Microsoft.Win32;
using System.IO;

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

        private void NewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Nieuw();
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();

                dlg.Filter = "bons | *.bon";

                if (dlg.ShowDialog() == true )
                {
                    using (StreamReader bestand = new StreamReader(dlg.FileName))
                    {
                        DatumBon.SelectedDate = Convert.ToDateTime(bestand.ReadLine());
                        AankomstLabelTijd.Content = bestand.ReadLine();
                        TeBetalenLabel.Content = bestand.ReadLine();
                        VertrekLabelTijd.Content = bestand.ReadLine();
                    }

                }
                
            }

            catch (Exception ex)
            {
                MessageBox.Show("crash" + ex.Message);
            }
            


        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();

                DateTime datum = (DateTime)DatumBon.SelectedDate;
                int lengteaankomsttijd = AankomstLabelTijd.Content.ToString().Length;
                int eerstedubbelpunt = AankomstLabelTijd.Content.ToString().IndexOf(":");
                string uur = AankomstLabelTijd.Content.ToString().Substring(0, eerstedubbelpunt);

                string minutenenseconden = AankomstLabelTijd.Content.ToString().Substring(eerstedubbelpunt+1, lengteaankomsttijd-eerstedubbelpunt-1);
                int tweededubbelpunt = minutenenseconden.IndexOf(":");

                string minuten = minutenenseconden.Substring(0, tweededubbelpunt);
                lengteaankomsttijd = minutenenseconden.Length;

                string seconden = minutenenseconden.Substring(tweededubbelpunt+1, lengteaankomsttijd - tweededubbelpunt-1);


                dlg.FileName = datum.Day.ToString()+"-" + datum.Month.ToString() + "-" + datum.Year.ToString() + "om" + uur + "-" + minuten + "-" + seconden + ".bon" ;
                dlg.DefaultExt = ".bon";
                dlg.Filter = "bons | *.bon";

              


                if (dlg.ShowDialog() == true)
                {
                    using (StreamWriter bestand = new StreamWriter(dlg.FileName))
                    {
                        bestand.WriteLine(datum.ToShortDateString());
                        bestand.WriteLine(AankomstLabelTijd.Content);
                        bestand.WriteLine(TeBetalenLabel.Content);
                        bestand.WriteLine(VertrekLabelTijd.Content);
                    }
                }
               
            }

            catch (Exception ex)
            {
                MessageBox.Show("crash" + ex.Message);
            }
        }
    }
}
