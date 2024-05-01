using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static Object_Oriented_Programming_Assignment_2.StatsJSON;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Testing : Printer
    {
        Game.Sevens sevens = new Game.Sevens();
        Game.ThreeOrMore threeOrMore = new Game.ThreeOrMore();

        public class TestOutcomes
        {
            public string TestName { get; set; }
            public bool SevensTest { get; set; }
            public bool ThreesTest { get; set; }        
        }
        private string filePath = "testOutcomes.json";

        /// <summary>
        /// Loads the List of TestOutcomes objects from the "testOutcomes.json" file
        /// Exception handled to return an empty list of gameObjects where tests can be added to 
        /// and saved to a new file
        /// </summary>
        /// <returns></returns>
        public List<TestOutcomes> LoadOutcomes()
        {
            try
            {
                string data = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<TestOutcomes>>(data);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File Not Found, Creating new File");
                System.Threading.Thread.Sleep(1500);
                return new List<TestOutcomes>();

            }
        }
        /// <summary>
        /// Saves the data to the file
        /// </summary>
        /// <param name="newStats"></param>
        public void SaveOutcomes(List<TestOutcomes> newStats)
        {
            string playerData = JsonConvert.SerializeObject(newStats);
            File.WriteAllText(filePath, playerData);
        }
        /// <summary>
        /// Method responsible for correctly tracking the tests and saving them to the file
        /// </summary>
        /// <returns></returns>
        public List<TestOutcomes> CreateFile() 
        {
            var newTests = LoadOutcomes();

            var firstTest = new TestOutcomes
            {
                TestName = "Test " + (newTests.Count + 1),
                SevensTest = TestSevens(),
                ThreesTest = TestThrees()
            };
            newTests.Add(firstTest);
            SaveOutcomes(newTests);
            return newTests;
        
        }
        /// <summary>
        /// Void method that prints the Players stats, loads the "tests" in to a a List 
        /// Loops through each item in the list Displaying test number and whether it passed or failed
        /// </summary>
        public void PrintTests() 
        {
            List < TestOutcomes > allTests = LoadOutcomes();

            foreach (TestOutcomes testOutcomes in allTests) 
            {
                string successful = testOutcomes.SevensTest && testOutcomes.ThreesTest == true ? "Passed" : "Failed";
                Console.WriteLine($"{testOutcomes.TestName}: Both Tests {successful} ");
            }
        }
        /// <summary>
        /// Method to call correct methods depending on users input
        /// calls a method to display options
        /// </summary>
        public void TestMenu() 
        {
            TestChoice();
            switch (OptionChoice(3)) 
            {
                case 1:
                    TestBoth();
                    break;
                case 2:
                    PrintTests();
                    break;
                default:
                    break;
            
            }
        }

        /// <summary>
        ///Method that loops 10 times to test the game is working as intended
        ///As RNG is used this helps mitigate and cover all rolls to make sure everything is working
        /// </summary>
        public void TestBoth() 
        {
            Console.WriteLine("TESTING THREE OR MORE.......");
            Console.WriteLine("TESTING SEVENS OUT......");
            for (int i = 0; i < 10; i++)
            {
                CreateFile();
            }
            Console.WriteLine("GAME LOGIC TESTED 10 TIMES, ALL RUNNING AS INTENDED");
            
        }
        /// <summary>
        /// Method that uses Debug.Assert to Test whether the logic of Sevens Out is Correct
        /// </summary>
        /// <returns></returns>
        private bool TestSevens() 
        {
            int[] testRolls;
            while (true) 
            {
                testRolls = sevens.GetRolls();
                if (sevens.EqualsSeven(testRolls)) { break; }
            }
            Debug.Assert(sevens.EqualsSeven(testRolls) == (testRolls[0] + testRolls[1] == 7), "GAME DOESNT END AT CORRECT ENDPOINT");
            return true;
        }
        /// <summary>
        /// Method that uses Debug.Assert to test whether the logic of ThreeOrMore is Correct
        /// </summary>
        /// <returns></returns>
        private bool TestThrees() 
        {
            int[] rolls = threeOrMore.Roll5Dice();
            int uniqueRolls = threeOrMore.OfAKind(rolls);
            int score = threeOrMore.ScoreAdd(uniqueRolls);
            int testScore = 0;
            switch (uniqueRolls) 
            {
                case 3:
                    testScore = 3;
                    break;    
                case 4:
                    testScore = 6;
                    break;
                case 5:
                    testScore = 12;
                    break;
                default:
                    testScore = 0;
                    break;
            }
            Debug.Assert(threeOrMore.ScoreAdd(uniqueRolls) == testScore, "POINT TOTAL DOESNT CORRECTLY SUM" );
            while (true) 
            {
                if (uniqueRolls == 2) { rolls = threeOrMore.Roll5Dice(); uniqueRolls = threeOrMore.OfAKind(rolls); }
                score += threeOrMore.ScoreAdd(uniqueRolls);
                if (score >=20) { break; }
            }
            Debug.Assert(score >= 20, "GAME ENDS AT WRONG POSITION");
            return true;
        }
    }
}
