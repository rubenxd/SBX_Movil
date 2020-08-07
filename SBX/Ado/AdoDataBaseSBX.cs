using System;
using System.IO;

using Mono.Data.Sqlite;

namespace SBX.Ado
{
    class AdoDataBaseSBX
    {
        public static SqliteConnection connection;
        public string CreateDataBase()
        {
            var respuesta = "";
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "adoDB_SBX.db3");
            
            bool exists = File.Exists(dbPath);
            if (!exists)
            {
                Mono.Data.Sqlite.SqliteConnection.CreateFile(dbPath);
               
                connection = new SqliteConnection("Data Source=" + dbPath);
                connection.Open();

                var commands = new[] {
                    "CREATE TABLE [Proveedor] (DNI ntext PRIMARY KEY, Nombre ntext, Ciudad ntext, Direccion ntext, " +
                    "Telefono ntext, Celular ntext, Email ntext,SitioWeb ntext);"
                    ,
                     "INSERT INTO [Proveedor] ([DNI], [Nombre],[Ciudad],[Direccion],[Telefono],[Celular],[Email],[SitioWeb]) " +
                    "VALUES ('0', 'N/A','','','0','0','0','0')"
                    ,
                    "CREATE TABLE [Producto] (Item ntext, Nombre ntext, Referencia ntext," +
                    " IVA FLOAT, Proveedor ntext,Cantidad ntext, Costo MONEY, PrecioVenta MONEY, Movimiento ntext, " +
                    " FOREIGN KEY(Proveedor) REFERENCES Proveedor(DNI));"  
                    ,
                     "CREATE TABLE [Cliente] (DNI ntext PRIMARY KEY, Nombre ntext, Ciudad ntext, Direccion ntext, " +
                    "Telefono ntext, Celular ntext, Email ntext,SitioWeb ntext);"
                    ,
                    "INSERT INTO [Cliente] ([DNI], [Nombre],[Ciudad],[Direccion],[Telefono],[Celular],[Email],[SitioWeb]) " +
                    "VALUES ('0', 'N/A','','','0','0','0','0')"
                };
                foreach (var command in commands)
                {
                    using (var c = connection.CreateCommand())
                    {
                        c.CommandText = command;
                        var i = c.ExecuteNonQuery();
                    }
                }
                respuesta = "Base de datos Creada Correctamente";
                connection.Close();
            }
            else
            {
                respuesta = "adoDB_SBX.db3";
            }

            return respuesta;
        }
    }
}