using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MessageBoardDAL
{
    public static class ForumDB
    {
        public static void NonQuery(string sproc, params SqlParameter[] parameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["messageboardDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = sproc;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddRange(parameters);
                comm.ExecuteNonQuery();
            }
        }
        public static DataTable QuerySproc(string sproc, params SqlParameter[] parameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["messageboardDB"].ConnectionString;
            using(SqlConnection conn = new SqlConnection(connectionString))
            using(SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = sproc;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddRange(parameters);

                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter(comm);
                adapter.Fill(table);
                
                return table;
            }           
        }

        public static object ExecuteScalar(string sproc, params SqlParameter[] parameters)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["messageboardDB"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand comm = conn.CreateCommand())
            {
                comm.CommandText = sproc;
                comm.CommandType = CommandType.StoredProcedure;
                comm.Parameters.AddRange(parameters);

                conn.Open();

                object res = comm.ExecuteScalar();
                return res;

            }
        }


    }
}
