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
        static private int thatGoodCounter = 0;
        static private int thatBadCounter = 0;

        static private List<ColorEntry> thatColorsGood = new List<ColorEntry>();
        static private List<ColorEntry> thatColorsBad = new List<ColorEntry>();

        public static int ThatGoodCounter { get => thatGoodCounter; set => thatGoodCounter = value; }
        public static int ThatBadCounter { get => thatBadCounter; set => thatBadCounter = value; }

        static public List<ColorEntry> ThatColorsGood { get => thatColorsGood; set => thatColorsGood = value; }
        static public List<ColorEntry> ThatColorsBad { get => thatColorsBad; set => thatColorsBad = value; }

        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public void InitColors()
        {
            ThatColorsGood.Add(new ColorEntry { ThatColor = Brushes.Chocolate });
            ThatColorsGood.Add(new ColorEntry { ThatColor = Brushes.PaleVioletRed });
            ThatColorsGood.Add(new ColorEntry { ThatColor = Brushes.Coral });
            ThatColorsGood.Add(new ColorEntry { ThatColor = Brushes.CornflowerBlue });
            ThatColorsGood.Add(new ColorEntry { ThatColor = Brushes.LightGreen });

            ThatColorsBad.Add(new ColorEntry {  ThatColor = Brushes.DimGray });
            ThatColorsBad.Add(new ColorEntry {  ThatColor = Brushes.Gray });
            ThatColorsBad.Add(new ColorEntry {  ThatColor = Brushes.LightGray });
            ThatColorsBad.Add(new ColorEntry {  ThatColor = Brushes.DarkGray });
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public ColorEntry Analyse(DataManEntry man_)
        {
            if (CheckFizoIsGood(man_) && CheckBadBoyIsGood(man_))
            {
                ThatGoodCounter = (ThatGoodCounter + 1) % ThatColorsGood.Count;
                return ThatColorsGood[ThatGoodCounter];
            }
            else
            {
                ThatBadCounter = (ThatBadCounter + 1) % ThatColorsBad.Count;
                return ThatColorsBad[ThatBadCounter];
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static public SolidColorBrush SetColorForElement(bool value_)
        {
            if(value_)
            {
                ThatGoodCounter = (ThatGoodCounter + 1) % ThatColorsGood.Count;
                return ThatColorsGood[ThatGoodCounter].ThatColor;
            }
            else
            {
                ThatBadCounter = (ThatBadCounter + 1) % ThatColorsBad.Count;
                return ThatColorsBad[ThatBadCounter].ThatColor;
            }
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private bool CheckFizoIsGood(DataManEntry man_)
        {
            if (man_.ThatMark == "" || man_.ThatMark == null || man_.ThatMark == "2") return false;
            else return true;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        static private bool CheckBadBoyIsGood(DataManEntry man_)
        {
            if (man_.ThatBads == "" || man_.ThatBads == null || man_.ThatBads == "0") return true;
            else return false;
        }
    }
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    //*///------------------------------------------------------------------------------------------
    public class ColorEntry
    {
        private SolidColorBrush thatColor;
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
            //return ColorControl.SetColorForElement((bool)value);
            return ((ColorEntry)value).ThatColor;
        }
        //*///------------------------------------------------------------------------------------------
        //*///------------------------------------------------------------------------------------------
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
