using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Database binding goes here
using MySql.Data.MySqlClient;



namespace DBEngine
{
    class DatabaseCon
    {
        private string myConnection;
        private MySqlConnection myConn;
        private MySqlDataAdapter myDataAdapter;

        public void initCon(){

            try{

                myConnection = "datasource=localhost;port=3306;username=root;password=root";
                myConn = new MySqlConnection(myConnection);
                myDataAdapter = new MySqlDataAdapter();
                myDataAdapter.SelectCommand = new MySqlCommand("select * from test.cal;", myConn);
                MySqlCommandBuilder cb = new MySqlCommandBuilder(myDataAdapter);
                myConn.Open();
                Console.WriteLine("\nConnected!!");
                myConn.Close();
            }
            catch(Exception Ex){
                Console.WriteLine(Ex.Message);
            }
            return;
        }

        public void queryDB(string query){


            return;
        }

    }
}
