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

namespace Torrent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UDPSocket thatSocketSend = new UDPSocket("127.0.0.1", 10001, "127.0.0.1", 10000);
        private UDPSocket thatSocketRecv = new UDPSocket("127.0.0.1", 10000, "127.0.0.1", 10001);
        private delegate string UpdateProgressBarDelegate();
        public MainWindow()
        {
            InitializeComponent();
            //MessageBox.Show(FSystem.ListDirContent("D:\\"));

            //thatSocketRecv.ReceiveFrom();

            UpdateProgressBarDelegate updatePbDelegate =new UpdateProgressBarDelegate(thatSocketRecv.ReceiveFrom);

            //Application.Current.Dispatcher.Invoke(updatePbDelegate, System.Windows.Threading.DispatcherPriority.Background);

     

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            thatSocketSend.SendTo("LETS FUCK");
        }
    }
}
