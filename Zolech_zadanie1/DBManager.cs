using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
//using Zolech_zadanie1;

namespace Zolech_zadanie1
{
    public class DBManager
    {
        private static string MZ_connection_string = ConfigurationManager.ConnectionStrings["MZ_DB"].ConnectionString;

        private static string MZ_getAll = "SELECT * from users;";
        private static string MZ_getAll2 = "SELECT COUNT(*) from users;";
        private static string MZ_getLogged = "SELECT * from users where zalogowany = 1;";
        private static string MZ_register = "INSERT INTO users(Id) values(@MZ_id);";
        private static string MZ_login = "UPDATE users SET zalogowany = 1 WHERE Id = @MZ_id; ";
        private static string MZ_logout = "UPDATE users SET zalogowany = 0 WHERE Id = @MZ_id; ";
        private static string MZ_forget = "DELETE from users WHERE Id = @MZ_id; ";
        private static string MZ_rozmiar = "UPDATE users SET zmiany_rozmiaru = zmiany_rozmiaru + 1 where Id = @MZ_id; ";
        private static string MZ_polozenie = "UPDATE users SET zmiany_polozenia = zmiany_polozenia + 1 where Id = @MZ_id; ";
        private static string MZ_logoutAll = "UPDATE users SET zalogowany = 0; ";

        internal object getALL()
        {
            return select(MZ_getAll);
        }

        internal object getLogged1()
        {
            return select(MZ_getLogged);
        }

        public static ArrayList getAll() { return select(MZ_getAll); }
        public static ArrayList getLogged() { return select(MZ_getLogged); }

        public static void register(string MZ_id)
        {
            update(MZ_register, MZ_id);
        }
        public static void login(string MZ_id)
        {
            update(MZ_login, MZ_id);
        }
        public static void logout(string MZ_id)
        {
            update(MZ_logout, MZ_id);
        }

        public static void rozmiar(string MZ_id)
        {
            update(MZ_rozmiar, MZ_id);
        }
        public static void polozenie(string MZ_id)
        {
            update(MZ_polozenie, MZ_id);
        }
        public static void logoutAll() { update(MZ_logoutAll, ""); }

        //opcjonalne
        public static void forget(string MZ_id)
        {
            update(MZ_forget, MZ_id);
        }


        public static ArrayList select(string MZ_query)
        {
            ArrayList MZ_list = new ArrayList();
            using (SqlConnection MZ_conn = new SqlConnection(MZ_connection_string))
            {
                SqlCommand MZ_command = new SqlCommand(MZ_query, MZ_conn);
                try
                {
                    MZ_conn.Open();
                    SqlDataReader MZ_reader = MZ_command.ExecuteReader();
                    while (MZ_reader.Read())
                    {
                        Uzytkownik MZ_uz = new Uzytkownik((string)MZ_reader[0], (DateTime)MZ_reader[1], (int)MZ_reader[2], (int)MZ_reader[3]);
                        MZ_list.Add(MZ_uz);
                    }
             MZ_reader.Close();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
            return MZ_list;
        }

        public static void update(string MZ_query, string MZ_param)
        {
            ArrayList MZ_list = new ArrayList();
            using (SqlConnection MZ_conn = new SqlConnection(MZ_connection_string))
            {
                SqlCommand MZ_command = new SqlCommand(MZ_query, MZ_conn);
                MZ_command.Parameters.AddWithValue("MZ_id", MZ_param);
                try
                {
                    MZ_conn.Open();
                    MZ_command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        
    }
}