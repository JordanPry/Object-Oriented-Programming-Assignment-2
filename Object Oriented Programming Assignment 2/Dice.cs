using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal class Dice
    {
        private Random _random;
        private int _roll;
        public Dice() 
        {
            _random = new Random();
            newRoll();
        }
        public void newRoll() 
        {
            _roll = DiceRoller();
        
        }
        /// <summary>
        /// Method that uses random to generate a number between 1-6
        /// </summary>
        /// <returns>an int between the range of 1-6 </returns>
        private int DiceRoller() { return _random.Next(1, 7); }
        /// <summary>
        /// Method that returns the read only variable Roll from the privatised data _roll
        /// </summary>
        public int Roll { get { return _roll; } }
    }
}
