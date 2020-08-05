using System;
using System.IO;
using Mono.Data.Sqlite;

namespace SBX.Ado
{
    class AdoCliente
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
        public string Cliente { get; set; }
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
                    "INSERT INTO [Cliente] ([DNI], [Nombre],[Ciudad],[Direccion],[Telefono],[Celular],[Email],[SitioWeb]) " +
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

                    output = "Cliente creado correctamente";
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
                    " UPDATE  [Cliente] SET [Nombre] = '"+Nombre+"',[Ciudad] = '"+Ciudad+"',[Direccion] = '"+Direccion+"',[Telefono] = '"+Telefono+"',[Celular] = '"+Celular+"',[Email] = '"+Email+"',[SitioWeb] = '"+SitioWeb+"' "+
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

                    output = "Cliente Editado correctamente";
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
                    contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Cliente) fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb from [Cliente]";
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
                    if (Cliente != "")
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Cliente WHERE [DNI] = '" + Cliente + "') fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb" +
                       " from [Cliente]" +
                       "WHERE [DNI] = '" + Cliente + "'";
                    }
                    else
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Cliente) fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb " +
                       "from [Cliente] ";
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
                    if (Cliente != "")
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Cliente WHERE [DNI] = '" + Cliente + "') fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb" +
                       " from [Cliente]" +
                       "WHERE [DNI] = '" + Cliente + "'";
                    }
                    else
                    {
                        contents.CommandText = "SELECT  (SELECT COUNT(*) FROM Cliente) fila,DNI,Nombre,Ciudad,Direccion,Telefono,Celular,Email,SitioWeb " +
                       "from [Cliente] ";
                    }

                    var r = contents.ExecuteReader();
                    int contador = 0;
                    if (r.HasRows)
                    {
                        int filas = Convert.ToInt32(r["fila"]);
                        foos = new String[filas];
                        while (r.Read())
                        {
                            foos[contador] = r["DNI"].ToString() + " - " + r["Nombre"].ToString() + " - " + r["Ciudad"].ToString() 
                            + " - " + r["Direccion"].ToString() + " - " + r["Telefono"].ToString() + " - " + r["Celular"].ToString() 
                            + " - " + r["Email"].ToString() + " - " + r["SitioWeb"].ToString();
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