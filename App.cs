// Här struntas i validering (för att förenkla koden)

using System;
using System.Collections.Generic;
using System.Linq;

namespace DatingSite.Demo
{

    class App
    {
        DataAccess _dataAccess = new DataAccess();
        Page _currentPage = Page.MainMenu;

        public void Run()
        {
            while (true)
            {
                switch (_currentPage)
                {
                    case Page.MainMenu:
                        PageMainMenu();
                        break;
                    case Page.EndProgram:
                        PageEndProgram();
                        return;
                }
            }
        }

        private void PageMainMenu()
        {
            Header("Huvudmeny");
            ShowAllUsersAnswersBrief();

            WriteLine("Vad vill du göra?");
            WriteLine("a) Gå till huvudmenyn");;
            WriteLine("C) Avsluta programmet");
            

            while (true)
            {
                ConsoleKey command = Console.ReadKey(true).Key;

                switch (command)
                {
                    case ConsoleKey.A:
                        _currentPage = Page.MainMenu;
                        return;
                    case ConsoleKey.B:
                        _currentPage = Page.AnswerQuestions;
                        return;
                    case ConsoleKey.C:
                        _currentPage = Page.EndProgram;
                        return;
                }
            }
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


        //public void PageAddPost()
        //{
        //    Header("Addera");
        //    ShowAllBlogPostsBrief();
        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Tags");
        //    ShowAllTagsBrief();

        //    Write("Vad är titeln? ");
        //    string nyTitle = Console.ReadLine();
        //    Write("Vem är authorn? ");
        //    string nyAuthor = Console.ReadLine();


        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();
            
        //    _dataAccess.AddBlogpost(nyTitle, nyAuthor);

        //    Console.ForegroundColor = ConsoleColor.Green;
        //    Console.WriteLine("Raden har lagts till");
        //    Console.ReadKey();
        //    _currentPage = Page.MainMenu;
        //}

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

        private void PageEndProgram()
        {
            Header("Avsluta");
            WriteLine("Tack för att du använt Bloggy");
            Console.ReadKey();
        }

        //private void ShowAllBlogPostsBrief()
        //{
        //    List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

        //    foreach (BlogPost bp in list)
        //    {
        //        WriteLine(bp.Id.ToString().PadRight(5) + bp.Title.PadRight(30) + bp.Author.PadRight(20));
        //    }

        //    WriteLine();
        //}


        private void ShowAllUsersAnswersBrief()
        {
            List<UsersAnswers> list = _dataAccess.GetAllAnswersBrief();

            foreach (UsersAnswers answer in list)
            {
                WriteLine(answer.Id.ToString().PadRight(8) + answer.Answer1.PadRight(10) + answer.Answer2.PadRight(10) + answer.Answer3.PadRight(10) + answer.Answer4.PadRight(10) + answer.Answer5.PadRight(0));            }

            WriteLine();
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

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }
}
