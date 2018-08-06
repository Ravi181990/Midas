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
    public class DataEntryDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageDataEntryAction
        {
            Selectall, GetbyID, Insert, Delete, Update
        }
        private object ManageDataEntryField(ManageDataEntryAction dbAction, DataEntry entity)
        {
            try
            {
                object Id1 = System.DBNull.Value;
                object DataEntryNm1 = System.DBNull.Value;
                object Description1 = System.DBNull.Value;
                object Category1 = System.DBNull.Value;
                object TechnicalName1 = System.DBNull.Value;
                object Plants1 = System.DBNull.Value;
                object Frequency1 = System.DBNull.Value;

                object DashboardVersion1 = System.DBNull.Value;
                object tcodes1 = System.DBNull.Value;
                object FunctionalArea1 = System.DBNull.Value;
                object AvailableFields1 = System.DBNull.Value;
                object DocumentLink1 = System.DBNull.Value;
                object UserGuides1 = System.DBNull.Value;
                object Remarks1 = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageDataEntryAction.Selectall:
                        break;
                    case ManageDataEntryAction.GetbyID:
                        Id1 = entity.Id;
                        break;
                    case ManageDataEntryAction.Insert:
                        DataEntryNm1 = entity.Name;
                        Description1 = entity.Description;
                        Category1 = entity.Category;
                        TechnicalName1 = entity.TechnicalName;
                        Plants1 = entity.Plants;
                        Frequency1 = entity.Frequency;
                        DashboardVersion1= entity.DashboardVersion;
                     
                     
                       
                        tcodes1 = entity.tcodes;
                        FunctionalArea1 = entity.FunctionalArea;
                        AvailableFields1 = entity.AvailableFields;
                        DocumentLink1 = entity.DocumentLink;
                        UserGuides1 = entity.UserGuides;
                        Remarks1 = entity.Remarks;
                       

                        break;
                    case ManageDataEntryAction.Delete:
                        Id1 = entity.Id;
                        break;
                    case ManageDataEntryAction.Update:
                        Id1 = entity.Id;
                        DataEntryNm1 = entity.Name;
                        Description1 = entity.Description;
                        Category1 = entity.Category;
                        TechnicalName1 = entity.TechnicalName;
                        Plants1 = entity.Plants;
                        Frequency1 = entity.Frequency;
                        DashboardVersion1 = entity.DashboardVersion;
                        tcodes1 = entity.tcodes;
                        FunctionalArea1 = entity.FunctionalArea;
                        AvailableFields1 = entity.AvailableFields;
                        DocumentLink1 = entity.DocumentLink;
                        UserGuides1 = entity.UserGuides;
                        Remarks1 = entity.Remarks;
                        break;
                  
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("DataEntryGuide", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@DataEntry_Id", SqlDbType.Int).Value = Id1;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar).Value = DataEntryNm1;
                cmd.Parameters.Add("@Description", SqlDbType.VarChar).Value = Description1;
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = Category1;
                cmd.Parameters.Add("@Technical_Name", SqlDbType.VarChar).Value = TechnicalName1;
                cmd.Parameters.Add("@Plant", SqlDbType.VarChar).Value = Plants1;
                cmd.Parameters.Add("@Frequency", SqlDbType.Int).Value = Frequency1;
                cmd.Parameters.Add("@Dashboard_Version", SqlDbType.VarChar).Value = DashboardVersion1;
                cmd.Parameters.Add("@T_Code",SqlDbType.VarChar).Value =tcodes1;
                cmd.Parameters.Add("@Functional_Area", SqlDbType.VarChar).Value = FunctionalArea1;
                cmd.Parameters.Add("@Available_Fields", SqlDbType.Int).Value = AvailableFields1;
                cmd.Parameters.Add("@Document_Link", SqlDbType.VarChar).Value = DocumentLink1;
                cmd.Parameters.Add("@User_Guides", SqlDbType.VarChar).Value = UserGuides1;
                cmd.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = Remarks1;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageDataEntryAction.Selectall:
                        List<DataEntry> lstdata = new List<DataEntry>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new DataEntry
                                    {
                                        Id = (int)reader["Id"],
                                        Name = (string)reader["Name"],
                                        Description = (string)reader["Description"],
                                        Category = (string)reader["Category"],
                                        TechnicalName = (string)reader["Technical_Name"],
                                        Plants = (string)reader["Plant"],
                                        Frequency = (string)reader["Frequency"],
                                        DashboardVersion = (float)reader["Dashboard_Version"],
                                        tcodes = (string)reader["T_Code"],
                                        FunctionalArea = (string)reader["Functional_Area"],
                                        AvailableFields = (string)reader["Available_Fields"],
                                        DocumentLink = (string)reader["Document_Link"],
                                        UserGuides = (string)reader["User_Guides"],
                                        Remarks = (string)reader["Remarks"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageDataEntryAction.GetbyID:
                        DataEntry data = new DataEntry();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.Name = (string)reader["Name"];
                                    data.Description = (string)reader["Description"];
                                    data.Category = (string)reader["Category"];
                                    data.TechnicalName = (string)reader["Technical_Name"];
                                    data.Plants = (string)reader["Plant"];
                                    data.Frequency = (string)reader["Frequency"];
                                    data.DashboardVersion = ((float)reader["Dashboard_Version"]);
                                    data.tcodes = (string)reader["T_Code"];
                                    data.FunctionalArea = (string)reader["Functional_Area"];
                                    data.AvailableFields = (string)reader["Available_Fields"];
                                    data.DocumentLink = (string)reader["Document_Link"];
                                    data.UserGuides = (string)reader["User_Guides"];
                                    data.Remarks = (string)reader["Remarks"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                  
                    case ManageDataEntryAction.Insert:
                        try
                        {
                            cmd.ExecuteNonQuery();
                            Result = 1;
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        break;
                    case ManageDataEntryAction.Delete:
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

                    case ManageDataEntryAction.Update:
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
        public List<DataEntry> GetAllDataEntry()
        {
            DataEntry list = new DataEntry();
            return (List<DataEntry>)ManageDataEntryField(ManageDataEntryAction.Selectall, list);
        }

        public DataEntry GetDataEntryByID(int id)
        {
            DataEntry obj = new DataEntry();
            obj.Id = id;
            return (DataEntry)ManageDataEntryField(ManageDataEntryAction.GetbyID, obj);
        }


        public int UpdateDataEntry(DataEntry field)
        {
            return (int)ManageDataEntryField(ManageDataEntryAction.Update, field);
        }



        public Int32 InsertDataEntry(DataEntry field)
        {
            try
            {
                var result= ManageDataEntryField(ManageDataEntryAction.Insert, field);

                return 1;

            }
            catch (SqlException ex)
            {

                throw ex;
            }
           
        }

        public string ConnectionString { get; set; }

        public int DeleteDataEntry(int id)
        {
            try
            {
                DataEntry obj = new DataEntry();
                obj.Id = id;
                var result = ManageDataEntryField(ManageDataEntryAction.Delete, obj);
                return 1;
            }
            catch (Exception)
            {

                //logging
                return -1;
            }

        }

    }
}