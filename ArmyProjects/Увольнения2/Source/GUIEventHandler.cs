using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Увольнения.Source
{
    static public class GUIEventHandler
    {
        static private List<object> thatChangedData = new List<object>();

        static public List<object> ThatChangedData { get => thatChangedData; set => thatChangedData = value; }

        
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
            if (CheckValid_GUIUvalSubTable(e_))
            {
                CalculateSum_GUIUvalSubTable(e_);
                SaveChangedData(e_.Row.Item);
            }
            
            (e_.Row.Item as UvalSubTableRow).OnPropertyChanged("ThatData");
        }
        //*///------------------------------------------------------------------------------------------
        static private void CalculateSum_GUIUvalSubTable(DataGridCellEditEndingEventArgs e_)
        {
            var row = (e_.Row.Item as UvalSubTableRow).ThatData;
            int columnIndex = e_.Column.DisplayIndex;
            row[row.Count - 1] = (Int32.Parse(row[row.Count - 1]) - Int32.Parse(row[columnIndex]) + Int32.Parse(((TextBox)e_.EditingElement).Text)).ToString();
            row[columnIndex] = ((TextBox)e_.EditingElement).Text;
        }
        //*///------------------------------------------------------------------------------------------
        static private bool CheckValid_GUIUvalSubTable(DataGridCellEditEndingEventArgs e_)
        {
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
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Fizo_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            List<string> names = new List<string> { "", "ThatSpeed", "ThatForce", "ThatStamina", "ThatMark", "ThatFree" };
            int columnIndex = e_.Column.DisplayIndex;

            if (CheckValid_Fizo(e_, columnIndex))
            {
                Save_Fizo(e_, columnIndex);
                SaveChangedData(e_.Row.Item);
            }

            (e_.Row.Item as FizoEntry).OnPropertyChanged(names[e_.Column.DisplayIndex]);
        }
        //*///------------------------------------------------------------------------------------------
        private static void Save_Fizo(DataGridCellEditEndingEventArgs e_, int columnIndex_)
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
        private static bool CheckValid_Fizo(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
            bool result = true;
            string error_mess = "";

            switch(columnIndex_)
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
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void BadBoy_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e_)
        {
            List<string> names = new List<string> { "", "ThatGoods", "ThatBads"};
            int columnIndex = e_.Column.DisplayIndex;

            if (CheckValid_BadBoy(e_, columnIndex))
            {
                Save_BadBoy(e_, columnIndex);
                SaveChangedData(e_.Row.Item);
            }

            (e_.Row.Item as BadBoyEntry).OnPropertyChanged(names[e_.Column.DisplayIndex]);
        }
        //*///------------------------------------------------------------------------------------------
        private static void Save_BadBoy(DataGridCellEditEndingEventArgs e_, int columnIndex_)
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
        private static bool CheckValid_BadBoy(DataGridCellEditEndingEventArgs e_, int columnIndex_)
        {
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
    }
}
