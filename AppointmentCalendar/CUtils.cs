using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBEngine  ;
using System.IO;
using System.Windows.Forms;

namespace Utils
{
    public static class CUtils
    {
        public const String dbString = "Data Source=appointments.db;Version=3;New=True;Compress=True;";

        public const String createTableQuery = @"CREATE TABLE IF NOT EXISTS calendar (aptid integer primary key AUTOINCREMENT, aptdate varchar(11), 
              starttime varchar(6), endtime varchar(6), aptheader varchar(100) , aptcomment varchar(100), author varchar(100));";

        public const string sampleData = "INSERT INTO calendar (aptdate, starttime, endtime, aptheader, aptcomment,author) VALUES ('2013-12-25','11:11','12:12', 'India', 'I love you','Ankur');";

        public const string removeRowMessage = "Successfully Deleted Appointment, Update Other Servers!!";

        public const String createIPTableQuery = @"CREATE TABLE IF NOT EXISTS user (ipAddr varchar(16) primary key );";

        public static String rootMachine = "";

        public const int TwoSeconds = 2000;
        public const int FiveSeconds = 5000;
        public const int TenSeconds = 10000;



        public const String ADD_APPOINTMENTS = "ADD";
        public const String MODIFY_APPOINTMENTS = "MODIFY";
        public const String REMOVE_APPOINTMENTS = "REMOVE";
        public const String REGISTER_ON_NETWORK = "REGISTER_ON_NW";
        public const String LEAVE_NETWORK = "LEAVE_NETWORK";
        public const String SYNCHRONIZE_DATABASE = "SYNC_DB";
        public const String SEND_IP_TO_MACHINES = "INSERT_IP_TO_DB";


        public static void delay(int millisec){

            for (int i = 0; i < millisec ; i++) { }
        }

        private static DatabaseCon dbConn;

        public static DatabaseCon getDBConn() {

            dbConn = new DatabaseCon();
            return dbConn;
        
        }

        public static String[] parse(String str) { 
        
            String []inputs = str.Split(';');
            return inputs;
        }

        public static void readRootMachine() {

            String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\root.txt";
            rootMachine = System.IO.File.ReadAllText(path);
            return;
        }



        public static void parseStringAddtoDB(String sql,String choice) {

            String query = "";
            String[] words = sql.Split(';');
            int numberOfResults = words.Length;
            
                foreach (string word in words)
                {
                    String[] inputs = word.Split(',');
                    switch (choice)
                    {
                        case "REGISTER_ON_NW":
                            
                            query = "INSERT INTO user (ipAddr) VALUES ('" + inputs[0] + "');";
                            
                            break;
                        case "SYNC_DB":
                            query = "INSERT INTO calendar (aptid,aptdate, starttime, endtime, aptheader, aptcomment,author) VALUES ('" + inputs[0] + "','" + inputs[1] + "','" + inputs[2] + "', '" + inputs[3] + "', '" + inputs[4] + "','" + inputs[5] + "','" + inputs[6]+ "');";
                            
                             break;
                    }
                    dbConn.queryDB(query);
                }
          
           return;
        }
    }
}
