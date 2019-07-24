using System;
using System.Collections.Generic;
using System.Text;

namespace DatingSite.Demo
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sexuality { get; set; }
        public string Gender { get; set; }
        public List<Answer> AnswerList { get; set; } = new List<Answer>();
        public override string ToString()
        {
            string personInfo = "";
            personInfo += Id.ToString().PadRight(8);
            personInfo += Name.PadRight(10);
            personInfo += Age.ToString().PadRight(10);
            personInfo += Gender.PadRight(10);
            personInfo += Sexuality.PadRight(10);
            return personInfo;
        }
        public static string HeaderRow()
        {
            string headerText = "";
            headerText += "Id".PadRight(8);
            headerText += "Name".PadRight(10);
            headerText += "Age".PadRight(10);
            headerText += "Gender".PadRight(10);
            headerText += "Sexuality".PadRight(10);
            return headerText;
        }

    }
}
