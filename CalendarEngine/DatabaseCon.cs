using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using Utils;



namespace DBEngine
{
    public class DatabaseCon
    {

        public SQLiteConnection sqlite_conn;
        public SQLiteCommand sqlite_cmd;
        public SQLiteDataReader sqlite_datareader;

    
        public DatabaseCon()
        {
        }

        public void initCon()
        {

            int row = queryDB(CUtils.createTableQuery);
            row = queryDB(CUtils.sampleData);
            
            //Console.WriteLine(row);

            //Display the results now
            //str = "SELECT * from calendar";
            //DataTable dtn = getDataSet(str);

            
       
        }

        public int queryDB(string sql)
        {

            sqlite_conn = new SQLiteConnection(CUtils.dbString);
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand(); 
            sqlite_cmd.CommandText = sql;
            int rowsUpdated = sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
            return rowsUpdated;
        }

        public DataTable getDataSet(string sql)
        {

            DataTable dt = new DataTable();
            try
            {
                sqlite_conn = new SQLiteConnection(CUtils.dbString);
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = sql;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    // Print out the content of the text field:
                   // System.Console.WriteLine(sqlite_datareader.GetDateTime(1).ToString());
                }
                
                dt.Load(sqlite_datareader);

                sqlite_datareader.Close();
                sqlite_conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return dt;
        }
    }
}
