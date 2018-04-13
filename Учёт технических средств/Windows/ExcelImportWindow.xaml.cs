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
using System.Windows.Shapes;

using Учёт_технических_средств.Helpers;

namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class ExcelImportWindow : Window
    {
        public ExcelImportWindow()
        {
            InitializeComponent();
            mainGrid.DataContext = DataController.dataContext;
        }

        private void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xls|.xlsx";
            dlg.Filter = "Файлы Microsoft Excel (.xls, .xlsx)|*.xls;*.xlsx";
            dlg.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, @"Excel");
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                DataController.showLoader();
                //Task t1 = Task.Run(() => DataController.updateMainBase(filename));
                DataController.mainBase = DataHelper.LoadFromXls(filename);

                DataController.closeLoader();
                DataHelper.RaiseDatabaseUpdateInfo();
            }
            DataController.CloseWindow(typeof(ExcelImportWindow));
        }
    }
}
