using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace Учёт_технических_средств.Helpers
{
    public enum States
    {
        Usual,
        Send,
        Received,
        InService
    }

    public class Category:BindableObject
    {
        public Category(int id=0)
        {
            CategoryId = 0;
        }
        private int categoryId;
        public int CategoryId
        {
            get
            {
                return categoryId;
            }
            set
            {
                if ((0 <= value) && (value <= 3))
                {
                    SetProperty(ref categoryId, value);
                    switch (value)
                    {
                        case 0:
                            CategoryName = "По умолчанию";
                            break;
                        case 1:
                            CategoryName = "Передано";
                            break;
                        case 2:
                            CategoryName = "Получено";
                            break;
                        case 3:
                            CategoryName = "Отправлено в ремонт";
                            break;
                    }
                }
                else throw new NotSupportedException();
                    

            }
        }

        private string categoryName;
        public string CategoryName
        {
            get { return categoryName; }
            set
            {
                SetProperty(ref categoryName, value);
            }
        }
    }

    public class ButtonStringConverter : IValueConverter
    {


        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Button a = (Button)value;
            return a.Content.ToString();
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    [ValueConversion(typeof(bool?), typeof(System.Windows.Visibility))]
    public class BoolVisibleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool? t = (bool)value;
            if (t == true)
                return System.Windows.Visibility.Visible;
            else
                return System.Windows.Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
    
    public class StatesStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int state = (int)value;
            string result;
            switch(state)
            {
                case 0:
                    result = "По умолчанию";
                    break;
                case 1:
                    result = "Передано";
                    break;
                case 2:
                    result = "Отправлено в ремонт";
                    break;
                case 3:
                    result = "Получено";
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }


        
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = (string)value;
            int result = 0;
            if (state.Equals("1"))
                result = 1;
            if (state.Equals("2"))
                result = 2;
            if (state.Equals("3"))
                result = 3;
            return result;
            
        }
    }

    public class RowDefinitionExtended : RowDefinition
    {
        // Variables
        public static DependencyProperty VisibleProperty;

        // Properties
        public Boolean Visible
        {
            get { return (Boolean)GetValue(VisibleProperty); }
            set { SetValue(VisibleProperty, value); }
        }

        // Constructors
        static RowDefinitionExtended()
        {
            VisibleProperty = DependencyProperty.Register("Visible",
                typeof(Boolean),
                typeof(RowDefinitionExtended),
                new PropertyMetadata(true, new PropertyChangedCallback(OnVisibleChanged)));

            RowDefinition.HeightProperty.OverrideMetadata(typeof(RowDefinitionExtended),
                new FrameworkPropertyMetadata(new GridLength(1, GridUnitType.Star), null,
                    new CoerceValueCallback(CoerceHeight)));

            RowDefinition.MinHeightProperty.OverrideMetadata(typeof(RowDefinitionExtended),
                new FrameworkPropertyMetadata((Double)0, null,
                    new CoerceValueCallback(CoerceMinHeight)));
        }

        // Get/Set
        public static void SetVisible(DependencyObject obj, Boolean nVisible)
        {
            obj.SetValue(VisibleProperty, nVisible);
        }
        public static Boolean GetVisible(DependencyObject obj)
        {
            return (Boolean)obj.GetValue(VisibleProperty);
        }

        static void OnVisibleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            obj.CoerceValue(RowDefinition.HeightProperty);
            obj.CoerceValue(RowDefinition.MinHeightProperty);
        }
        static Object CoerceHeight(DependencyObject obj, Object nValue)
        {
            return (((RowDefinitionExtended)obj).Visible) ? nValue : new GridLength(0);
        }
        static Object CoerceMinHeight(DependencyObject obj, Object nValue)
        {
            return (((RowDefinitionExtended)obj).Visible) ? nValue : (Double)0;
        }
    }
    
    
    public class StatesBrushConverter:IValueConverter
    {
        public object  Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string state = (string) value;
            Brush result;
            switch(state)
            {
                case "По умолчанию":
                    result = System.Windows.Media.Brushes.AntiqueWhite;
                    break;
                case "Передано":
                    result = System.Windows.Media.Brushes.Yellow;
                    break;
                case "Отправлено в сервис":
                    result = System.Windows.Media.Brushes.Orange;
                    break;
                case "Получено":
                    result = System.Windows.Media.Brushes.LightSeaGreen;
                    break;
                default:
                    result = System.Windows.Media.Brushes.AntiqueWhite;
                    break;
            }
            return result;

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}