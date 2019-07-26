using DatingSite.Demo.Domain;
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
        public List<PersonAnswerForQuestion> PersonAnswers { get; set; } = new List<PersonAnswerForQuestion>();

        static int _columnWidth = 12;
        public override string ToString()
        {
            string personInfo = "";
            personInfo += Id.ToString().PadRight(_columnWidth);
            personInfo += Name.PadRight(_columnWidth);
            personInfo += Age.ToString().PadRight(_columnWidth);
            personInfo += Gender.PadRight(_columnWidth);
            personInfo += Sexuality.PadRight(_columnWidth);
            return personInfo;
        }
        public static string HeaderRow()
        {
            string headerText = "";
            headerText += "Id".PadRight(_columnWidth);
            headerText += "Name".PadRight(_columnWidth);
            headerText += "Age".PadRight(_columnWidth);
            headerText += "Gender".PadRight(_columnWidth);
            headerText += "Sexuality".PadRight(_columnWidth);
            return headerText;
        }
    }
}
