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
    public class VersionDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageVersionAction
        {
            Selectall, GetbyID, Insert, Delete, Update, Version
        }
        private object ManageVersionField(ManageVersionAction dbAction, VersioModal entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object StartVersion = System.DBNull.Value;
                object EndVersion = System.DBNull.Value;
                object version = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageVersionAction.Selectall:
                        break;
                    case ManageVersionAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageVersionAction.Insert:
                        StartVersion = entity.StartVesion;
                        EndVersion = entity.EndVersion;
                        version = String.Concat(StartVersion, EndVersion);

                        break;
                    case ManageVersionAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageVersionAction.Update:
                        Id = entity.Id;
                        StartVersion = entity.StartVesion;
                        EndVersion = entity.EndVersion;
                        version = String.Concat(StartVersion, EndVersion);
                        break;
                    case ManageVersionAction.Version:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("Version", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@Version_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@StartVersion_Nm", SqlDbType.VarChar).Value = StartVersion;
                cmd.Parameters.Add("@EndVersion", SqlDbType.VarChar).Value = EndVersion;
                cmd.Parameters.Add("@Version", SqlDbType.VarChar).Value = version;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageVersionAction.Selectall:
                        List<VersioModal> lstdata = new List<VersioModal>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new VersioModal
                                    {
                                        Id = (int)reader["Id"],
                                        StartVesion = (string)reader["StartVersion"],
                                        EndVersion = (string)reader["PerVersion"],
                                        Version = (string)reader["Version"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageVersionAction.GetbyID:
                        VersioModal data = new VersioModal();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.StartVesion = (string)reader["StartVersion"];
                                    data.EndVersion = (string)reader["PerVersion"];
                                    data.Version = (string)reader["Version"];

                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageVersionAction.Version:
                        List<VersioModal> data1 = new List<VersioModal>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new VersioModal
                                    {
                                        Id = (int)reader["Value"],
                                        Version = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageVersionAction.Insert:
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
                    case ManageVersionAction.Delete:
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

                    case ManageVersionAction.Update:
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
        public List<VersioModal> GetAllVersio()
        {
            VersioModal list = new VersioModal();
            return (List<VersioModal>)ManageVersionField(ManageVersionAction.Selectall, list);
        }

        public List<VersioModal> GetAllVersioName()
        {
            VersioModal list = new VersioModal();
            return (List<VersioModal>)ManageVersionField(ManageVersionAction.Version, list);
        }
        public VersioModal GetVersioByID(int id)
        {
            VersioModal obj = new VersioModal();
            obj.Id = id;
            return (VersioModal)ManageVersionField(ManageVersionAction.GetbyID, obj);
        }


        public int UpdateVersio(VersioModal field)
        {
            return (int)ManageVersionField(ManageVersionAction.Update, field);
        }



        public int InsertVersio(VersioModal field)
        {
            return (int)ManageVersionField(ManageVersionAction.Insert, field);
        }

        public string ConnectionString { get; set; }

        public int DeleteVersio(int id)
        {
            try
            {
                VersioModal obj = new VersioModal();
                obj.Id = id;
                var result = ManageVersionField(ManageVersionAction.Delete, obj);
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