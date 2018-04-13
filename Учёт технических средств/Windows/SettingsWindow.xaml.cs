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
using System.IO;

using Учёт_технических_средств;
using Учёт_технических_средств.Helpers;

namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            chkBoxGrid.DataContext = DataController.dataContext;
            //fileChsGrid.DataContext = DataController.dataContext;
            //genStngGrid.DataContext = DataController.dataContext;
        }

       

        private void Save_Btn_Click(object sender, RoutedEventArgs e)
        {
            DataHelper.SaveXmlContext(DataController.dataContext, System.IO.Path.Combine(DataHelper.FolderPath(), "settings.xml"));
            DataController.CloseWindow(typeof(SettingsWindow));
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 

            dlg.DefaultExt = ".xml";
            dlg.Filter = "Excel file (.xml)|*.xml";
            
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {

                // Open document 
                string filename = dlg.FileName;
                fileChooser.Text = filename;

            }


        }
    }
}
