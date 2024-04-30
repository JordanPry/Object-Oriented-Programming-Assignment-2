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

        public List<GameStats> LoadStats()
        {
            try
            {
                string data = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<GameStats>>(data);
            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine("File Not Found, Creating file, Creating new File");
                System.Threading.Thread.Sleep(1500);
                return CreateFile();
            
            }
        }
        public void SaveStats(List<GameStats> newStats)
        {
            string playerData = JsonConvert.SerializeObject(newStats);
            File.WriteAllText(filePath, playerData);
        }
        public List<GameStats> CreateFile() 
        {
            string[] defaultNames = { "Player 1", "Player 2", "Computer" };
            var defaultPlayers = new List<GameStats>();

            foreach (string name in defaultNames)
            {
                var defaultPlayer = new GameStats
                {
                    PlayerName = name,
                    Game7Wins = 0,
                    Game3Wins = 0,
                    Game7Played = 0,
                    Game3Played = 0
                };
                defaultPlayers.Add(defaultPlayer);
            }
            SaveStats(defaultPlayers);
            return defaultPlayers;
        
        }
        public string LoadPlayer() 
        {
            while (true)
                {
                string playerName = NewPlayerName(); //Using same function as creating new player to loop to find name
                if (playerName == "Q") { return playerName; }
                List<GameStats> stats = LoadStats();
                bool nameExists = stats.Exists(p => p.PlayerName == playerName);
                if (nameExists)
                {
                    GameStats player = stats.Find(p => p.PlayerName == playerName);
                    Console.WriteLine("Player Found");
                    return player.PlayerName;
                }
                else { Console.WriteLine("Player Not Found, Try again"); }
            }
        
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
        public void AddPlayer(string playerName)
        {

            List<GameStats> players = LoadStats();
            bool nameExists = players.Exists(p => p.PlayerName == playerName);
            if (!nameExists && playerName != "Q")
            {
                GameStats newPlayer = new GameStats
                {
                    PlayerName = playerName,
                    Game7Wins = 0,
                    Game3Wins = 0,
                    Game7Played = 0,
                    Game3Played = 0
                };
                players.Add(newPlayer);
                SaveStats(players);
            }
            else if (playerName == "Q") { }
            else
            {
                Console.WriteLine("Player Name already exists please enter a Different Name");
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
                        player.Game3Wins = 0;
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
