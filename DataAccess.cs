using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatingSite.Demo
{
    public class DataAccess
    {
        private const string conString = "Server=(localdb)\\mssqllocaldb; Database=DatingSite";


        public List<Person> GetAllPersons()
        {
            var sql = @"SELECT [Id], [Name], [Age], [Gender], [Sexuality]
                        FROM Person";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Person>();

                while (reader.Read())
                {
                    var person = new Person
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Name = reader.GetSqlString(1).Value,
                        Age = reader.GetSqlInt32(2).Value,
                        Gender = reader.GetSqlString(3).Value,
                        Sexuality = reader.GetSqlString(4).Value
                    };
                    list.Add(person);
                }

                return list;

            }
        }



        //public List<BlogPost> GetAllBlogPostsBrief()
        //{
        //    var sql = @"SELECT [Id], [Author], [Title]
        //                FROM BlogPost";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();

        //        SqlDataReader reader = command.ExecuteReader();

        //        var list = new List<BlogPost>();

        //        while (reader.Read())
        //        {
        //            var bp = new BlogPost
        //            {
        //                Id = reader.GetSqlInt32(0).Value,
        //                Author = reader.GetSqlString(1).Value,
        //                Title = reader.GetSqlString(2).Value
        //            };
        //            list.Add(bp);
        //        }

        //        return list;

        //    }
        //}

        //public BlogPost GetPostById(int postId)
        //{
        //    var sql = @"SELECT [Id], [Author], [Title]
        //                FROM BlogPost 
        //                WHERE Id=@Id";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.Add(new SqlParameter("Id", postId));

        //        SqlDataReader reader = command.ExecuteReader();

        //        if (reader.Read())
        //        {
        //            var bp = new BlogPost
        //            {
        //                Id = reader.GetSqlInt32(0).Value,
        //                Author = reader.GetSqlString(1).Value,
        //                Title = reader.GetSqlString(2).Value
        //            };
        //            return bp;

        //        }

        //        return null;

        //    }
        //}

        //public void AddBlogpost(string title, string author)
        //{
        //    var sql = "INSERT INTO BlogPost (Author, Title) VALUES (@title,@author)";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //       // command.Parameters.Add(new SqlParameter("Id", id));
        //        command.Parameters.Add(new SqlParameter("Title", title));
        //        command.Parameters.Add(new SqlParameter("Author", author));
        //        command.ExecuteNonQuery();
        //    }
        //}

        //public void UpdateBlogpost(int id, string title)
        //{
        //    var sql = "UPDATE BlogPost SET Title=@Title WHERE Id=@id";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.Add(new SqlParameter("Id", id));
        //        command.Parameters.Add(new SqlParameter("Title", title));
        //        command.ExecuteNonQuery();
        //    }
        //}

        //public void DeleteBlogpost(int id)
        //{
        //    var sql = "DELETE FROM BlogPost WHERE ID=@id ";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.Add(new SqlParameter("Id", id));
        //        command.ExecuteNonQuery();
        //    }
        //}


        //public void AddTag(string tag)
        //{
        //    var sql = "INSERT INTO TagPost (Tag) VALUES (@tag)";

        //    using (SqlConnection connection = new SqlConnection(conString))
        //    using (SqlCommand command = new SqlCommand(sql, connection))
        //    {
        //        connection.Open();
        //        command.Parameters.Add(new SqlParameter("Tag", tag));
        //        command.ExecuteNonQuery();
        //    }
        //}

    }
}