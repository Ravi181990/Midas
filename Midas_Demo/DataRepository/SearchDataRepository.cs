using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using Midas_Demo.Models;

namespace Midas_Demo.DataRepository
{
    public class SearchDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageSearchAction
        {
            Selectall,Insert
        }
        private object ManageSearch(ManageSearchAction dbAction, SearchModel entity)
        {
            try
            {
                object Id1 = System.DBNull.Value;
                object DataEntryNm1 = System.DBNull.Value;
                object Description1 = System.DBNull.Value;
                object Category1 = System.DBNull.Value;
                object TechnicalName1 = System.DBNull.Value;
                object Plants1 = System.DBNull.Value;                
                object DashboardVersion1 = System.DBNull.Value;
                object tcodes1 = System.DBNull.Value;              
                object AvailableFields1 = System.DBNull.Value;                
                object Remarks1 = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageSearchAction.Selectall:
                        break;
                  
                    case ManageSearchAction.Insert:
                        DataEntryNm1 = entity.Name;
                        Description1 = entity.Description;
                        Category1 = entity.Category;
                        TechnicalName1 = entity.Tech_Name;
                        Plants1 = entity.Plant;               
                        tcodes1 = entity.Transactions;
                        AvailableFields1 = entity.Fields;
                        Remarks1 = entity.Remaks;

                        break;
                   
                 default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("Category", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@DataEntry_Id", SqlDbType.Int).Value = Id1;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = DataEntryNm1;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description1;
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = Category1;
                cmd.Parameters.Add("@Technical_Name", SqlDbType.VarChar).Value = TechnicalName1;
                cmd.Parameters.Add("@Plant", SqlDbType.VarChar).Value = Plants1;              
                cmd.Parameters.Add("@Dashboard_Version", SqlDbType.VarChar).Value = DashboardVersion1;
                cmd.Parameters.Add("@T_Code", SqlDbType.VarChar).Value = tcodes1;               
                cmd.Parameters.Add("@Available_Fields", SqlDbType.Int).Value = AvailableFields1;               
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks1;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageSearchAction.Selectall:
                        List<SearchModel> lstdata = new List<SearchModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new SearchModel
                                    {
                                        Id = (int)reader["Id"],
                                        Name = (string)reader["Name"],
                                        Description = (string)reader["Description"],
                                        Category = (string)reader["Category"],
                                        Tech_Name = (string)reader["Technical_Name"],
                                        DashboardVersion = (float)reader["Dashboard_Version"],
                                        Transactions = (string)reader["T_Code"],
                                        Fields = (string)reader["Available_Fields"],                     
                                        Remaks = (string)reader["Remarks"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                  
                       
                   
                    case ManageSearchAction.Insert:
                        try
                        {
                            cmd.ExecuteNonQuery();
                            Result = 1;
                        }
                        catch (Exception ex)
                        {

                            Result = -1;
                        }
                        break;
                    default:
                        break;
                }

                return Result;
            }
            catch (Exception ex)
            {
                throw ex;

            }



        }
        public List<SearchModel> GetAllData()
        {
            SearchModel list = new SearchModel();
            return (List<SearchModel>)ManageSearch(ManageSearchAction.Selectall, list);
        }

       



        public int InsertSearchData(SearchModel category)
        {
            return (int)ManageSearch(ManageSearchAction.Insert, category);
        }


      
    }
}