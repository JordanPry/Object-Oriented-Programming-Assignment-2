using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Program 
    {
        /// <summary>
        /// Main UI of the screen from here all choices are displayed 
        /// depending on user choice proceeds to the appropriate methods
        /// </summary>
        public static void StartScreen()
        {
            StatsJSON stats = new StatsJSON();
            Game.Sevens sevens = new Game.Sevens();
            Game.ThreeOrMore threes = new Game.ThreeOrMore();
            Printer printer = new Printer();
            printer.UI();
            int choice = printer.OptionChoice(9);
            switch (choice)
            {
                case 1:
                    sevens.GameStart(printer, stats);
                    break;
                case 2:
                    threes.GameStart(printer, stats);
                    break;
                case 3:
                    printer.PrintRules();
                    break;
                case 4:
                    stats.AddPlayer(printer.NewPlayerName());
                    break;
                case 5:
                    stats.PrintStats();
                    break;
                case 6:
                    Console.WriteLine("TESTING CLASS NEEDS IMPLIMENTING");
                    break;
                case 7:
                    Console.Clear();
                    break;
                case 8:
                    stats.ResetStats();
                    break;
                case 9:
                    bool exitCode = printer.ConfirmChoice() == 1 ? true : false;
                    if (exitCode) { Environment.Exit(0); }
                    break;
            }
        }
        /// <summary>
        /// Main entry point of the application, will continously run the program until the exit code is entered
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            while (true) {StartScreen(); }
        }
    }
}
