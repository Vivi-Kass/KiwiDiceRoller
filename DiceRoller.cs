using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiDiceRoller
{
   


    internal class DiceRoller
    {
        //advantage (A), Disadvantage (D), None (N)    
        private const char kAdvantage = 'A';
        private const char kDisadvantage = 'D';
        private const char kNoVantage = 'N';

        private int numOfSides = 0; //number of sides the die has
        private int numOfDice = 0; //number of dice
        private int modifier = 0; //any modifier
        private int difficultyClass = 0;
        private char advantage = kNoVantage; //advantage state
        private bool modPerDie = false; //will the mod be added to each die?

        internal int NumOfSides
        {
            get { return numOfSides; }
            set { numOfSides = value; }
        }

        internal int NumOfDice
        {
            get { return numOfDice; }
            set { numOfDice = value; }
        }

        internal int Modifier
        {
            get { return modifier; }
            set { modifier = value; }
        }

        internal int DifficultyClass
        {
            get { return difficultyClass; }
            set { difficultyClass = value; }
        }

        internal char Advantage
        {
            get { return advantage; }
            set { advantage = value; }
        }

        internal bool ModPerDie
        {
            get { return modPerDie; }
            set { modPerDie = value; }
        }


        /*
        * Function: DiceRoller constructor
        * Description: Instantiates the diceroller with the given data. Data will be validated in the MainWindow so it can simply be carried over
        * Parameters: int inputNumOfSides, int inputNumOfDice, int inputModifier, int inputDifficultyClass, char inputAdvantage, bool inputModPerDie
        * Returns: void
        */
        internal DiceRoller(int inputNumOfSides, int inputNumOfDice, int inputModifier, int inputDifficultyClass, char inputAdvantage, bool inputModPerDie)
        {
            NumOfSides = inputNumOfSides;
            NumOfDice = inputNumOfDice;
            Modifier = inputModifier;
            DifficultyClass = inputDifficultyClass;
            Advantage = inputAdvantage;
            ModPerDie = inputModPerDie;
        }


        /*
        * Function: RollNoVantage
        * Description: Rolls the dice using the data it has been given, no advantage
        * Parameters: void
        * Returns: A list of strings with the results
        */
        internal List<string> RollNoVantage()
        {
            List<string> results = new List<string>(); //list of the results
            Random rndm = new Random(); //random number generator
            int runningTotal = 0;

            for (int i = 1; i <= numOfDice; i++)
            {
                int generatedNum = rndm.Next(1, NumOfSides); //get the number
                int generatedNumWithMod = generatedNum + Modifier;
                string tempResult = "";

                tempResult += "Roll " + i.ToString() + ": "; //Roll #: 
                runningTotal += generatedNumWithMod; 
                tempResult += generatedNum.ToString(); //Roll#: ##
                if (ModPerDie == true)
                {
                    if (Modifier > 0) //mod is positive, if 0, do nothing
                    {
                        tempResult += " + " + Modifier.ToString() + " = " + generatedNumWithMod.ToString();
                    }
                    else if (Modifier < 0) //mod is negative
                    {
                        tempResult += " - " + Modifier.ToString() + " = " + generatedNumWithMod.ToString();
                    }

                    runningTotal += generatedNumWithMod;

                }
                else
                {
                    runningTotal += generatedNum; //add the unmodified number to the running total
                }

                results.Add(tempResult); //add the results to the list

            }

            if (ModPerDie == false && Modifier != 0) //if mod per die is false and mod is not 0 add the mod to the running total
            {
                results.Add("Modifier for total: " + Modifier.ToString());
                runningTotal += Modifier;
            }


            results.Add("Total: " + runningTotal.ToString());


            return results;
        }








    }
}
