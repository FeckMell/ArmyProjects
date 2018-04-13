
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

using System.ComponentModel;
using Учёт_технических_средств;
using Учёт_технических_средств.Source;
using Учёт_технических_средств.Helpers;

namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для WorkWindow.xaml
    /// </summary>
    public partial class WorkWindow : Window
    {
        public WorkWindow()
        {

            InitializeComponent();
            mainDataGrid.ItemsSource = DataController.mainBase.Products;
            chooseAll = false;
            

            BackEnd currentContext = new BackEnd();
            currentContext.DeepCopy(DataController.dataContext);
            MainGrid.DataContext = DataController.dataContext;
            chkBoxGrid.DataContext = currentContext;
            productStateColumn.ItemsSource = DataController.possibleStates;
            chooseStat.ItemsSource = DataController.possibleStates;
            DataHelper.DataBaseUpdatedInfo += BaseUpdated;
            
        }

        private List<string> stateList;

        private bool chooseAll;
        
        private string searchKey;
        public string SearchKey
        {
            get
            {
                return searchKey;
            }
            set
            {
                searchKey = value;
            }
        }

        #region Обработчики кнопок
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            //SearchKey = SearchField.Text;
            //DataController.mainBase.SearchInBase(SearchKey);
            DataController.showLoader();
        }

        private void archiveBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.mainBase.MoveChosenToAnotherBase(DataController.archive);
            
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.mainBase.DeleteChosenProducts();
            DataController.MainBaseChanged = true;
        }

        private void exportExcel_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.FileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xls|.xlsx";
            dlg.Filter = "Файлы Excel (.xls)|*.xls;*.xlsx";
            dlg.InitialDirectory = System.IO.Path.Combine( DataHelper.FolderPath(), @"Excel");
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                DataHelper.SaveToXls(DataController.mainBase, filename);
            }
        }

        private void importExcel_btn_Click(object sender, RoutedEventArgs e)
        {
            /*
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xls|.xlsx";
            dlg.Filter = "Файлы Microsoft Excel (.xls, .xlsx)|*.xls;*.xlsx";

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
                mainDataGrid.ItemsSource = DataController.mainBase.Products;
            }*/
            DataController.ShowWindow(typeof(ExcelImportWindow));
        }

        private void BaseUpdated(object sender, DataBaseUpdatedInfoEventArgs e)
        {
            //  pBar.Value = e.Stat;
            // progText.Text = e.Stat.ToString() + "%";
            makeCommit();
        }
        public void makeCommit()
        {
            mainDataGrid.ItemsSource = DataController.mainBase.Products;
        }

        private void exportBD_btn_Click(object sender, RoutedEventArgs e)
        {
            

            Microsoft.Win32.FileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            // Set filter for file extension and default file extension 
            dlg.InitialDirectory = System.IO.Path.Combine(DataHelper.FolderPath(), @"Data");
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файлы баз данных (.xml)|*.xml";
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                DataHelper.SaveToXml(DataController.mainBase, filename);
            }
           
            
        }

        private void importBD_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файлы баз данных (.xml)|*.xml";
            dlg.InitialDirectory = System.IO.Path.Combine(DataHelper.FolderPath(), @"Data");
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                DataController.mainBase = DataHelper.LoadFromXml(filename);
                DataController.openedFile = filename;
                mainDataGrid.ItemsSource = DataController.mainBase.Products;
                DataHelper.RaiseDatabaseUpdateInfo();
            }

            

        }

        #endregion

        /*
        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString(); 
        }

        private void showTableButton_Click(object sender, RoutedEventArgs e)
        {
            ShowTable = !ShowTable;
        }
        */
        private void mainDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataController.MainBaseChanged=true;
        }

        private void chooseStateBtn_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)chooseStat.SelectedValue;
            DataController.mainBase.setStatusToChosen(s);
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            DataController.mainBase.Products.Insert(0, new Product( exploitationPlace:DataController.dataContext.DefaultExpluatationPlace, 
                resp:DataController.dataContext.DefaultResponsible));
            DataController.mainBase.CheckNumeration();
            DataController.MainBaseChanged = true;
        }

        private void chooseAll_Click(object sender, RoutedEventArgs e)
        {
            chooseAll = !chooseAll;
            DataController.mainBase.SelectAll(chooseAll);
        }
        /*
        private void discardAllBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.mainBase.SelectAll(false);
        }
        */
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DataHelper.SaveToXml(DataController.mainBase, DataController.openedFile);
        }

        private void addBD_btn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".xml";
            dlg.Filter = "Файлы баз данных (.xml)|*.xml";
            dlg.InitialDirectory = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data");
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string filename = dlg.FileName;
                ProductDatabase current = new ProductDatabase();
                current = DataHelper.LoadFromXml(filename);
                DataController.mainBase.AddSeveralProducts(current);
                current = null;
                //DataController.mainBase = DataHelper.LoadFromXml(filename);
                mainDataGrid.ItemsSource = DataController.mainBase.Products;
                DataHelper.RaiseDatabaseUpdateInfo();
            }
        }

      

    }

   /* public class ButtonStringConverter:IValueConverter
    {
       

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //Button a = (Button)value;
          //  return a.Content.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }*/

    

   

    
}
