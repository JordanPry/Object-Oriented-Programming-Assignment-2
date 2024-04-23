using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Printer
    {
        public void RollEnter(int playerNum)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"Player {playerNum} Enter to roll your dice: ");
            Console.WriteLine("------------------------------------------------");
            Console.ReadLine();
        }
        public  void UI()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Welcome to the Dice Games");
            Console.WriteLine("Please Select Below");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("1: Play Sevens Out");
            Console.WriteLine("2: Play Three or More");
            Console.WriteLine("3: Learn the Game Rules");
            Console.WriteLine("4: View the Scoreboard");
            Console.WriteLine("5: Test Functionality of Games");
            Console.WriteLine("6: Clear Console");
            Console.WriteLine("7: Reset Scoreboard");
            Console.WriteLine("8: Exit Game");
            Console.WriteLine("------------------------------------------------");
        }
        public void PrintRules() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Sevens Out:");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Roll two dice");
            Console.WriteLine("If it is a 7 - Game Ends");
            Console.WriteLine("Any other total is added to your score");
            Console.WriteLine("If a double is rolled;\ndouble the total is added to your score");
            Console.WriteLine("Highest Score Wins");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Three or More: ");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Roll 5 dice");
            Console.WriteLine("If 2-of-a-kind is rolled\nPlayer may choose to rethrow all;\nOr the remaining dice.");
            Console.WriteLine("3-of-a-kind: 3 points");
            Console.WriteLine("4-of-a-kind: 6 points");
            Console.WriteLine("5-of-a-kind: 12 points");
            Console.WriteLine("First to 20 Points Wins");
            Console.WriteLine("------------------------------------------------");

        }
        public void TwoPlayerText() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Would You like to Play:");
            Console.WriteLine("1: Against the Computer");
            Console.WriteLine("2: Against another Player");
            Console.WriteLine("------------------------------------------------");
        }
        public int ConfirmChoice() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Are you sure you would like to proceed?");
            Console.WriteLine("1: Yes?");
            Console.WriteLine("2: No?");
            Console.WriteLine("------------------------------------------------");
            return OptionChoice(2);
        
        }
        public int OptionChoice(int optionLength)
        {
            int userChoice;
            while (true)
            {
                if (!int.TryParse(Console.ReadLine(), out userChoice))
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("Please Enter only Integers");
                    Console.WriteLine("------------------------------------------------");
                    continue;
                }
                if (userChoice > 0 && userChoice <= optionLength) { return userChoice; }
                else
                {
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine($"Please enter an integer between 1-{optionLength}.");
                    Console.WriteLine("------------------------------------------------");
                }

            }
        }
    }
}
