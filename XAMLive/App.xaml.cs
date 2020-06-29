using Prism;
using Prism.Ioc;
using XAMLive.ViewModels;
using XAMLive.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;
using XAMLive.Data;
using System.IO;
using System.Reflection;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XAMLive
{
    public partial class App
    {
        static Database database;
        public string camino = null;

        public static Database Database
        {
            get
            {
                string pathSQLite;
                if (database == null)
                {
                    //database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CaminoSantiago.db3"));
                    pathSQLite = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CaminoSantiago.db3");
                    Console.WriteLine("Path SQLite: " + pathSQLite);
                    System.Console.WriteLine("DEBUG - App:Database  Se va a llamar a new Database()");
                    database = new Database(pathSQLite);
                    //Console.WriteLine("Path SQLite: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)));
                }
                else
                    System.Console.WriteLine("DEBUG - App:Database  database no es null");
                return database;
            }
        }

        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            Console.WriteLine("DEBUG - AppOnInitialized()");
            await NavigationService.NavigateAsync("NavigationPage/MisCaminos");

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //containerRegistry.RegisterForNavigation<Ver, VerViewModel>();
            containerRegistry.RegisterForNavigation<MisCaminos, MisCaminosViewModel>();
        }
    }

    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            Console.WriteLine("DEBUG - ImageResourceExtension - ProvideValue  Source <{0}>", Source == null ? "NULL" : Source);

            if (Source == null)
            {
                return null;
            }

            // Do your translation lookup here, using whatever method you require
            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);

            return imageSource;
        }
    }
}
