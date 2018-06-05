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

namespace Uval4.Windows
{
    /// <summary>
    /// Interaction logic for WindowPeriodDelete.xaml
    /// </summary>
    public partial class WindowPeriodDelete : Window
    {
        static private WindowPeriodDelete thatWindow;
        static private PeriodsEntry thatTarget;
        public static PeriodsEntry ThatTarget { get => thatTarget; set => thatTarget = value; }
        public static WindowPeriodDelete ThatWindow { get => thatWindow; set => thatWindow = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public WindowPeriodDelete(PeriodsEntry target_)
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
            ThatTarget.DeleteFromDB();
            MainWindow.ThatWindow.Update();
            MessageBox.Show("Период \"" + ThatTarget.ThatName + "\" успешно удален.");
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
