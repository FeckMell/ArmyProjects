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
    /// Interaction logic for WindowEditPeriod.xaml
    /// </summary>
    public partial class WindowPeriodEdit : Window
    {
        static private WindowPeriodEdit thatWindow;
        static private PeriodsEntry thatTarget;
        public static WindowPeriodEdit ThatWindow { get => thatWindow; set => thatWindow = value; }
        public static PeriodsEntry ThatTarget { get => thatTarget; set => thatTarget = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public WindowPeriodEdit(PeriodsEntry target_)
        {
            InitializeComponent();
            ThatWindow = this;
            ThatTarget = target_;

            EditMessage.Content += ThatTarget.ThatName;
            PeriodSelect.ItemsSource = Periods.ThatData;

            string year = ThatTarget.ThatName.Split(' ')[0];
            for (int i = 0; i < PeriodYear.Items.Count; ++i) if ((PeriodYear.Items[i] as TextBlock).Text == year) PeriodYear.SelectedIndex = i;
        
            string month = ThatTarget.ThatName.Split(' ')[1];
            for (int i = 0; i < PeriodMonth.Items.Count; ++i) if ((PeriodMonth.Items[i] as TextBlock).Text == month) PeriodMonth.SelectedIndex = i;

            string weeks = ThatTarget.ThatWeeks.ToString();
            for (int i = 0; i < PeriodWeeks.Items.Count; ++i) if ((PeriodWeeks.Items[i] as TextBlock).Text == weeks) PeriodWeeks.SelectedIndex = i;

            if (Periods.ThatData.Count == 0) { ; }
            if (Periods.ThatData[0] == ThatTarget)
            {
                RadioButBefore.IsChecked = true;
                RadioButAfter.IsChecked = false;
                PeriodSelect.SelectedIndex = 1;
            }
            else
            {
                for (int i = 0; i < Periods.ThatData.Count; ++i) if (Periods.ThatData[i] == ThatTarget) PeriodSelect.SelectedIndex = i - 1;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButSave_Click(object sender, RoutedEventArgs e)
        {

        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            ThatTarget = null;
            ThatWindow = null;
            Close();
        }
    }
}
