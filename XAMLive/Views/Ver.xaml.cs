using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XAMLive.Views
{
    public partial class Ver : ContentPage
    {
        public Ver()
        {
            
            InitializeComponent();
            Console.WriteLine("DEBUG - Ver.Ver()");
        }
    }

    public class StringNullOrEmptyBoolConverter : IValueConverter
    {
        // De momento lo utilizo en CellAlojamiento.xaml para que la etiqueta "subtipo" no se muestre si el valor del subtipo es 0 no tiene nada.
        // Info en: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/data-binding/converters
        /// <summary>Returns false if string is null or empty o its vale is 0
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = value as string;
            return !(string.IsNullOrWhiteSpace(s) || s == "0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}