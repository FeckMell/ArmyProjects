using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Увольнения.Source;

namespace Увольнения
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Initers();
            Binders();

            UvalDataManager.GetMan();
            UvalDataManager.GetUvalInfo();
            UvalDataManager.GetBadBoy();
            UvalDataManager.GetFizo();
        }
        private void Initers()
        {
            EditControl.Init(this);
            GUIUvalTable.Init(UvalStack);

            ButAddPeriod.Click += EditControl.AddPeriod;
            ButSave.Click += UvalDataManager.SaveChangedData;
            GridFizo.CellEditEnding += GUIEventHandler.Fizo_CellEditEnding;
            GridBadBoy.CellEditEnding += GUIEventHandler.BadBoy_CellEditEnding;
        }
        private void Binders()
        {
            GridNames.ItemsSource = DataMan.ThatData;
            GridBadBoy.ItemsSource = BadBoy.ThatData;
            GridFizo.ItemsSource = Fizo.ThatData;
        }
    }
}
