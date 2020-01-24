using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XAMLive.Views.DataTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CellAlojamiento : ViewCell
    {
        public CellAlojamiento()
        {
            Console.WriteLine("CONSTR - CellAlojamiento()");
            InitializeComponent();
        }
    }
}