/*
 * File:            Dice.cs
 * Project:         Kiwi Dice Roller
 * Programmer:      Keiran Morton
 * First Version:   30/12/2022 (dd/mm/yyyy)
 * Description: This file contains the revamped logic for the dice rolling component of the program.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiDiceRoller
{
    /*
     * Class       : Dice
     * Description	: Contains all of the logic for the dice
     */
    internal class Dice
    {
        const string kAdvantage = "Adv";
        const string kDisadvantage = "Disad";
        const string kNoVantage = "N/A";

        const int kRollState = 1;
        const int kModPerRollState = 2;

        private int numOfDice;
        private int numOfSides;
        private int modifier;
        private int difficultyClass;
        private string advtageState;
        private bool modEachDie;

        private int rollA;
        private int rollB;
        private int rollTotal;


        /*
         * Function    : Dice constructor
         * Description	: Creates the dice object. Can take 2 parameters and uses defaults
         * Parameters	: int newNumOfDice, int newNumOfSides, int newModifier = 0, int newDifficultyClass = 0, char newAdvantageState = kNoVantage, bool newModEachDie = false
         * Return		: Nothing
         */
        internal Dice(int newNumOfDice, int newNumOfSides, int newModifier = 0, int newDifficultyClass = 0, string newAdvantageState = kNoVantage, bool newModEachDie = false)
        {
            numOfDice = newNumOfDice;
            numOfSides = newNumOfSides;
            modifier = newModifier;
            difficultyClass = newDifficultyClass;
            advtageState = newAdvantageState;
            modEachDie = newModEachDie;

            //roles with mods
            rollA = 0;
            rollB = 0;

            rollTotal = 0;
        }

        /*
         * Function    : GenerateRolls
         * Description	: Generates the dice rolls using the constructed values
         * Parameters	: void
         * Return		: void
         */
        internal List<string>  GenerateRolls()
        {
            List<string> results = new List<string>(); //list of the results            
            Random rndm = new Random(); //random number generator

            //For each die to roll
            for (int rollIndex = 1; rollIndex <= numOfDice; rollIndex++)
            {
                string currentRollString = "";
                Roll(rndm);
                currentRollString = GenerateString(currentRollString, kRollState, rollIndex);

                if (modEachDie == true && modifier != 0) //if true and there is a mod
                {
                    AddModToDie();
                    currentRollString = GenerateString(currentRollString, kModPerRollState);
                }

                AddTotal();

                results.Add(currentRollString); //add the current roll string to the results string
            }

            if (modEachDie == false && modifier != 0) //if not moding each die. Add the mod to the total at the end if applicable
            {
                rollTotal += modifier;

                results.Add("Modifier for total: " + modifier.ToString());
            }


            results.Add("Total: " + rollTotal.ToString());


            return results;
        }


        /*
         * Function    : Roll
         * Description	: Rolls one die, and a second one if at some sort of vantage
         * Parameters	: void
         * Return		: void
         */
        private void Roll(Random rndm)
        {          
            rollA = rndm.Next(1, (numOfSides + 1)); //between 1 and sides+1 because next is not inclusive for max

            if (advtageState != kNoVantage) //roll a second die if at some sort of vantage state
            {
                rollB = rndm.Next(1, (numOfSides + 1)); //between 1 and sides+1 because next is not inclusive for max
            }         
        }

        /*
         * Function    : AddModToDie
         * Description	: Adds the modifier to the roll A, and roll B if at vantage
         * Parameters	: void
         * Return		: void
         */
        private void AddModToDie()
        {
            rollA += modifier; //add mod to rollA

            if (advtageState != kNoVantage) //if at some sort of vantage, add to rollB
            {
                rollB += modifier;
            }
        }


        /*
         * Function    : AddTotal
         * Description	: Adds the appropriate roll to the total. Based on the vantage state
         * Parameters	: void
         * Return		: void
         */
        private void AddTotal ()
        {
            if (advtageState == kAdvantage)
            {
                if (rollA >= rollB)
                {
                    rollTotal += rollA;
                }
                else
                {
                    rollTotal += rollB;
                }
            }
            else if (advtageState == kDisadvantage)
            {
                if (rollA <= rollB)
                {
                    rollTotal += rollA;
                }
                else
                {
                    rollTotal += rollB;
                }
            }
            else //no vantage
            {
                rollTotal += rollA;
            }
        }


        /*
         * Function    : AddTotal
         * Description	: Adds the appropriate roll to the total. Based on the vantage state
         * Parameters	: void
         * Return		: void
         */
        private string GenerateString(string originalString, int currentState, int rollNumber = 0)
        {

            if (currentState == kRollState)
            {
                
                if (advtageState != kNoVantage) //Second die is rolled
                {
                    originalString = "Roll " + rollNumber + "A: " + rollA;
                    originalString += "\nRoll " + rollNumber + "B: " + rollB;
                }
                else
                {
                    originalString = "Roll " + rollNumber + ": " + rollA; //no letter
                }

            }
            else if (currentState == kModPerRollState)
            {
                
                if (advtageState != kNoVantage) //Second die is rolled
                {
                    int newLine = originalString.IndexOf('\n'); //find new line

                    originalString = originalString.Insert(newLine, " + " + modifier + " = " + rollA); //insert the roll mod and total before the newline
                    originalString += " + " + modifier + " = " + rollB; //add rollB mod and total at the end
                }
                else //easy to add
                {
                    originalString += " + " + modifier + " = " + rollA;
                }

            }

            return originalString;
        }

    }
}
