using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfClientCurrency.ServiceReferenceCurrency;

namespace WpfClientCurrency
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CurrencyServiceClient currencyService = new CurrencyServiceClient();
        private CurrencyList currencyList;

        public MainWindow()
        {
            InitializeComponent();

            currencyList = currencyService.GetAllCurrencies();
            lvCurrencies.ItemsSource = cmbSource.ItemsSource = cmbTarget.ItemsSource = currencyList;
            cmbSource.DisplayMemberPath = cmbTarget.DisplayMemberPath = "Key";
        }

        private void tbAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.Text[0]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Currency source = cmbSource.SelectedValue as Currency;
            Currency target = cmbTarget.SelectedValue as Currency;
            double amount = int.Parse(tbAmount.Text);
            double result = Math.Round(currencyService.Convert(source, target, amount), 2);
            tbResult.Text =$"{amount} {source.Key} is {result} {target.Key}";  
        }
    }
}
