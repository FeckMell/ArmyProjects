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
    public class UvalTableElement
    {
        private List<string> thatHeaders;//[0] - superheader, [i] - subheaders.
        private Label thatLabel;
        private DataGrid thatDataGrid;
        private StackPanel thatStackPanel;

        public List<string> ThatHeaders { get => thatHeaders; private set => thatHeaders = value; }
        public Label ThatLabel { get => thatLabel; private set => thatLabel = value; }
        public DataGrid ThatDataGrid { get => thatDataGrid; private set => thatDataGrid = value; }
        public StackPanel ThatStackPanel { get => thatStackPanel; private set => thatStackPanel = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public UvalTableElement()
        {
            GenLabel();
            GenDG();
            GenStack();
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public void SetHeaders(List<string> list_)
        {
            ThatHeaders = new List<string>(list_.ToArray<string>());
            
            //Set SuperHeader name
            ThatLabel.Content = ThatHeaders[0];

            //Set headers for other columns
            for(int i=1; i<ThatHeaders.Count;++i)
            {
                GenColumn(ThatHeaders[i], i - 1, false);
            }

            //Set final header with sum of other columns
            GenColumn("Итого", ThatHeaders.Count, true);
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
                VerticalContentAlignment = VerticalAlignment.Center
            };
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        private void GenDG()
        {
            ThatDataGrid = new DataGrid
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden,
                VerticalScrollBarVisibility = ScrollBarVisibility.Hidden
            };
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
