using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using XAMLive.Models;

namespace XAMLive.ViewModels
{
    public class MisCaminosViewModel : BindableBase, INavigationAware, INotifyPropertyChanged
    {
        INavigationService _navigationService;

        public new event PropertyChangedEventHandler PropertyChanged;
        private new void RaisePropertyChanged(string propertyName = null)
        {
            Console.WriteLine("DEBUG3 - MisCaminosVM - RaisePropertyChanged{0}", propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TablaMisCaminos> _listaMisCaminos;
        public ObservableCollection<TablaMisCaminos> listaMisCaminos
        {
            get { return _listaMisCaminos; }
            ///set { SetProperty(ref _listaPoblaciones, value); }
            set
            {
                if (_listaMisCaminos == value)
                    return;
                SetProperty(ref _listaMisCaminos, value);
                RaisePropertyChanged(nameof(listaMisCaminos));
            }
        }


        private DelegateCommand<string> _AddCaminoClicked;
        public DelegateCommand<string> AddCaminoClicked =>
            _AddCaminoClicked ?? (_AddCaminoClicked = new DelegateCommand<string>(ExecuteAddCaminoClicked));

        void ExecuteAddCaminoClicked(string parameter)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteAddCaminoClicked({0})", parameter);
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteAddCaminoClicked  UriPath: {0}", _navigationService.GetNavigationUriPath());
            //_navigationService.NavigateAsync("EntryCAMINOS");
        }

        private DelegateCommand<TablaMisCaminos> _ItemTappedCommand;
        public DelegateCommand<TablaMisCaminos> ItemTappedCommand =>
            _ItemTappedCommand ?? (_ItemTappedCommand = new DelegateCommand<TablaMisCaminos>(ExecuteItemTappedCommand));

        void ExecuteItemTappedCommand(TablaMisCaminos camino)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteItemTappedCommand({0})  entrar...", camino);
            var navigationParams = new NavigationParameters();
            navigationParams.Add("camino", camino);
            //_navigationService.NavigateAsync("EntryCAMINOS", navigationParams);
        }
       

        private DelegateCommand<string> _BorrarMiCamino;
        public DelegateCommand<string> BorrarMiCamino =>
            _BorrarMiCamino ?? (_BorrarMiCamino = new DelegateCommand<string>(ExecuteBorrarMiCamino));

        async void ExecuteBorrarMiCamino(string id)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino  id:{0}", id);
            var respuesta = false;
            Device.BeginInvokeOnMainThread(async () =>
            {
                respuesta = await App.Current.MainPage.DisplayAlert("Aviso", "Confirme que quiere borrar el camino:", "Borrar", "Cancelar");    //("Error", "Hay que dar un nombre a tu camino", "OK");
            });

            if (respuesta)
            {
                Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino  SE VA A BORRAR EL CAMINO!!!");
                /*
                int res = await App.Database.DeleteMiCaminoAsync(int.Parse(id));
                Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino  Se han borrado {0} registros", res);

                if (res > 0)
                {
                    Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino: Se va a quitar de la lista el camino para que no se vea");
                    TablaMisCaminos tmc = new TablaMisCaminos(int.Parse(id));
                    bool quitar = false;
                    quitar = listaMisCaminos.Remove(tmc);
                    Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino: Remove ha devuelto {0}", quitar);
                }
                */
            }
            else
                Console.WriteLine("DEBUG - MisCaminosVM - ExecuteBorrarMiCamino  SE CANCELA EL BORRADO DEL CAMINO");
        }

        private DelegateCommand<string> _AmpliarMiCamino;
        public DelegateCommand<string> AmpliarMiCamino =>
            _AmpliarMiCamino ?? (_AmpliarMiCamino = new DelegateCommand<string>(ExecuteAmpliarMiCamino));

        async void ExecuteAmpliarMiCamino(string id)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteAmpliarMiCamino  id:{0}", id);
            var navigationParams = new NavigationParameters();

            TablaMisCaminos tmc = await App.Database.GetMisCaminosAsync(int.Parse(id));

            if (tmc == null)
            {
                Console.WriteLine("DEBUG3 - MisCaminosVM - ExecuteAmpliarMiCamino NO HAY REGISTROS. retornamos");
                return;
            }

            navigationParams.Add("option", 2);
            navigationParams.Add("tmc", tmc);
#pragma warning disable CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
            //_navigationService.NavigateAsync("VerCamino", navigationParams);            
#pragma warning restore CS4014 // Como esta llamada no es 'awaited', la ejecución del método actual continuará antes de que se complete la llamada. Puede aplicar el operador 'await' al resultado de la llamada.
        }

        private DelegateCommand<string> _VerEtapasMiCamino;
        public DelegateCommand<string> VerEtapasMiCamino =>
            _VerEtapasMiCamino ?? (_VerEtapasMiCamino = new DelegateCommand<string>(ExecuteVerEtapasMiCamino));

        async void ExecuteVerEtapasMiCamino(string id)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - ExecuteVerEtapasMiCamino  id:{0}", id);
            var navigationParams = new NavigationParameters();

            TablaMisCaminos tmc = await App.Database.GetMisCaminosAsync(int.Parse(id));

            if (tmc == null)
            {
                Console.WriteLine("DEBUG3 - MisCaminosVM - ExecuteVerEtapasMiCamino NO HAY REGISTROS. retornamos");
                Global.nombreFicheroDeMiCamino = null;
                return;
            }


            navigationParams.Add("tmc", tmc);
            //await _navigationService.NavigateAsync("VerEtapas", navigationParams);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - OnNavigatedFrom");
        }


        public void OnNavigatedTo(INavigationParameters parameters)
        {           
            var navigationMode = parameters.GetNavigationMode();
            Console.WriteLine("DEBUG3 - MisCaminosVM - OnNavigatedTo GetNavigationMode() es {0}", navigationMode);

            if (navigationMode == NavigationMode.Back)
            {
                Console.WriteLine("DEBUG3 - MisCaminosVM - OnNavigatedTo Vamos a recargar mis caminos");
                CargarCaminosAsync();
            }
        }

        async void CargarCaminosAsync()
        {
            Console.WriteLine("DEBUG - MisCaminosVM - CargarCaminosAsync");
            //string query = "select * from TablaCAMINOS";
            List<TablaMisCaminos> miLista = await App.Database.GetMisCaminosAsync(); // QueryAsync<TablaCAMINOS>(query);
            listaMisCaminos = new ObservableCollection<TablaMisCaminos>(miLista);
        }
        public MisCaminosViewModel(INavigationService navigationService)
        {
            Console.WriteLine("DEBUG - MisCaminosVM - Constructor");
            _navigationService = navigationService;

            CargarCaminosAsync();


        }
    }
}
