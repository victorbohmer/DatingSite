using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo.Domain
{
    class Matching
    {
        readonly int baseScore = 10;
        public double YourMatchingScore { get; set; } = 0;
        public double TheirMatchingScore { get; set; } = 0;
        public string MatchName { get; set; }
        public Matching(Person personToMatch, Person matchablePerson)
        {
            MatchName = matchablePerson.Name;
            List<PersonAnswerForQuestion> matchAnswerList = matchablePerson.PersonAnswers;

            foreach (PersonAnswerForQuestion userAnswer in personToMatch.PersonAnswers)
            {
                try
                {
                    PersonAnswerForQuestion matchAnswer = matchAnswerList.Where(x => x.QuestionId == userAnswer.QuestionId).First();
                    YourMatchingScore += MatchingAdjustment(userAnswer, matchAnswer);
                    TheirMatchingScore += MatchingAdjustment(matchAnswer, userAnswer);
                }   
                catch
                {
                    //No score is added/subtracted if the matchable person hasn't answered the question
                }

            }
        }

        private double MatchingAdjustment(PersonAnswerForQuestion firstPersonAnswer, PersonAnswerForQuestion secondPersonAnswer)
        {
            return (baseScore - Math.Abs(firstPersonAnswer.DesiredAnswerScore - secondPersonAnswer.GivenAnswerScore)) * firstPersonAnswer.Important * firstPersonAnswer.QuestionWeight;
        }

    }
}
