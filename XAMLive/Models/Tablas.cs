using System;
using SQLite;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace XAMLive.Models
{
    public class RespString
    {
        public string nombrePoblacion { get; set; }
    }
    public class TablaCAMINOS : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int _longitud;
        private void RaisePropertyChanged(string propertyName = null)
        {
            //Console.WriteLine("DEBUG3 - Tablas - TablaPOBLACIONES - RaisePropertyChanged{0}", propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull, MaxLength(20)]
        public string nombreCortoCamino { get; set; }  // Se utilizará para dar nombre a la tabla en la BD
        [NotNull, MaxLength(30)]
        public string nombreLargoCamino { get; set; }
        public int longitud
        {
            get { return _longitud; }
            set
            {
                if (_longitud != value)
                {
                    _longitud = value;
                    RaisePropertyChanged(nameof(longitud));
                }
            }
        }
        [MaxLength(1000)]
        public string informacion { get; set; }
        [NotNull]
        public DateTime fecUltMod { get; set; }
    }

    public class TablaPOBLACIONES : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        int _altitud;
        private void RaisePropertyChanged(string propertyName = null)
        {
            //Console.WriteLine("DEBUG3 - Tablas - TablaPOBLACIONES - RaisePropertyChanged{0}", propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed, MaxLength(30)]
        public string nombrePoblacion { get; set; }
        [MaxLength(15)]
        public string provincia { get; set; }
        public int numHabitantes { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int altitud
        {
            get { return _altitud; }
            set
            {
                if (_altitud != value)
                {
                    _altitud = value;
                    RaisePropertyChanged(nameof(altitud));
                }
            }
        }
        public bool albergueMunicipal { get; set; }
        public bool albergueParroquial { get; set; }
        public bool alberguePrivado { get; set; }
        public bool restaurante { get; set; }
        public bool cafeteria { get; set; }
        public bool tienda { get; set; }
        public bool cajero { get; set; }
        public bool fuente { get; set; }
        public bool farmacia { get; set; }
        public bool hospital { get; set; }
        public bool bus { get; set; }
        public bool tren { get; set; }
        public bool oficinaDeCorreos { get; set; }
        public DateTime fecUltMod { get; set; }
    }

    public class TablaALOJAMIENTOS
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed]
        public int idPoblacion { get; set; }
        [MaxLength(50)]
        public string nombreAlojamiento { get; set; }
        [MaxLength(50)]
        public string direccion { get; set; }
        [MaxLength(10)]
        public string tipo { get; set; }
        [MaxLength(12)]
        public string subTipo { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        [MaxLength(25)]
        public string personaContacto { get; set; }
        [MaxLength(40)]
        public string email { get; set; }
        [MaxLength(50)]
        public string web { get; set; }
        [MaxLength(12)]
        public string telefono1 { get; set; }
        [MaxLength(12)]
        public string telefono2 { get; set; }
        [MaxLength(12)]
        public string telefono3 { get; set; }
        [MaxLength(20)]
        public string precio { get; set; }
        [MaxLength(50)]
        public string disponibilidad { get; set; } // Fechas en las que está disponible el albergue
        public int numPlazas { get; set; }
        public int numHabitaciones { get; set; }
        public bool soloPeregrinos { get; set; }
        public bool admiteReserva { get; set; }
        [MaxLength(5)]
        public string horaApertura { get; set; }
        [MaxLength(5)]
        public string horaCierre { get; set; }
        public bool accesibilidad { get; set; } // Acceso a personas con movilidad reducida
        public bool taquillas { get; set; }
        public bool sabanas { get; set; }  // Si dan sábanas.
        public bool mantas { get; set; }
        public bool toallas { get; set; }
        public bool lavadero { get; set; }
        public bool lavadora { get; set; }
        public bool secadora { get; set; }
        public bool calefaccion { get; set; }
        public bool cocina { get; set; }
        public bool microondas { get; set; }
        public bool frigorifico { get; set; }
        public bool maquinaBebidas { get; set; }
        public bool maquinaVending { get; set; }
        public bool jardin { get; set; } // Si tiene un lugar cerrado para las bicis
        public bool piscina { get; set; }
        public bool bicis { get; set; }
        public bool establo { get; set; }
        public bool mascotas { get; set; } // Si admiten o no mascotas
        public bool wifi { get; set; }
        [MaxLength(300)]
        public string observaciones { get; set; }
        public DateTime fecUltMod { get; set; }
    }

    // Tabla donde cada usuario de la aplicación guardará la información básica de sus caminos, es decir, un nombre identificativo,
    // el camino base del que se parte, un comentario general, las bifurcaciones definidas, las etapas establecidas.
    // Tengo que ver si la fecha de inicio y el inicio y fin de etapa se ponen también aquí. También podría ser opcional el número de kms total.  
    public class TablaMisCaminos: IEquatable<TablaMisCaminos>
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull, MaxLength(30)]
        public string miNombreCamino { get; set; }
        [MaxLength(100)]
        public string descripcion { get; set; } // Pequeña descripción de mi camino.
        [NotNull, MaxLength(20)]
        public string caminoBase { get; set; } // Podemos utilizar la nomenclatura "nombreCortoCamino". Si mi camino abarca varios caminos (por ejemplo Camino de Madrid y camino francés), poder identificar a qué camino pertenece esa etapa.
        [NotNull, Indexed]   
        public DateTime dia { get; set; } // Fecha en la que se iniciaría este camino.
        [MaxLength(500)]
        public string bifurcaciones { get; set; } // Contiene pares inicioBifurcacion#poblacionSiguiente separados por ";". En caso de coger la bifurcación por defecto, no hace falta contemplarla aquí.
        [MaxLength(1000)]
        public string etapas { get; set; } // Contiene una cadena o listado de poblaciones en las que se hace noche, separadas por punto y coma.
                                           // Además, entre corchetes se podría poner algún comentario. O incluso además del comentario podría ponerse el número de días de esa etapa.
                                           // En ese caso podríamos usar el "#" como separador. Por ejemplo: Tábara#2#Vuelta por la Sierra de la Culebra;Rionegro;Requejo
                                           // Con eso indicaría que en Tábara me quedo un día y al día siguiente saldría hacia Rionegro que sería la siguiente etapa. Y al día siguiente hasta Requejo.        
        public int numDias { get; set; }
        public int numEtapas { get; set; }
        public double longitud { get; set; }
        public bool Equals(TablaMisCaminos other)
        {
            //throw new NotImplementedException();
            Console.WriteLine("DEBUG - Tablas - TablaMisCaminos - Equals this.id:{0}", this.id);
            if (other is null)
                return false;

            if (other.id == this.id)
                return true;

            return false;

        }

        public TablaMisCaminos() { }
        public TablaMisCaminos(int _id)
        {
            id = _id;
        }
        public TablaMisCaminos(string _miNombreCamino, string _descripcion, string _caminoBase,
                               string _dia, string _bifurcaciones, string _etapas,
                               int _numDias, int _numEtapas, double _longitud)
        {
            miNombreCamino = _miNombreCamino;
            descripcion = _descripcion;
            caminoBase = _caminoBase;
            try
            {
                dia = Convert.ToDateTime(_dia);
#pragma warning disable CS0168 // La variable 'e' se ha declarado pero nunca se usa
            } catch (InvalidCastException e)
#pragma warning restore CS0168 // La variable 'e' se ha declarado pero nunca se usa
            {
                Console.WriteLine("DEBUG3 - Tablas - TablaMisCaminos() Ha saltado la excepción al hacer el ToDateTime de {0}", _dia);
                dia = System.DateTime.Today;
            }
            bifurcaciones = _bifurcaciones;
            etapas = _etapas;
            numDias = _numDias;
            numEtapas = _numEtapas;
            longitud = _longitud;

        }
    }

    public class TablaBaseCaminos : INotifyPropertyChanged, IEquatable<TablaBaseCaminos>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        byte _esEtapa;
        double _acumuladoEtapa;
        private void RaisePropertyChanged(string propertyName = null)
        {
            //Console.WriteLine("DEBUG3 - Tablas - TablaBaseCaminos - RaisePropertyChanged{0}", propertyName);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        

        public bool Equals(TablaBaseCaminos other)
        {
            //throw new NotImplementedException();
            if (other is null)
                return false;

            if (other.id != -1)
            {
                if (other.id == this.id) // || other.nombrePoblacion == this.nombrePoblacion) //_xx_ULT Ha sido comentado pq al pinchar en una bifurcación en el CN en Pontarrón, borraba las dos apariciones de "Bifurcación Hazas-Villanueva" pero igual este comentario afecta a algo inesperado !!!
                    return true;
            } else
            {
                if (other.nombrePoblacion != null && (other.nombrePoblacion == this.nombrePoblacion))
                    return true;
            }

            return false;

        }

        public TablaBaseCaminos()
        {
            Init();
        }
        public TablaBaseCaminos(int _id)
        {
            Init();
            id = _id;
            //nombrePoblacion = "";
        }
        public TablaBaseCaminos(string nodo)
        {
            Init();
            nombrePoblacion = nodo;
            id = -1;
            
        }

        public void Init()
        {
            nombrePoblacion = null;
            checkboxEnabled = true;
        }

        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed, MaxLength(30)]
        public string nombrePoblacion { get; set; }
        public bool IniBifurcacion { get; set; }
        public bool FinBifurcacion { get; set; }
        [MaxLength(100)]
        public string nodosAnteriores { get; set; }
        [MaxLength(100)]
        public string nodosSiguientes { get; set; }
        [MaxLength(25)]
        public string distanciaNodosSiguientes { get; set; }
        //public DateTime fecUltMod { get; set; }
        [Ignore]
        public double acumulado { get; set; }
        [Ignore]
        public double acumuladoEtapa
        {
            get { return _acumuladoEtapa; }
            set
            {
                if (_acumuladoEtapa != value)
                {
                    _acumuladoEtapa = value;
                    RaisePropertyChanged(nameof(acumuladoEtapa));
                }
            }
        }

        [Ignore]
        public double distanciaAlFinal { get; set; }
        [Ignore]
        public bool esVisible { get; set; }
        [Ignore]
        public bool tieneAlbergue { get; set; }
        [Ignore]
        public byte esEtapa   // Para el checbox que sirve para establecer las etapas
                              //_xx_ETAPAS Podría ser un entero de forma que si es > 0 inicaría el número de días.
                              // Ej: si esEtapa vale 2 significaría que el primer día te quedas ahí y el siguiente sales hacia la siguiente etapa.
        {
            get { return _esEtapa; }
            set
            {
                if (_esEtapa != value)
                {
                    _esEtapa = value;
                    RaisePropertyChanged(nameof(esEtapa));
                }
            }
        }
        [Ignore]
        public bool checkboxEnabled { get; set; }  // Se va a utilizar para no permitir modificar el checkbox que señala un nodo como etapa.
                                                   // Por ejemplo, cuando se ven los nodos de una etapa, el primero y el último aparecerán marcados
                                                   // y no se permitirá modificarlos (es decir, desactivarlos).

    }

    public class TablaCaminoDeMadrid : TablaBaseCaminos
    {
        
    }

    public class TablaSanSalvador : TablaBaseCaminos
    {
        
    }

    public class TablaSanabres : TablaBaseCaminos
    {

    }
    public class TablaFinisterre : TablaBaseCaminos
    {

    }

    public class TablaCaminoDelNorte : TablaBaseCaminos
    {

    }

    public class TablaPrimitivo : TablaBaseCaminos
    {

    }
}
