using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagement.Models
{
    public class Categories
    {
        public int Category { get; set;}
        public string CategoryName { get; set;}

        //public static List<Categories> GetAllCategories()
        //{
        //    List<Categories> lst = new List<Categories>();

        //    SqlConnection cn = new SqlConnection();
        //    cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
        //    try
        //    {
        //        cn.Open();
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Connection = cn;
        //        cmd.CommandType = CommandType.Text;
        //        cmd.CommandText = "select * from Categories";

        //        SqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            Categories c = new Categories();
        //            c.Category = dr.GetInt32("Category");
        //            c.CategoryName = dr.GetString("CategoryName");
                   

        //            lst.Add(c);
        //        }
        //        dr.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        //Console.WriteLine(ex.Message);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        cn.Close();
        //    }

        //    return lst;
        //}

        public static List<SelectListItem> GetAllCategoriesSelectListItem()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Categories";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categories c = new Categories();
                    c.Category = dr.GetInt32("Category");
                    c.CategoryName = dr.GetString("CategoryName");
                    SelectListItem s = new SelectListItem(dr.GetString("CategoryName"),dr.GetInt32("Category").ToString());

                    lst.Add(s);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return lst;
        }

        public static List<Categories> GetAllCategories()
        {
            List<Categories> lst = new List<Categories>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Categories";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Categories c = new Categories();
                    c.Category = dr.GetInt32("Category");
                    c.CategoryName = dr.GetString("CategoryName");
                    

                    lst.Add(c);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                throw ex;
            }
            finally
            {
                cn.Close();
            }

            return lst;
        }
    }
}
