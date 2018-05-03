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
using Учёт_технических_средств.Windows;
namespace Учёт_технических_средств
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

        private void databaseSelectionBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.ShowWindow(typeof(WorkWindow));

        }

        private void archiveBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.ShowWindow(typeof(ArchiveWindow));
        }

        private void settingsBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.ShowWindow(typeof(SettingsWindow));
        }
    }
}
