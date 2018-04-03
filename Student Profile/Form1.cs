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
using System.IO;

namespace Student_Profile
{
    public partial class Form1 : Form
    {
        SQLite sql;
        public List<string> jmbg_sqlite = new List<string>();
        string picture;
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.Text = "";
            sql = new SQLite();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void Profile(string JMBG, string Name, string Date, string Gender, string School, string Department, string Semester, string Address, string Phone, string Picture)
        {
            textBox1.Text = JMBG;
            textBox2.Text = Name;
            textBox3.Text = Address;
            textBox4.Text = Phone;
            dateTimePicker1.Text = Date;
            if (Gender == "MALE") radioButton1.Checked = true;
            else if (Gender == "FAMALE") radioButton2.Checked = true;
            else radioButton2.Checked = false;
            comboBox1.Text = Department;
            comboBox2.Text = School;
            comboBox3.Text = Semester;
            pictureBox1.ImageLocation = Picture;
        }
        public void Update()
        {
            sql.Read_SQLite_JMBG("ProfileDB.db", "ProfileTBL");
            jmbg_sqlite = sql.jmbg_sqlite;
            picture = null;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            if (comboBox2.Text == "Skola1")
            {
                comboBox1.Items.Add("Smer1");
                comboBox1.Items.Add("Smer2");
                comboBox1.Items.Add("Smer3");
            }
            else if (comboBox2.Text == "Skola2")
            {
                comboBox1.Items.Add("Smer4");
                comboBox1.Items.Add("Smer5");
                comboBox1.Items.Add("Smer6");
            }
            else if (comboBox2.Text == "Skola3")
            {
                comboBox1.Items.Add("Smer7");
                comboBox1.Items.Add("Smer8");
                comboBox1.Items.Add("Smer9");
            }
        }
        private void upload_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPEG Image | *.jpg";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                picture = ofd.FileName;
            }
            pictureBox1.ImageLocation = picture;
        }
        private void add_new_Click(object sender, EventArgs e)
        {
            Profile("", "", "", "", null, null, null, "", "", "");
        }
        private void save_btn_Click(object sender, EventArgs e)
        {
            string project_pictures;
            try
            {
                project_pictures = "Pictures/" + textBox1.Text + ".jpg";
                File.Copy(picture, project_pictures);
            }
            catch
            {
                project_pictures = "";
            }
            sql.Connection_SQLite("ProfileDB.db");
            string rb = "";
            if (radioButton1.Checked == true) rb = radioButton1.Text;
            else if (radioButton2.Checked == true) rb = radioButton2.Text;
            else rb = "";
            MessageBox.Show(sql.Add_SQLite(textBox1.Text, textBox2.Text, dateTimePicker1.Text, rb, comboBox2.Text, comboBox1.Text, comboBox3.Text, textBox3.Text, textBox4.Text, project_pictures, "ProfileTBL"), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sql.Close_SQLite();
            Update();
        }
        private void delete_btn_Click(object sender, EventArgs e)
        {
            string project_pictures = "Pictures/" + textBox1.Text + ".jpg";
            File.Delete(project_pictures);
            sql.Connection_SQLite("ProfileDB.db");
            string rb = "";
            if (radioButton1.Checked == true) rb = radioButton1.Text;
            else if (radioButton2.Checked == true) rb = radioButton2.Text;
            MessageBox.Show(sql.Delete_SQLite(textBox1.Text, "ProfileTBL"),"Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            sql.Close_SQLite();
            Profile("", "", "", "", null, null, null, "", "", "");
            Update();
        }
        private void next_btn_Click(object sender, EventArgs e)
        {
            string next_jmbg = "";
            int jmbg_lenght = jmbg_sqlite.Count;
            for (int i = 0; i < jmbg_lenght; i++)
            {
                if (textBox1.Text == jmbg_sqlite[i])
                {
                    try
                    {
                        next_jmbg = jmbg_sqlite[i + 1];
                        break;
                    }
                    catch
                    {
                        next_jmbg = jmbg_sqlite[0];
                        break;
                    }
                    
                }

            }
            if (next_jmbg == "")
            {
                try
                {
                    Update();
                    next_jmbg = jmbg_sqlite[0];
                }
                catch { }
            }
            if (next_jmbg != "")
            {
                sql.Read_SQLite_Profile("ProfileDB.db", "ProfileTBL", next_jmbg);
                Profile(sql.profile[0], sql.profile[1], sql.profile[2], sql.profile[3], sql.profile[4], sql.profile[5], sql.profile[6], sql.profile[7], sql.profile[8], sql.profile[9]);
            }      
        }
        private void last_btn_Click(object sender, EventArgs e)
        {
            string last_jmbg = "";
            int jmbg_lenght = jmbg_sqlite.Count;
            for (int i = 0; i < jmbg_lenght; i++)
            {
                if (textBox1.Text == jmbg_sqlite[i])
                {
                    try
                    {
                        last_jmbg = jmbg_sqlite[i - 1];
                        break;
                    }
                    catch
                    {
                        last_jmbg = jmbg_sqlite[jmbg_lenght-1];
                        break;
                    }
                }

            }
            if (last_jmbg == "")
            {
                try
                {
                    Update();
                    last_jmbg = jmbg_sqlite[jmbg_lenght - 1];
                }
                catch { }

            }
            if (last_jmbg != "")
            {
                sql.Read_SQLite_Profile("ProfileDB.db", "ProfileTBL", last_jmbg);
                Profile(sql.profile[0], sql.profile[1], sql.profile[2], sql.profile[3], sql.profile[4], sql.profile[5], sql.profile[6], sql.profile[7], sql.profile[8], sql.profile[9]);
            }

        }
        private void find_btn_Click(object sender, EventArgs e)
        {
            Update();
            sql.Connection_SQLite("ProfileDB.db");
            bool t = sql.Read_SQLite(textBox1.Text, "ProfileTBL");
            sql.Close_SQLite();
            if (t == true)
            {
                sql.Read_SQLite_Profile("ProfileDB.db", "ProfileTBL", textBox1.Text);
                Profile(sql.profile[0], sql.profile[1], sql.profile[2], sql.profile[3], sql.profile[4], sql.profile[5], sql.profile[6], sql.profile[7], sql.profile[8], sql.profile[9]);
            }
        }
    }
}
