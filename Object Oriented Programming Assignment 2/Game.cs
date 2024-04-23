using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Game : Printer
    {
        public bool isTwoPlayer() 
        {
            TwoPlayerText();
            bool twoPlayer = OptionChoice(2) == 1 ? false : true;
            return twoPlayer;
        }
        public class ThreeOrMore 
        {
            public void Test() { Console.WriteLine("This works"); }
        }
        public class Sevens : Dice
        {
            public int[] GetRolls()
            {
                newRoll();
                int dice1 = Roll;
                newRoll();
                int dice2 = Roll;
                return new int[] { dice1, dice2 };
            }
            public void GameStart()
            {
                Game game = new Game();
                bool twoPlayer = game.isTwoPlayer();
                Printer printer = new Printer();
                int playerTotal = 0;
                int computerTotal = 0;
                string gameWinner = "";
                string gameLoser = "";
                bool gameOn = true;
                string opType = twoPlayer ? "Player 2" : "Computer";
                while (gameOn) 
                {
                    printer.RollEnter(1);
                    int[] playerRolls = GetRolls();
                    if (twoPlayer)
                    {
                        printer.RollEnter(2);
                    }
                    else 
                    {
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine("Computer Rolling....");
                        System.Threading.Thread.Sleep(500);
                    }
                    int[] computerRolls = GetRolls();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine($"You rolled a: {playerRolls[0]}\nYou rolled a: {playerRolls[1]}");
                    Console.WriteLine($"{opType} rolled a: {computerRolls[0]}\nAI rolled a: {computerRolls[1]}");
                    Console.WriteLine("------------------------------------------------");
                    if (playerRolls[0] + playerRolls[1] == 7 || computerRolls[0] + computerRolls[1] == 7) { gameOn = false; }
                    else  
                    {
                        if (playerRolls[0] == playerRolls[1]) { playerTotal += playerRolls.Sum() * 2; } 
                        else { playerTotal += playerRolls.Sum(); }

                        if (computerRolls[0] == computerRolls[1]) { computerTotal += computerRolls.Sum() * 2; }
                        else { computerTotal += computerRolls.Sum(); }
                    }
                    Console.WriteLine($"Player Total Score: {playerTotal}");
                    Console.WriteLine($"{opType} Total Score: {computerTotal}");
                    Console.WriteLine("------------------------------------------------");    
                }
                bool isDraw = false;
                if (playerTotal > computerTotal) { gameWinner = "Player 1"; gameLoser = opType; }
                else if (playerTotal == computerTotal) { isDraw = true; gameWinner = "Player 1"; gameLoser = opType; }
                else { gameWinner = opType; gameLoser = "Player 1"; }
                EndGame(isDraw, gameWinner, gameLoser, "Sevens");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Game Over Returning to Start Screen");
                Console.WriteLine("------------------------------------------------");      
            }
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
            }
        }
    }
}
