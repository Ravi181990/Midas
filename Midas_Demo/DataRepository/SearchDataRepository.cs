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
            Search, Insert
        }
        private object ManageSearch(ManageSearchAction dbAction, SearchModel entity)
        {
            try
            {
                object DataEntryNm1 = System.DBNull.Value;
                object Description1 = System.DBNull.Value;
                object Category1 = System.DBNull.Value;
                object TechnicalName1 = System.DBNull.Value;
                object Plants1 = System.DBNull.Value;              
                object tcodes1 = System.DBNull.Value;
                object AvailableFields1 = System.DBNull.Value;
                object Remarks1 = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageSearchAction.Search:
                       
                        DataEntryNm1 = entity.Name;
                        Description1 = entity.Description;
                        Remarks1 = entity.Remaks;
                        TechnicalName1 = entity.Tech_Name;
                        Category1 = entity.Category;                        
                        Plants1 = entity.Plant;
                        tcodes1 = entity.Transactions;
                        AvailableFields1 = entity.Fields;
                        break;           

                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("SearchCriteria", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;             
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = DataEntryNm1;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description1;
                cmd.Parameters.Add("@Category_id", SqlDbType.VarChar).Value = Category1;
                cmd.Parameters.Add("@Technical_Name", SqlDbType.VarChar).Value = TechnicalName1;
                cmd.Parameters.Add("@Plant_id", SqlDbType.VarChar).Value = Plants1;                
                cmd.Parameters.Add("@T_Code_id", SqlDbType.VarChar).Value = tcodes1;                           
                cmd.Parameters.Add("@Available_Fields_id", SqlDbType.VarChar).Value =AvailableFields1;               
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks1;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();



                switch (dbAction)
                {
                    case ManageSearchAction.Search:
                        SearchModel lstdata = new SearchModel();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {

                                   
                                    lstdata.Name = (string)reader["Name"];
                                    lstdata.Description = (string)reader["Description"];
                                    lstdata.Category = (List<string>)reader["Category_Id"];
                                    lstdata.Tech_Name = (string)reader["Technical_Name"];
                                    lstdata.Plant = (List<string>)reader["Plant_Id"];
                                    lstdata.DashboardVersion = (float)reader["Dashboard_Version"];
                                    lstdata.Transactions = (List<string>)reader["T_Code_Id"];
                                    lstdata.Fields = (List<string>)reader["Available_Fields_Id"];
                                    lstdata.Remaks = (string)reader["Remarks"];
                                } ;
                             }
                            }
                            Result = lstdata;
                            conn.Close();
                        
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
    
        public SearchModel InsertSearchData(SearchModel obj)
        {
            SearchModel search = new SearchModel();
            search.Name = obj.Name;
            search.Description = obj.Description;
            search.Remaks = obj.Remaks;
            search.Tech_Name = obj.Tech_Name;
            search.Category = obj.Category;
            search.Plant = obj.Plant;
            search.Transactions = obj.Transactions;
            search.Fields = obj.Fields;

            return (SearchModel) ManageSearch(ManageSearchAction.Search, search);
        }



    }
}
