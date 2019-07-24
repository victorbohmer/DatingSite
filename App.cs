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
            _menu.SetupAppMenu(this);
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

            _dataAccess.AddPerson( newPerson);         

            ReturnToMenuAfterKeyPress($"{newPerson.Name.ToUpper()} HAS BEEN REGISTERED!");            
        }


        public void PageDeletePerson()
        {
            UI.Header("Delete Person");
            ShowAllPersons();
            List<Person> list = _dataAccess.GetAllPersons();
            int id = UI.GetNumericInput("Write the person ID that you want to delete: ");
            var personToDelete = list.Where(x => x.Id == id).Single();
            _dataAccess.DeletePerson(personToDelete);

            ReturnToMenuAfterKeyPress($"{personToDelete.Name.ToUpper()} HAS BEEN DELETED.");
        }

        public void PageCreateQuestion()
        {
            UI.Header("Create new question");

            string questionText = UI.GetSQLValidString("Enter question text: ");

            Question newQuestion = new Question { Text = questionText };

            AddAnswersToQuestion(newQuestion);

            _dataAccess.AddQuestion(newQuestion);
            _dataAccess.AddAnswers(newQuestion);

            ReturnToMenuAfterKeyPress("Question has been saved");

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

                string answerText = UI.GetSQLValidString("Add another answer? (enter blank to exit) ");

                if (answerText == "")
                {
                    if (newQuestion.Answers.Count < 2)
                        UI.Write("Questions must have at least two answers", ConsoleColor.Red);
                    else
                        break;
                }
                else
                {
                    int answerScore = UI.GetNumericInput("Enter the weighted score of the answer (used to compare it when calculating match%): ");
                    Answer newAnswer = new Answer { Text = answerText, Score = answerScore };
                    newQuestion.Answers.Add(newAnswer);
                }

            }
        }

        //public void PageDeletePost()
        //{
        //    Header("Radera");
        //    ShowAllBlogPostsBrief();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Tags");
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



        //private void ShowAllBlogPostsBrief()
        //{
        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

        //    foreach (BlogPost bp in list)
        //    {
        //        WriteLine(bp.Id.ToString().PadRight(5) + bp.Title.PadRight(30) + bp.Author.PadRight(20));
        //    }

        //    WriteLine();
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

        private void PageEndProgram()
        {
            //Header("Avsluta");
            //WriteLine("Tack för att du använt Bloggy");
            Console.ReadKey();
        }

        

    }
}
