using System;
using System.IO;
using Android.Database.Sqlite;
using Mono.Data.Sqlite;

namespace SBX.Ado
{
    class AdoInventario
    {
        public static SqliteConnection connection;
        public string Item { get; set; }
        public string Nombre { get; set; }
        public string Referencia { get; set; }
        public string IVA { get; set; }
        public string proveedor { get; set; }
        public string Cantidad { get; set; }
        public string costo { get; set; }
        public string precioventa { get; set; }
        public string movimiento { get; set; }
        public string output = "";
        public string AdoCreate()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");            
            bool exists = File.Exists(dbPath);

            if (exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                var commandsInsert = new[] {
                    "INSERT INTO [Producto] ([Item], [Nombre],[Referencia],[IVA],[Proveedor],[Cantidad],[Costo],[PrecioVenta],[Movimiento]) " +
                    "VALUES ('"+Item+"', '"+Nombre+"','"+Referencia+"','"+IVA+"','"+proveedor+"','"+Cantidad+"','"+costo+"','"+precioventa+"','"+movimiento+"')"
                };
                foreach (var command in commandsInsert)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var i = c.ExecuteNonQuery();
                    }
                }

                output = "Producto creado correctamente";
                connection.Close();
            }
            else
            {
                output += "No existe base de datos";              
            }
                   
            return output;
        }
        public String[] AdoSelect()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
            bool exists = File.Exists(dbPath);
            String[] foos = null;
            if (exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Producto) fila,[Item],[Nombre],[Referencia],[IVA],[Proveedor],[Cantidad],[Costo],[PrecioVenta] from [Producto]";
                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]);
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["Item"].ToString() + " - "+ r["Nombre"].ToString() + " - " +r["Referencia"].ToString();
                            contador++;
                        }
                    }
                    else 
                    {
                        foos = new String[1];
                        foos[0] = "";
                    }      
                }
                connection.Close();
            }
      
            return foos;
        }

        public String[] AdoSelectMaxItem()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
            bool exists = File.Exists(dbPath);
            String[] foos = null;
            if (exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT  MAX([Item]) Item from [Producto]";
                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = 1;
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["Item"].ToString();
                            contador++;
                        }
                    }
                    else
                    {
                        foos = new String[1];
                        foos[0] = "";
                    }
                }
                connection.Close();
            }

            return foos;
        }

        public String[] AdoSelectProveedores()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
            bool exists = File.Exists(dbPath);
            String[] foos = null;
            if (exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                using (var contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT (SELECT COUNT(*) FROM Proveedor) fila,[DNI],[Nombre] from [Proveedor]";
                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]); ;
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["DNI"].ToString() + "-" + r["Nombre"].ToString();
                            contador++;
                        }
                    }
                    else
                    {
                        foos = new String[1];
                        foos[0] = "";
                    }
                }
                connection.Close();
            }

            return foos;
        }
        
        public String[] AdoSelectID()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
            bool exists = File.Exists(dbPath);
            String[] foos = null;
            if (exists)
            {
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                using (var contents = connection.CreateCommand())
                {
                    if (Item != "")
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Producto WHERE [Item] = '" + Item + "') fila,[Item],[Nombre],[Referencia],[IVA]," +
                       "[Proveedor],[Costo],[PrecioVenta] from [Producto]" +
                       "WHERE [Item] = '" + Item +"'";
                    }
                    else
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Producto) fila,[Item],[Nombre],[Referencia],[IVA]," +
                       "[Proveedor],[Costo],[PrecioVenta] from [Producto] ";
                    }  
                   
                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]);
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["Item"].ToString() + " - " + r["Nombre"].ToString() + " - " + r["Referencia"].ToString();
                            contador++;
                        }
                    }
                    else
                    {
                        foos = new String[1];
                        foos[0] = "";
                    }
                }
                connection.Close();
            }

            return foos;
        }
    }
}