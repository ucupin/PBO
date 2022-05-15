using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace PassByReferenceForm
{
    class Database
    {
        public SQLiteConnection conn;

        public Database()
        {
            conn = new SQLiteConnection("Data Source=passbyref.sqlite3");
        }
        public void openConn()
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
        }

        public void closeConn()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
        }
    }
    public partial class Form1 : Form
    {
        private int genderid { get; set; }
        public Form1() 
        { 
            InitializeComponent();
            Database db = new Database();
            string query = "SELECT class_name FROM class";
            db.openConn();
            SQLiteCommand cmd = new SQLiteCommand(query, db.conn);
            SQLiteDataReader DR = cmd.ExecuteReader();

            while (DR.Read())
            {
                comboBox1.Items.Add(DR["class_name"]);

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt;
            ExecReader(out dt, $"SELECT stud_id, stud_name, gender_type, class_name FROM student INNER JOIN gender ON gender_id = stud_gender_id INNER JOIN class ON class_id = stud_class_id WHERE stud_name = '{TextInsert.Text}' OR stud_name LIKE '%{TextInsert.Text}%';");
        }
        private void TextInsert_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(TextInsert.Text) || TextInsert.Text == " "))
            {
                radioButton1.Enabled = true;
                radioButton2.Enabled = true;
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton1.Checked = false;
                radioButton2.Enabled = false;
                radioButton2.Checked = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (!(radioButton2.Checked))
            {
                comboBox1.Enabled = false;
            }
            else
            {
                genderid = 2;
                comboBox1.Enabled = true;
            }

        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (!(radioButton1.Checked))
            {
                comboBox1.Enabled = false;
            }
            else
            {
                genderid = 1;
                comboBox1.Enabled = true;

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void button2_Click(object sender, EventArgs e) 
        {
            DataTable dt = new DataTable();
            label2.Visible = false;
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            if (ExecQuery(ref dt, $"INSERT INTO student(stud_id, stud_name, stud_gender_id, stud_class_id) VALUES ((SELECT max(stud_id) FROM student) + 1, '{TextInsert.Text}', {genderid}, (SELECT class_id FROM class WHERE class_name = '{comboBox1.Text}'));") == true)
            {
                label2.ForeColor = Color.Green;
                label2.Visible = true;
                label2.Text = "TRUE";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Visible = true;
                label2.Text = "FALSE";
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            label2.Visible = false;
            label2.Font = new Font(label2.Font, FontStyle.Bold);
            if (ExecQuery(ref dt, $"UPDATE student SET stud_name = '{textBox1.Text}' WHERE stud_name = '{TextInsert.Text}';") == true)
                {
                label2.ForeColor = Color.Green;
                label2.Visible = true;
                label2.Text = "TRUE";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Visible = true;
                label2.Text = "FALSE";
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            label2.Visible = false;
            label2.Font = new Font(label2.Font, FontStyle.Bold);            
            if (ExecQuery(ref dt, $"DELETE FROM student WHERE stud_name = '{TextInsert.Text}';") == true)
            {
                label2.ForeColor = Color.Green;
                label2.Visible = true;
                label2.Text = "TRUE";
            }
            else
            {
                label2.ForeColor = Color.Red;
                label2.Visible = true;
                label2.Text = "FALSE";
            }
        }
        private void dataBox_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DataTable dt;
            ExecReader(out dt, "SELECT stud_id, stud_name, gender_type, class_name FROM student INNER JOIN gender ON gender_id = stud_gender_id INNER JOIN class ON class_id = stud_class_id;");
        }
        public bool ExecReader(out DataTable dt, string query)
        {
            Database db = new Database();
            db.openConn();
            SQLiteCommand cmd = new SQLiteCommand(query, db.conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            dataBox.DataSource = dt;
            db.closeConn();
            cmd.Dispose();       
            return true;
        }
        public bool ExecQuery(ref DataTable dt, string query)
        {
            Database db = new Database();
            db.openConn();
            SQLiteCommand cmd = new SQLiteCommand(query, db.conn);
            if (string.IsNullOrEmpty(TextInsert.Text) || TextInsert.Text == " ")
            {
                return false;
            }
            else
            {
                cmd.ExecuteNonQuery();
                ExecReader(out dt, "SELECT stud_id, stud_name, gender_type, class_name FROM student INNER JOIN gender ON gender_id = stud_gender_id INNER JOIN class ON class_id = stud_class_id;");
                return true;
            }
            db.closeConn();
            cmd.Dispose();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(string.IsNullOrEmpty(textBox1.Text) || textBox1.Text == " "))
            {
                button3.Enabled = true;
            }
            else
            {
                button3.Enabled = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}