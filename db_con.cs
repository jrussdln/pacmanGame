using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Odbc;

namespace pacmanGame
{

    public static class db_con
    {
        // Path to your Access database file
        private static readonly string _databasePath = @"C:\Users\hp\Documents\Visual Studio 2013\Projects\pacmanGame\pacmanGame_db.accdb";

        // ODBC connection string using string concatenation (no $)
        private static readonly string _connectionString =
            "Driver={Microsoft Access Driver (*.mdb, *.accdb)};Dbq=" + _databasePath + ";Uid=Admin;Pwd=;";

        /// <summary>
        /// Returns a new OdbcConnection instance using the predefined connection string.
        /// </summary>
        public static OdbcConnection GetConnection()
        {
            try
            {
                return new OdbcConnection(_connectionString);
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new InvalidOperationException("Failed to create a database connection.", ex);
            }
        }
    }
}
