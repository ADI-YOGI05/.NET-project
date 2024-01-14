using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryManagement.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public Categories Category { get; set; }

        public static void AddBook(Books obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.CommandText = "AddBook";
                cmdInsert.Parameters.AddWithValue("@BookId", obj.BookId);
                cmdInsert.Parameters.AddWithValue("@Title", obj.Title);
                cmdInsert.Parameters.AddWithValue("@Author", obj.Author);
                cmdInsert.Parameters.AddWithValue("@Category", obj.Category.Category);


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
        public static List<Books> GetAllBooks()
        {
            List<Books> books = new List<Books>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Books inner join Categories on Books.Category = Categories.Category";

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Books book = new Books();
                    book.BookId = dr.GetInt32("BookId");
                    book.Title = dr.GetString("Title");
                    book.Author = dr.GetString("Author");
                    book.Category = new Categories();
                    book.Category.Category = dr.GetInt32("Category");
                    book.Category.CategoryName = dr.GetString("CategoryName");


                    books.Add(book);
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

            return books;
        }

        public static Books GetSingleBook(int id)
        {
            Books book = null;
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Books inner join Categories on Books.Category = Categories.Category where BookId = @BookId";
                cmd.Parameters.AddWithValue("BookId", id);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    book = new Books();
                    book.BookId = dr.GetInt32("BookId");
                    book.Title = dr.GetString("Title");
                    book.Author = dr.GetString("Author");
                    book.Category = new Categories();
                    book.Category.Category = dr.GetInt32("Category");
                    book.Category.CategoryName = dr.GetString("CategoryName");

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

            return book;
        }
        public static void UpdateWithStoredProcedure(Books obj)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.CommandText = "UpdateBook";
                cmdInsert.Parameters.AddWithValue("@BookId", obj.BookId);
                cmdInsert.Parameters.AddWithValue("@Title", obj.Title);
                cmdInsert.Parameters.AddWithValue("@Author", obj.Author);
                cmdInsert.Parameters.AddWithValue("@Category", obj.Category.Category);


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
        public static void DeleteWithStoredProcedure(int BookId)
        {
            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmdInsert = new SqlCommand();
                cmdInsert.Connection = cn;
                cmdInsert.CommandType = CommandType.StoredProcedure;
                cmdInsert.CommandText = "DeleteBook";
                cmdInsert.Parameters.AddWithValue("@BookId", BookId);


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
        public static List<Books> GetAllBooksByCategory(int cat)
        {
            List<Books> books = new List<Books>();

            SqlConnection cn = new SqlConnection();
            cn.ConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LibraryManagement;Integrated Security=True;";
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from Books  inner join Categories on Books.Category = Categories.Category where Books.Category=@Cat";
                cmd.Parameters.AddWithValue("Cat", cat);
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Books book = new Books();
                    book.BookId = dr.GetInt32("BookId");
                    book.Title = dr.GetString("Title");
                    book.Author = dr.GetString("Author");
                    book.Category = new Categories();
                    book.Category.Category = dr.GetInt32("Category");
                    book.Category.CategoryName = dr.GetString("CategoryName");


                    books.Add(book);
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

            return books;
        }

    }
}
