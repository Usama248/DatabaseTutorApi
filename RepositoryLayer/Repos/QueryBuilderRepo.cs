using CommonLayer.Helper;
using DatabaseTutor.DTOs;
using DatabaseTutor.DTOs.RequestDTOs.StudentQuery;
using Microsoft.Data.SqlClient;
using RepositoryLayer.Infrastructures;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repos
{
    public class QueryBuilderRepo : IQueryBuilderRepo
    {
        public ResponseDTO<dynamic> GetDatabaseAsLookup()
        {
            string connectionString = "Data Source=DESKTOP-46MKD1L; Integrated Security=True;";
            List<string> databaseNames = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.databases", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            databaseNames.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return Responses.OKGetAll<dynamic>("Class", databaseNames.Select(p => new
            {
                key = p,
                value = p
            }));
        }

        public ResponseDTO<dynamic> GetTableAsLookup(string DatabaseName)
        {
            List<string> TableNames = new List<string>();
            string connectionString = "Data Source=DESKTOP-46MKD1L; Integrated Security=True;Initial Catalog=" + DatabaseName + ";";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");
                foreach (DataRow row in schema.Rows)
                {
                    TableNames.Add(row[2].ToString());
                }
            }
            return Responses.OKGetAll<dynamic>("Class", TableNames.Select(p => new
            {
                key = p,
                value = p
            }));
        }


        public ResponseDTO<dynamic> GetTableColumnsAsLookup(string DatabaseName, string TableName)
        {
            string connectionString = "Data Source=DESKTOP-46MKD1L; Integrated Security=True;Initial Catalog=" + DatabaseName + ";";
            List<string> Columns = new List<string>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT name from sys.columns WHERE object_id = OBJECT_ID('" + TableName + "')", con))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Columns.Add(dr[0].ToString());
                        }
                    }
                }
            }
            return Responses.OKGetAll<dynamic>("Class", Columns.Select(p => new
            {
                label = p,
                value = p
            }));
        }
        public ResponseDTO<dynamic> ExecuteQuery(ExecuteQueryRequestDTO requestQuery)
        {
            try
            {
                string connectionString = "Data Source=DESKTOP-46MKD1L; Integrated Security=True;Initial Catalog=" + requestQuery.Database + ";";
                var dataTable = new DataTable();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(requestQuery.Query, con))
                    {
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                             dataTable.Load(dr);
                        }
                    }
                }
                return Responses.OKGetAll<dynamic>("Class", new
                {
                    columns= dataTable.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray(),

                    Rows = dataTable.Rows.Cast<DataRow>()
                                 .Select(x => x.ItemArray)
                                 .ToArray(),
                });
            }
            catch (Exception ex)
            {
                return Responses.SomethingWentWrong<dynamic>(ex.Message, null);
            }
        }
    }
}
