using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Увольнения.Source
{
    public class GUIUvalSubTable
    {
        private int thatPeriodID;
        private string thatSuperHeader;
        private List<string> thatHeaders;
        private UvalSubTable thatData;

        private Label thatLabel;
        private DataGrid thatDataGrid;
        private StackPanel thatStackPanel;

        public int ThatPeriodID { get => thatPeriodID; set => thatPeriodID = value; }
        public string ThatSuperHeader { get => thatSuperHeader; set => thatSuperHeader = value; }
        public List<string> ThatHeaders { get => thatHeaders; set => thatHeaders = value; }
        public Label ThatLabel { get => thatLabel; set => thatLabel = value; }
        public DataGrid ThatDataGrid { get => thatDataGrid; set => thatDataGrid = value; }
        public StackPanel ThatStackPanel { get => thatStackPanel; set => thatStackPanel = value; }
        public UvalSubTable ThatData { get => thatData; set => thatData = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public GUIUvalSubTable(List<List<object>> records_)
        {
            GenLabel();
            GenDG();
            GenStack();
            ParseAndBindData(records_);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SetHeaders()
        {
            //Set SuperHeader name
            ThatLabel.Content = ThatSuperHeader;

            //Set headers for other columns
            for (int i = 0; i < ThatHeaders.Count; ++i) GenColumn(ThatHeaders[i], i, false);

            //Set final header with sum of other columns
            GenColumn("Итого", ThatHeaders.Count, true);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void ParseAndBindData(List<List<object>> records_)
        {
            ThatData = new UvalSubTable(records_)
            {
                ThatPeriodID = ThatPeriodID
            };
            ThatDataGrid.ItemsSource = ThatData.ThatData;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenColumn(string header_, int i_, bool readonly_)
        {
            DataGridTextColumn result = new DataGridTextColumn
            {
                Binding = new Binding(String.Format("ThatData[{0}]", i_)),
                Header = header_,
                IsReadOnly = readonly_
            };
            ThatDataGrid.Columns.Add(result);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenLabel()
        {
            ThatLabel = new Label
            {
                Height = 30,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Top,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                BorderThickness = new Thickness(1, 1, 1, 1),
                BorderBrush = SystemColors.ActiveCaptionTextBrush
            };
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenDG()
        {
            ThatDataGrid = new DataGrid
            {
                CanUserAddRows = false,
                AutoGenerateColumns = false,
                CanUserDeleteRows=false,
                CanUserReorderColumns=false,
                CanUserSortColumns = false,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            };
            ThatDataGrid.CellEditEnding += GUIEventHandler.GUIUvalSubTable_CellEditEnding;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenStack()
        {
            ThatStackPanel = new StackPanel
            {
                Width = 250,
                VerticalAlignment = VerticalAlignment.Stretch,
                Orientation = Orientation.Vertical
            };
            ThatStackPanel.Children.Add(ThatLabel);
            ThatStackPanel.Children.Add(ThatDataGrid);
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
}
