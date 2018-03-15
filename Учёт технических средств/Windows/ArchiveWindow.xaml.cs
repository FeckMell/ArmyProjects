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

using Учёт_технических_средств;
using Учёт_технических_средств.Helpers;

namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для ArchiveWindow.xaml
    /// </summary>
    public partial class ArchiveWindow : Window
    {
        public ArchiveWindow()
        {
            InitializeComponent();
            mainDataGrid.ItemsSource = DataController.archive.Products;
            productStateColumn.ItemsSource = DataController.possibleStates;
        }

        private void restoreBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.archive.MoveChosenToAnotherBase(DataController.mainBase);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.archive.DeleteChosenProducts();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
