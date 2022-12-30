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
        const char kAdvantage = 'a';
        const char kDisadvantage = 'd';
        const char kNoVantage = 'n';

        private int numOfDice;
        private int numOfSides;
        private int modifier;
        private int difficultyClass;
        private char advtageState;
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
        Dice(int newNumOfDice, int newNumOfSides, int newModifier = 0, int newDifficultyClass = 0, char newAdvantageState = kNoVantage, bool newModEachDie = false)
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
        List<string> GenerateRolls()
        {
            List<string> results = new List<string>(); //list of the results
            string currentRoll = "";

            //For each die to roll
            for (int i = numOfDice; i >= numOfDice; i--)
            {
                Roll();

                if (modEachDie == true && modifier != 0) //if true and there is a mod
                {
                    AddModToDie();
                }

                AddTotal();

            }

            if (modEachDie == false && modifier != 0) //if not moding each die. Add the mod to the total at the end if applicable
            {
                rollTotal += modifier;
            }


            return results;
        }


        /*
         * Function    : Roll
         * Description	: Rolls one die, and a second one if at some sort of vantage
         * Parameters	: void
         * Return		: void
         */
        void Roll()
        {
            Random rndm = new Random(); //random number generator

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
        void AddModToDie()
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
        void AddTotal ()
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




    }
}
