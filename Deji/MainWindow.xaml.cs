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

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Display
            UnitsStore.Init(GridUnits, SearchTextBox);
            RecordsStore.Init(GridRecords);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void FocusRowUnits(object sender, SelectionChangedEventArgs e)
        {
            RecordsStore.Display((e.Source as DataGrid).CurrentCell.Item as UnitElement);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ClickAddUnit(object sender, RoutedEventArgs e)
        {
           if( UnitsController.AddUnit(e.Source as Button)) MessageBox.Show("Новый военнослужащий добавлен!");
            else MessageBox.Show("Заполните все поля!");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ClickAddRecord(object sender, RoutedEventArgs e)
        {
            if (RecordsController.AddRecord(e.Source as Button)) MessageBox.Show("Новая запись добавлена!");
            else MessageBox.Show("Заполните все поля!");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        /*Sub info event handlers*/
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            UnitsStore.Update();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void LoadedRank(object sender, RoutedEventArgs e)
        {
            FormDataLoading.LoadRank(e.Source as ComboBox);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void LoadedPart(object sender, RoutedEventArgs e)
        {
            FormDataLoading.LoadPart(e.Source as ComboBox);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void LoadedType(object sender, RoutedEventArgs e)
        {
            FormDataLoading.LoadType(e.Source as ComboBox);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void LoadedWho(object sender, RoutedEventArgs e)
        {
            FormDataLoading.LoadWho(e.Source as ComboBox);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void LoadedDrochit(object sender, RoutedEventArgs e)
        {
            FormDataLoading.LoadDrochit(e.Source as ComboBox);
        }

        private void qqq(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("qqq");
        }



        //RichTextBox f = new RichTextBox();
        //string c = new TextRange(f.Document.ContentStart, f.Document.ContentEnd).Text;
        //
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
