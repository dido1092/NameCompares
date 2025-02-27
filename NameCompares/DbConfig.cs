using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameCompares
{
    public class DbConfig
    {
        public static string ConnectionString =
                @"Server=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\NameCompares.mdf;Database=NameCompares;Integrated Security=True;Encrypt=False;MultipleActiveResultSets=true;";
    }
}
