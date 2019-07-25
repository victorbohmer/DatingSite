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

            foreach (Matching match in matchList)
            {
                UI.WriteLine(match.ToString(), ConsoleColor.Magenta);
            }
            UI.WriteLine();
        }

    }
}
