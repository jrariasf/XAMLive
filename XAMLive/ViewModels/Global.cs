using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XAMLive.Models;

namespace XAMLive.ViewModels
{
    public class Global : BindableBase
    {
        public static char[] separador = { ';' };
        public static char[] separadorDePares = { '#' };
        public static CultureInfo culture = new CultureInfo("en-US");
        public static String[] diaDeLaSemana = new String[] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

        public static string nombreFicheroDeMiCamino = null;
        public static string subetapasModificadas = null;
        public static string appName = AppInfo.Name; // Application Name
        public static string packageName = AppInfo.PackageName; // Package Name/Application Identifier


        public Global()
        {
            

        }

        public static int FuncionGlobal(int numero, out int resultado)
        {
            resultado = numero * 4;
            return resultado * 2;
        }

    }

    public class Etapa
    {
        public int orden { get; set; }
        public string dia { get; set; }
        public string poblacion_inicio_etapa { get; set; }
        public string poblacion_fin_etapa { get; set; }
        //public string distancia { get; set; }
        public double distancia { get; set; }

        public Etapa(int _orden, string _dia, string _poblacion_INI, string _poblacion_FIN, double _distancia)
        {
            orden = _orden;
            dia = _dia;
            poblacion_inicio_etapa = _poblacion_INI;
            poblacion_fin_etapa = _poblacion_FIN;
            distancia = _distancia;
        }
    }


    public class DameImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return !string.IsNullOrEmpty($"{value}");
            //var valor = (bool)object;
            string source;

            if (value == null || !value.GetType().Equals(typeof(bool)))
            {
                source = "XAMLive.Images." + parameter + ".png";
            }
            else
            {
                string valor = ((bool)value).ToString().ToLower();
                source = "XAMLive.Images." + parameter + "_" + valor + ".png";
            }

            Console.WriteLine("DEBUG - DameImageSourceConverter - source <{0}>", source);

            var imageSource = ImageSource.FromResource(source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
