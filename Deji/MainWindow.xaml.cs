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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Deji.Data.DB_DejDataSet dB_DejDataSet = ((Deji.Data.DB_DejDataSet)(this.FindResource("dB_DejDataSet")));
            // Load data into the table Record. You can modify this code as needed.
            Deji.Data.DB_DejDataSetTableAdapters.RecordTableAdapter dB_DejDataSetRecordTableAdapter = new Deji.Data.DB_DejDataSetTableAdapters.RecordTableAdapter();
            dB_DejDataSetRecordTableAdapter.Fill(dB_DejDataSet.Record);
            System.Windows.Data.CollectionViewSource recordViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("recordViewSource")));
            recordViewSource.View.MoveCurrentToFirst();
        }
    }
}
