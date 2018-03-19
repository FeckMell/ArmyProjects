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
    /// 


    /*
     Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\DejiDB.mdf;Integrated Security=True
         */
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Display
            UnitsStore.Init(GridUnits);
            RecordsStore.Init(GridRecords);

            SQLConnector.Init();
            SearchController.Init();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void FocusRowUnits(object sender, SelectionChangedEventArgs e)
        {
            //Получение элемента строки: ((e.Source as DataGrid).CurrentCell.Item as UnitElement)
            RecordsStore.Display((e.Source as DataGrid).CurrentCell.Item as UnitElement);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            SearchController.PerformSearch((e.Source as TextBox).Text);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
