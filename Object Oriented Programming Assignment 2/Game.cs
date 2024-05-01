using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Game : Printer
    {
        /// <summary>
        /// Void method that calls the stats class from the object "stats" 
        /// parameters passed are used to determine how the stats class will update and save the player stats
        /// </summary>
        /// <param name="isDraw"></param>
        /// <param name="gameWinner"></param>
        /// <param name="gameLoser"></param>
        /// <param name="gameType"></param>
        public void EndGame(bool isDraw, string gameWinner, string gameLoser, string gameType)
        {
            StatsJSON stats = new StatsJSON();
            if (isDraw)
            {
                Console.WriteLine("Game Ends in a draw....");
                stats.UpdateStats(gameWinner, gameType, false);
                stats.UpdateStats(gameLoser, gameType, false);
            }
            else
            {
                Console.WriteLine($"{gameWinner} WINS!!!!!");
                stats.UpdateStats(gameWinner, gameType, true);
                stats.UpdateStats(gameLoser, gameType, false);
            }
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("Game Over Returning to Start Screen");
            Console.WriteLine("------------------------------------------------");
        }
        /// <summary>
        /// Method of the "Game" class to be used for both child classes to determine 1 or 2 players
        /// </summary>
        /// <returns>
        /// True if user answer == 1
        /// False if user answer == 2
        /// </returns>
        public bool isTwoPlayer() 
        {
            TwoPlayerText();
            bool twoPlayer = OptionChoice(2) == 1 ? false : true;
            return twoPlayer;
        }
        public class ThreeOrMore : Dice
        {
            /// <summary>
            /// creates an int array which contains 5 numbers between 1-6
            /// uses the newRoll method from the inherited dice class to get a new integer
            /// </summary>
            /// <returns></returns>
            public int[] Roll5Dice() 
            {
                int roll;
                int[] rolls = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    newRoll();
                    roll = Roll;
                    rolls[i] = roll;
                }
                return rolls;
            }
            /// <summary>
            /// Method to calculate unique amount of rolls within a users total rolls
            /// uses a list and if number is not found within list is added
            /// </summary>
            /// <param name="rolls"></param>
            /// <returns> the total amount of items within the list which is the unique amount of rolls </returns>
            public int OfAKind(int[] rolls) 
            {
                int total;
                List<int> uAmount = new List<int>();
                foreach (int roll in rolls) 
                {
                    if (!uAmount.Contains(roll)) 
                    {
                        uAmount.Add(roll);
                    }
                }
                total = uAmount.Count;
                return total;
            }
            /// <summary>
            /// Method to determine how much to add to playrs score
            /// </summary>
            /// <param name="amount"></param>
            /// <returns>int correspending to number</returns>
            public int ScoreAdd(int amount) 
            {
                switch (amount) 
                {
                    case 3:
                        return 3;
                    case 4:
                        return 6;
                    case 5:
                        return 12;
                    default: 
                        return 0;
                }
            }
            /// <summary>
            /// Simple booleon to ask user if theyre would like to roll again
            /// </summary>
            /// <param name="playerName"></param>
            /// <param name="printer"></param>
            /// <returns>
            /// True if user enters 1
            /// False if user enters 2
            /// </returns>
            bool RollAgain(string playerName, Printer printer) 
            {
                Console.WriteLine($"{playerName} Would you like to roll again?");
                return printer.OptionChoice(2) == 1;
            
            }
            /// <summary>
            /// Method to test whether the game has ended
            /// </summary>
            /// <param name="game"></param>
            /// <param name="player1Name"></param>
            /// <param name="opName"></param>
            /// <param name="player1Total"></param>
            /// <param name="player2Total"></param>
            /// <returns>
            /// false if the game logic has been completed
            /// true if game still needs to be played
            /// </returns>
            public bool IsGameOver(Game game, string player1Name, string opName, int player1Total, int player2Total) 
            {
                if (player1Total >= 20)
                {
                    game.EndGame(false, player1Name, opName, "Threes");
                    return false;
                }
                else if (player2Total >= 20)
                {
                    game.EndGame(false, opName, player1Name, "Threes");
                    return false;
                }
                return true;

            }
            /// <summary>
            /// Method that handles all the logic for the game implimentation
            /// </summary>
            /// <param name="printer"></param>
            /// <param name="stats"></param>
            public void GameStart(Printer printer, StatsJSON stats) 
            {
                Game game = new Game();
                bool twoPlayer = game.isTwoPlayer();
                string player1Name = stats.LoadPlayer();
                if (player1Name == "Q") { return; }
                string opName = twoPlayer ? stats.LoadPlayer() : "Computer";
                int player1Total = 0;
                int player2Total = 0;
                bool gameOn = true;

                while (gameOn)
                {
                    printer.RollEnter(player1Name);
                    int[] player1Rolls = Roll5Dice();
                    if (twoPlayer) { printer.RollEnter(opName); }
                    int[] player2Rolls = Roll5Dice();
                    int player1OfAKind = OfAKind(player1Rolls);
                    int player2OfAKind = OfAKind(player2Rolls);
                    //Print Player 1 Rolls and their Unique amount of rolls
                    printer.PrintRolls(player1Rolls, player1Name);
                    printer.PrintUniqueRolls(player1Name, player1OfAKind);
                    //Print Player 2 Rolls and their Unique amount of rolls
                    printer.PrintRolls(player2Rolls, opName);
                    printer.PrintUniqueRolls(opName, player2OfAKind);
                    if (player1OfAKind <= 2 && RollAgain(player1Name, printer))
                    {
                        player1Rolls = Roll5Dice();
                    }
                    else 
                    {
                        player1Total += ScoreAdd(player1OfAKind);
                        printer.PrintScore(player1Name, player1Total);

                    }
                    if (player2OfAKind <= 2 && RollAgain(opName, printer))
                    {
                        player2Rolls = Roll5Dice();
                    }
                    else
                    {
                        player2Total += ScoreAdd(player2OfAKind);
                        printer.PrintScore(opName, player2Total);
                    }
                    gameOn = IsGameOver(game, player1Name, opName, player1Total, player2Total);
                }
            }
        }
        public class Sevens : Dice
        {
            /// <summary>
            /// Creates and returns an int array containing users rolls
            /// </summary>
            /// <returns></returns>
            public int[] GetRolls()
            {
                newRoll();
                int dice1 = Roll;
                newRoll();
                int dice2 = Roll;
                return new int[] { dice1, dice2 };
            }
            /// <summary>
            /// MEthod to test the rolls are equal to 7
            /// </summary>
            /// <param name="rolls"></param>
            /// <returns>
            /// true if the sum of rolls == 7
            /// otherwise false
            /// </returns>
            public bool EqualsSeven(int[] rolls) 
            {
                return rolls.Sum() == 7;
            
            }
            /// <summary>
            /// Method that handles all of the game logic 
            /// </summary>
            /// <param name="printer"></param>
            /// <param name="stats"></param>
            public void GameStart(Printer printer, StatsJSON stats)
            {
                Game game = new Game();
                string player1Name = stats.LoadPlayer();
                if (player1Name == "Q") { return; }
                string opName = game.isTwoPlayer() ? stats.LoadPlayer() : "Computer";
                int playerTotal = 0;
                int computerTotal = 0;
                string gameWinner = "";
                string gameLoser = "";
                bool gameOn = true;

                while (gameOn) 
                {
                    printer.RollEnter(player1Name);
                    int[] playerRolls = GetRolls();
                    if (opName != "Computer") { printer.RollEnter(opName); }
                    int[] computerRolls = GetRolls();
                    printer.PrintRolls(playerRolls, player1Name);
                    printer.PrintRolls(computerRolls, opName);
                    
                    if (EqualsSeven(playerRolls)|| EqualsSeven(computerRolls)) { gameOn = false; }
                    else  
                    {
                        if (playerRolls[0] == playerRolls[1]) { playerTotal += playerRolls.Sum() * 2; } 
                        else { playerTotal += playerRolls.Sum(); }

                        if (computerRolls[0] == computerRolls[1]) { computerTotal += computerRolls.Sum() * 2; }
                        else { computerTotal += computerRolls.Sum(); }
                    }
                    printer.PrintScore(player1Name, playerTotal);
                    printer.PrintScore(opName, computerTotal);
                }

                bool isDraw = false;

                if (playerTotal > computerTotal)
                { gameWinner = player1Name; gameLoser = opName; }

                else if (playerTotal == computerTotal) 
                { isDraw = true; gameWinner = player1Name; gameLoser = opName; }

                else { gameWinner = opName; gameLoser = player1Name; }
                game.EndGame(isDraw, gameWinner, gameLoser, "Sevens");  
            }
        }
    }
}
