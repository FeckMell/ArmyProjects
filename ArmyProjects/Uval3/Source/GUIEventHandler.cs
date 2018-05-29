using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Uval3.Windows;

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
        static private void RememberChangedData(object data_)
        {
            if (!ThatChangedData.Contains(data_)) ThatChangedData.Add(data_);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void GUIUvalSubTable_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            GGUIUvalTable.Work(e_);
        }
        //*///------------------------------------------------------------------------------------------
        static public void Fizo_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            GGUIFizoEdit.Work(e_);
        }
        //*///------------------------------------------------------------------------------------------
        static public void BadBoy_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            GGUIBadBoyEdit.Work(e_);
        }
        //*///------------------------------------------------------------------------------------------
        static public void ButAddPeriod_Click(object sender, RoutedEventArgs e_)
        {
            GGUIAddPeriod.Work();
        }
        //*///------------------------------------------------------------------------------------------
        static public void ButAddMan_Click(object sender, RoutedEventArgs e_)
        {
            GGUIAddMan.Work();
        }
        //*///------------------------------------------------------------------------------------------
        static public void ButSaveChangedData_Click(object sender, RoutedEventArgs e_)
        {
            foreach (var e in ThatChangedData)
            {
                if (e is DataManEntry) (e as DataManEntry).SaveDataToDB();
                if (e is RecordsEntry) (e as RecordsEntry).ThatMan.SaveDataToDB();
            }

            ThatChangedData.Clear();
            MainWindow.ThatWindow.Update();
            MessageBox.Show("Изменения сохранены.");
        }
        //*///------------------------------------------------------------------------------------------
        static public void ManListEdit(object sender, RoutedEventArgs e)
        {
            string action = (e.Source as MenuItem).Header.ToString();
            DataManEntry target = GUIItemConteiner.ThatGridNames.CurrentItem as DataManEntry;
            if (action == "Удалить военнослужащего")
            {
                WindowManDelete win = new WindowManDelete(target);
                win.ShowDialog();
            }
            else if (action == "Изменить военнослужащего")
            {
                WindowManEdit win = new WindowManEdit(target);
                win.ShowDialog();
            }
        }
        //*///------------------------------------------------------------------------------------------
        static public void PeriodListEdit(object sender, RoutedEventArgs e)
        {
            string action = (e.Source as MenuItem).Header.ToString();
            int periodid = Int32.Parse((e.Source as MenuItem).ItemStringFormat);

            if (action == "Удалить период")
            {
                //WindowDeleteMan win = new WindowDeletePeriod(Periods.GetPeriodByID(periodid));
                //win.ShowDialog();
            }
            else if (action == "Изменить период")
            {
                WindowPeriodEdit win = new WindowPeriodEdit(Periods.GetPeriodByID(periodid));
                win.ShowDialog();
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GGUIAddMan
        {
            static public void Work()
            {
                if (CheckManFields())
                {
                    var name = GUIItemConteiner.ThatManName.Text;
                    var platoon = GUIItemConteiner.ThatManPlatoon.Text;
                    var wdk = GUIItemConteiner.ThatManWDK.Text;
                    var records = AddMan_CalcRecordsTemplate();
                    //добавить военнослужащего
                    SQLConnector.NoReturnQuery(string.Format("INSERT INTO Man (Name, WDK, Platoon, Records) VALUES ('{0}', '{1}', '{2}', '{3}')",
                       name,
                       wdk,
                       platoon,
                       records
                       ));
                    //очистить поля
                    GUIItemConteiner.ThatManName.Text = "";
                    GUIItemConteiner.ThatManPlatoon.Text = "";
                    GUIItemConteiner.ThatManWDK.Text = "";

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

                var name = GUIItemConteiner.ThatManName.Text;
                var platoon = GUIItemConteiner.ThatManPlatoon.Text;
                var wdk = GUIItemConteiner.ThatManWDK.Text;

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
            //*///------------------------------------------------------------------------------------------
            static private string AddMan_CalcRecordsTemplate()
            {
                string result = "";
                foreach (var e in Periods.ThatData)
                {
                    result += e.ThatID.ToString() + ":";
                    for (var i = 0; i < e.ThatWeeks - 1; ++i) result += ",";
                    result += "|";
                }
                if (string.IsNullOrEmpty(result)) return "";
                else return result.Remove(result.Length - 1);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GGUIUvalTable
        {
            static public void Work(DataGridCellEditEndingEventArgs e_)
            {
                if (GUIUvalSubTable_CheckValid(e_))
                {
                    CalculateSum_GUIUvalSubTable(e_);
                    RememberChangedData(e_.Row.Item);
                }
            (e_.Row.Item as RecordsEntry).OnPropertyChanged();
            }
            //*///------------------------------------------------------------------------------------------
            static private bool GUIUvalSubTable_CheckValid(DataGridCellEditEndingEventArgs e_)
            {
                if (string.IsNullOrEmpty(((TextBox)e_.EditingElement).Text)) return true;
                bool result = Int32.TryParse(((TextBox)e_.EditingElement).Text, out int i);
                if (result)
                {
                    return result;
                }
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
                row.ThatRecords[col] = text;
                row.ThatResult = row.SumData();
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GGUIFizoEdit
        {
            static public void Work(DataGridCellEditEndingEventArgs e_)
            {
                int columnIndex = e_.Column.DisplayIndex;

                if (Fizo_CheckValid(e_, columnIndex))
                {
                    Fizo_Save(e_, columnIndex);
                    RememberChangedData(e_.Row.Item as DataManEntry);
                }

            (e_.Row.Item as DataManEntry).OnPropertyChanged();
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
                        (e_.Row.Item as DataManEntry).ThatSpeed = ((TextBox)e_.EditingElement).Text;
                        break;
                    case 2:
                        (e_.Row.Item as DataManEntry).ThatForce = ((TextBox)e_.EditingElement).Text;
                        break;
                    case 3:
                        (e_.Row.Item as DataManEntry).ThatStamina = ((TextBox)e_.EditingElement).Text;
                        break;
                    case 4:
                        (e_.Row.Item as DataManEntry).ThatMark = ((TextBox)e_.EditingElement).Text;
                        break;
                    case 5:
                        (e_.Row.Item as DataManEntry).ThatFreedom = ((TextBox)e_.EditingElement).Text;
                        break;
                    default:
                        break;
                }
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GGUIBadBoyEdit
        {
            static public void Work(DataGridCellEditEndingEventArgs e_)
            {
                int columnIndex = e_.Column.DisplayIndex;

                if (BadBoy_CheckValid(e_, columnIndex))
                {
                    BadBoy_Save(e_, columnIndex);
                    RememberChangedData(e_.Row.Item as DataManEntry);
                }

           (e_.Row.Item as DataManEntry).OnPropertyChanged();
            }
            //*///------------------------------------------------------------------------------------------
            private static void BadBoy_Save(DataGridCellEditEndingEventArgs e_, int columnIndex_)
            {
                switch (columnIndex_)
                {
                    case 1:
                        (e_.Row.Item as DataManEntry).ThatGoods = ((TextBox)e_.EditingElement).Text;
                        break;
                    case 2:
                        (e_.Row.Item as DataManEntry).ThatBads = ((TextBox)e_.EditingElement).Text;
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
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GGUIAddPeriod
        {
            static public void Work()
            {
                string year = GUIItemConteiner.ThatPeriodYear.Text;
                string month = GUIItemConteiner.ThatPeriodMonth.Text;
                int weeks = GUIItemConteiner.ThatPeriodWeeks.SelectedIndex + 1;

                if (CheckPeriodFields())
                {
                    //get period position {TODO}
                    var position = CalcPeriodPosition();

                    //change positions for other periods in DB{TODO}
                    //Add new period to DB
                    SQLConnector.NoReturnQuery(string.Format("INSERT INTO Periods (Name, Weeks, PeriodPosition) VALUES ('{0}', '{1}', '{2}')",
                        year + " " + month,
                        weeks,
                        position
                        ));
                    //Add records for all people for this period
                    var qqq = string.Format("SELECT id FROM Periods WHERE PeriodPosition='{0}'", position);
                    int id = Int32.Parse(SQLConnector.Select(string.Format("SELECT id FROM Periods WHERE PeriodPosition='{0}'", position))[0][0].ToString());
                    //Make record template
                    RecordsEntry record = new RecordsEntry(weeks, id);

                    foreach (var e in DataMan.ThatData)
                    {
                        e.ThatRecords.Add(record);
                        e.SaveDataToDB();
                    }
                    //очистить поля
                    GUIItemConteiner.ThatPeriodYear.SelectedIndex = -1;
                    GUIItemConteiner.ThatPeriodMonth.SelectedIndex = -1;
                    GUIItemConteiner.ThatPeriodWeeks.SelectedIndex = -1;
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

                if (GUIItemConteiner.ThatPeriodYear.SelectedIndex == -1)
                {
                    result = false;
                    error += "Введите год для периода.";
                }
                if (GUIItemConteiner.ThatPeriodMonth.SelectedIndex == -1)
                {
                    result = false;
                    error += "Введите месяц для периода.";
                }
                if (GUIItemConteiner.ThatPeriodWeeks.SelectedIndex == -1)
                {
                    result = false;
                    error += "Введите количество дней увольнений для периода.";
                }
                if (GUIItemConteiner.ThatPeriodSelect.SelectedIndex == -1)
                {
                    result = false;
                    error += "Введите позицию периода.";
                }

                if (!result) MessageBox.Show(error);

                return result;
            }
            //*///------------------------------------------------------------------------------------------
            static private int CalcPeriodPosition()
            {
                if (Periods.ThatData.Count == 0) return 0;
                int index = GUIItemConteiner.ThatPeriodSelect.SelectedIndex;

                if(GUIItemConteiner.ThatRadioButtonAfter.IsChecked.Value)
                {
                    for (int i = index + 1; i < Periods.ThatData.Count; ++i)
                    {
                        Periods.ThatData[i].ThatPeriodPosition += 1;
                        Periods.ThatData[i].SaveDataToDB();
                    }
                    return Periods.ThatData[index].ThatPeriodPosition + 1;
                }
                else
                {
                    for (int i = index; i < Periods.ThatData.Count; ++i)
                    {
                        Periods.ThatData[i].ThatPeriodPosition += 1;
                        Periods.ThatData[i].SaveDataToDB();
                    }
                    return Periods.ThatData[index].ThatPeriodPosition - 1;
                }
            }
            //*///------------------------------------------------------------------------------------------
            static private string CalcRecordsTemplate()
            {
                string result = "";
                for (var i = 0; i < GUIItemConteiner.ThatPeriodWeeks.SelectedIndex; ++i)//last week without ','
                {
                    result += ",";
                }
                return result;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private class GUIItemConteiner
        {
            //Add Man
            static private TextBox thatManName = MainWindow.ThatWindow.FindName("ManName") as TextBox;
            static private TextBox thatManWDK = MainWindow.ThatWindow.FindName("ManWDK") as TextBox;
            static private TextBox thatManPlatoon = MainWindow.ThatWindow.FindName("ManPlatoon") as TextBox;

            //Add Period
            static private ComboBox thatPeriodWeeks = MainWindow.ThatWindow.FindName("PeriodWeeks") as ComboBox;
            static private ComboBox thatPeriodMonth = MainWindow.ThatWindow.FindName("PeriodMonth") as ComboBox;
            static private ComboBox thatPeriodYear = MainWindow.ThatWindow.FindName("PeriodYear") as ComboBox;
            static private RadioButton thatRadioButtonBefore = MainWindow.ThatWindow.FindName("RadioButBefore") as RadioButton;
            static private RadioButton thatRadioButtonAfter = MainWindow.ThatWindow.FindName("RadioButAfter") as RadioButton;
            static private ComboBox thatPeriodSelect = MainWindow.ThatWindow.FindName("PeriodSelect") as ComboBox;

            //UvalTable
            static private DataGrid thatGridNames = MainWindow.ThatWindow.FindName("GridNames") as DataGrid;

            public static TextBox ThatManName { get => thatManName; set => thatManName = value; }
            public static TextBox ThatManWDK { get => thatManWDK; set => thatManWDK = value; }
            public static TextBox ThatManPlatoon { get => thatManPlatoon; set => thatManPlatoon = value; }
            public static ComboBox ThatPeriodWeeks { get => thatPeriodWeeks; set => thatPeriodWeeks = value; }
            public static ComboBox ThatPeriodMonth { get => thatPeriodMonth; set => thatPeriodMonth = value; }
            public static ComboBox ThatPeriodYear { get => thatPeriodYear; set => thatPeriodYear = value; }
            public static RadioButton ThatRadioButtonBefore { get => thatRadioButtonBefore; set => thatRadioButtonBefore = value; }
            public static RadioButton ThatRadioButtonAfter { get => thatRadioButtonAfter; set => thatRadioButtonAfter = value; }
            public static ComboBox ThatPeriodSelect { get => thatPeriodSelect; set => thatPeriodSelect = value; }
            public static DataGrid ThatGridNames { get => thatGridNames; set => thatGridNames = value; }
        }
    }
}
