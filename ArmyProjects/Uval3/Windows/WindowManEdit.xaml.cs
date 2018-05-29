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
    /// Interaction logic for WindowEditMan.xaml
    /// </summary>
    public partial class WindowManEdit : Window
    {
        static private WindowManEdit thatWindow;
        static private DataManEntry thatTarget;
        public static WindowManEdit ThatWindow { get => thatWindow; set => thatWindow = value; }
        public static DataManEntry ThatTarget { get => thatTarget; set => thatTarget = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public WindowManEdit(DataManEntry target_)
        {
            InitializeComponent();
            ThatWindow = this;
            ThatTarget = target_;

            EditMessage.Content += ThatTarget.ThatName;

            ManName.Text = ThatTarget.ThatName;
            ManPlatoon.Text = ThatTarget.ThatPlatoon;
            ManWDK.Text = ThatTarget.ThatWDK.ToString();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ButSave_Click(object sender, RoutedEventArgs e)
        {
            string old_name = ThatTarget.ThatName;

            if (CheckFields())
            {
                ThatTarget.ThatName = ManName.Text;
                ThatTarget.ThatPlatoon = ManPlatoon.Text;
                ThatTarget.ThatWDK = Int32.Parse(ManWDK.Text);

                DataMan.SaveDataToDB(ThatTarget);
                MainWindow.ThatWindow.Update();
                MessageBox.Show("Военнослужащий " + old_name + " успешно отредактирован.");
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

            var name = ManName.Text;
            var platoon = ManPlatoon.Text;
            var wdk = ManWDK.Text;

            if (string.IsNullOrEmpty(name))
            {
                result = false;
                error += "Введите имя военнослужащего.";
            }
            if (!(string.IsNullOrEmpty(platoon) || Int32.TryParse(platoon, out int i1)))
            {
                result = false;
                error += "Введите номер взвода в виде целого числа.";
            }
            if (!(string.IsNullOrEmpty(wdk) || Int32.TryParse(wdk, out int i2)))
            {
                result = false;
                error += "Введите ШДК в виде целого числа.";
            }

            if (!result) MessageBox.Show(error);

            return result;
        }
    }
}
