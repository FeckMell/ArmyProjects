using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Uval3.Source
{
    static public class GUIEventHandler
    {
        static private List<object> thatChangedData = new List<object>();

        static public List<object> ThatChangedData { get => thatChangedData; set => thatChangedData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private void SaveChangedData(object data_)
        {
            if (!ThatChangedData.Contains(data_)) ThatChangedData.Add(data_);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void GUIUvalSubTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            if (GUIUvalSubTable_CheckValid(e_))
            {
                CalculateSum_GUIUvalSubTable(e_);
                SaveChangedData(e_.Row.Item);
            }

            (e_.Row.Item as RecordsEntry).OnPropertyChanged();
        }
        //*///------------------------------------------------------------------------------------------
        static private bool GUIUvalSubTable_CheckValid(DataGridCellEditEndingEventArgs e_)
        {
            if (string.IsNullOrEmpty(((TextBox)e_.EditingElement).Text)) return true;
            bool result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i);
            if (result) return result;
            else
            {
                e_.Cancel = true;
                MessageBox.Show("Введено не целое число. Данные должны быть целым числом.");
                return result;
            }
        }
        //*///------------------------------------------------------------------------------------------
        static private void CalculateSum_GUIUvalSubTable(DataGridCellEditEndingEventArgs e_)
        {
            var row = e_.Row.Item as RecordsEntry;
            var col = e_.Column.DisplayIndex;
            var text = (e_.EditingElement as TextBox).Text;
            row.ThatData[col] = text;
            row.ThatResult = row.SumData();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Fizo_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            int columnIndex = e_.Column.DisplayIndex;

            if (Fizo_CheckValid(e_, columnIndex))
            {
                Fizo_Save(e_, columnIndex);
                SaveChangedData(e_.Row.Item);
            }

            (e_.Row.Item as FizoEntry).OnPropertyChanged();
        }
        //*///------------------------------------------------------------------------------------------
        private static bool Fizo_CheckValid(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
            if (string.IsNullOrEmpty(((TextBox)e_.EditingElement).Text)) return true;
            bool result = true;
            string error_mess = "";

            switch (columnIndex_)
            {
                case 1:
                    result = Double.TryParse(((TextBox)e_.EditingElement).Text, out double i1);
                    error_mess = "Введено не число. Данные должны быть числом. Дробная часть отделяется запятой.";
                    break;
                case 2:
                    result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i2);
                    error_mess = "Введено не целое число. Данные должны быть целым числом.";
                    break;
                case 3:
                    result = Double.TryParse(((TextBox)e_.EditingElement).Text, out double i3);
                    error_mess = "Введено не число. Данные должны быть числом. Дробная часть отделяется запятой.";
                    break;
                case 4:
                    result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i4);
                    error_mess = "Введено не целое число. Данные должны быть целым числом.";
                    break;
                default:
                    break;
            }
            if (!result) { e_.Cancel = true; MessageBox.Show(error_mess); }

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        private static void Fizo_Save(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
            switch (columnIndex_)
            {
                case 1:
                    (e_.Row.Item as FizoEntry).ThatSpeed = ((TextBox)e_.EditingElement).Text;
                    break;
                case 2:
                    (e_.Row.Item as FizoEntry).ThatForce = ((TextBox)e_.EditingElement).Text;
                    break;
                case 3:
                    (e_.Row.Item as FizoEntry).ThatStamina = ((TextBox)e_.EditingElement).Text;
                    break;
                case 4:
                    (e_.Row.Item as FizoEntry).ThatMark = ((TextBox)e_.EditingElement).Text;
                    break;
                case 5:
                    (e_.Row.Item as FizoEntry).ThatFree = ((TextBox)e_.EditingElement).Text;
                    break;
                default:
                    break;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void BadBoy_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            int columnIndex = e_.Column.DisplayIndex;

            if (BadBoy_CheckValid(e_, columnIndex))
            {
                BadBoy_Save(e_, columnIndex);
                SaveChangedData(e_.Row.Item);
            }

           (e_.Row.Item as BadBoyEntry).OnPropertyChanged();
        }
        //*///------------------------------------------------------------------------------------------
        private static void BadBoy_Save(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
            switch (columnIndex_)
            {
                case 1:
                    (e_.Row.Item as BadBoyEntry).ThatGoods = ((TextBox)e_.EditingElement).Text;
                    break;
                case 2:
                    (e_.Row.Item as BadBoyEntry).ThatBads = ((TextBox)e_.EditingElement).Text;
                    break;
                default:
                    break;
            }
        }
        //*///------------------------------------------------------------------------------------------
        private static bool BadBoy_CheckValid(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
            if (string.IsNullOrEmpty(((TextBox)e_.EditingElement).Text)) return true;
            bool result = true;
            string error_mess = "";

            switch (columnIndex_)
            {
                case 1:
                    result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i1);
                    error_mess = "Введено не целое число. Данные должны быть целым числом.";
                    break;
                case 2:
                    result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i2);
                    error_mess = "Введено не целое число. Данные должны быть целым числом.";
                    break;
                default:
                    break;
            }
            if (!result) { e_.Cancel = true; MessageBox.Show(error_mess); }

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void ButSaveChangedData_Click(object sender, RoutedEventArgs e_)
        {
            foreach (var e in ThatChangedData)
            {
                if (e is RecordsEntry) (e as RecordsEntry).SaveDataToDB();
                else if (e is FizoEntry) (e as FizoEntry).SaveDataToDB();
                else if (e is BadBoyEntry) (e as BadBoyEntry).SaveDataToDB();
            }

            ThatChangedData.Clear();
            MainWindow.ThatWindow.Update();
            MessageBox.Show("Изменения сохранены.");
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void ButAddPeriod_Click(object sender, RoutedEventArgs e_)
        {
            if (CheckPeriodFields())
            {
                var periodname = (MainWindow.ThatWindow.FindName("PeriodMonth") as TextBox).Text + " " + (MainWindow.ThatWindow.FindName("PeriodYear") as TextBox).Text;
                var dates = CalcPeriodData();
                var records = CalcRecordsTemplate();
                //получить id периода в соответствии с выбранной позицией {TODO}
                var periodid = CalcPeriodID();
                //изменить id всех остальных {TODO}
                //добавить в базу новый период
                SQLConnector.NoReturnQuery(string.Format("INSERT INTO Periods (Name, Data, idPeriod) VALUES ('{0}', '{1}', '{2}')",
                    periodname,
                    dates,
                    periodid
                    ));
                //добавить записи соответствующие всем людям и этому периоду
                var id = SQLConnector.Select(string.Format("SELECT id FROM Periods WHERE idPeriod='{0}'", periodid))[0][0];
                foreach (var e in DataMan.ThatData)
                {
                    SQLConnector.NoReturnQuery(string.Format("INSERT INTO Records (idMan, idPeriod, Data) VALUES ('{0}', '{1}', '{2}')",
                        e.ThatID,
                        id,
                        records
                        ));
                }
                //очистить поля
                (MainWindow.ThatWindow.FindName("PeriodMonth") as TextBox).Text = "";
                (MainWindow.ThatWindow.FindName("PeriodYear") as TextBox).Text = "";
                (MainWindow.ThatWindow.FindName("PeriodDate") as TextBox).Text = "";
                (MainWindow.ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Clear();
                //сообщение об успехе
                MainWindow.ThatWindow.Update();
                MessageBox.Show("Успешно добавлен новый период.");
            }
            else return;
        }
        //*///------------------------------------------------------------------------------------------
        static private bool CheckPeriodFields()
        {
            bool result = true;
            string error = "";
            List<string> names_ = new List<string> { "PeriodYear", "PeriodMonth" };
            List<string> errors = new List<string> { "Введите год.", "Введите месяц." };

            for (int i = 0; i < names_.Count; ++i)
            {
                if (string.IsNullOrEmpty((MainWindow.ThatWindow.FindName(names_[i]) as TextBox).Text))
                {
                    result = false;
                    error += errors[i];
                }
            }

            if ((MainWindow.ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Count == 0)
            {
                result = false;
                error += "Введите даты увольнений.";
            }

            if (!result) MessageBox.Show(error);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        static private string CalcPeriodData()
        {
            string result = "";
            foreach (var e in (MainWindow.ThatWindow.FindName("PeriodDatesList") as ListBox).Items)
                result += e.ToString() + ",";
            result = result.Remove(result.Length - 1);
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        static private int CalcPeriodID()
        {
            int result = Periods.ThatData[Periods.ThatData.Count - 1].ThatPeriodID + 1;
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        static private string CalcRecordsTemplate()
        {
            string result = "";
            for (var i = 0; i < (MainWindow.ThatWindow.FindName("PeriodDatesList") as ListBox).Items.Count - 1; ++i)
            {
                result += ",";
            }
            return result;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void ButAddMan_Click(object sender, RoutedEventArgs e_)
        {
            if (CheckManFields())
            {
                var name = (MainWindow.ThatWindow.FindName("ManNameTextBox") as TextBox).Text;
                var platoon = (MainWindow.ThatWindow.FindName("PlatoonTextBox") as TextBox).Text;
                var mannum = (MainWindow.ThatWindow.FindName("ManNumTextBox") as TextBox).Text;
                //добавить военнослужащего
                SQLConnector.NoReturnQuery(string.Format("INSERT INTO Man (Name, ManNum, Platoon) VALUES ('{0}', '{1}', '{2}')",
                   name,
                   mannum,
                   platoon
                   ));
                var id = SQLConnector.Select(string.Format("SELECT id FROM Man WHERE Name='{0}'", name))[0][0];
                //добавить физо
                SQLConnector.NoReturnQuery(string.Format("INSERT INTO Fizo (manid) VALUES ('{0}')", id));
                //добавить взыскания
                SQLConnector.NoReturnQuery(string.Format("INSERT INTO BadBoy (manid) VALUES ('{0}')", id));
                //добавить записи для каждого периода
                foreach(var e in Periods.ThatData)
                {
                    SQLConnector.NoReturnQuery(string.Format("INSERT INTO Records (idMan, idPeriod, Data) VALUES ('{0}', '{1}', '{2}')",
                        id,
                        e.ThatPeriodID,
                        AddMan_CalcRecordsTemplate(e)
                        ));
                }
                //очистить поля
                (MainWindow.ThatWindow.FindName("ManNameTextBox") as TextBox).Text = "";
                (MainWindow.ThatWindow.FindName("ManNumTextBox") as TextBox).Text = "";
                (MainWindow.ThatWindow.FindName("PlatoonTextBox") as TextBox).Text = "";
                //сообщение об успехе
                MainWindow.ThatWindow.Update();
                MessageBox.Show("Успешно добавлен новый военнослужащий.");
            }
            else return;
        }
        //*///------------------------------------------------------------------------------------------
        private static bool CheckManFields()
        {
            bool result = true;
            string error = "";

            var name = (MainWindow.ThatWindow.FindName("ManNameTextBox") as TextBox).Text;
            var platoon = (MainWindow.ThatWindow.FindName("PlatoonTextBox") as TextBox).Text;
            var mannum = (MainWindow.ThatWindow.FindName("ManNumTextBox") as TextBox).Text;

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
            if (!(string.IsNullOrEmpty(mannum) || Int32.TryParse(mannum, out int i2)))
            {
                result = false;
                error += "Введите ШДК в виде целого числа.";
            }

            if (!result) MessageBox.Show(error);

            return result;
        }
        //*///------------------------------------------------------------------------------------------
        static private string AddMan_CalcRecordsTemplate(PeriodsEntry period_)
        {
            string result = "";
            for (var i = 0; i < period_.ThatDates.Count - 1; ++i)
            {
                result += ",";
            }
            return result;
        }
    }
}
