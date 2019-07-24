namespace DatingSite.Demo
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public int Score { get; set; }
        public bool Important { get; set; }
    }
}
