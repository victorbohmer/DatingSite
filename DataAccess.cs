using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatingSite.Demo
{
    public class DataAccess
    {
        private const string conString = "Server=(localdb)\\mssqllocaldb; Database=DatingSite";


        public List<UsersAnswers> GetAllAnswersBrief()
        {
            var sql = @"SELECT [Id], [Answer1], [Answer2], [Answer3], [Answer4], [Answer5]
                        FROM UsersAnswers";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<UsersAnswers>();

                while (reader.Read())
                {
                    var answer = new UsersAnswers
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Answer1 = reader.GetSqlString(1).Value,
                        Answer2 = reader.GetSqlString(2).Value,
                        Answer3 = reader.GetSqlString(3).Value,
                        Answer4 = reader.GetSqlString(4).Value,
                        Answer5 = reader.GetSqlString(5).Value,
                    };
                    list.Add(answer);
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
