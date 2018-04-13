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
        int initedTabs = 0;
        public MainWindow()
        {
            InitializeComponent();

            //Display
            InitFormElements();
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
            if (UnitsController.AddUnit(e.Source as Button))
            {
                UpdateLists();
                MessageBox.Show("Новый военнослужащий добавлен!");
            }
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
        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            UnitsStore.Update();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void TabClick(object sender, SelectionChangedEventArgs e)
        {
            //if window is not inited - exception. This avoids it
            if (initedTabs == 0)
            {
                initedTabs++;
                return;
            }
            //Strange exception avoid. Happens(method called) when click on grid.
            if ((e.Source as TabControl) == null) return;

            if (((e.Source as TabControl).Items[0] as TabItem).IsSelected)//Tab with DB grids
            {
                UpdateGrids();
            }
            if (((e.Source as TabControl).Items[1] as TabItem).IsSelected)//Tab with adding units and records forms
            {
                UpdateLists();
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        /*Sub info event handlers*/
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void UpdateGrids()
        {
            UnitsStore.Update();
            RecordsStore.Display(GridUnits.CurrentCell.Item as UnitElement);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void UpdateLists()
        {
            FormDataLoading.LoadWho(ElWho);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void InitFormElements()
        {
            FormDataLoading.LoadRank(ElRank);
            FormDataLoading.LoadPart(ElPart);
            FormDataLoading.LoadType(ElType);
            FormDataLoading.LoadDrochit(ElDrochit);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
