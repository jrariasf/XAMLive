using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using XAMLive.Models;



namespace XAMLive.Data
{
    public class Database
    {
        public readonly SQLiteAsyncConnection _database;
        public readonly SQLiteConnection _db;
        //readonly SQLiteAsyncConnection _database;
        public Database(string dbPath)
        {
            System.Console.WriteLine("DEBUG - Database: Vamos a abrir la BD y a crear las tablas");
            _database = new SQLiteAsyncConnection(dbPath);
            _db = new SQLiteConnection(dbPath);
            _database.CreateTableAsync<TablaCAMINOS>().Wait();
            _database.CreateTableAsync<TablaPOBLACIONES>().Wait();
            _database.CreateTableAsync<TablaALOJAMIENTOS>().Wait();

            _database.CreateTableAsync<TablaMisCaminos>().Wait();

            _database.CreateTableAsync<TablaCaminoDeMadrid>().Wait();
            _database.CreateTableAsync<TablaSanSalvador>().Wait();
            _database.CreateTableAsync<TablaSanabres>().Wait();
            _database.CreateTableAsync<TablaFinisterre>().Wait();
            _database.CreateTableAsync<TablaCaminoDelNorte>().Wait();
            _database.CreateTableAsync<TablaPrimitivo>().Wait();

        }


        public List<TablaBaseCaminos> GetPoblacionesCamino(string camino)
        {
            string sql = "select * from Tabla" + camino;

            return _db.Query<TablaBaseCaminos>(sql);
        }
        public Task<List<TablaBaseCaminos>> GetPoblacionesCaminoAsync(string camino)
        {
            string sql = "select * from Tabla" + camino;

            return _database.QueryAsync<TablaBaseCaminos>(sql);
        }

        public List<string> GetPoblacionesConAlbergue(string camino)
        {
            string sql = "select TablaPOBLACIONES.nombrePoblacion from TablaPOBLACIONES INNER JOIN Tabla" + camino +
                         " ON TablaPOBLACIONES.nombrePoblacion = Tabla" + camino +
                         ".nombrePoblacion where TablaPOBLACIONES.albergueMunicipal = 1 OR TablaPOBLACIONES.albergueParroquial = 1 OR TablaPOBLACIONES.alberguePrivado = 1";

            List<RespString> result = _db.Query<RespString>(sql);

            List<string> miLista = new List<string>();
            foreach (RespString item in result)
            {
                miLista.Add(item.nombrePoblacion);
            }

            return miLista;

        }


        public Task<List<TablaCaminoDeMadrid>> GetCaminoDeMadridAsync()
        {
            return _database.Table<TablaCaminoDeMadrid>().ToListAsync();
        }

        public Task<List<TablaSanSalvador>> GetCaminoSanSalvadorAsync()
        {
            return _database.Table<TablaSanSalvador>().ToListAsync();
        }

        //CAMINOS:
        public Task<List<TablaCAMINOS>> GetCaminosAsync()
        {
            return _database.Table<TablaCAMINOS>().ToListAsync();
        }

        public Task<TablaCAMINOS> GetCaminosAsync(int id) => _database.Table<TablaCAMINOS>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();

