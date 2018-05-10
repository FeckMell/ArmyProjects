using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Увольнения.Source;

namespace Увольнения
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Initers();
            Binders();
            DataUvalManager.GetMan();            
        }
        private void Initers()
        {
            SQLConnector.Init();
            DataMan.Init();
            DataUvalManager.Init();
        }
        private void Binders()
        {
            GridNames.ItemsSource = DataMan.ThatData;
        }
        private void InitData()
        {
            UvalTableElementGUI v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Декабрь 2017", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Январь 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Февраль 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Март 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Апрель 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Май 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
            v = new UvalTableElementGUI();
            v.SetHeaders(new List<string> { "Июнь 2018", "1-2", "7-8", "11-13", "20-21", "27-28" });
            UvalStack.Children.Add(v.ThatStackPanel);
        }
    }
}
