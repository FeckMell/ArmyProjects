using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Uval3.Source
{
    static public class ColorControl
    {
        static private List<ColorEntry> thatColorsGood = new List<ColorEntry>();
        static private List<ColorEntry> thatColorsBad = new List<ColorEntry>();

        static public List<ColorEntry> ThatColorsGood { get => thatColorsGood; set => thatColorsGood = value; }
        static public List<ColorEntry> ThatColorsBad { get => thatColorsBad; set => thatColorsBad = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void InitColors()
        {
            ThatColorsGood.Add(new ColorEntry { ThatID = 0, ThatColor = Brushes.Chocolate });
            ThatColorsGood.Add(new ColorEntry { ThatID = 1, ThatColor = Brushes.PaleVioletRed });
            ThatColorsGood.Add(new ColorEntry { ThatID = 2, ThatColor = Brushes.Coral });
            ThatColorsGood.Add(new ColorEntry { ThatID = 3, ThatColor = Brushes.CornflowerBlue });
            ThatColorsGood.Add(new ColorEntry { ThatID = 4, ThatColor = Brushes.LightGreen });
            ThatColorsGood.Add(new ColorEntry { ThatID = 5, ThatColor = Brushes.Honeydew });

            ThatColorsBad.Add(new ColorEntry { ThatID = 100, ThatColor = Brushes.DimGray });
            ThatColorsBad.Add(new ColorEntry { ThatID = 101, ThatColor = Brushes.Gray });
            ThatColorsBad.Add(new ColorEntry { ThatID = 102, ThatColor = Brushes.LightGray });
            ThatColorsBad.Add(new ColorEntry { ThatID = 103, ThatColor = Brushes.DarkGray });
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void SetColorsForDG()
        {
            int good = 0;
            int bad = 0;
            foreach(var e in DataMan.ThatData)
            {
                if(e.CheckFizoIsGood() && e.CheckBadBoyIsGood())
                {
                    e.ThatColor = ThatColorsGood[good % ThatColorsGood.Count];
                    foreach (var record in e.ThatRecords) record.ThatColor = e.ThatColor;
                    ++good;
                }
                else
                {
                    e.ThatColor = ThatColorsBad[bad % ThatColorsBad.Count];
                    foreach (var record in e.ThatRecords) record.ThatColor = e.ThatColor;
                    ++bad;
                }

                e.ThatFizo.ThatColor = e.ThatColor;
                e.ThatBadBoy.ThatColor = e.ThatColor;
            }
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class ColorEntry
    {
        private int thatID;
        private SolidColorBrush thatColor;

        public int ThatID { get => thatID; set => thatID = value; }
        public SolidColorBrush ThatColor { get => thatColor; set => thatColor = value; }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
    }
    public class IntColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (value as ColorEntry).ThatColor;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
