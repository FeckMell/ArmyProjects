﻿using System;
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

            if (Periods.ThatData.Count == 0) {; }
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
            string old_name = ThatTarget.ThatName;

            if (CheckFields())
            {
                ThatTarget.ThatName = PeriodYear.Text + " " + PeriodMonth.Text;
                ThatTarget.ThatWeeks = Int32.Parse(PeriodWeeks.Text);
                ThatTarget.ThatPeriodPosition = CalcPeriodPosition();

                ThatTarget.SaveChangesToDB();
                MainWindow.ThatWindow.Update();
                MessageBox.Show("Период \"" + old_name + "\" успешно отредактирован.");
                ThatTarget = null;
                ThatWindow = null;
                Close();
            }
            else
            {
                return;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButCancel_Click(object sender, RoutedEventArgs e)
        {
            ThatTarget = null;
            ThatWindow = null;
            Close();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private bool CheckFields()
        {
            bool result = true;
            string error = "";

            if (PeriodYear.SelectedIndex == -1)
            {
                result = false;
                error += "Введите год для периода.";
            }
            if (PeriodMonth.SelectedIndex == -1)
            {
                result = false;
                error += "Введите месяц для периода.";
            }
            if (PeriodWeeks.SelectedIndex == -1)
            {
                result = false;
                error += "Введите количество дней увольнений для периода.";
            }
            if (PeriodSelect.SelectedIndex == -1)
            {
                result = false;
                error += "Введите позицию периода.";
            }

            if (!result) MessageBox.Show(error);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private int CalcPeriodPosition()
        {
            if (Periods.ThatData.Count == 0) return 0;
            int index = PeriodSelect.SelectedIndex;

            if (RadioButAfter.IsChecked.Value)
            {
                for (int i = index + 1; i < Periods.ThatData.Count; ++i)
                {
                    Periods.ThatData[i].ThatPeriodPosition += 1;
                    Periods.ThatData[i].SaveChangesToDB();
                }
                return Periods.ThatData[index].ThatPeriodPosition + 1;
            }
            else
            {
                for (int i = index; i < Periods.ThatData.Count; ++i)
                {
                    Periods.ThatData[i].ThatPeriodPosition += 1;
                    Periods.ThatData[i].SaveChangesToDB();
                }
                return Periods.ThatData[index].ThatPeriodPosition - 1;
            }
        }
    }
}
