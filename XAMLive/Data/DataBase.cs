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
            _database.CreateTableAsync<TablaCaminoDeMadrid>().Wait();
            _database.CreateTableAsync<TablaSanSalvador>().Wait();
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

        public string DamePoblacion(int id)
        {
            string comando = "select nombrePoblacion from TablaPOBLACIONES where id=?"; // + id;
            //var cmd = new SQLiteCommand(_db);
            List<TablaPOBLACIONES> miLista = _db.Query<TablaPOBLACIONES>(comando, id);
            Console.WriteLine(miLista[0].nombrePoblacion);
            return miLista[0].nombrePoblacion;

        }
        public Task<int> SavePoblacionesAsync(TablaPOBLACIONES note)
        {
            //if (note.NombrePoblacion != null && note.NombrePoblacion != "" && note.NombrePoblacion.Length > 0)
            if (note.id != 0)
            {
                return _database.UpdateAsync(note);
            }
            else
            {
                return _database.InsertAsync(note);
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

        public Task<List<TablaALOJAMIENTOS>> GetAlojamientosByCityAsync(int id) => _database.Table<TablaALOJAMIENTOS>()
                            .Where(i => i.idPoblacion == id)
                            .ToListAsync();
        public List<TablaALOJAMIENTOS> GetAlojamientosByCity(int idPoblacion)
        {
            Console.WriteLine("DEBUG - GetAlojamientosByCity: idPoblacion:{0}", idPoblacion);
            //List<TablaALOJAMIENTOS> miLista = _db.Table<TablaALOJAMIENTOS>().Where(i => i.idPoblacion == id).ToList();

            string comando = "select * from TablaALOJAMIENTOS where idPoblacion=?";
            List<TablaALOJAMIENTOS> miLista = _db.Query<TablaALOJAMIENTOS>(comando, idPoblacion);

            Console.WriteLine("DEBUG - GetAlojamientosByCity: Count:{0}", miLista.Count);

            //Console.WriteLine("DEBUG - GetAlojamientosByCity: {0}", miLista.ToString());
            //Console.WriteLine("DEBUG - GetAlojamientosByCity({0}): {1}", idPoblacion, miLista[0].nombreAlojamiento);
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
    }
}
