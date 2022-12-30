using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiDiceRoller
{
    internal class Dice
    {
        const char kAdvantage = 'a';
        const char kDisadvantage = 'd';
        const char kNoVantage = 'n';

        private int numOfDice;
        private int numOfSides;
        private int modifier;
        private int difficultyClass;
        private char advtageState;
        private bool modEachDie;


        Dice(int newNumOfDice, int newNumOfSides, int newModifier = 0, int newDifficultyClass = 0, char newAdvantageState = kNoVantage, bool newModEachDie = false)
        {
            numOfDice = newNumOfDice;
            numOfSides = newNumOfSides;
            modifier = newModifier;
            difficultyClass = newDifficultyClass;
            advtageState = newAdvantageState;
            modEachDie = newModEachDie;
        }




    }
}
