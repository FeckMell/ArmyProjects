using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Deji
{
    public static class UnitsController
    {
        private static Button thatButton;
        private static ComboBox thatRank;
        private static TextBox thatName;
        private static ComboBox thatPart;
        private static ComboBox thatType;

        private static bool thatInited = false;
        
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool AddUnit(Button but_)
        {
            //Init form for easier access to form fields
            if (!thatInited) Init(but_);

            //Get data from form
            Dictionary<string,string> result = new Dictionary<string, string>
            {
                {"Rank", GetRank() },
                {"Name", GetName() },
                {"Part", GetPart() },
                {"Type", GetTypee() }
            };

            //Check for data validation
            if (!CheckValid(result)) return false;

            //Create query
            string query = "INSERT INTO dbo.Units VALUES ( N'{0}', N'{1}', N'{2}', N'{3}' )";
            string s = string.Format(query, result["Name"], result["Rank"], result["Part"], result["Type"]);

            //Add to DB
            SQLConnector.Insert(s);
            
            //Clear form
            ClearFields();

            return true;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void ModifyUnit()
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void DelUnit()
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void Init(Button but_)
        {
            thatInited = true;
            thatButton = but_;

            //Parent for all elements in this form
            StackPanel panel = (but_.Parent as StackPanel).Parent as StackPanel;

            thatRank = (panel.Children[0] as StackPanel).Children[1] as ComboBox;
            thatName = (panel.Children[1] as StackPanel).Children[1] as TextBox;
            thatPart = (panel.Children[2] as StackPanel).Children[1] as ComboBox;
            thatType = (panel.Children[3] as StackPanel).Children[1] as ComboBox;
        }
        private static string GetRank()
        {
            if (thatRank.SelectedValue == null) return "";

            return thatRank.SelectedValue.ToString();
        }
        private static string GetName()
        {
            return thatName.Text;
        }
        private static string GetPart()
        {
            if (thatPart.SelectedValue == null) return "";

            return thatPart.SelectedValue.ToString();
        }
        private static string GetTypee()
        {
            if (thatType.SelectedValue == null) return "";

            return thatType.SelectedValue.ToString();
        }
        private static bool CheckValid(Dictionary<string,string> data_)
        {
            if (string.IsNullOrEmpty(data_["Name"])) return false;
            if (string.IsNullOrEmpty(data_["Rank"])) return false;
            if (string.IsNullOrEmpty(data_["Type"])) return false;
            if (string.IsNullOrEmpty(data_["Part"])) return false;

            return true;
        }
        private static void ClearFields()
        {
            thatRank.SelectedItem = null;
            thatName.Text = "";
            thatPart.SelectedItem = null;
            thatType.SelectedItem = null;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}