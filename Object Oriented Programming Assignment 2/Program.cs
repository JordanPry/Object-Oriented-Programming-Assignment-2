using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Program
    {
        public static void StartScreen()
        {
            StatsJSON stats = new StatsJSON();
            Game.Sevens sevens = new Game.Sevens();
            Printer printer = new Printer();
            printer.UI();
            int choice = printer.OptionChoice(8);
            switch (choice)
            {
                case 1:
                    sevens.GameStart();
                    break;
                case 2:
                    sevens.GameStart();
                    break;
                case 3:
                    printer.PrintRules();
                    break;
                case 4:
                    stats.PrintStats();
                    break;
                case 5:
                    Console.WriteLine("TESTING CLASS NEEDS IMPLIMENTING");
                    break;
                case 6:
                    Console.Clear();
                    break;
                case 7:
                    stats.ResetStats();
                    break;
                case 8:
                    bool exitCode = printer.ConfirmChoice() == 1 ? true : false;
                    if (exitCode) { Environment.Exit(0); }
                    break;
            }
        }
        static void Main(string[] args)
        {
            while (true) {StartScreen(); }
            
        }
    }
}
