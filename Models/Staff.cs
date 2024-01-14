using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LibraryManagement.Models
{
    public class Staff
    {
        public string Email { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Phone { get; set; }

        public static void AddStaff(Staff obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.CommandText = "AddStaff";
                cmdInsert.Parameters.AddWithValue("@Email", obj.Email);
                cmdInsert.Parameters.AddWithValue("@Name", obj.Name);
                cmdInsert.Parameters.AddWithValue("@Password", obj.Password);
                cmdInsert.Parameters.AddWithValue("@Phone", obj.Phone);


                cmdInsert.ExecuteNonQuery();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
        }

        public static Staff ValidateStaff(Staff obj)
        {
            Staff s = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Staff where Email=@Email and Password=@Password";
                cmd.Parameters.AddWithValue("@Email", obj.Email);
                //cmd.Parameters.AddWithValue("@Name", obj.Name);
                cmd.Parameters.AddWithValue("@Password", obj.Password);
                //cmd.Parameters.AddWithValue("@Phone", obj.Phone);


                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    s=new Staff();
                    s.Name=dr.GetString("Name");
                    s.Email = dr.GetString("Email");
                    s.Password = dr.GetString("Password");
                    s.Phone = dr.GetString("Phone");
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cn.Close();
            }
            return s;
        }
    }
}
