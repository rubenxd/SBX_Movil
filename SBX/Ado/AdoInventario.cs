using System;
using System.IO;
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
        public string costo { get; set; }
        public string precioventa { get; set; }

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
                    "INSERT INTO [Producto] ([Item], [Nombre],[Referencia],[IVA],[Proveedor],[Costo],[PrecioVenta]) " +
                    "VALUES ('"+Item+"', '"+Nombre+"','"+Referencia+"','"+IVA+"','"+proveedor+"','"+costo+"','"+precioventa+"')"
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
                    contents.CommandText = "SELECT [Item],[Nombre],[Referencia],[IVA],[Proveedor],[Costo],[PrecioVenta] from [Producto]";
                    var r = contents.ExecuteReader();
                    output = "";
                    int contador = 0;
                    foos = new String[100];
                    while (r.Read())
                    {
                        foos[contador] = r["Item"].ToString();
                        contador++;
                    }
                }

                connection.Close();
            }
      
            return foos;
        }
    }
}