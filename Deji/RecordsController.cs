using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Deji
{
    public static class RecordsController
    {
        private static Button thatButton;
        private static ComboBox thatIdMan;
        private static TextBox thatDate;
        private static TextBox thatTime;
        private static CheckBox thatArrival;
        private static ComboBox thatDrochit;
        private static RichTextBox thatComment;

        private static bool thatInited = false;
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static bool AddRecord(Button but_)
        {
            if (!thatInited) Init(but_);

            List<string> result = new List<string>
            {
                GetIdMan(),
                GetDate(),
                GetTime(),
                GetArrival(),
                GetDrochit()
            };

            //Check for data validation
            foreach (string e in result) if (e == "") return false;

            //Add comment
            result.Add(GetComment());

            //Add to DB
            SQLConnector.Insert("INSERT INTO dbo.Records VALUES ( '" + result[0] + "','" + result[1] + "','" + result[2] + "','" + result[3] + "','" + result[4] + "','" + result[5] + "' )");

            //Clear form
            ClearFields();

            return true;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void DelRecord()
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public static void ModifyRecord()
        {
            throw new System.NotImplementedException();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private static void Init(Button but_)
        {
            thatInited = true;
            thatButton = but_;

            //Parrent for all elements in this form
            StackPanel panel = (but_.Parent as StackPanel).Parent as StackPanel;

            thatIdMan = (panel.Children[0] as StackPanel).Children[1] as ComboBox;
            thatDate = (panel.Children[1] as StackPanel).Children[1] as TextBox;
            thatTime = (panel.Children[2] as StackPanel).Children[1] as TextBox;
            thatArrival = (panel.Children[3] as StackPanel).Children[1] as CheckBox;
            thatDrochit = (panel.Children[4] as StackPanel).Children[1] as ComboBox;
            thatComment = (panel.Children[5] as StackPanel).Children[1] as RichTextBox;
        }
        private static string GetIdMan()
        {
            if (thatIdMan.SelectedValue == null) return "";

            string unit = thatIdMan.SelectedValue.ToString();
            string idman = unit.Substring(0, unit.IndexOf(":"));

            return idman;
        }
        private static string GetDate()
        {
            return thatDate.Text;
        }
        private static string GetTime()
        {
            return thatTime.Text;
        }
        private static string GetArrival()
        {
            if (thatArrival.IsChecked.Value) return "Пришёл";
            else return "Не пришёл";
        }
        private static string GetDrochit()
        {
            if (thatDrochit.SelectedValue == null) return "";

            return thatDrochit.SelectedValue.ToString();
        }
        private static string GetComment()
        {
            string comment_text = new TextRange(thatComment.Document.ContentStart, thatComment.Document.ContentEnd).Text;
            return comment_text.Substring(0, comment_text.Length - 2);
        }
        private static void ClearFields()
        {
            thatArrival.IsChecked = false;
            thatDate.Text = "";
            thatTime.Text = "";
            thatDrochit.SelectedItem = null;
            thatIdMan.SelectedItem = null;
            thatComment.Document = new FlowDocument();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}