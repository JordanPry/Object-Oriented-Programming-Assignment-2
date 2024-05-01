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

        /// <summary>
        /// Attempts to load data from the JSON file defined by the file path name within the class
        /// Exception handled to call a method to Create a new file if file is not found
        /// </summary>
        /// <returns>List of the GameStats class with default peramators </returns>
        public List<GameStats> LoadStats()
        {
            try
            {
                string data = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<GameStats>>(data);
            }
            catch (FileNotFoundException) 
            {
                Console.WriteLine("File Not Found, Creating new File");
                System.Threading.Thread.Sleep(1500);
                return CreateFile();
            
            }
        }
        /// <summary>
        /// Method used for Updating the stats to a file using the JSon format
        /// </summary>
        /// <param name="newStats"></param>
        public void SaveStats(List<GameStats> newStats)
        {
            string playerData = JsonConvert.SerializeObject(newStats);
            File.WriteAllText(filePath, playerData);
        }
        /// <summary>
        /// Method Ran when the file cannot be found, creates 3 default players with generic names
        /// </summary>
        /// <returns>
        ///  List of the GameStats class with default peramators
        /// </returns>
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
        /// <summary>
        /// Attempts to find a "Player" based on the user inputted name if player is not found
        /// The method will continue to ask the user to input names until either a name is found
        /// or the user enters "Q" to return to the main menu screen
        /// </summary>
        /// <returns>Either the Found player name or Q</returns>
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
        /// <summary>
        /// Class responsible for updating the data within the Stats file
        /// Finds the player within the List of GameStats and uses a switch case to 
        /// correctly find and update the neccessary data 
        /// </summary>
        /// <param name="playerName"></param>
        /// <param name="gameType"></param>
        /// <param name="gameWin"></param>
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
        /// <summary>
        /// Method used for adding users own personal players
        /// Checks whethere the user exists and if not
        /// will allow the addition of a new player to the GameStats file
        /// </summary>
        /// <param name="playerName"></param>
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
        /// <summary>
        /// Method used for Resetting all users stats apart from name to 0
        /// Stats will only reset after the user confirms their choice to reset
        /// </summary>
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
        /// <summary>
        /// Void method that prints the Players stats, loads the "stats" in to a a List 
        /// Loops through each item in the list Displaying users stats
        /// in a formatted way
        /// </summary>
        public void PrintStats() 
        {
            Console.WriteLine("------------------------------------------------");
            Console.WriteLine("                  Game Stats");
            Console.WriteLine("------------------------------------------------");
            List<GameStats> playerStats = LoadStats();
            foreach (var player in playerStats)
            {
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"Name: {player.PlayerName}\nSevens Wins: {player.Game7Wins}\nThree or More Wins:{player.Game3Wins}\nSevens Played: {player.Game7Played}\nThree or More Played:{player.Game3Played}");
                Console.WriteLine("------------------------------------------------");
            }
        }
    }
}
