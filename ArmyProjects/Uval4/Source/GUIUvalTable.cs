using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Uval4.Source
{
    static public class GUIUvalTable
    {
        static private List<GUIUvalTableEntry> thatTables = new List<GUIUvalTableEntry>();
        static private DockPanel thatForm = MainWindow.ThatWindow.FindName("UvalStack") as DockPanel;

        public static List<GUIUvalTableEntry> ThatTables { get => thatTables; set => thatTables = value; }
        public static DockPanel ThatForm { get => thatForm; set => thatForm = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
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
            GenContexMenu();
            GenDG();
            GenStack();
            BindData();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void BindData()
        {
            for(int i=0;i<ThatPeriodData.ThatWeeks;++i)
            {
                DataGridTextColumn col_data = new DataGridTextColumn
                {
                    Binding = new Binding(string.Format("ThatRecords[{0}]", i)),
                    Header = string.Format("№{0}", i + 1),
                    IsReadOnly = false,
                    Width = DataGridLength.Auto
                };
                ThatDataGrid.Columns.Add(col_data);
            }

            DataGridTextColumn col_result = new DataGridTextColumn
            {
                Binding = new Binding("ThatResult"),
                Header = "Итого",
                IsReadOnly = true,
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            };
            ThatDataGrid.Columns.Add(col_result);

            ThatDataGrid.ItemsSource = ThatPeriodData.ThatRecords;
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
                BorderBrush = SystemColors.ActiveCaptionTextBrush,
                Content = ThatPeriodData.ThatName
            };
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenContexMenu()
        {
            MenuItem del = new MenuItem { Header = "Удалить период" };
            MenuItem edt = new MenuItem { Header = "Изменить период" };

            del.Click+=GUIEventHandler.PeriodListEdit;
            edt.Click += GUIEventHandler.PeriodListEdit;

            del.ItemStringFormat = ThatPeriodData.ThatID.ToString();
            edt.ItemStringFormat = ThatPeriodData.ThatID.ToString();

            ContextMenu menu = new ContextMenu();
            menu.Items.Add(del);
            menu.Items.Add(edt);

            ThatLabel.ContextMenu = menu;
        }
    }
}
