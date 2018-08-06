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
    public class FrequencyDataRepository
    {

        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageFrequencyAction
        {
            Selectall, GetbyID, Insert, Delete, Update, FrequencyName
        }
        private object ManageFrequency(ManageFrequencyAction dbAction, FrequencyModel entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object FrequencyNm = System.DBNull.Value;
                object Status = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageFrequencyAction.Selectall:
                        break;
                    case ManageFrequencyAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageFrequencyAction.Insert:
                        FrequencyNm = entity.Frequency_Nm;
                        Status = entity.Status;

                        break;
                    case ManageFrequencyAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageFrequencyAction.Update:
                        Id = entity.Id;
                        FrequencyNm = entity.Frequency_Nm;
                        Status = entity.Status;
                        break;
                    case ManageFrequencyAction.FrequencyName:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("Frequency", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@Frequency_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Frequency_Nm", SqlDbType.VarChar).Value = FrequencyNm;
                cmd.Parameters.Add("@Frequency_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageFrequencyAction.Selectall:
                        List<FrequencyModel> lstdata = new List<FrequencyModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new FrequencyModel
                                    {
                                        Id = (int)reader["Id"],
                                        Frequency_Nm = (string)reader["Frequency_Name"],
                                        Status = (string)reader["Status"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageFrequencyAction.GetbyID:
                        FrequencyModel data = new FrequencyModel();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.Frequency_Nm = (string)reader["Frequency_Name"];
                                    data.Status = (string)reader["Status"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageFrequencyAction.FrequencyName:
                        List<FrequencyModel> data1 = new List<FrequencyModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new FrequencyModel
                                    {
                                        Id = (int)reader["Value"],
                                        Frequency_Nm = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageFrequencyAction.Insert:
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
                    case ManageFrequencyAction.Delete:
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

                    case ManageFrequencyAction.Update:
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
        public List<FrequencyModel> GetAllFrequency()
        {
            FrequencyModel list = new FrequencyModel();
            return (List<FrequencyModel>)ManageFrequency(ManageFrequencyAction.Selectall, list);
        }

        public List<FrequencyModel> GetAllFrequencyname()
        {
            FrequencyModel list = new FrequencyModel();
            return (List<FrequencyModel>)ManageFrequency(ManageFrequencyAction.FrequencyName, list);
        }
        public FrequencyModel GetFrequencyByID(int id)
        {
            FrequencyModel obj = new FrequencyModel();
            obj.Id = id;
            return (FrequencyModel)ManageFrequency(ManageFrequencyAction.GetbyID, obj);
        }


        public int UpdateFrequency(FrequencyModel frequency)
        {
            return (int)ManageFrequency(ManageFrequencyAction.Update, frequency);
        }



        public int InsertFrequency(FrequencyModel frequency)
        {
            return (int)ManageFrequency(ManageFrequencyAction.Insert, frequency);
        }

        public string ConnectionString { get; set; }

        public int DeleteFrequency(int id)
        {
            try
            {
                FrequencyModel obj1 = new FrequencyModel();
                obj1.Id = id;
                var result = ManageFrequency(ManageFrequencyAction.Delete, obj1);
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