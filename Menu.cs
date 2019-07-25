using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatingSite.Demo
{

    class Menu
    {
        private List<MenuOption> menuOptions = new List<MenuOption>();
        private ConsoleKey currentPageKey;
        public bool Quit = false;

        class MenuOption
        {
            public Action AppMethod { get; }
            public ConsoleKey Key { get; }
            public string Prompt { get; }
            public MenuOption(Action appMethod, ConsoleKey key, string prompt)
            {
                AppMethod = appMethod;
                Key = key;
                Prompt = prompt;
            }
        }

        public void SetupMainMenu (App app)
        {
            currentPageKey = ConsoleKey.F5;
            menuOptions.Clear();
            NewMenuOption(app.PageMainMenu, ConsoleKey.F5, "F5) Refresh menu");
            NewMenuOption(app.PageSwitchToAdminMenu, ConsoleKey.A, "A) Log in as admin");
            NewMenuOption(app.PageSwitchToUserMenu, ConsoleKey.U, "U) Log in as user");
        }

        public void SetupAdminMenu(App app)
        {
            currentPageKey = ConsoleKey.F5;
            menuOptions.Clear();
            NewMenuOption(app.PageAdminMenu, ConsoleKey.F5, "F5) Refresh menu");
            NewMenuOption(app.PageSwitchToMainMenu, ConsoleKey.B, "B) Back to main menu");
            NewMenuOption(app.PageCreateQuestion, ConsoleKey.C, "C) Create a question");
            NewMenuOption(app.PageDeleteQuestion, ConsoleKey.D, "D) Delete a question");           
        }
        
            public void SetupUserMenu(App app)
        {
            currentPageKey = ConsoleKey.F5;
            menuOptions.Clear();
            NewMenuOption(app.PageUserMenu, ConsoleKey.F5, "F5) Refresh menu");
            NewMenuOption(app.PageSwitchToMainMenu, ConsoleKey.B, "B) Back to main menu");
            NewMenuOption(app.PageAnswerQuestions, ConsoleKey.A, "A) Answer questions");
            NewMenuOption(app.PageAddPerson, ConsoleKey.R, "R) Register an account");         
            NewMenuOption(app.PageDeletePerson, ConsoleKey.D, "D) Delete an account"); 
            NewMenuOption(app.PageCheckMatch, ConsoleKey.V, "V) View matching");
            
        }
        private void NewMenuOption(Action menuAction, ConsoleKey hotkey, string prompt)
        {
            menuOptions.Add(new MenuOption(menuAction, hotkey, prompt));
        }
        public void SwitchPageByUserInput()
        {
            Console.WriteLine("What do you want to do?");
            menuOptions.ForEach(x => Console.WriteLine(x.Prompt));

            ConsoleKey keyInput = Console.ReadKey().Key;

            if (menuOptions.Select(x => x.Key).Contains(keyInput))
                currentPageKey = keyInput;
            else
                currentPageKey = ConsoleKey.F5;
        }

        public void GoToMenu()
        {
            currentPageKey = ConsoleKey.F5;
        }
        public void RefreshMenu()
        {
            Action menuAction = menuOptions.Where(x => x.Key == currentPageKey).First().AppMethod;
            menuAction();
        }
    }
}
