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
    public class AvailableFieldDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageAvailableFieldAction
        {
            Selectall, GetbyID, Insert, Delete, Update, AvailableFieldName
        }
        private object ManageAvailableField(ManageAvailableFieldAction dbAction, AvailableFieldModel entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object AvailableFieldNm = System.DBNull.Value;
                object Status = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageAvailableFieldAction.Selectall:
                        break;
                    case ManageAvailableFieldAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageAvailableFieldAction.Insert:
                        AvailableFieldNm = entity.AvailableField_Nm;
                        Status = entity.AvailableField_Status;

                        break;
                    case ManageAvailableFieldAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageAvailableFieldAction.Update:
                        Id = entity.Id;
                        AvailableFieldNm = entity.AvailableField_Nm;
                        Status = entity.AvailableField_Status;
                        break;
                    case ManageAvailableFieldAction.AvailableFieldName:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("AvailableField", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@AvailableField_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@AvailableField_Nm", SqlDbType.VarChar).Value = AvailableFieldNm;
                cmd.Parameters.Add("@AvailableField_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageAvailableFieldAction.Selectall:
                        List<AvailableFieldModel> lstdata = new List<AvailableFieldModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new AvailableFieldModel
                                    {
                                        Id = (int)reader["Id"],
                                        AvailableField_Nm = (string)reader["AvailableField_Name"],
                                        AvailableField_Status = (string)reader["Status"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageAvailableFieldAction.GetbyID:
                        AvailableFieldModel data = new AvailableFieldModel();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.AvailableField_Nm = (string)reader["AvailableField_Name"];
                                    data.AvailableField_Status = (string)reader["Status"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageAvailableFieldAction.AvailableFieldName:
                        List<AvailableFieldModel> data1 = new List<AvailableFieldModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new AvailableFieldModel
                                    {
                                        Id = (int)reader["Value"],
                                        AvailableField_Nm = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageAvailableFieldAction.Insert:
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
                    case ManageAvailableFieldAction.Delete:
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

                    case ManageAvailableFieldAction.Update:
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
        public List<AvailableFieldModel> GetAllAvailableField()
        {
            AvailableFieldModel list = new AvailableFieldModel();
            return (List<AvailableFieldModel>)ManageAvailableField(ManageAvailableFieldAction.Selectall, list);
        }

        public List<AvailableFieldModel> GetAllAvailableFieldModelName()
        {
            AvailableFieldModel list = new AvailableFieldModel();
            return (List<AvailableFieldModel>)ManageAvailableField(ManageAvailableFieldAction.AvailableFieldName, list);
        }
        public AvailableFieldModel GetAvailableFieldByID(int id)
        {
            AvailableFieldModel obj = new AvailableFieldModel();
            obj.Id = id;
            return (AvailableFieldModel)ManageAvailableField(ManageAvailableFieldAction.GetbyID, obj);
        }


        public int UpdateAvailableField(AvailableFieldModel field)
        {
            return (int)ManageAvailableField(ManageAvailableFieldAction.Update, field);
        }



        public int InsertAvailableField(AvailableFieldModel field)
        {
            return (int)ManageAvailableField(ManageAvailableFieldAction.Insert, field);
        }

        public string ConnectionString { get; set; }

        public int DeleteAvailableField(int id)
        {
            try
            {
                AvailableFieldModel obj = new AvailableFieldModel();
                obj.Id = id;
                var result = ManageAvailableField(ManageAvailableFieldAction.Delete, obj);
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