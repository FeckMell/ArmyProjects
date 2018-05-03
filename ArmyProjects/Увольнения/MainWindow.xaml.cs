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
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            /*ContentControl table = new ContentControl();
            table.Template = Resources["UvalTable"] as ControlTemplate;
            UvalStack.Children.Add(table);*/
            UvalStack.Children.Add(GenStack(GenLabel(), GenDG()));
        }

        private Label GenLabel()
        {
            Label result = new Label
            {
                Height = 30,
                Content = "Label",
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center
            };
            return result;
        }
        private DataGrid GenDG()
        {
            DataGrid result = new DataGrid
            {
                Height = 490,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            };
            return result;
        }
        private StackPanel GenStack(Label l_, DataGrid dg_)
        {
            StackPanel result = new StackPanel
            {
                Width = 200,
                VerticalAlignment = VerticalAlignment.Stretch,
                Orientation = Orientation.Vertical
            };
            result.Children.Add(l_);
            result.Children.Add(dg_);
            return result;
        }
    }
}
