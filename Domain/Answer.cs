namespace DatingSite.Demo
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public bool Important { get; set; }

        public override string ToString()
        {
            string output = "";
            output += Text.PadRight(30);
            output += Score;
            return output;
        }

        public static string HeaderRow()
        {
            string output = "";
            output += "Answer".PadRight(30);
            output += "Score";
            return output;
        }

    }
}