        public Task<int> SaveCaminosAsync(TablaCAMINOS note)
        {
            if (note.id != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteCaminosAsync(TablaCAMINOS note)
        {
            return _database.DeleteAsync(note);
        }


        // POBLACIONES:
        public Task<List<TablaPOBLACIONES>> GetPoblacionesAsync()
        {
            return _database.Table<TablaPOBLACIONES>().ToListAsync();
        }

   
        public Task<TablaPOBLACIONES> GetPoblacionesAsync(int id) => _database.Table<TablaPOBLACIONES>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();

        public string DameNombrePoblacionPorId(int id)
        {
            string comando = "select nombrePoblacion from TablaPOBLACIONES where id=?"; // + id;
            //var cmd = new SQLiteCommand(_db);
            List<TablaPOBLACIONES> miLista = _db.Query<TablaPOBLACIONES>(comando, id);
            Console.WriteLine(miLista[0].nombrePoblacion);
            return miLista[0].nombrePoblacion;

        }

        public int DameIdPoblacionDeNombre(string poblacion)
        {
            string comando = "select id from TablaPOBLACIONES where nombrePoblacion=?";
            //Console.WriteLine("DEBUG - VerVM-OnNavigatedTo() comando:{0}", comando);
            var idPoblacion = App.Database._db.ExecuteScalar<int>(comando, poblacion);
            Console.WriteLine("DEBUG - DameIdPoblacionDeNombre - La poblacion {0} se corresponde con id: {1}", 
                poblacion, idPoblacion);
            return idPoblacion; //_xx_ Ver qué pasa si pedimos el id de una población que no existe !!!
        }

        public Task<List<TablaPOBLACIONES>> DamePoblacionesPorNombre(string poblacion)
        {
            string comando = "select * from TablaPOBLACIONES where nombrePoblacion=?"; // + id;
            //var cmd = new SQLiteCommand(_db);
            //List<TablaPOBLACIONES> miLista = _database.QueryAsync<TablaPOBLACIONES>(comando, poblacion);            
            return _database.QueryAsync<TablaPOBLACIONES>(comando, poblacion);

        }
        public async Task<int> SavePoblacionesAsync(TablaPOBLACIONES note)
        {           
            if (note == null)
            {
                Console.WriteLine("DEBUG - SavePoblacionesAsync(null)  retornamos -1");
                return -1; 
            }
            
            if (note.id != 0)
            {
                return await _database.UpdateAsync(note);
            }
            else
            {
                return await _database.InsertAsync(note);
            }
        }

        public Task<int> DeletePoblacionesAsync(TablaPOBLACIONES note)
        {
            return _database.DeleteAsync(note);
        }


        // ALOJAMIENTOS:
        public Task<List<TablaALOJAMIENTOS>> GetAlojamientosAsync()
        {
            return _database.Table<TablaALOJAMIENTOS>().ToListAsync();
        }

        public Task<TablaALOJAMIENTOS> GetAlojamientosAsync(int id) => _database.Table<TablaALOJAMIENTOS>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();

        public Task<List<TablaALOJAMIENTOS>> GetAlojamientosByIdPoblacionAsync(int id) => _database.Table<TablaALOJAMIENTOS>()
                            .Where(i => i.idPoblacion == id)
                            .ToListAsync();
        public List<TablaALOJAMIENTOS> GetAlojamientosByIdPoblacion(int idPoblacion)
        {
            Console.WriteLine("DEBUG - GetAlojamientosByCity: idPoblacion:{0}", idPoblacion);
            //List<TablaALOJAMIENTOS> miLista = _db.Table<TablaALOJAMIENTOS>().Where(i => i.idPoblacion == id).ToList();

            string comando = "select * from TablaALOJAMIENTOS where idPoblacion=?";
            List<TablaALOJAMIENTOS> miLista = _db.Query<TablaALOJAMIENTOS>(comando, idPoblacion);

            Console.WriteLine("DEBUG - GetAlojamientosByCity: Count:{0}", miLista.Count);

            return miLista;
        }

        /*
        public Task<List<TablaALOJAMIENTOS>> GetAlojamientosQueryAsync(string query)
        {
            Console.WriteLine("DEBUG - GetAlojamientosQueryAsync() query:{0}", query);           
            return _database.QueryAsync<TablaALOJAMIENTOS>(query);             
        }
        */
        public async Task<List<TablaALOJAMIENTOS>> GetAlojamientosQueryAsync(string query)
        {
            Console.WriteLine("DEBUG - GetAlojamientosQueryAsync() query:{0}", query);
            //List<TablaALOJAMIENTOS> miLista;
            //miLista = await _database.QueryAsync<TablaALOJAMIENTOS>(query);
            //miLista = _db.Query<TablaALOJAMIENTOS>(query);
            //return miLista;
            return await _database.QueryAsync<TablaALOJAMIENTOS>(query);
        }

        public Task<int> SaveAlojamientosAsync(TablaALOJAMIENTOS note)
        {
            if (note.id != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteAlojamientosAsync(TablaALOJAMIENTOS note)
        {
            return _database.DeleteAsync(note);
        }

        //MisCaminos:
        public Task<List<TablaMisCaminos>> GetMisCaminosAsync()
        {
            return _database.Table<TablaMisCaminos>().ToListAsync();
        }
        public Task<TablaMisCaminos> GetMisCaminosAsync(int id) => _database.Table<TablaMisCaminos>()
                            .Where(i => i.id == id)
                            .FirstOrDefaultAsync();

        async public Task<int> SaveMiCaminoAsync(TablaMisCaminos note)
        {
            if (note.id != 0)
            {
                return await _database.UpdateAsync(note);
            }
            else
            {
                //Vamos a ver si ya hay un camino con ese nombre:
                TablaMisCaminos reg = await _database.Table<TablaMisCaminos>().Where(i => i.miNombreCamino == note.miNombreCamino).FirstOrDefaultAsync();
                if (reg != null)
                {
                    Console.WriteLine("DEBUG - Database - SaveMiCaminoAsync UPDATE del registro con nombre <{0}> en TablaMisCaminos", note.miNombreCamino);
                    note.id = reg.id; // recordemos que id era 0 al entrar en SaveMiCaminoAsync(). Igual hay que avisar de que se va a sobreescribir ?? _xx_PENDIENTE
                    return await _database.UpdateAsync(note);
                }
                return await _database.InsertAsync(note);
            }
        }

        public Task<int> DeleteMiCaminoAsync(TablaMisCaminos note)
        {
            return _database.DeleteAsync(note);
        }

        async public Task<int> DeleteMiCaminoAsync(int id)
        {
            Console.WriteLine("DEBUG - Database - DeleteMiCaminoAsync id:{0}", id);
            // Buscar primero el registro con ese id y luego borrarlo:
            TablaMisCaminos reg  = await _database.Table<TablaMisCaminos>().Where(i => i.id == id).FirstOrDefaultAsync();
            if (reg == null)
            {
                Console.WriteLine("DEBUG - Database - DeleteMiCaminoAsync NO encontrado el registro con ese id <{0}> en TablaMisCaminos", id);
                return 0;
            }
            Console.WriteLine("DEBUG - Database - DeleteMiCaminoAsync Despues de buscar el id: {0}", id);
            return await _database.DeleteAsync(reg);
        }
    }
}
