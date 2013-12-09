using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using CalendarClient;
using System.Data.SQLite;
using System.Threading;
using CalendarEngine;
using DBEngine;
using Utils;



namespace AppointmentCalendar
{
    public partial class AppointmentViewer : Form
    {
        private Client clientObject;
        private DatabaseCon dbConn;
        private static Server serverObject;
        private System.Windows.Forms.Timer timer;

        public AppointmentViewer()
        {
            InitializeComponent();

            dbConn = CUtils.getDBConn();  // new DatabaseCon();
            initializeDBConnections();
         
            clientObject = new Client();
            serverObject = new Server();
            initServerThread();
            InitTimer();

            populateGrid();

        }

        private void initializeDBConnections() {
            
            String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\appointments.db";
            if (!File.Exists(path))
            {

                dbConn.initCon();

            }   
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }


        private  void populateGrid() {

          /* String str = dbConn.getDataSet("select * from calendar");
           if (str == "")
           {
               MessageBox.Show("Empty Data");
           }
            CUtils.parseStringAddtoDB(str, "ADD");
            MessageBox.Show(str);
            return;
         */
            dataGridView1.Rows.Clear();


            DatabaseCon db = CUtils.getDBConn();// DatabaseCon();
           
            db.sqlite_conn = new SQLiteConnection(CUtils.dbString);
            db.sqlite_conn.Open();
            db.sqlite_cmd = db.sqlite_conn.CreateCommand();
            db.sqlite_cmd.CommandText = "select * from calendar";
            db.sqlite_datareader = db.sqlite_cmd.ExecuteReader();
            
            while (db.sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {

                dataGridView1.Rows.Add(db.sqlite_datareader[1],db.sqlite_datareader[2],
                    db.sqlite_datareader[3],db.sqlite_datareader[4],db.sqlite_datareader[5],
                    db.sqlite_datareader[6]);

            }

            if (dataGridView1.Rows.Count > 0){
                //Tweak for modify and remove buttons

                modify_button.Enabled = true;
                modify_button.BackColor = System.Drawing.Color.White;
                remove_button.Enabled = true;
                remove_button.BackColor = System.Drawing.Color.White;

            }
            else {
                modify_button.Enabled = false;
                modify_button.BackColor = System.Drawing.Color.Black;
                remove_button.Enabled = false;
                remove_button.BackColor = System.Drawing.Color.Black;
            
            
            
            }


            db.sqlite_datareader.Close();
            db.sqlite_conn.Close();

          
              
        }



        private void form_Closing(object sender, CancelEventArgs e)
        {
            populateGrid();
        }


        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0 && dataGridView1.SelectedRows.Count > 0) {
                modify_button.Enabled = true;
                modify_button.BackColor = System.Drawing.Color.White;

                remove_button.Enabled = true;
                remove_button.BackColor = System.Drawing.Color.White;

            }
        }

