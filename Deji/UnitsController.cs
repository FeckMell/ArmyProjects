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
            if (!thatInited) Init(but_);

            List<string> result = new List<string>
            {
                GetRank(),
                GetName(),
                GetPart(),
                GetTypee()
            };

            //Check for data validation
            foreach (string e in result) if (e == "") return false;

            //Add to DB
            SQLConnector.Insert("INSERT INTO dbo.Units VALUES ( '" + result[1] + "','" + result[0] + "','" + result[2] + "','" + result[3] + "' )");
            
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

            //Parrent for all elements in this form
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