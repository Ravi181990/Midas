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
    public class CategoryDataRepository
    {
        SqlCommand cmd;
        SqlDataAdapter db;
        string Conectionstring = ConfigurationManager.ConnectionStrings["MidasDemo"].ConnectionString;

        enum ManageCategoryAction
        {
            Selectall, GetbyID, Insert, Delete,Update, Catename
        }
        private object ManageCategory(ManageCategoryAction dbAction, Category entity)
        {
            try
           {
                object Id = System.DBNull.Value;
                object CategoryNm = System.DBNull.Value;
                object Status= System.DBNull.Value;
                object Action = System.DBNull.Value;
                object Result = System.DBNull.Value;

                switch (dbAction)
                {
                    case ManageCategoryAction.Selectall:
                        break;
                    case ManageCategoryAction.GetbyID:
                        Id = entity.Id;
                        break;
                    case ManageCategoryAction.Insert:
                        CategoryNm = entity.CategoryNm;
                        Status = entity.Status;
                       
                        break;
                    case ManageCategoryAction.Delete:
                        Id = entity.Id;
                        break;
                    case ManageCategoryAction.Update:
                        Id = entity.Id;
                        CategoryNm = entity.CategoryNm;
                        Status = entity.Status;
                        break;
                    case ManageCategoryAction.Catename:
                        break;
                    default:
                        break;

                }

                var conn = new SqlConnection(Conectionstring);
                conn.Open();
                cmd = new SqlCommand("Category", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;
                cmd.Parameters.Add("@Category_Id", SqlDbType.Int).Value = Id;
                cmd.Parameters.Add("@Category_Nm", SqlDbType.VarChar).Value = CategoryNm;
                cmd.Parameters.Add("@Category_Status", SqlDbType.VarChar).Value = Status;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar).Value = dbAction.ToString();


                switch (dbAction)
                {
                    case ManageCategoryAction.Selectall:
                        List<Category> lstdata = new List<Category>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    lstdata.Add(new Category
                                    {
                                       Id = (int)reader["Id"],
                                        CategoryNm = (string)reader["Category_Name"],
                                        Status = (string)reader["Status"],
                                      });
                                }
                            }
                            Result = lstdata;
                            conn.Close();
                        }
                        break;
                    case ManageCategoryAction.GetbyID:
                        Category data = new Category();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data.Id = (int)reader["Id"];
                                    data.CategoryNm = (string)reader["Category_Name"];
                                    data.Status = (string)reader["Status"];
                                   
                                };
                            }
                        }
                        Result = data;
                        conn.Close();
                        break;
                    case ManageCategoryAction.Catename:
                        List<Category> data1 = new List<Category>();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    data1.Add(new Category
                                    {
                                        Id = (int)reader["Value"],
                                        CategoryNm = (string)reader["text"],
                                        
                                    });
                                }
                            }
                            Result = data1;
                            conn.Close();
                        }
                        break;
                    case ManageCategoryAction.Insert:
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
                    case ManageCategoryAction.Delete:
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

                    case ManageCategoryAction.Update:
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
        public List<Category> GetAllCategory()
        {
            Category list = new Category();
            return (List<Category>)ManageCategory(ManageCategoryAction.Selectall, list);
        }

        public List<Category> GetAllCategoryname()
        {
            Category list = new Category();
            return (List<Category>)ManageCategory(ManageCategoryAction.Catename, list);
        }
        public Category GetCategoryByID(int id)
        {
            Category obj = new Category();
            obj.Id = id;
            return (Category)ManageCategory(ManageCategoryAction.GetbyID, obj);
        }


        public int UpdateCategory(Category category)
        {
            return (int)ManageCategory(ManageCategoryAction.Update,category);
        }



        public int InsertCategory(Category category)
        {
            return (int)ManageCategory(ManageCategoryAction.Insert, category);
        }

        public string ConnectionString { get; set; }

        public int DeleteCategory(int id)
        {
            try
            {
                Category obj = new Category();
                obj.Id = id;
                var result = ManageCategory(ManageCategoryAction.Delete, obj);
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