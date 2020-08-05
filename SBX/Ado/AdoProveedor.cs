using System;
using System.IO;
using Mono.Data.Sqlite;

namespace SBX.Ado
{
    class AdoProveedor
    {
        public static SqliteConnection connection;
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string SitioWeb { get; set; }
        public string output = "";
        public string AdoCreate()
        {
            try
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
                bool exists = File.Exists(dbPath);

                if (exists)
                {
                    connection = new SqliteConnection("Data Source=" + dbPath);
                    connection.Open();

                    var commandsInsert = new[] {
                    "INSERT INTO [Proveedor] ([DNI], [Nombre],[Ciudad],[Direccion],[Telefono],[Celular],[Email],[SitioWeb]) " +
                    "VALUES ('"+DNI+"', '"+Nombre+"','"+Ciudad+"','"+Direccion+"','"+Telefono+"','"+Celular+"','"+Email+"','"+SitioWeb+"')"
                };
                    foreach (var command in commandsInsert)
                    {
                        using (var c = connection.CreateCommand())
                        {
                            c.CommandText = command;
                            var i = c.ExecuteNonQuery();
                        }
                    }

                    output = "Proveedor creado correctamente";
                    connection.Close();
                }
                else
                {
                    output += "No existe base de datos";
                }
            }
            catch (Exception ex)
            {
                output = "Error: " + ex.Message;
            }       

            return output;
        }
    }
}