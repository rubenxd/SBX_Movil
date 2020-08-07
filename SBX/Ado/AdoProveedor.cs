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
        public string Proveedor { get; set; }
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
        public string AdoEditar()
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
                    " UPDATE  [Proveedor] SET [Nombre] = '"+Nombre+"',[Ciudad] = '"+Ciudad+"',[Direccion] = '"+Direccion+"',[Telefono] = '"+Telefono+"',[Celular] = '"+Celular+"',[Email] = '"+Email+"',[SitioWeb] = '"+SitioWeb+"' "+
                    " WHERE  [DNI] = '"+DNI.Trim()+"'"
                };
                    foreach (var command in commandsInsert)
                    {
                        using (var c = connection.CreateCommand())
                        {
                            c.CommandText = command;
                            var i = c.ExecuteNonQuery();
                        }
                    }

                    output = "Proveedor Editado correctamente";
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
                    if (Proveedor != "")
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Proveedor WHERE [DNI] = '" + Proveedor + "') fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb" +
                       " from [Proveedor]" +
                       "WHERE [DNI] = '" + Proveedor + "'";
                    }
                    else
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Proveedor) fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb " +
                       "from [Proveedor] ";
                    }

                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]);
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["DNI"].ToString() + " - " + r["Nombre"].ToString();
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
        public string AdoEliminar()
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
                    " DELETE FROM  [Proveedor] "+
                    " WHERE  [DNI] = '"+DNI.Trim()+"'"
                };
                    foreach (var command in commandsInsert)
                    {
                        using (var c = connection.CreateCommand())
                        {
                            c.CommandText = command;
                            var i = c.ExecuteNonQuery();
                        }
                    }

                    output = "Proveedor eliminado correctamente";
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
        public String[] AdoSelectIDTodos()
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
                    if (Proveedor != "")
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Proveedor WHERE [DNI] = '" + Proveedor + "') fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb" +
                       " from [Proveedor]" +
                       "WHERE [DNI] = '" + Proveedor + "'";
                    }
                    else
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Proveedor) fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb " +
                       "from [Proveedor] ";
                    }

                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]);
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["DNI"].ToString() + " -- " + r["Nombre"].ToString() + " -- " + r["Ciudad"].ToString()
                            + " -- " + r["Direccion"].ToString() + " -- " + r["Telefono"].ToString() + " -- " + r["Celular"].ToString()
                            + " -- " + r["Email"].ToString() + " -- " + r["SitioWeb"].ToString();
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