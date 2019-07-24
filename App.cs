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

            Console.WriteLine("Press any key to return to the main menu");
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
            Console.WriteLine("What is your name? ");
            newPerson.Name = Console.ReadLine();
            Console.WriteLine("How old are you? ");
            newPerson.Age = int.Parse(Console.ReadLine());
            Console.WriteLine("What is your gender? ");
            newPerson.Gender = Console.ReadLine();
            Console.WriteLine("What is yoour sexuality? ");
            newPerson.Sexuality = Console.ReadLine();
                        
            _dataAccess.AddPerson( newPerson);         

            ReturnToMenuAfterKeyPress("Person has been registered");
            //  _currentPage = MainMenu;
        }

        //public void PageAddTag()
        //{
        //    Header("AdderaTag");
        //    ShowAllBlogPostsBrief();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Tags");
        //    ShowAllTagsBrief();

        //    Write("Vad är taggen? ");
        //    string nyTag = Console.ReadLine();

        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

        //    _dataAccess.AddTag(nyTag);

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Taggen har lagts till");
        //    Console.ReadKey();
        //    _currentPage = Page.MainMenu;
        //}


        public void PageCreateQuestion()
        {
            UI.Header("Create new question");

            string questionText = UI.GetSQLValidString("Enter question text");

            Question newQuestion = new Question { Text = questionText };

            AddAnswersToQuestion(newQuestion);

        }

        private void AddAnswersToQuestion(Question newQuestion)
        {
            while (true)
            {
                UI.Header("Add answers");
                UI.WriteLine(newQuestion.ToString());

                string answerText = UI.GetSQLValidString("Add another answer? (enter blank to exit) ");

                if (answerText == "")
                {
                    if (newQuestion.Answers.Count < 2)
                    {

                    }

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

            WriteLine();
        }

        private void PageEndProgram()
        {
            Header("Avsluta");
            WriteLine("Tack för att du använt Bloggy");
            Console.ReadKey();
        }
        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }

        private void WriteLine(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }
    }
}
