using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViviDiceRoller
{
   
    internal class DiceRoller
    {
        //advantage (A), Disadvantage (D), None (N)    
        private const string kAdvantage = "Adv";
        private const string kDisadvantage = "Disad";
        private const string kNoVantage = "N/A";

        private int numOfSides = 0; //number of sides the die has
        private int numOfDice = 0; //number of dice
        private int modifier = 0; //any modifier
        private int difficultyClass = 0;
        private string advantage = kNoVantage; //advantage state
        private bool? modPerDie = false; //will the mod be added to each die?

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

        internal string Advantage
        {
            get { return advantage; }
            set { advantage = value; }
        }

        internal bool? ModPerDie
        {
            get { return modPerDie; }
            set { modPerDie = value; }
        }


        /*
        * Function: DiceRoller constructor
        * Description: Instantiates the diceroller with the given data. Data will be validated in the MainWindow so it can simply be carried over
        * Parameters: int inputNumOfSides, int inputNumOfDice, int inputModifier, int inputDifficultyClass, char inputAdvantage, bool? inputModPerDie
        * Returns: void
        */
        internal DiceRoller(int inputNumOfSides, int inputNumOfDice, int inputModifier, int inputDifficultyClass, string inputAdvantage, bool? inputModPerDie)
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
            int numOfSucc = 0;
            int numOfFail = 0;
            int numOfCritSucc = 0;
            int numOfCritFail = 0;

            for (int i = 1; i <= numOfDice; i++)
            {
                //when getting rndm.next, num of sides has to add 1. Otherwise the highest side will not roll.
                int generatedNum = rndm.Next(1, (NumOfSides + 1)); //get the number
                int generatedNumWithMod = generatedNum + Modifier;
                string tempResult = "";

                tempResult += "Roll " + i.ToString() + ": "; //Roll #:                
                tempResult += generatedNum.ToString(); //Roll#: ##
                if (ModPerDie == true)
                {
                    if (Modifier > 0) //mod is positive, if 0, do nothing
                    {
                        tempResult += " + " + Modifier.ToString() + " = " + generatedNumWithMod.ToString();
                    }
                    else if (Modifier < 0) //mod is negative
                    {
                        tempResult += " - " + (Modifier * -1).ToString() + " = " + generatedNumWithMod.ToString();
                    }
                    runningTotal += generatedNumWithMod;
                }
                else
                {
                    runningTotal += generatedNum; //add the unmodified number to the running total
                }

                if (generatedNum == 1) //check for nat 1
                {
                    numOfCritFail++;
                }
                else if (generatedNum == numOfSides) //check for crit success
                {
                    numOfCritSucc++;
                }


                if(difficultyClass != 0)
                {
                    if(ModPerDie == true)
                    {
                        if(generatedNumWithMod >= difficultyClass)
                        {
                            numOfSucc++;
                        }
                        else
                        {
                            numOfFail++;
                        }
                    }
                    else
                    {
                        if (generatedNum >= difficultyClass)
                        {
                            numOfSucc++;
                        }
                        else
                        {
                            numOfFail++;
                        }
                    }
                }
                results.Add(tempResult); //add the results to the list
            }

            if (ModPerDie == false && Modifier != 0) //if mod per die is false and mod is not 0 add the mod to the running total
            {
                results.Add("Modifier for total: " + Modifier.ToString());
                runningTotal += Modifier;
            }


            results.Add("Total: " + runningTotal.ToString());
            results.Add("Successes: " + numOfSucc.ToString());
            results.Add("Failures: " + numOfFail.ToString());
            results.Add("Critical Successes: " + numOfCritSucc.ToString());
            results.Add("Critical Failures: " + numOfCritFail.ToString());

            return results;
        }






        /*
        * Function: RollVantage
        * Description: Rolls the dice using the data it has been given, with advantage or disadvantage, depending on the given string
        * Parameters: void
        * Returns: A list of strings with the results
        */
        internal List<string> RollVantage(string vantageType)
        {
            List<string> results = new List<string>(); //list of the results
            Random rndm = new Random(); //random number generator
            int runningTotal = 0;
            int numOfSucc = 0;
            int numOfFail = 0;
            int numOfCritSucc = 0;
            int numOfCritFail = 0;

            for (int i = 1; i <= numOfDice; i++)
            {
                //when getting rndm.next, num of sides has to add 1. Otherwise the highest side will not roll.
                int generatedNumA = rndm.Next(1, (NumOfSides + 1)); //get the number
                int generatedNumWithModA = generatedNumA + Modifier;
                string tempResultA = "";

                int generatedNumB = rndm.Next(1, (NumOfSides + 1)); //get the number
                int generatedNumWithModB = generatedNumB + Modifier;
                string tempResultB = "";

                tempResultA += "Roll " + i.ToString() + "a: "; //Roll #: 
                tempResultB += "Roll " + i.ToString() + "b: "; //Roll #: 


                tempResultA += generatedNumA.ToString(); //Roll#a: ##
                tempResultB += generatedNumB.ToString(); //Roll#b: ##

                if (ModPerDie == true)
                {
                    if (Modifier > 0) //mod is positive, if 0, do nothing
                    {
                        tempResultA += " + " + Modifier.ToString() + " = " + generatedNumWithModA.ToString();
                        tempResultB += " + " + Modifier.ToString() + " = " + generatedNumWithModB.ToString();
                    }
                    else if (Modifier < 0) //mod is negative
                    {
                        tempResultA += " - " + (Modifier * -1).ToString() + " = " + generatedNumWithModA.ToString();
                        tempResultB += " - " + (Modifier * -1).ToString() + " = " + generatedNumWithModB.ToString();
                    }

                    if (vantageType == kAdvantage)
                    {
                        if (generatedNumWithModA >= generatedNumWithModB) //if greater, or equal, use A.
                        {
                            runningTotal += generatedNumWithModA;
                        }
                        else  //otherwise use B
                        {
                            runningTotal += generatedNumWithModB;
                        }

                    }
                    else //otherwise disadvantage
                    {
                        if (generatedNumWithModA <= generatedNumWithModB) //if less, or equal, use A.
                        {
                            runningTotal += generatedNumWithModA;
                        }
                        else  //otherwise use B
                        {
                            runningTotal += generatedNumWithModB;
                        }
                    }
                    
                }
                else
                {
                    if (vantageType == kAdvantage)
                    {
                        if (generatedNumWithModA >= generatedNumB) //if greater, or equal, use A.
                        {
                            runningTotal += generatedNumA;
                        }
                        else  //otherwise use B
                        {
                            runningTotal += generatedNumB;
                        }
                    }
                    else //otherwise disadvantage
                    {
                        if (generatedNumWithModA <= generatedNumB) //if less, or equal, use A.
                        {
                            runningTotal += generatedNumA;
                        }
                        else  //otherwise use B
                        {
                            runningTotal += generatedNumB;
                        }
                    }

                }


                if (vantageType == kAdvantage) //if at advantage
                {
                    if (generatedNumA == 1 && generatedNumB == 1) //if both roll 1 at advantage it's a crit fail
                    {
                        numOfCritFail++;
                    }
                    else if (generatedNumA == numOfSides || generatedNumB == numOfSides) //when at advantage if either crit succeed, that's a crit success
                    {
                        numOfCritSucc++;
                    }
                }
                else //disadvantage
                {
                    if (generatedNumA == 1 || generatedNumB == 1) //if one of them roll 1 at disadvantage it's a crit fail
                    {
                        numOfCritFail++;
                    }
                    else if (generatedNumA == numOfSides && generatedNumB == numOfSides) //when at disadvantage if both crit succeed, that's a crit success
                    {
                        numOfCritSucc++;
                    }
                }
                


                if (difficultyClass != 0)
                {
                    if (ModPerDie == true)
                    {
                        if (vantageType == kAdvantage)
                        {
                            if (generatedNumWithModA >= difficultyClass || generatedNumWithModB >= difficultyClass) //if either beat the DC at advantage, it beats
                            {
                                numOfSucc++;
                            }
                            else //otherwise fail
                            {
                                numOfFail++;
                            }
                        }
                        else //disadvantage
                        {
                            if (generatedNumWithModA >= difficultyClass && generatedNumWithModB >= difficultyClass) //if both beat the DC at disadvantage, it beats
                            {
                                numOfSucc++;
                            }
                            else //otherwise fail
                            {
                                numOfFail++;
                            }
                        }
                    }
                    else //mod is added at the end
                    {
                        if (vantageType == kAdvantage)
                        {
                            if (generatedNumA >= difficultyClass || generatedNumB >= difficultyClass) //if either beat the DC at advantage, it beats
                            {
                                numOfSucc++;
                            }
                            else //otherwise fail
                            {
                                numOfFail++;
                            }
                        }
                        else //disadvantage
                        {
                            if (generatedNumA >= difficultyClass && generatedNumB >= difficultyClass) //if both beat the DC at disadvantage, it beats
                            {
                                numOfSucc++;
                            }
                            else //otherwise fail
                            {
                                numOfFail++;
                            }
                        }
                    }
                }

                results.Add(tempResultA); //add the results of A to the list    
                results.Add(tempResultB); //add the results of B to the list    
            }
                

            if (ModPerDie == false && Modifier != 0) //if mod per die is false and mod is not 0 add the mod to the running total
            {
                results.Add("Modifier for total: " + Modifier.ToString());
                runningTotal += Modifier;
            }


            results.Add("Total: " + runningTotal.ToString());
            results.Add("Successes: " + numOfSucc.ToString());
            results.Add("Failures: " + numOfFail.ToString());
            results.Add("Critical Successes: " + numOfCritSucc.ToString());
            results.Add("Critical Failures: " + numOfCritFail.ToString());

            return results;
        }

    }
}
