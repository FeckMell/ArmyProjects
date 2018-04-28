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
            //Init form for easier access to form fields
            if (!thatInited) Init(but_);

            //Get data from form
            Dictionary<string, string> result = new Dictionary<string, string>
            {
                {"IdMan", GetIdMan() },
                {"Time", GetTime() },
                {"Date", GetDate() },
                {"Arrival", GetArrival() },
                {"Drochit", GetDrochit() },
                {"Comment", GetComment() }
            };
            
            //Check for all data entered
            if (!CheckValid(result)) return false;

            //Make query
            string query = "INSERT INTO dbo.Records VALUES ( N'{0}', N'{1}', N'{2}', N'{3}', N'{4}', N'{5}' )";
            string s = string.Format(query, result["IdMan"], result["Date"], result["Time"], result["Arrival"], result["Drochit"], result["Comment"]);

            //Add data to DB
            SQLConnector.Insert(s);

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
        private static bool CheckValid(Dictionary<string,string> data_)
        {
            if (string.IsNullOrEmpty(data_["IdMan"])) return false;
            if (string.IsNullOrEmpty(data_["Date"])) return false;
            if (string.IsNullOrEmpty(data_["Drochit"])) return false;
            if (data_["Arrival"] == "Не пришёл")
            {
                if (!string.IsNullOrEmpty(data_["Time"])) return false;
            }
            else
            {
                if (string.IsNullOrEmpty(data_["Time"])) return false;
            }
            
            return true;
        }
        private static void ClearFields()
        {
            thatArrival.IsChecked = false;
            thatDate.Text = "";
            thatTime.Text = "";
            thatDrochit.SelectedItem = null;
            thatIdMan.SelectedItem = null;

            //var panel = thatComment.Parent as StackPanel;
            //thatComment.Document = new FlowDocument();
            //thatComment = (panel.Children[5] as StackPanel).Children[1] as RichTextBox;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}