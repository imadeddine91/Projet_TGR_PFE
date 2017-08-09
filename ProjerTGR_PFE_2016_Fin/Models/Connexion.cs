using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjerTGR_PFE_2016_Fin.Models
{
    public class Connexion
    {
        public static SqlConnection cn = new SqlConnection(@"Server=.\MSSQLEXPRESS;DataBase=Base_Final_TGR2016;Trusted_Connection=true");
        public static SqlCommand cmd;
        public static SqlDataReader rd;
        public static SqlDataAdapter dap;


        public static void connexion()
        {
            if (Connexion.cn.State == ConnectionState.Closed)
            {
                Connexion.cn.Open();
            }
        }

        public static void deconnexion()
        {
            if (Connexion.cn.State == ConnectionState.Open)
            {
                Connexion.cn.Close();
            }
        }
    }
}