using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace XAMLive.Models
{
    public class TablaCAMINOS
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull, MaxLength(20)]
        public string nombreCortoCamino { get; set; }  // Se utilizará para dar nombre a la tabla en la BD
        [NotNull, MaxLength(30)]
        public string nombreLargoCamino { get; set; }
        public int longitud { get; set; }
        [MaxLength(1000)]
        public string informacion { get; set; }
        [NotNull]
        public DateTime fecUltMod { get; set; }
    }

    public class TablaPOBLACIONES
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [Indexed, MaxLength(30)]
        public string nombrePoblacion { get; set; }
        [MaxLength(15)]
        public string provincia { get; set; }
        public int numHabitantes { get; set; }
        public double latitud { get; set; }
        public double longitud { get; set; }
        public int altitud { get; set; }
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

    public class TablaMisCaminos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }
        [NotNull, Indexed]   // Podriamos ordenar por este campo a la hora de visualizar esta tabla. Pero un cambio en una fecha de un registro obliga a cambiar el resto de fechas en los restantes registros posteriores.
        public DateTime dia { get; set; } // Fecha en la que se sale de "inicioEtapa". O también, fecha en la que se hace noche en finEtapa.
        [NotNull, MaxLength(30)]
        public string caminoBase { get; set; } // Por sUtilizar la nomenclatura "nombreCortoCamino". Si mi camino abarca varios caminos (por ejemplo Camino de Madrid y camino francés), poder identificar a qué camino pertenece esa etapa.
        [MaxLength(30)]
        public string inicioEtapa { get; set; } // contiene el nombrePoblacion desde donde comenzamos ese día.
        [MaxLength(30)]
        public string finEtapa { get; set; } // contiene el nombrePoblacion donde terminamos ese día.
        [MaxLength(100)]
        public string comentarios { get; set; } // Campo para poner comentarios como: día de descanso, visitar ruinas de tal, comer en no sé dónde, etc...
    }

    public class TablaBaseCaminos
    {
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
    }

    public class TablaCaminoDeMadrid : TablaBaseCaminos
    {
        /*
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
        */
    }

    public class TablaSanSalvador : TablaBaseCaminos
    {
        /*
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
        */
    }
}
