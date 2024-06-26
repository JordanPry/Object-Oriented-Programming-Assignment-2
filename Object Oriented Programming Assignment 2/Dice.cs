﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_Oriented_Programming_Assignment_2
{
    internal abstract class DiceRoller 
    {
        public abstract int[] RollDice(int rollsAmount);
    
    }
    internal class Dice : DiceRoller
    {
        private Random _random;
        private int _roll;
        public Dice() 
        {
            _random = new Random();
            newRoll();
        }
        /// <summary>
        /// Method that resets the roll and generates a new random number for roll
        /// </summary>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="diceAmount"></param>
        /// <returns></returns>
        public override int[] RollDice(int diceAmount)
        {
            int roll;
            int[] rolls = new int[diceAmount];
            for (int i = 0; i < diceAmount; i++)
            {
                newRoll();
                roll = Roll;
                rolls[i] = roll;
            }
            return rolls;
        }
    }
}
