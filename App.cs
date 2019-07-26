
using DatingSite.Demo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatingSite.Demo
{

    class App
    {
        DataAccess _dataAccess = new DataAccess();
        Menu _menu = new Menu();
        UserInterface UI = new UserInterface();

        public void Run()
        {
            _menu.SetupMainMenu(this);
            while (!_menu.Quit)
            {
                _menu.RefreshMenu();
            }
        }
        private void ReturnToMenuAfterKeyPress(string returnText)
        {
            Console.WriteLine(returnText);

            Console.WriteLine("\n Press any key to return to the menu");
            Console.ReadKey();
            _menu.GoToMenu();
        }

        public void PageMainMenu()
        {
            UI.WriteLogo();
            _menu.SwitchPageByUserInput();
        }
        public void PageAdminMenu()
        {
            UI.Header("Admin menu");
            ShowAllPersons();
            _menu.SwitchPageByUserInput();
        }
        public void PageUserMenu()
        {
            UI.Header("User menu");
            _menu.SwitchPageByUserInput();
        }
        public void PageSwitchToAdminMenu()
        {
            _menu.SetupAdminMenu(this);
        }
        public void PageSwitchToUserMenu()
        {
            _menu.SetupUserMenu(this);
        }
        public void PageSwitchToMainMenu()
        {
            _menu.SetupMainMenu(this);
        }
        public void PageAddPerson()
        {
            UI.Header("Add Person");

            ShowAllPersons();

            Person newPerson = new Person();

            Console.WriteLine();

            newPerson.Name = UI.GetSQLValidString("What is your name? ");
            newPerson.Age = UI.GetNumericInput("How old are you? ");
            newPerson.Gender = UI.GetSQLValidString("What is your gender? ");
            newPerson.Sexuality = UI.GetSQLValidString("What is your sexuality? ");

            _dataAccess.AddPerson(newPerson);

            ReturnToMenuAfterKeyPress($"{newPerson.Name} has been registred!");
        }


        public void PageDeletePerson()
        {
            UI.Header("Delete Person");
            ShowAllPersons();
            List<Person> list = _dataAccess.GetAllPersons();
            int id = UI.GetNumericInput("Write the person ID that you want to delete: ");
            var personToDelete = list.Where(x => x.Id == id).Single();
            _dataAccess.DeletePerson(personToDelete);

            ReturnToMenuAfterKeyPress($"{personToDelete.Name} has been deleted!");
        }

        public void PageCreateQuestion()
        {
            UI.Header("Create new question");
            ShowAllQuestions();
            string questionText = UI.GetSQLValidString("Enter question text: ");
            int questionWeight = UI.GetNumericInput("Enter the weight (importance) of the question");
            Question newQuestion = new Question { Text = questionText, Weight = questionWeight };
            AddAnswersToQuestion(newQuestion);

            _dataAccess.AddQuestion(newQuestion);
            _dataAccess.AddAnswers(newQuestion);

            ReturnToMenuAfterKeyPress("Question has been saved");
        }

        public void PageDeleteQuestion()
        {
            UI.Header("Delete a Question");
            ShowAllQuestions();
            List<Question> list1 = _dataAccess.GetAllQuestions();

            int id = UI.GetNumericInput("Write the question ID that you want to delete: ");
            var questionToDelete = list1.Where(x => x.Id == id).Single();

            _dataAccess.DeleteAnswer(questionToDelete);
            _dataAccess.DeleteQuestion(questionToDelete);
            ReturnToMenuAfterKeyPress($"{questionToDelete.Text} has been deleted!");
        }

        public void PageCheckMatch()
        {
            UI.Header("See match with other users");
            List<Person> personList = _dataAccess.GetAllPersonsWithAnswers();
            ShowAllPersons(personList);

            int idInput = UI.GetNumericInput("Enter the person Id you want to see matches for: ");

            Person personToMatch = personList.Where(x => x.Id == idInput).First();
            personList.Remove(personToMatch);

            MatchMaker matchMaker = new MatchMaker();
            matchMaker.CalculateMatches(personList, personToMatch);

            matchMaker.PrintMatches(UI);

            ReturnToMenuAfterKeyPress("");
        }

        private void AddAnswersToQuestion(Question newQuestion)
        {
            while (true)
            {
                UI.Header("Add answers");
                UI.WriteLine(newQuestion.ToString());
                UI.WriteLine(Answer.HeaderRow(), ConsoleColor.Blue);
                UI.WriteLine(newQuestion.ShowAllAnswers());
                UI.WriteLine();

                string answerText = UI.GetSQLValidString("New answer: (Enter blank to exit) ");

                if (answerText == "")
                {
                    if (newQuestion.Answers.Count < 2)
                        UI.Write("Questions must have at least two answers", ConsoleColor.Red);
                    else
                        break;
                }
                else
                {
                    int answerScore = UI.GetNumericInput("Enter the weighted score of the answer (It is used for comparing when calculating match%): ");
                    Answer newAnswer = new Answer { Text = answerText, Score = answerScore };
                    newQuestion.Answers.Add(newAnswer);
                }
            }
        }

        public void PageAnswerQuestions()
        {
            UI.Header("Answer Questions");
            List<Answer> answerList = _dataAccess.GetAllAnswers();
            List<Question> questionList = _dataAccess.GetAllQuestions();


            Person answeringPerson = GetExistingPerson("Enter your Id if you already have an account: ");
            UI.WriteLine($"Hi {answeringPerson.Name}!");

            List<PersonAnswerForQuestion> userAnswerList = new List<PersonAnswerForQuestion>();
            foreach (Question question in questionList)
            {
                userAnswerList.Add(GetPersonAnswer(question, answerList));
            }

            _dataAccess.AddPersonAnswers(answeringPerson.Id, userAnswerList);

            ReturnToMenuAfterKeyPress($"Thank you for answering the questions, {answeringPerson.Name}!");
        }

        private PersonAnswerForQuestion GetPersonAnswer(Question question, List<Answer> answerList)
        {
            UI.WriteLine($"{question.Text}");
            List<Answer> validAnswers = answerList.Where(x => x.QuestionId == question.Id).ToList();
            for (int answerIndex = 0; answerIndex < validAnswers.Count; answerIndex++)
            {
                UI.WriteLine($"{answerIndex + 1}: {validAnswers[answerIndex].Text}");
            }
            PersonAnswerForQuestion newPersonAnswer = new PersonAnswerForQuestion { QuestionId = question.Id };

            int givenAnswerIndex = UI.GetNumericInput("Enter your answer here: ") - 1;
            newPersonAnswer.GivenAnswerId = validAnswers[givenAnswerIndex].Id;

            int desiredAnswerIndex = UI.GetNumericInput("Enter your dream partner answer here: ") - 1;
            newPersonAnswer.DesiredAnswerId = validAnswers[desiredAnswerIndex].Id;

            char response = char.Parse(UI.GetSQLValidString("Choose how important your dream partner answer to you: " +
                                                            "\n1: Very important" +
                                                            "\n2: Somewhat important\n" +
                                                            "\n3: Not important at all"
                                                            ).ToUpper());
            switch (response)
            {
                case '1':
                    newPersonAnswer.Important = 1.00;
                    break;
                case '2':
                    newPersonAnswer.Important = 0.50;
                    break;
                case '3':
                    newPersonAnswer.Important = 0;
                    break;
                default:
                    newPersonAnswer.Important = 0.50;
                    break;
            }
            return newPersonAnswer;

        }

        private void ShowAllPersons(List<Person> personList = null)
        {
            if (personList is null)
                personList = _dataAccess.GetAllPersons();

            UI.WriteLine(Person.HeaderRow(), ConsoleColor.Blue);
            foreach (Person person in personList)
            {
                Console.WriteLine(person.ToString());
            }
            UI.WriteLine();
        }


        private void ShowAllQuestions()
        {

            UI.WriteLine(Question.HeaderRow(), ConsoleColor.Blue);
            List<Question> questionList = _dataAccess.GetAllQuestions();

            foreach (Question question in questionList)
            {
                Console.WriteLine(question.ToString());
            }
            UI.WriteLine();
        }

        private Person GetExistingPerson(string prompt)
        {
            List<Person> personList = _dataAccess.GetAllPersons();

            while (true)
            {
                int personIdToCheck = UI.GetNumericInput(prompt);
                try
                {
                    return personList.Where(x => x.Id == personIdToCheck).First();
                }
                catch
                {
                    UI.WriteLine($"We could not find your id {personIdToCheck}!", ConsoleColor.Red);
                }
            }
        }


        //private string CheckExistingPerson(string prompt)
        //{
        //    List<Person> personList = _dataAccess.GetAllPersons();
        //    string personToCheck = "";
        //    while (true)
        //    {
        //        try
        //        {
        //            personToCheck = UI.GetSQLValidString(prompt).Trim().ToLower();
        //            personList.Where(x => x.Name == personToCheck).First();
        //            UI.WriteLine($"We found your name {personToCheck}!", ConsoleColor.Red);
        //        }
        //        catch (Exception)
        //        {

        //            UI.WriteLine($"We found your name {personToCheck}!", ConsoleColor.Red);
        //        }
              
        //    }
        //}

    }
}
