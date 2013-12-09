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
            row = queryDB(CUtils.createIPTableQuery);

            //Just insert your own user ip in the DB
            String sampleIPData1 = "INSERT INTO user (ipAddr) VALUES ('" + Environment.MachineName + "');";
            queryDB(sampleIPData1);
            
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

        public String queryPrimeKey(string sql)
        {
            String primaryKey = "";
            sqlite_conn = new SQLiteConnection(CUtils.dbString);
            sqlite_conn.Open();
            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = sql;
            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read()) {
                primaryKey = sqlite_datareader[0].ToString();
            }
            sqlite_datareader.Close();
            sqlite_conn.Close();
            return primaryKey;
        }



       
        //Each row is returned in a semi-colon separated manner and each column is separated by comma
        public String getDataSet(string sql)
        {
            String row = "";
            String newRow = "";
            try
            {
                sqlite_conn = new SQLiteConnection(CUtils.dbString);
                sqlite_conn.Open();
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = sql;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                int fieldCount = sqlite_datareader.FieldCount;
                int iCounter =0 ; //Start reading from the first column
                while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
                {
                    int temp = fieldCount;
                    iCounter = 0;
                    while(temp > 0 ){

                        row += sqlite_datareader[iCounter].ToString() ;
                        if (temp - 1 > 0)
                            row = row + ",";
                        iCounter++;
                        temp--;
                    }
                    row += ";";
                    
                   // System.Console.WriteLine(sqlite_datareader.GetDateTime(1).ToString());
                }
                 newRow = row.TrimEnd(';');
  
                sqlite_datareader.Close();
                sqlite_conn.Close();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return newRow;
        }
 
    }
    
    
    }
