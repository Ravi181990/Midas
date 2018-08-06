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
    public class PlantDataRepository
    {

        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManagePlantAction
        {
            Selectall, GetbyID, Insert, Delete, Update, PlantName
        }
        private object ManagePlant(ManagePlantAction dbAction, Plant entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object PlantNM = System.DBNull.Value;
                object Status = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManagePlantAction.Selectall:
                        break;
                    case ManagePlantAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManagePlantAction.Insert:
                        PlantNM = entity.Plant_Nm;
                        Status = entity.Plant_Status;

                        break;
                    case ManagePlantAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManagePlantAction.Update:
                        Id = entity.Id;
                        PlantNM = entity.Plant_Nm;
                        Status = entity.Plant_Status;
                        break;
                    case ManagePlantAction.PlantName :
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("Plant", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@Plant_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Plant_Nm", SqlDbType.VarChar).Value = PlantNM;
                cmd.Parameters.Add("@Plant_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManagePlantAction.Selectall:
                        List<Plant> lstdata = new List<Plant>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new Plant
                                    {
                                        Id = (int)reader["Id"],
                                        Plant_Nm = (string)reader["Plant_Name"],
                                        Plant_Status = (string)reader["Status"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManagePlantAction.GetbyID:
                        Plant data = new Plant();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.Plant_Nm = (string)reader["Plant_Name"];
                                    data.Plant_Status = (string)reader["Status"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManagePlantAction.PlantName:
                        List<Plant> data1 = new List<Plant>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new Plant
                                    {
                                        Id = (int)reader["Value"],
                                        Plant_Nm = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManagePlantAction.Insert:
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
                    case ManagePlantAction.Delete:
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

                    case ManagePlantAction.Update:
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
        public List<Plant> GetAllPlant()
        {
            Plant list = new Plant();
            return (List<Plant>)ManagePlant(ManagePlantAction.Selectall, list);
        }

        public List<Plant> GetAllPlantName()
        {
            Plant list = new Plant();
            return (List<Plant>)ManagePlant(ManagePlantAction.PlantName, list);
        }
        public Plant GetCategoryByID(int id)
        {
            Plant obj = new Plant();
            obj.Id = id;
            return (Plant)ManagePlant(ManagePlantAction.GetbyID, obj);
        }


        public int UpdatePlant(Plant plant)
        {
            return (int)ManagePlant(ManagePlantAction.Update,plant);
        }



        public int InsertPlant(Plant plant)
        {
            return (int)ManagePlant(ManagePlantAction.Insert, plant);
        }

        public string ConnectionString { get; set; }

        public int DeletePlant(int id)
        {
            try
            {
                Plant obj = new Plant();
                obj.Id = id;
                var result = ManagePlant(ManagePlantAction.Delete, obj);
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