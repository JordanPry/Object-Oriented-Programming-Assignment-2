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
        public bool isTwoPlayer() 
        {
            TwoPlayerText();
            bool twoPlayer = OptionChoice(2) == 1 ? false : true;
            return twoPlayer;
        }
        public class ThreeOrMore : Dice
        {
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
            public int ScoreAdd(int amount) 
            {
                switch (amount) 
                {
                    case 2:
                        return 0;
                    case 3:
                        return 3;
                    case 4:
                        return 6;
                    case 5:
                        return 12;
                    case 6:
                        return 12;
                    default: 
                        return 0;
                }
            }
            public void GameStart() 
            {
                StatsJSON stats = new StatsJSON();
                Printer printer = new Printer();
                Game game = new Game();
                bool twoPlayer = game.isTwoPlayer();
                string player1Name = stats.LoadPlayer();
                string opName = "Computer";
                if (twoPlayer) { opName = stats.LoadPlayer(); }
                int player1Total = 0;
                int player2Total = 0;
                bool gameOn = true;
                while (gameOn)
                {
                    printer.RollEnter(player1Name);
                    int[] player1Rolls = Roll5Dice();
                    if (twoPlayer) { printer.RollEnter(opName); }
                    else
                    {
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine("Computer Rolling....");
                        System.Threading.Thread.Sleep(500);
                    }
                    int[] player2Rolls = Roll5Dice();
                    int player1OfAKind = OfAKind(player1Rolls);
                    int player2OfAKind = OfAKind(player2Rolls);
                    if (player1OfAKind <= 2)
                    {

                    }
                    else 
                    {
                        player1Total += ScoreAdd(player1OfAKind);
                    
                    }
                    if (player2OfAKind <= 2)
                    {

                    }
                    else
                    {
                        player2Total += ScoreAdd(player2OfAKind);
                    }
                    if (player1Total >= 20)
                    {
                        game.EndGame(false, player1Name, opName, "Threes");
                        gameOn  = false;
                    }
                    else if (player2Total >= 20) 
                    {
                        game.EndGame(false, player1Name, opName, "Threes");
                        gameOn = false;
                    }
                }
            }
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
                StatsJSON stats = new StatsJSON();
                Printer printer = new Printer();
                Game game = new Game();
                bool twoPlayer = game.isTwoPlayer();
                string player1Name = stats.LoadPlayer();
                string opName = "Computer";
                if (twoPlayer) { opName = stats.LoadPlayer(); }
                int playerTotal = 0;
                int computerTotal = 0;
                string gameWinner = "";
                string gameLoser = "";
                bool gameOn = true;
                while (gameOn) 
                {
                    printer.RollEnter(player1Name);
                    int[] playerRolls = GetRolls();
                    if (twoPlayer)
                    {
                        printer.RollEnter(opName);
                    }
                    else 
                    {
                        Console.WriteLine("------------------------------------------------");
                        Console.WriteLine("Computer Rolling....");
                        System.Threading.Thread.Sleep(500);
                    }
                    int[] computerRolls = GetRolls();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine($"{player1Name} rolled a: {playerRolls[0]}\n {player1Name}  rolled a: : {playerRolls[1]}");
                    Console.WriteLine($"{opName} rolled a: {computerRolls[0]}\n{opName} rolled a: {computerRolls[1]}");
                    Console.WriteLine("------------------------------------------------");
                    if (playerRolls[0] + playerRolls[1] == 7 || computerRolls[0] + computerRolls[1] == 7) { gameOn = false; }
                    else  
                    {
                        if (playerRolls[0] == playerRolls[1]) { playerTotal += playerRolls.Sum() * 2; } 
                        else { playerTotal += playerRolls.Sum(); }

                        if (computerRolls[0] == computerRolls[1]) { computerTotal += computerRolls.Sum() * 2; }
                        else { computerTotal += computerRolls.Sum(); }
                    }
                    Console.WriteLine($"{player1Name} Total Score: {playerTotal}");
                    Console.WriteLine($"{opName} Total Score: {computerTotal}");
                    Console.WriteLine("------------------------------------------------");    
                }
                bool isDraw = false;
                if (playerTotal > computerTotal) { gameWinner = player1Name; gameLoser = opName; }
                else if (playerTotal == computerTotal) { isDraw = true; gameWinner = player1Name; gameLoser = opName; }
                else { gameWinner = opName; gameLoser = player1Name; }
                game.EndGame(isDraw, gameWinner, gameLoser, "Sevens");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("Game Over Returning to Start Screen");
                Console.WriteLine("------------------------------------------------");      
            }
        }
    }
}
