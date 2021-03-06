﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo
{
    public class Question
    {
        public int Id { get; set; }
        public int Weight { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers = new List<Answer>();

        //public override string ToString()
        //{
        //    string output = $"Question: \n{Text}\n";
        //    return output;
        //}

        public string ShowAllAnswers()
        {
            string output = string.Join('\n', Answers.Select(x => x.ToString()));
            return output;
        }

        public override string ToString()
        {
            string QuestionInfo = "";
            QuestionInfo += Id.ToString().PadRight(8);
            QuestionInfo += Text.PadRight(10);
            return QuestionInfo;
        }
        public static string HeaderRow()
        {
            string headerText = "";
            headerText += "Id".PadRight(8);
            headerText += "Text".PadRight(10);
            return headerText;
        }


    }
}
