using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatingSite.Demo
{
    public class DataAccess
    {
        private const string conString = "Server=(localdb)\\mssqllocaldb; Database=DatingSite";

        
        public List<Question> GetAllQuestions()
        {
            var sql = @"SELECT [Id], [Text]
                        FROM Question";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Question>();

                while (reader.Read())
                {
                    var Question = new Question
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        Text = reader.GetSqlString(2).Value,            
                    };
                    list.Add(Question);
                }
                return list;
            }
        }


        public List<Answer> GetAllAnswers()
        {
            var sql = @"SELECT [Id], [QuestionId], [Text], [Score]
                        FROM Answer";

            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                var list = new List<Answer>();

                while (reader.Read())
                {
                    var Answer = new Answer
                    {
                        Id = reader.GetSqlInt32(0).Value,
                        QuestionId = reader.GetSqlInt32(1).Value,
                        Text = reader.GetSqlString(2).Value,
                        Score = reader.GetSqlInt32(3).Value                        
                    };
                    list.Add(Answer);
                }

                return list;
            }
        }


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

        public void AddPerson(Person newPerson)
        {
            var sql = "INSERT INTO Person (Name, Age, Gender, Sexuality) VALUES (@Name, @Age, @Gender, @Sexuality)";

            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("Name", newPerson.Name));
            parameterList.Add(new SqlParameter("Age", newPerson.Age));
            parameterList.Add(new SqlParameter("Gender", newPerson.Gender));
            parameterList.Add(new SqlParameter("Sexuality", newPerson.Sexuality));

            ExecuteSql(sql, parameterList);
        }

        public void DeletePerson(Person oldPerson)
        {
            var sql = "DELETE FROM Person WHERE Id=@Id ";
            List<SqlParameter> parameterList = new List<SqlParameter>();
            parameterList.Add(new SqlParameter("Id", oldPerson.Id));

            ExecuteSql(sql, parameterList);
        }


        public void ExecuteSql(string sql, List<SqlParameter> parameterList)
        {
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                foreach (SqlParameter parameter in parameterList)
                {
                    command.Parameters.Add(parameter);
                }
                command.ExecuteNonQuery();
            }
        }

        public int ExecuteSqlAndReturnAffectedId(string sql, List<SqlParameter> parameterList)
        {
            int output;
            using (SqlConnection connection = new SqlConnection(conString))
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                connection.Open();
                foreach (SqlParameter parameter in parameterList)
                {
                    command.Parameters.Add(parameter);
                }
                output = (int)command.ExecuteScalar();
            }
            return output;
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