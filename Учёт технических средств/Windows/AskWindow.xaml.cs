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

namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для AskWindow.xaml
    /// </summary>
    public partial class AskWindow : Window
    {
        public AskWindow()
        {
            InitializeComponent();
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.IsSaved = false;
            DataController.CloseWindow(typeof(AskWindow));
        }

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            DataController.IsSaved = true;
            DataController.CloseWindow(typeof(AskWindow));
        }
    }
}
