using System;
using System.Collections.Generic;
using System.Text;

namespace DatingSite.Demo.Domain
{
    public class UserAnswerForQuestion
    {
        public int QuestionId { get; set; }  // not done yet
        public int QuestionWeight { get; set; }
        public int GivenAnswerScore { get; set; }  
        public int DesiredAnswerScore{ get; set; }  
        public double Important { get; set; } // done
        public int GivenAnswerId { get; set; }  // done
        public int DesiredAnswerId { get; set; }   // done

    }
}
