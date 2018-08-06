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
    public class FunctionAreaDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageFunctionAreaAction
        {
            Selectall, GetbyID, Insert, Delete, Update, FunctionAreaName
        }
        private object ManageFunction(ManageFunctionAreaAction dbAction, FunctionAreaModel entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object FunctionAreaNm = System.DBNull.Value;
                object Status = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageFunctionAreaAction.Selectall:
                        break;
                    case ManageFunctionAreaAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageFunctionAreaAction.Insert:
                        FunctionAreaNm = entity.FunctionArea_Name;
                        Status = entity.FunctionArea_Staus;

                        break;
                    case ManageFunctionAreaAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageFunctionAreaAction.Update:
                        Id = entity.Id;
                        FunctionAreaNm = entity.FunctionArea_Name;
                        Status = entity.FunctionArea_Staus;
                        break;
                    case ManageFunctionAreaAction.FunctionAreaName:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("FunctionArea", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@FunctionArea_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@FunctionArea_Nm", SqlDbType.VarChar).Value = FunctionAreaNm;
                cmd.Parameters.Add("@FunctionArea_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageFunctionAreaAction.Selectall:
                        List<FunctionAreaModel> lstdata = new List<FunctionAreaModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new FunctionAreaModel
                                    {
                                        Id = (int)reader["Id"],
                                        FunctionArea_Name = (string)reader["FunctionalArea_Name"],
                                        FunctionArea_Staus = (string)reader["Status"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageFunctionAreaAction.GetbyID:
                        FunctionAreaModel data = new FunctionAreaModel();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.FunctionArea_Name = (string)reader["FunctionalArea_Name"];
                                    data.FunctionArea_Staus = (string)reader["Status"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageFunctionAreaAction.FunctionAreaName:
                        List<FunctionAreaModel> data1 = new List<FunctionAreaModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new FunctionAreaModel
                                    {
                                        Id = (int)reader["Value"],
                                        FunctionArea_Name = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageFunctionAreaAction.Insert:
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
                    case ManageFunctionAreaAction.Delete:
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

                    case ManageFunctionAreaAction.Update:
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
        public List<FunctionAreaModel> GetAllFunction()
        {
            FunctionAreaModel list = new FunctionAreaModel();
            return (List<FunctionAreaModel>)ManageFunction(ManageFunctionAreaAction.Selectall, list);
        }

        public List<FunctionAreaModel> GetAllfunctionname()
        {
            FunctionAreaModel list = new FunctionAreaModel();
            return (List<FunctionAreaModel>)ManageFunction(ManageFunctionAreaAction.FunctionAreaName, list);
        }
        public FunctionAreaModel GetFunctionByID(int id)
        {
            FunctionAreaModel obj = new FunctionAreaModel();
            obj.Id = id;
            return (FunctionAreaModel)ManageFunction(ManageFunctionAreaAction.GetbyID, obj);
        }


        public int UpdateFunction(FunctionAreaModel function)
        {
            return (int)ManageFunction(ManageFunctionAreaAction.Update, function);
        }



        public int InsertFunction(FunctionAreaModel function)
        {
            return (int)ManageFunction(ManageFunctionAreaAction.Insert, function);
        }

        public string ConnectionString { get; set; }

        public int DeleteFunction(int id)
        {
            try
            {
                FunctionAreaModel obj = new FunctionAreaModel();
                obj.Id = id;
                var result = ManageFunction(ManageFunctionAreaAction.Delete, obj);
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