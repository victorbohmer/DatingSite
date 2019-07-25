// Här struntas i validering (för att förenkla koden)

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
            Logo();
            UI.Write("Are you an admin or a user?\n(Eneter A for Admin or U for user)  ");
            string response = Console.ReadLine().Trim().ToLower();

            if (response == "A")
            {
                _menu.SetupAdminMenu(this);
            }
            else if (response == "B")
            {
                _menu.SetupAppMenu(this);
            }

            while (!_menu.Quit)
            {
                _menu.RefreshMenu();
            }
        }


        private void ReturnToMenuAfterKeyPress(string returnText)
        {
            Console.WriteLine(returnText);

            Console.WriteLine("\n Press any key to return to the main menu");
            Console.ReadKey();
            _menu.MainMenu();
        }
        public void PageMainMenu()
        {
            UI.Header("Main menu");
            ShowAllPersons();
            _menu.SwitchPageByUserInput();
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
            ShowAllPersons();
            string questionText = UI.GetSQLValidString("Enter question text: ");
            Question newQuestion = new Question { Text = questionText };
            AddAnswersToQuestion(newQuestion);

            _dataAccess.AddQuestion(newQuestion);
            _dataAccess.AddAnswers(newQuestion);

            ReturnToMenuAfterKeyPress("Question has been saved");
        }


        public void PageDeleteQuestion()
        {
            UI.Header("Delete a Question");
            ShowAllPersons();
            List<Question> list1 = _dataAccess.GetAllQuestions();
            List<Answer> list2 = _dataAccess.GetAllAnswers();
            List<Answer> answersToDelete = new List<Answer>();

            int id = UI.GetNumericInput("Write the question ID that you want to delete: ");
            var questionToDelete = list1.Where(x => x.Id == id).Single();
            answersToDelete = list2.Where(y => y.QuestionId == id).ToList();

            _dataAccess.DeleteQuestion(questionToDelete);
            for (int i = 0; i < answersToDelete.Count; i++)
            {
                _dataAccess.DeleteAnswer(answersToDelete[i]);
            }

            ReturnToMenuAfterKeyPress($"{questionToDelete.Text} has been deleted!");
        }

        public void PageCheckMatch()
        {
            UI.Header("See match with other users");
            List<Person> personList = _dataAccess.GetAllPersonsWithAnswers();
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

                string answerText = UI.GetSQLValidString("Add another answer? (Enter blank to exit) ");

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
            List<Question> QuestionList = _dataAccess.GetAllQuestions();
            //List<string> UserAnswerForQuestion = new List<string>();

            foreach (Question question in QuestionList)
            {
                List<Answer> validAnswers = answerList.Where(x => x.QuestionId == question.Id).ToList();
                for (int answerIndex = 0; answerIndex < validAnswers.Count; answerIndex++)
                {
                    UI.WriteLine($"{answerIndex + 1}: {validAnswers[answerIndex].Text}");
                }
                int userChoice = UI.GetNumericInput("Choose one of the following answers: ");

                //UserAnswerForQuestion.Add(userAnswer);
            }

            ReturnToMenuAfterKeyPress("Thank you for answering the questions!");
        }


        //public void PageDeletePost()
        //{
        //    Header("Radera");
        //    ShowAllBlogPostsBrief();
        //    ShowAllTagsBrief();

        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

        //    Write("Vilken bloggpost vill du radera? ");
        //    int postId = int.Parse(Console.ReadLine());

        //    Console.WriteLine("Du valde att radera den här posten");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    var post = list.Where(x => x.Id == postId).Single();
        //    Console.WriteLine($"{post.Id}  " + post.Title + post.Author);
        //    Console.ResetColor();

        //    _dataAccess.DeleteBlogpost(postId);

        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine("Den här posten finns inte längre! ");
        //    Console.ResetColor();

        //    Console.ReadKey();
        //    _currentPage = Page.MainMenu;
        //}



        //public void PageUpdatePost()
        //{
        //    Header("Uppdatera");
        //    ShowAllBlogPostsBrief();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Tags");
        //    ShowAllTagsBrief();
        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

        //    Write("Vilken bloggpost vill du uppdatera? ");
        //    int postId = int.Parse(Console.ReadLine());            

        //    Console.WriteLine("Du valde att uppdatera den här posten");
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    var post = list.Where(x => x.Id == postId).Single();
        //    Console.WriteLine($"{post.Id}  " + post.Title + post.Author);
        //    Console.ResetColor();

        //    Write("Uppdatera titeln? ");
        //    string nyTitle = Console.ReadLine();
        //    _dataAccess.UpdateBlogpost(postId, nyTitle);

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Raden har uppdaterat");
        //    Console.ResetColor();

        //    Console.ReadKey();
        //    _currentPage = Page.MainMenu;
        //}

        private void ShowAllPersons()
        {

            UI.WriteLine(Person.HeaderRow(), ConsoleColor.Blue);
            List<Person> personList = _dataAccess.GetAllPersons();

            foreach (Person person in personList)
            {
                Console.WriteLine(person.ToString());
            }
            UI.WriteLine();
        }

        public void Logo(int length = 25, int height = 12)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    if (i == 0)
                    {
                        if (j == 0 || j == height
                                || j == length - 1)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }

                    else if (i == height - 1)
                    {
                        Console.Write("*");
                    }

                    else if ((j < i || j > height - i) &&
                                    (j < height + i ||
                                    j >= length - i))
                        Console.Write("#");
                    else
                        Console.Write(" ");
                }

                Console.WriteLine();
            }

        }
    }
}
