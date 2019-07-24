using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers = new List<Answer>();

        public override string ToString()
        {
            string output = $"Question: \n{Text}\n";
            return output;
        }

        public string ShowAllAnswers()
        {
            string output = string.Join('\n', Answers.Select(x => x.ToString()));
            return output;
        }

    }
}
