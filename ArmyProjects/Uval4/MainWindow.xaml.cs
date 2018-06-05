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
using System.Windows.Shapes;
using Uval4.Source;

namespace Uval4
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
            ColorControl.InitColors();
            ThatWindow = this;
            Binders();

            Periods.UpdateDataFromBD();
            Man.UpdateDataFromBD();
            GUIUvalTable.Update();
            MNGRBackup.Work();

        }
        private void SearchStringChange(object sender, TextChangedEventArgs e)
        {
            Man.Filter(FilterTextBox.Text);
        }
        private void Binders()
        {
            GridNames.ItemsSource = Man.ThatData;
            GridFizo.ItemsSource = Man.ThatData;
            GridBadBoy.ItemsSource = Man.ThatData;
            PeriodSelect.ItemsSource = Periods.ThatData;

            ButAddPeriod.Click += GUIEventHandler.ButAddPeriod_Click;
            ButSave.Click += GUIEventHandler.ButSaveChangedData_Click;
            ButAddMan.Click += GUIEventHandler.ButAddMan_Click;

            (GridNames.ContextMenu.Items[0] as MenuItem).Click += GUIEventHandler.ManListEdit;
            (GridNames.ContextMenu.Items[1] as MenuItem).Click += GUIEventHandler.ManListEdit;
            GridFizo.CellEditEnding += GUIEventHandler.Fizo_CellEditEnding;
            GridBadBoy.CellEditEnding += GUIEventHandler.BadBoy_CellEditEnding;
        }
        public void Update()
        {
            Clear();

            Periods.UpdateDataFromBD();
            Man.UpdateDataFromBD();
            Man.Filter(FilterTextBox.Text);
            GUIUvalTable.Update();
            PeriodSelect.Items.Refresh();
        }
        private void Clear()
        {
            Periods.Clear();
            Man.Clear();
        }
    }
}
