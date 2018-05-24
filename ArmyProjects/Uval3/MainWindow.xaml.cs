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
using Uval3.Source;

namespace Uval3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow thatWindow;
        static public MainWindow ThatWindow { get => thatWindow; set => thatWindow = value; }

        public MainWindow()
        {
            InitializeComponent();
            ThatWindow = this;
            Periods.UpdateDataFromBD();
            DataMan.UpdateDataFromBD("");

            ColorControl.InitColors();
            ColorControl.SetColorsForDG();

            GUIUvalTable.Init(UvalStack);
            GUIUvalTable.Update();

            Binders();
            //var e =this.Resources.FindName("DataGridCellColoringName");
            //var e2 = Resources.Keys;
            //var e3 = Resources.Values;
            //var e4 = Resources["DataGridCellColoringKey"];
        }

        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            Clear();

            Periods.UpdateDataFromBD();
            DataMan.UpdateDataFromBD(FilterTextBox.Text);

            ColorControl.SetColorsForDG();
            GUIUvalTable.Update();
        }
        public void Update()
        {
            Clear();

            Periods.UpdateDataFromBD();
            DataMan.UpdateDataFromBD(FilterTextBox.Text);

            ColorControl.SetColorsForDG();
            GUIUvalTable.Update();
        }

        private void Clear()
        {
            Periods.Clear();
            DataMan.Clear();
            Fizo.Clear();
            BadBoy.Clear();
            Records.Clear();
        }

        private void Binders()
        {
            GridNames.ItemsSource = DataMan.ThatData;
            GridFizo.ItemsSource = Fizo.ThatData;
            GridBadBoy.ItemsSource = BadBoy.ThatData;

            ButAddPeriod.Click += GUIEventHandler.ButAddPeriod_Click;
            ButSave.Click += GUIEventHandler.ButSaveChangedData_Click;
            ButAddMan.Click += GUIEventHandler.ButAddMan_Click;

            GridFizo.CellEditEnding += GUIEventHandler.Fizo_CellEditEnding;
            GridBadBoy.CellEditEnding += GUIEventHandler.BadBoy_CellEditEnding;
        }
        private void ButAddDate_Click(object sender, RoutedEventArgs e_)
        {
            PeriodDatesList.Items.Add(PeriodDate.Text);
            PeriodDate.Text = "";
        }
        private void PeriodDatesList_Delete(object sender, RoutedEventArgs e)
        {
            PeriodDatesList.Items.Clear();
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    
}
