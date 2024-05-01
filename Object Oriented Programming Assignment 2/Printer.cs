using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Printer
    {
        /// <summary>
        /// Printer method that gives user information for the UI
        /// </summary>
        /// <param name="playerName"></param>
        public void RollEnter(string playerName)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{playerName} press enter to roll your dice: ");
            Console.WriteLine("------------------------------------------------");
            Console.ReadLine();
        }
        /// <summary>
        /// Printer for the main UI displaying the users option choices
        /// </summary>
        public  void UI()
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Welcome to the Dice Games");
            Console.WriteLine("Please Select Below");
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("1: Play Sevens Out");
            Console.WriteLine("2: Play Three or More");
            Console.WriteLine("3: Learn the Game Rules");
            Console.WriteLine("4: Add new Player");
            Console.WriteLine("5: View the Scoreboard");
            Console.WriteLine("6: Test Functionality of Games");
            Console.WriteLine("7: Clear Console");
            Console.WriteLine("8: Reset Scoreboard");
            Console.WriteLine("9: Exit Game");
            Console.WriteLine("------------------------------------------------");
        }
        /// <summary>
        /// Rules and instructions for how the game works
        /// </summary>
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
        /// <summary>
        /// Displays prompts for user to select from 
        /// </summary>
        public void TwoPlayerText() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Would You like to Play:");
            Console.WriteLine("1: Against the Computer");
            Console.WriteLine("2: Against another Player");
            Console.WriteLine("------------------------------------------------");
        }
        /// <summary>
        /// Option for user to confirm their choices made
        /// </summary>
        /// <returns>1 or 2 depending on user choice </returns>
        public int ConfirmChoice() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Are you sure you would like to proceed?");
            Console.WriteLine("1: Yes?");
            Console.WriteLine("2: No?");
            Console.WriteLine("------------------------------------------------");
            return OptionChoice(2);
        
        }
        /// <summary>
        /// 
        /// </summary>
        public void TestChoice() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Would You like to:");
            Console.WriteLine("1: Test the games");
            Console.WriteLine("2: View Previous test data");
            Console.WriteLine("3: Return to main menu");
            Console.WriteLine("------------------------------------------------");

        }
        /// <summary>
        /// DIsplays and prompts user for a name then formats string so first case is upper and rest is lower
        /// Checks whether name exists already within the player database
        /// </summary>
        /// <returns>formatted player name</returns>
        public string NewPlayerName() 
        {
            string newPlayerName;
            while(true) 
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Please enter Name:");
                Console.WriteLine("Enter \"Q\" to return to Main Menu ");
                Console.WriteLine("------------------------------------------------");
                while (true)
                {
                    newPlayerName = Console.ReadLine();
                    Console.WriteLine("------------------------------------------------");
                    if (string.IsNullOrEmpty(newPlayerName))
                    { 
                        Console.WriteLine("Please enter a name, dont leave field empty."); 
                        Console.WriteLine("------------------------------------------------");
                    }
                    else { break; }
                }
                newPlayerName = char.ToUpper(newPlayerName[0]) + newPlayerName.Substring(1).ToLower();
                switch (ConfirmChoice() )
                {
                    case 1:
                        return newPlayerName;
                    case 2:
                        continue;
                }   
            }
        }
        /// <summary>
        /// Prints players score
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="playerScore"></param>
        public void PrintScore(string playerName, int playerScore)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{playerName}'s Score: {playerScore}");
            Console.WriteLine("------------------------------------------------");

        }
        /// <summary>
        /// Prints unique rolls by player
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="uniqueRolls"></param>
        public void PrintUniqueRolls(string playerName, int uniqueRolls)
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"{playerName} has {uniqueRolls} unique rolls");
            Console.WriteLine("------------------------------------------------");
        }
        /// <summary>
        /// Method which is used throughout the code to confirm user chouses in a error handled way 
        /// </summary>
        /// <param name="optionLength"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Prints the players rolls in order and displays them
        /// </summary>
        /// <param name="rolls"></param>
        /// <param name="playerName"></param>
        public void PrintRolls(int[] rolls, string playerName) 
        {
            int counter = 1;
            Console.WriteLine(playerName);
            Console.WriteLine("------------------------------------------------");
            foreach (int roll in rolls) 
            {
                Console.WriteLine($"Roll {counter}: {roll}");
            }
            Console.WriteLine("------------------------------------------------");
        }
    }
}
