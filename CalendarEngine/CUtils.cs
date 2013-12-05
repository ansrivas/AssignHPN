using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public static class CUtils
    {
        public const String dbString = "Data Source=appointments.db;Version=3;New=True;Compress=True;";

        public const String createTableQuery = @"CREATE TABLE IF NOT EXISTS calendar (aptid integer primary key AUTOINCREMENT, aptdate varchar(11), 
              starttime varchar(6), endtime varchar(6), aptheader varchar(100) , aptcomment varchar(100), author varchar(100));";

        public const string sampleData = "INSERT INTO calendar (aptdate, starttime, endtime, aptheader, aptcomment,author) VALUES ('2013-12-25','11:11','12:12', 'India', 'I love you','Ankur');";

        public const string removeRowMessage = "Successfully Deleted Appointment, Update Other Servers!!";

    }
}
