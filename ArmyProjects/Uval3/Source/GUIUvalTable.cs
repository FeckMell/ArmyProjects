using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Uval3.Source
{
    static public class GUIUvalTable
    {
        static private List<GUIUvalTableEntry> thatTables = new List<GUIUvalTableEntry>();
        static private DockPanel thatForm;

        public static List<GUIUvalTableEntry> ThatTables { get => thatTables; set => thatTables = value; }
        public static DockPanel ThatForm { get => thatForm; set => thatForm = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Init(DockPanel panel_)
        {
            ThatForm = panel_;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Update()
        {
            Clear();
            CreateSubTables();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private void CreateSubTables()
        {
            foreach(var e in Periods.ThatData)
            {
                GUIUvalTableEntry subtable = new GUIUvalTableEntry(e);
                ThatTables.Add(subtable);
                ThatForm.Children.Add(subtable.ThatStackPanel);
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void Clear()
        {
            ThatForm.Children.Clear();
            ThatTables.Clear();
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class GUIUvalTableEntry
    {
        private PeriodsEntry thatPeriodData;

        private Label thatLabel;
        private DataGrid thatDataGrid;
        private StackPanel thatStackPanel;

        public PeriodsEntry ThatPeriodData { get => thatPeriodData; set => thatPeriodData = value; }
        public Label ThatLabel { get => thatLabel; set => thatLabel = value; }
        public DataGrid ThatDataGrid { get => thatDataGrid; set => thatDataGrid = value; }
        public StackPanel ThatStackPanel { get => thatStackPanel; set => thatStackPanel = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public GUIUvalTableEntry(PeriodsEntry e_)
        {
            ThatPeriodData = e_;

            GenLabel();
            GenDG();
            GenStack();
            BindData();
        }

        private void BindData()
        {
            for(int i= 0; i < ThatPeriodData.ThatDates.Count;++i )
            {
                DataGridTextColumn column1 = new DataGridTextColumn
                {
                    Binding = new Binding(string.Format("ThatData[{0}]", i)),
                    Header = ThatPeriodData.ThatDates[i],
                    IsReadOnly = false
                };
                ThatDataGrid.Columns.Add(column1);
            }

            DataGridTextColumn column2 = new DataGridTextColumn
            {
                Binding = new Binding("ThatResult"),
                Header = "Итого",
                IsReadOnly = true
            };
            ThatDataGrid.Columns.Add(column2);

            ThatDataGrid.ItemsSource = ThatPeriodData.ThatRecords;
        }

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

        private void GenDG()
        {
            ThatDataGrid = new DataGrid
            {
                CanUserAddRows = false,
                AutoGenerateColumns = false,
                CanUserDeleteRows = false,
                CanUserReorderColumns = false,
                CanUserSortColumns = false,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                RowStyle = MainWindow.ThatWindow.Resources["DataGridCellColoringKey"] as Style,
            };
            ThatDataGrid.CellEditEnding += GUIEventHandler.GUIUvalSubTable_CellEditEnding;
            //Grid.SetRow(ThatDataGrid, 1);
        }

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
                BorderBrush = SystemColors.ActiveCaptionTextBrush,
                Content = ThatPeriodData.ThatName
            };
            //Grid.SetRow(ThatLabel, 0);
        }
    }
}
