using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using XAMLive.Models;
using XAMLive.Views;
using XAMLive;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace XAMLive.ViewModels
{
    class Respuesta
    {
        public string nombreAlojamiento { get; set; }
    }

    public class RespPoblaciones
    {        
        public int idPoblacion { get; set; }
        public string nombrePoblacion { get; set; }       
    }
    public class VerViewModel : BindableBase, INavigationAware
    {
        INavigationService _navigationService;

        private ObservableCollection<TablaALOJAMIENTOS> _listaAlojamientos;
        public ObservableCollection<TablaALOJAMIENTOS> listaAlojamientos
        {
            get { return _listaAlojamientos; }
            set { SetProperty(ref _listaAlojamientos, value); }
        }

       
        private ObservableCollection<RespPoblaciones> _poblacionesConAlojamiento;
        public ObservableCollection<RespPoblaciones> poblacionesConAlojamiento
        {
            get { return _poblacionesConAlojamiento; }
            set { SetProperty(ref _poblacionesConAlojamiento, value); }
        }

        private bool _ordenAscendente;
        public bool ordenAscendente
        {
            get { return _ordenAscendente; }
            set { SetProperty(ref _ordenAscendente, value); }
        }

        private string _laquery;
        public string laquery
        {
            get { return _laquery; }
            set { SetProperty(ref _laquery, value); }
        }

        public int idPoblacionActual = -1;

        private int _IndexValue;
        public int IndexValue
        {
            get { return _IndexValue; }
            set { SetProperty(ref _IndexValue, value); }
        }

        private RespPoblaciones _SelectedPoblacion;
        public RespPoblaciones SelectedPoblacion
        {
            get { return _SelectedPoblacion; }
            set { SetProperty(ref _SelectedPoblacion, value); }
        }

        private string _nombreBoton;
        public string nombreBoton
        {
            get { return _nombreBoton; }
            set { SetProperty(ref _nombreBoton, value); }
        }

        private DelegateCommand<string> _recargar;
        public DelegateCommand<string> Recargar =>
            _recargar ?? (_recargar = new DelegateCommand<string>(ExecuteRecargar));

        void ExecuteRecargar(string parameter)
        {
            _navigationService.NavigateAsync("Ver?listado=albergues");
        }

        private DelegateCommand<string> _ordenarPor;
        public DelegateCommand<string> OrdenarPor =>
            _ordenarPor ?? (_ordenarPor = new DelegateCommand<string>(ExecuteOrdenarPor));

        // Para registrar los cambios en el desplegable Población:
        private DelegateCommand<RespPoblaciones> _SelectedPoblationChanged;
        public DelegateCommand<RespPoblaciones> SelectedPoblationChanged =>
            _SelectedPoblationChanged ?? (_SelectedPoblationChanged = new DelegateCommand<RespPoblaciones>(ExecuteSelectedPoblationChanged));

        void ExecuteSelectedPoblationChanged(RespPoblaciones parameter)
        {
            Console.WriteLine("DEBUG - VerVM - ExecuteSelectedPoblationChanged() - entrar...  idPoblacionActual:{0}", idPoblacionActual);

#if DEBUG
            Console.WriteLine("DEBUG - VerVM - ExecuteSelectedPoblationChanged() - entrar...");

            if (parameter == null)
            {
                if (SelectedPoblacion == null)
                    Console.WriteLine("DEBUG - VerVM - ExecuteSelectedPoblationChanged() - parameter y " +
                        "SelectedPoblacion son null. No ohacemos nada");
                else
                {
                    Console.WriteLine("DEBUG - VerVM - ExecuteSelectedPoblationChanged() - parameter es null - " +
                        "SelectedPoblacion ---> idPoblacion:{0}  nombrePoblacion:{1}",
                     SelectedPoblacion.idPoblacion, SelectedPoblacion.nombrePoblacion);
                }
            } else
            {
                Console.WriteLine("DEBUG - VerVM - ExecuteSelectedPoblationChanged()  parameter --> idPoblacion:{0}   nombrePoblacion:{1}",
                               parameter.idPoblacion, parameter.nombrePoblacion);
            }
#endif


            /*
            int entero = 0;
            if (parameter.HasValue)
                entero = parameter.Value;
            */
            idPoblacionActual = parameter == null ? (SelectedPoblacion == null ? idPoblacionActual : SelectedPoblacion.idPoblacion) : parameter.idPoblacion;
            List<TablaALOJAMIENTOS> miLista = App.Database.GetAlojamientosByCity(idPoblacionActual);
            listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(miLista);       

        }


        // Para ejecutar una query que hayamos tecleado:
        private DelegateCommand<string> _ejecutarQuery;
        public DelegateCommand<string> EjecutarQuery =>
            _ejecutarQuery ?? (_ejecutarQuery = new DelegateCommand<string>(ExecuteEjecutarQuery));

        async void ExecuteEjecutarQuery(string parameter)
        {
            Console.WriteLine("DEBUG - VerVM - ExecuteEjecutarQuery() parameter:{0}  laQuery:{1}", 
                parameter, laquery);
            
            //List<TablaALOJAMIENTOS> miLista = await  App.Database.GetAlojamientosAsync();
            List<TablaALOJAMIENTOS> miLista = await App.Database.GetAlojamientosQueryAsync(laquery);

            Console.WriteLine("DEBUG - VerVM - ExecuteEjecutarQuery() Count: {0}", miLista.Count);

            listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(miLista);


        }
  

        void ExecuteOrdenarPor(string parameter)
        {
            Console.WriteLine("DEBUG - VerVM - ExecuteOrdenarPor parameter:{0}   ordenAscendente es {1}", parameter, ordenAscendente);
            Console.WriteLine("DEBUG - VerVM - ExecuteOrdenarPor UriPath: {0}", _navigationService.GetNavigationUriPath());
            //_navigationService.GoBackAsync();

            Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor() ANTES DE ORDENAR");
            int contador = 0;
            foreach (TablaALOJAMIENTOS registro in listaAlojamientos)
            {
                Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor() cont:{0}  Nombre:{1}   precio:{2}", contador, registro.nombreAlojamiento, registro.precio);
                Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor() cont:{0}  Nombre:{1}   precio:{2}  PadLeft:{3}  Split:{4}", contador++,
                    registro.nombreAlojamiento, registro.precio, registro.precio.PadLeft(10, '0'),
                    float.Parse(registro.precio.Split(new char[] { '-', '+' })[0].PadLeft(10, '0')));
                    //registro.precio.Split(new char[] { '-', '+' })[0].PadLeft(10, '0'));
                    //registro.precio.PadLeft(10, '0').Split(new char[] { '-', '+' })[0]);;
            }
                
            if (parameter == "alfabetico")
            {
                if (ordenAscendente)
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderBy(x => x.nombreAlojamiento));
                else
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderByDescending(x => x.nombreAlojamiento));
            }
            else if (parameter == "numplazas")
            {
                if (ordenAscendente)
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderBy(x => x.numPlazas));
                else
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderByDescending(x => x.numPlazas));
            }
            else if (parameter == "precio")
            {
                if (ordenAscendente)
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderBy(x => float.Parse(x.precio.Split(new char[] { '-', '+' })[0].PadLeft(10, '0')) ));
                    //listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderBy(x => ((x.precio.PadLeft(10, '0')).Split(new char[] { '-', '+' }))[0]  ));
                else
                    listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderByDescending(x => float.Parse(x.precio.Split(new char[] { '-', '+' })[0].PadLeft(10, '0'))));
            }
            else
                Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor(): Opción no contemplada!!");

            //listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(listaAlojamientos.OrderBy(x => x.numPlazas));

            Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor() DESPUES DE ORDENAR");
            foreach (TablaALOJAMIENTOS registro in listaAlojamientos)
            {
                Console.WriteLine("DEBUG - VerVM-ExecuteOrdenarPor() Nombre alojamiento:{0}   precio:{1}", registro.nombreAlojamiento, registro.precio);
            }
        }

        public VerViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ordenAscendente = false;
            nombreBoton = "Pulsa ya coñññioooo";

            IndexValue = 5;

            Console.WriteLine("CONSTR - VerViewModel()  idPoblacionActual:{0}", idPoblacionActual);

            // Cargamos el Picker con las poblaciones que tienen alojamientos:
            CargarPoblacionesQueryAsync();
            

        }

        //async IEnumerable<int> EjecutaQueryAsync(string query)
        async void CargarPoblacionesQueryAsync()
        {
            /*
            string query = "select distinct(idPoblacion) from TablaALOJAMIENTOS"; 
            List<RespPoblaciones> miLista = await App.Database._database.QueryAsync<RespPoblaciones>(query);
            
            foreach (RespPoblaciones reg in miLista)
            {
                poblacionesConAlojamiento.Add(reg.idPoblacion.ToString());
            }
            */

            // Para el Picker que contiene el listado de poblaciones con albergue:
            /*
            poblacionesConAlojamiento = new ObservableCollection<RespPoblaciones>();
            poblacionesConAlojamiento.Add("1");
            poblacionesConAlojamiento.Add("37");
            poblacionesConAlojamiento.Add("66");
            poblacionesConAlojamiento.Add("100");
            poblacionesConAlojamiento.Add("300");
            */

            string query = "select distinct(idPoblacion), nombrePoblacion from TablaALOJAMIENTOS INNER JOIN TablaPOBLACIONES ON TablaPOBLACIONES.id = TablaALOJAMIENTOS.idPoblacion";
            List<RespPoblaciones> miLista = await App.Database._database.QueryAsync<RespPoblaciones>(query);
            poblacionesConAlojamiento = new ObservableCollection<RespPoblaciones>(miLista);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            var listado = parameters["listado"] as string;
            var idPoblacion = parameters["idPoblacion"] as string;

            Console.WriteLine("DEBUG - OnNavigatedTo() listado:{0}  idPoblacion:{1}   idPoblacionActual:{2}", listado, idPoblacion, idPoblacionActual);

            if (idPoblacion == null)
            {
                if (listaAlojamientos != null)
                    return; // Utilizamos la lista de Alojamientos que ya tuviésemos en memoria.
                else
                    idPoblacion = idPoblacionActual.ToString();
            }

            string comando = "select count(*) from TablaALOJAMIENTOS where idPoblacion=?";
            //Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() comando:{0}", comando);
            var count = App.Database._db.ExecuteScalar<int>(comando, idPoblacion);
            Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() res:{0}", count);

            List<int> cantidad = App.Database._db.Query<int>(comando, idPoblacion);
            if (cantidad.Count() > 0)
                Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() cantidad:{0}", cantidad[0]);
            else
                Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() La lista cantidad está vacía");

            comando = "select nombreAlojamiento from TablaALOJAMIENTOS where idPoblacion=?";
            List<Respuesta> nombres = App.Database._db.Query<Respuesta>(comando, idPoblacion);
            Console.WriteLine("DEBUG - VerVM-OnNavigatedTo(): Hay {0} alojamientos", nombres.Count());
            foreach (Respuesta nombre in nombres)
            {                
                Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() Nombre alojamiento:{0}", nombre.nombreAlojamiento);
            }

            //comando = "select * from TablaALOJAMIENTOS where idPoblacion=?";
            //List<TablaALOJAMIENTOS> miLista = App.Database._db.Query<TablaALOJAMIENTOS>(comando, idPoblacion);

            idPoblacionActual = int.Parse(idPoblacion);
            List<TablaALOJAMIENTOS> miLista = App.Database.GetAlojamientosByCity(idPoblacionActual);     

            Console.WriteLine("DEBUG - VerVM-OnNavigatedTo(): Hay {0} alojamientos", miLista.Count());
            foreach (TablaALOJAMIENTOS registro in miLista)
            {
                Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() Nombre alojamiento:{0}   precio:{1}", registro.nombreAlojamiento, registro.precio);
            }

            listaAlojamientos = new ObservableCollection<TablaALOJAMIENTOS>(miLista);            

        }
    }
}
