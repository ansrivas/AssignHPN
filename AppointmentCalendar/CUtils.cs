using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBEngine  ;

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

        public const String sampleIPData1 = "INSERT INTO user (ipAddr) VALUES ('192.168.1.1');";
        public const String sampleIPData2 = "INSERT INTO user (ipAddr) VALUES ('192.168.1.2');";

        private static DatabaseCon dbConn;

        public static DatabaseCon getDBConn() {

            dbConn = new DatabaseCon();
            return dbConn;
        
        }

        public static String[] parse(String str) { 
        
        String []inputs = str.Split(';');

        return inputs;
        }


        public static void parseStringAddtoDB(String sql,String choice) {

            String query = "";
            String[] words = sql.Split(';');
            int numberOfResults = words.Length;
            for (int i = 0; i < numberOfResults; i++)
            {
                foreach (string word in words)
                {
                    String[] inputs = word.Split(',');
                    switch (choice)
                    {
                        case "ADD":
                            //  query =  @"INSERT INTO calendar (aptdate, starttime, endtime, aptheader, aptcomment,author) VALUES ('" + inputs[1] + "','" + inputs[2] + "','" + inputs[3] + "','" + inputs[4] + "','" + inputs[5] + "','" + inputs[6] + ");";

                            break;
                        case "MODIFY":
                            //  query = "Update calendar set aptdate='"+date+ "', starttime='" + starttime+ "', endtime ='"+ endtime + "', aptheader='"+ header + "', aptcomment ='" +comments + "',author='"+ author +"'  where aptid='"+ primaryKey +"';";
                            break;

                        case "REMOVE":
                            break;
                    }
                }

              
             dbConn.queryDB(query);
            }



            return;
        }
    }
}
