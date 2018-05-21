using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Увольнения.Source
{
    static public class EditControl
    {
        static private Window thatWindow;

        public static Window ThatWindow { get => thatWindow; set => thatWindow = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Init(Window window_)
        {
            ThatWindow = window_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void AddPeriod(object sender, RoutedEventArgs e_)
        {
            if (CheckPeriodFields())
            {
                //получить id периода в соответствии с выбранной позицией
                //изменить id всех остальных
                //добавить в базу новый период
                SQLConnector.NoReturnQuery(string.Format("INSERT INTO Periods (Name, Data, idPeriod) VALUES ('{0}', '{1}', '{2}')", 
                    (ThatWindow.FindName("PeriodMonth") as TextBox).Text + " " + (ThatWindow.FindName("PeriodYear") as TextBox).Text,
                    CalcPeriodData(),
                    CalcPeriodID()
                    ));
                //добавить записи соответствующие всем людям и этому периоду
                foreach (var e in DataMan.ThatData)
                {
                    SQLConnector.NoReturnQuery(string.Format("INSERT INTO Records (idMan, idPeriod, Data) VALUES ('{0}', '{1}', '{2}')",
                        e.ThatID,
                        SQLConnector.Select("SELECT id FROM Periods WHERE idPeriod='"+ CalcPeriodID() + "'")[0][0],
                        CalcPeriodDataForMan()
                        ));
                }
                //очистить поля
                (ThatWindow.FindName("PeriodMonth") as TextBox).Text = "";
                (ThatWindow.FindName("PeriodYear") as TextBox).Text = "";
                (ThatWindow.FindName("PeriodDate") as TextBox).Text = "";
                (ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Clear();
                //сообщение об успехе
                MessageBox.Show("Успешно добавлен новый период.");
            }
            else return;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static bool CheckPeriodFields()
        {
            bool result = true;
            string error = "";
            List<string> names_ = new List<string>{ "PeriodYear" , "PeriodMonth" };
            List<string> errors = new List<string> { "Введите год.", "Введите месяц." };

            for(int i=0;i<names_.Count;++i)
            {
                if (string.IsNullOrEmpty((ThatWindow.FindName(names_[i]) as TextBox).Text))
                {
                    result = false;
                    error += errors[i];
                }
            }

            if ((ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Count==0)
            {
                result = false;
                error += "Введите даты увольнений.";
            }

            if (!result) MessageBox.Show(error);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private string CalcPeriodData()
        {
            string result = "";
            foreach (var e in (ThatWindow.FindName("PeriodDatesList") as ListBox).Items)
                result += e.ToString() + ",";
            result = result.Remove(result.Length - 1);
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private int CalcPeriodID()
        {
            int result = GUIUvalTable.ThatTables[GUIUvalTable.ThatTables.Count - 1].ThatPeriodID + 1;
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private string CalcPeriodDataForMan()
        {
            string result = "";
            for (var i = 0; i < (ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Count - 1; ++i)
            {
                result += ",";
            }
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
