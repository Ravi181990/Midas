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
    public class TCodeDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageTcodeAction
        {
            Selectall, GetbyID, Insert, Delete, Update, TcodeName
        }
        private object ManageTCode(ManageTcodeAction dbAction, TcodeModel entity)
        {
            try
            {
                object Id = System.DBNull.Value;
                object TcodeNm = System.DBNull.Value;
                object FunctionalArea = System.DBNull.Value;
                object Status = System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageTcodeAction.Selectall:
                        break;
                    case ManageTcodeAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageTcodeAction.Insert:
                        TcodeNm = entity.T_CodeName;
                        FunctionalArea = entity.FunctionArea;
                        Status = entity.Tcode_Status;

                        break;
                    case ManageTcodeAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageTcodeAction.Update:
                        Id = entity.Id;
                        TcodeNm = entity.T_CodeName;
                       
                        Status = entity.Tcode_Status;
                        break;
                    case ManageTcodeAction.TcodeName:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("TCode", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@Tcode_Id", System.Data.SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@TCode_Nm", System.Data.SqlDbType.VarChar).Value = TcodeNm;
                cmd.Parameters.Add("@FunctionalArea", System.Data.SqlDbType.VarChar).Value = FunctionalArea;
                cmd.Parameters.Add("@Tcode_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageTcodeAction.Selectall:
                        List<TcodeModel> lstdata = new List<TcodeModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new TcodeModel
                                    {
                                        Id = (int)reader["Id"],
                                        T_CodeName = (string)reader["T_Code"],
                                        FunctionArea = (String)reader["FunctionalArea_Name"],
                                        Tcode_Status = (string)reader["Status"],
                                    });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageTcodeAction.GetbyID:
                        TcodeModel data = new TcodeModel();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.T_CodeName = (string)reader["T_Code"];
                                    data.FunctionArea = (String)reader["FunctionalArea_Name"];
                                    data.Tcode_Status = (string)reader["Status"];

                                };
                            }

                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageTcodeAction.TcodeName:
                        List<TcodeModel> data1 = new List<TcodeModel>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new TcodeModel
                                    {
                                        Id = (int)reader["Value"],
                                        T_CodeName = (string)reader["text"],

                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageTcodeAction.Insert:
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
                    case ManageTcodeAction.Delete:
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

                    case ManageTcodeAction.Update:
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
        public List<TcodeModel> GetAllTcode()
        {
            TcodeModel list = new TcodeModel();
            return (List<TcodeModel>)ManageTCode(ManageTcodeAction.Selectall, list);
        }

        public List<TcodeModel> GetAllTcodename()
        {
            TcodeModel list = new TcodeModel();
            return (List<TcodeModel>)ManageTCode(ManageTcodeAction.TcodeName, list);
        }
        public TcodeModel GetTcodeByID(int id)
        {
            TcodeModel obj = new TcodeModel();
            obj.Id = id;
            return (TcodeModel)ManageTCode(ManageTcodeAction.GetbyID, obj);
        }


        public int UpdateTcode(TcodeModel tcode)
        {
            return (int)ManageTCode(ManageTcodeAction.Update, tcode);
        }



        public int InsertTcode(TcodeModel tcode)
        {
            return (int)ManageTCode(ManageTcodeAction.Insert, tcode);
        }

        public string ConnectionString { get; set; }

        public int DeleteTcode(int id)
        {
            try
            {
                TcodeModel obj = new TcodeModel();
                obj.Id = id;
                var result = ManageTCode(ManageTcodeAction.Delete, obj);
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