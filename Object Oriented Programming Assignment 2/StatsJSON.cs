using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class StatsJSON : Printer
    {
        public class GameStats
        {
            public string PlayerName { get; set; }
            public int Game7Wins { get; set; }
            public int Game3Wins { get; set; }
            public int Game7Played { get; set; }
            public int Game3Played { get; set; }
        }

        private string filePath = "gameStats.json";

        public  List<GameStats> LoadStats()
        {
            string data = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<GameStats>>(data);
        }
        public void SaveStats(List<GameStats> newStats)
        {
            string data = JsonConvert.SerializeObject(newStats);
            File.WriteAllText(filePath, data);
        }
        public void UpdateStats(string playerName, string gameType, bool gameWin) 
        {
            List<GameStats> stats = LoadStats();
            GameStats player = stats.Find(p => p.PlayerName == playerName);
            int gameIndex = gameType == "Sevens" ? 0 : 1;
            switch (gameIndex) 
            {
                case 0:
                    if (gameWin) {player.Game7Wins++; }
                    player.Game7Played++;
                    SaveStats(stats);
                    return;
                case 1:
                    if (gameWin) { player.Game3Wins++; }
                    player.Game3Played++;
                    SaveStats(stats);
                    return;
            }  
        }

        public void ResetStats()
        {
            int userChoice = ConfirmChoice();
            switch (userChoice) {
                case 1:
                    List<GameStats> playerStats = LoadStats();
                    foreach (var player in playerStats)
                    {
                        player.Game7Wins = 0;
                        player.Game7Played = 0;
                        player.Game3Played = 0;
                        player.Game3Played = 0;
                    }
                    SaveStats(playerStats);
                    return;
                case 2:
                    return;
            }
        
        
        }
        public void PrintStats() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("                  Game Stats");
            Console.WriteLine("------------------------------------------------");
            List<StatsJSON.GameStats> playerStats = LoadStats();
            foreach (var player in playerStats)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Name: {player.PlayerName}\nSevens Wins: {player.Game7Wins}\nThree or More Wins:{player.Game3Wins}\nSevens Played: {player.Game7Played}\nThree or More Played:{player.Game3Played}");
                Console.WriteLine("------------------------------------------------");
            }
        }
    }
}
