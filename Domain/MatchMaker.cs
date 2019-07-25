using DatingSite.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo
{
    public class MatchMaker
    {
        List<Matching> matchList = new List<Matching>();
        string personName;
        public double MaxYourMatchingScore { get; private set; }
        public double MaxTheirMatchingScore { get; private set; }

        public void CalculateMatches (List<Person> matchablePersonList, Person personToMatch)
        {
            personName = personToMatch.Name;
            foreach (Person matchablePerson in matchablePersonList)
            {

                matchList.Add(new Matching(personToMatch, matchablePerson));

            }
            MaxYourMatchingScore = matchList.Select(x => x.YourMatchingScore).Max();
            MaxTheirMatchingScore = matchList.Select(x => x.TheirMatchingScore).Max();

        }
        public void PrintMatches(UserInterface UI)
        {
            UI.Header($"Showing matches for {personName}");

            string headerText = "Name".PadRight(8) + "Your match%".PadRight(12) + "Their match".PadRight(12);
            UI.WriteLine(headerText);
            foreach (Matching match in matchList.OrderByDescending(x => x.YourMatchingScore))
            {
                string matchInfo = "";
                matchInfo += match.MatchName.PadRight(8);
                matchInfo += (match.YourMatchingScore / MaxYourMatchingScore).ToString().PadRight(12);
                matchInfo += (match.TheirMatchingScore / MaxTheirMatchingScore).ToString().PadRight(12);
                UI.WriteLine(matchInfo, ConsoleColor.Magenta);
            }
            UI.WriteLine();
        }

    }
}
