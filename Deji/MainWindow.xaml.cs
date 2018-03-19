using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Deji
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Display
            UnitsStore.Init(GridUnits);
            RecordsStore.Init(GridRecords);

            SQLConnector.Init("InitString");
            SearchController.Init();
        }

        private void FocusRowUnits(object sender, SelectionChangedEventArgs e)
        {
            //Получение элемента строки: ((e.Source as DataGrid).CurrentCell.Item as UnitElement)
            RecordsStore.Display((e.Source as DataGrid).CurrentCell.Item as UnitElement);

           // MessageBox.Show("call\nType:"+ (e.Source as DataGrid).CurrentCell.Item.GetType()+"\nData:"+ ((e.Source as DataGrid).CurrentCell.Item as UnitElement).ThatName);
        }

        private void qqq(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("qqq");
        }

        private void eee(object sender, TextChangedEventArgs e)
        {
           // MessageBox.Show("eee");
        }

        private void ttt(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("ttt");
        }
    }
}