        private void remove_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) {
                int Row = dataGridView1.CurrentRow.Index;
                String date = dataGridView1[0, Row].Value.ToString();
                String starttime = dataGridView1[1, Row].Value.ToString();
                String endtime = dataGridView1[2, Row].Value.ToString();
                String header = dataGridView1[3, Row].Value.ToString();
                String comments = dataGridView1[4, Row].Value.ToString();

                String sql = "DELETE FROM calendar WHERE aptdate='" + date + "' AND starttime ='" + starttime + "' AND endtime ='" + endtime + "' AND aptheader='" + header + "' AND aptcomment = '" + comments + "' AND author ='"+ Environment.MachineName+ "'";
                dbConn.queryDB(sql);

                String getIpAndPort = "select * from user";

                String listOfIPs = dbConn.getDataSet(getIpAndPort);
                String[] hosts = CUtils.parse(listOfIPs);


                //Now create channel factory and call others
                //Fetch the IP from, loop through it and conn

                foreach (String ip in hosts)
                {
                    if (!ip.Equals("") && !ip.Equals(Environment.MachineName))
                    {
                        clientObject.initClientConfig(ip, "REMOVE", sql);
                        CUtils.delay(10000);
                    }

                }
                

                MessageBox.Show(CUtils.removeRowMessage,"Important Message", MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation,
                               MessageBoxDefaultButton.Button1);
                populateGrid();
                //Call other servers through XML-RPC call
            }
        }

        private void modify_button_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int Row = dataGridView1.CurrentRow.Index;
                String date = dataGridView1[0, Row].Value.ToString();
                String starttime = dataGridView1[1, Row].Value.ToString();
                String endtime = dataGridView1[2, Row].Value.ToString();
                String header = dataGridView1[3, Row].Value.ToString();
                String comments = dataGridView1[4, Row].Value.ToString();
                String author = dataGridView1[5, Row].Value.ToString();
                


                ModifyAppointment modAppointmentwindow = new ModifyAppointment();
                modAppointmentwindow.setupValues( header, comments, date, starttime, endtime, author);
                modAppointmentwindow.FormClosing += form_Closing;
                modAppointmentwindow.ShowDialog();

               

               

                //Call other servers through XML-RPC call
            }

            //fetch the parameters to modify, write the sql and call dbConn.query()
        }

        private void add_button_Click(object sender, EventArgs e)
        {
            AddAppointment addAppointmentWin = new AddAppointment();
            addAppointmentWin.FormClosing += form_Closing;
            addAppointmentWin.ShowDialog();
        }

        private static void initServerThread() {

            Thread t = new Thread(new ThreadStart(launchServer));
            t.Start();
        }

        public static void launchServer()
        {
            serverObject.initiateServer();
        }

      


        private void leave_button_Click(object sender, EventArgs e)
        {
            String getIpAndPort = "select * from user";

            String listOfIPs = dbConn.getDataSet(getIpAndPort);
            String[] hosts = CUtils.parse(listOfIPs);


            //Now create channel factory and call others
            //Fetch the IP from, loop through it and conn

            foreach (String ip in hosts)
            {
                if (!ip.Equals("") && !ip.Equals(Environment.MachineName))
                {
                    clientObject.initClientConfig(ip, "LEAVE_NETWORK", Environment.MachineName);
                    CUtils.delay(10000);
                }
            }


            for (int i = 0; i < 10000; i++) { }

            String path = Path.GetDirectoryName(Application.ExecutablePath) + "\\appointments.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            MessageBox.Show("Successfully left the Calendar Network");
            this.Close();
        }

        private void join_button_Click(object sender, EventArgs e)
        {
            String machineNames = clientObject.initClientConfig(CUtils.rootMachine, "REGISTER_ON_NW","");
            CUtils.parseStringAddtoDB(machineNames, "REGISTER_ON_NW");
            
            for (int i = 0; i < 5000; i++) { }

            String db = clientObject.initClientConfig("BAGEND", "SYNC_DB", "");
            CUtils.parseStringAddtoDB(db,"SYNC_DB");


            //Send my ip to all the machines now
             String getIpAndPort = "select * from user";

            String listOfIPs = dbConn.getDataSet(getIpAndPort);
            String[] hosts = CUtils.parse(listOfIPs);


            //Now create channel factory and call others
            //Fetch the IP from, loop through it and conn

            foreach (String ip in hosts)
            {
                if (!ip.Equals("") && !ip.Equals(Environment.MachineName))
                {

                    clientObject.initClientConfig(ip, "INSERT_IP_TO_DB", Environment.MachineName);
                    CUtils.delay(10000);
                }
            }
        }

        private void refresh_button_Click(object sender, EventArgs e)
        {
            populateGrid();
        }


        
        public void InitTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Interval = 10000; // in miliseconds
            timer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            populateGrid();
        }

     
  
    }


  


    
      
    
}
