using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Student_Profile
{
    class SQLite
    {
        SQLiteConnection sql_conn;
        public List<string> jmbg_sqlite = new List<string>();
        public string[] profile = new string[10];
        public void Connection_SQLite(string url)
        {
            sql_conn = new SQLiteConnection("Data Source= " + url + ";Version=3;");
            sql_conn.Open();
        }
        public bool Read_SQLite(string JMBG, string table)
        {
            //Connection_SQLite("ProfileDB.db");

            string sql = "select * from " + table;
            SQLiteCommand cmd = new SQLiteCommand(sql, sql_conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            bool t = false;
            while (reader.Read())
            {

                if (JMBG == reader.GetString(0))
                {
                    t = true;
                }
            }

            return t;
        }
        public string Add_SQLite(string JMBG, string Name, string Date, string Gender, string School, string Department, string Semester, string Address, string Phone, string Picture, string table)
        {
            //Connection_SQLite("ProfileDB.db");
            using (SQLiteConnection con = new SQLiteConnection(sql_conn))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = "INSERT INTO "+ table + " (JMBG, Name, Date, Gender, School, Department, Semester, Address, Phone, Picture) VALUES (@JMBG, @Name, @Date, @Gender, @School, @Department, @Semester, @Address, @Phone, @Picture)";
                    cmd.Connection = con;

                    bool t = Read_SQLite(JMBG, table);
                    if (!t)
                    {
                        cmd.Parameters.Add(new SQLiteParameter("@JMBG", JMBG));
                        cmd.Parameters.Add(new SQLiteParameter("@Name", Name));
                        cmd.Parameters.Add(new SQLiteParameter("@Date", Date));
                        cmd.Parameters.Add(new SQLiteParameter("@Gender", Gender));
                        cmd.Parameters.Add(new SQLiteParameter("@School", School));
                        cmd.Parameters.Add(new SQLiteParameter("@Department", Department));
                        cmd.Parameters.Add(new SQLiteParameter("@Semester", Semester));
                        cmd.Parameters.Add(new SQLiteParameter("@Address", Address));
                        cmd.Parameters.Add(new SQLiteParameter("@Phone", Phone));
                        cmd.Parameters.Add(new SQLiteParameter("@Picture", Picture));

                        if (cmd.ExecuteNonQuery() == 1)
                        {
                            return "User Created Successfuly .....";
                        }
                        else return "User wasn't created successfuly!";
                    }
                    else return "User already exists!!!";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            }
        }
        public void Close_SQLite()
        {
            sql_conn.Close();
        }
        public string Delete_SQLite(string JMBG, string table)
        {
            //Connection_SQLite("ProfileDB.db");
            using (SQLiteConnection con = new SQLiteConnection(sql_conn))
            {
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    cmd.CommandText = "DELETE FROM " + table + " where JMBG = '" + JMBG + "'";
                    cmd.Connection = con;
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        return "User Delete Successfuly .......";
                    }
                    else return "User wasn't delete successfuly!";
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }
        public void Read_SQLite_JMBG(string db, string table)
        {
            Connection_SQLite(db);

            string sql = "select * from " + table;
            SQLiteCommand cmd = new SQLiteCommand(sql, sql_conn);
            SQLiteDataReader reader = cmd.ExecuteReader();
            jmbg_sqlite.Clear();
            while (reader.Read())
            {
                jmbg_sqlite.Add(reader.GetString(0));
            }
            Close_SQLite();
        }
        public void Read_SQLite_Profile(string db, string table, string jmbg)
        {
            Connection_SQLite(db);

            string sql = "select * from " + table;
            SQLiteCommand cmd = new SQLiteCommand(sql, sql_conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (jmbg == reader.GetString(0))
                {
                    profile[0] = reader.GetString(0);
                    profile[1] = reader.GetString(1);
                    profile[2] = reader.GetString(2);
                    profile[3] = reader.GetString(3);
                    profile[4] = reader.GetString(4);
                    profile[5] = reader.GetString(5);
                    profile[6] = reader.GetString(6);
                    profile[7] = reader.GetString(7);
                    profile[8] = reader.GetString(8);
                    profile[9] = reader.GetString(9);
                }
            }
        }
    }
}
