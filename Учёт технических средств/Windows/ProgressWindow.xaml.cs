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
using Учёт_технических_средств.Helpers;


namespace Учёт_технических_средств.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProgressWindow.xaml
    /// </summary>
    public partial class ProgressWindow : Window, INotifyPropertyChanged
    {
        public ProgressWindow()
        {
            val = 0;
            InitializeComponent();
            DataHelper.ProgressInfo += ProgressUpdated;
           // pBar.Value = 0;
          //  progText.Text = "0%";
        }

        private int val;
        public int Val { 
            get { return val; }
            set { 
                val = value;
                OnPropertyChanged("Val");
            }
        }

        private void ProgressUpdated(object sender, ProgressInfoEventArgs e)
        {
          //  pBar.Value = e.Stat;
           // progText.Text = e.Stat.ToString() + "%";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          //  pBar.Value += 10;
        }
    }

}
