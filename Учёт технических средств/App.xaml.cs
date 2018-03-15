using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


using Учёт_технических_средств.Windows;

namespace Учёт_технических_средств
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Учёт_технических_средств.Windows.ProgressWindow progWind = new ProgressWindow();
            progWind.Show();
            
            
            DataController.initBases();

            progWind.Hide();

            DataController.ShowWindow(typeof(MainWindow));
            //Учёт_технических_средств.MainWindow main = new MainWindow();

           // main.Show();
        }

        

     
        public DataController mainController;
        
    }
}
