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
        static private MainWindow thatWindow;
        static public MainWindow ThatWindow { get => thatWindow; set => thatWindow = value; }

        public MainWindow()
        {
            InitializeComponent();
            ThatWindow = this;
            ColorControl.InitColors();
            Binders();

            Periods.UpdateDataFromBD();
            DataMan.UpdateDataFromBD("");
            GUIUvalTable.Update();
        }

        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            Clear();

            Periods.UpdateDataFromBD();
            DataMan.UpdateDataFromBD(FilterTextBox.Text);
            GUIUvalTable.Update();
        }

        private void Clear()
        {
            Periods.Clear();
            DataMan.Clear();
            Records.Clear();
        }

        private void Binders()
        {
            GridNames.ItemsSource = DataMan.ThatData;
            GridFizo.ItemsSource = DataMan.ThatData;
            GridBadBoy.ItemsSource = DataMan.ThatData;

            ButAddPeriod.Click += GUIEventHandler.ButAddPeriod_Click;
            ButSave.Click += GUIEventHandler.ButSaveChangedData_Click;
            ButAddMan.Click += GUIEventHandler.ButAddMan_Click;

            GridFizo.CellEditEnding += GUIEventHandler.Fizo_CellEditEnding;
            GridBadBoy.CellEditEnding += GUIEventHandler.BadBoy_CellEditEnding;
        }
        
        
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    
}
