using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBEngine;
using Utils;
using CalendarClient;
using System.Data.SQLite;

namespace AppointmentCalendar
{
    public partial class AppointmentViewer : Form
    {
        private Client clientObject;
        private DatabaseCon dbConn;
        public AppointmentViewer()
        {
            InitializeComponent();
            dbConn = new DatabaseCon();
            clientObject = new Client();


            populateGrid();

            modify_button.Enabled = false;
            modify_button.BackColor = System.Drawing.Color.Black;

            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
        }


        private void populateGrid() {


            dataGridView1.Rows.Clear();

         
            DatabaseCon db = new DatabaseCon();
           
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

                String sql = "DELETE FROM calendar WHERE aptdate='" + date + "' AND starttime ='" + starttime + "' AND endtime ='" + endtime + "' AND aptheader='" + header + "' AND aptcomment = '" + comments + "' AND author ='Ankur'";
                dbConn.queryDB(sql);

                MessageBox.Show("Successfully Deleted Appointment, Update Other Servers!!");
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



                ModifyAppointment modAppointmentwindow = new ModifyAppointment();
                modAppointmentwindow.setupValues(header,comments, date, starttime, endtime);
                modAppointmentwindow.ShowDialog();

               

                MessageBox.Show("Successfully Deleted Appointment, Update Other Servers!!");
                populateGrid();
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



  
    }
}
