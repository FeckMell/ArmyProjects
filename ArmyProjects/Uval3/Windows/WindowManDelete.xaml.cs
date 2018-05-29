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
using Uval3.Source;

namespace Uval3.Windows
{
    /// <summary>
    /// Interaction logic for DeleteWindow.xaml
    /// </summary>
    public partial class WindowManDelete : Window
    {
        static private WindowManDelete thatWindow;
        static private DataManEntry thatTarget;
        public static DataManEntry ThatTarget { get => thatTarget; set => thatTarget = value; }
        public static WindowManDelete ThatWindow { get => thatWindow; set => thatWindow = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public WindowManDelete(DataManEntry target_)
        {
            InitializeComponent();
            ThatWindow = this;
            ThatTarget = target_;

            DeleteMessage.Content = ThatTarget.ThatName + "?";
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            DataMan.DeleteDataManEntry(ThatTarget);
            MessageBox.Show("Военнослужащий " + ThatTarget.ThatName + " успешно удален.");
            ThatTarget = null;
            ThatWindow = null;
            Close();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            ThatTarget = null;
            ThatWindow = null;
            Close();
        }
    }
}
