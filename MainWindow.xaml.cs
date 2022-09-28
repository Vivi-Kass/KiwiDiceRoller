/*
 * File: MainWindow.xaml.cs
 * Project: KiwiDiceRoller
 * Programmer: Keiran Morton
 * First Version:  (26/09/2022)
 * Description: Desinged in VS 2022 with WPD in .NET Framework 4.8. This program is a simple dice rolling application specialized for Dungeons and Dragons 5th edition.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;

namespace KiwiDiceRoller
{
     /*
     * Name: MainNotepad
     * Purpose: This is the main window for the program.
     */
    public partial class MainWindow : Window
    {
        //advantage (A), Disadvantage (D), None (N)    
        private const string kAdvantage = "Adv";
        private const string kDisadvantage = "Disad";
        private const string kNoVantage = "N/A";

        private bool validSides = false;
        private bool validDice = false;
        private bool validMod = true;
        private bool validDC = true;


        private bool validInput = true;
        private int savedNumOfSides = 0; //number of sides the die has
        private int savedNumOfDice = 0; //number of dice
        private int savedModifier = 0; //any modifier
        private int savedDifficultyClass = 0;
        private string savedAdvantage = kNoVantage; //advantage state
        private bool? savedModPerDie = false; //will the mod be added to each die?


        private bool ValidInput
        {
            get { return validInput; }
            set { validInput = value; }
        }

        private int SavedNumOfSides
        {
            set { savedNumOfSides = value; }
            get { return savedNumOfSides; }
        }

        private int SavedNumOfDice
        {
            set { savedNumOfDice = value; }
            get { return savedNumOfDice; }
        }

        private int SavedModifier
        {
            set { savedModifier = value; }
            get { return savedModifier; }
        }

        private int SavedDifficultyClass
        {
            set { savedDifficultyClass = value; }
            get { return savedDifficultyClass; }
        }

        private string SavedAdvantage 
        {
            set { savedAdvantage = value; }
            get { return savedAdvantage; }
        }
            
        private bool? SavedModPerDie
        {
            set { savedModPerDie = value; }
            get { return savedModPerDie; }
        }

        private bool ValidSides
        {
            set { validSides = value; }
            get { return validSides; }
        }
        private bool ValidDice
        {
            set { validDice = value; }
            get { return validDice; }
        }
        private bool ValidMod
        {
            set { validMod = value; }
            get { return validMod; }
        }
        private bool ValidDC
        {
            set { validDC = value; }
            get { return validDC; }
        }

        /*
        * Function: MainNotepad constructor
        * Description: Initializes the window
        * Parameters: void
        * Returns: void
        */
        public MainWindow()
        {
            InitializeComponent();
        }


        /*
        * Function: Window_Closed 
        * Description: Shutdown the program when the main window is closed
        * Parameters: object sender, EventArgs e
        * Returns: void
        */
        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        /*
        * Function: CheckInput
        * Description: check if the number of dice was changed
        * Parameters: object sender, TextChangedEventArgs e
        * Returns: void
        */
        private void CheckInput()
        {           
            //Check all input
            if(ValidDice == true && ValidSides == true && ValidMod == true && ValidDC == true)
            {
                validInput = true;
            }
            else
            {
                validInput = false;
            }

            savedModPerDie = modPerDie.IsChecked;


            //get advantage state
            savedAdvantage = advantageState.SelectedIndex.ToString();

            if (validInput == true)
            {
                rollButton.IsEnabled = true;
            }
            else 
            { 
                rollButton.IsEnabled = false; 
            }
        }



        /*
        * Function: NumOfDice_TextChanged 
        * Description: check for valid input
        * Parameters: object sender, TextChangedEventArgs e
        * Returns: void
        */
        private void NumOfDice_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidDice = true;

            //check number of dice
            try
            {
                SavedNumOfDice = Convert.ToUInt16(numOfDice.Text); //unsigned so it is positive
                if (Convert.ToUInt16(numOfDice.Text) == 0) //can't roll 0 dice
                {
                    ValidDice = false;
                }

            }
            catch (Exception)
            {
                ValidDice = false;
            }


            CheckInput();
        }
       /*
       * Function: NumOfSides_TextChanged 
       * Description: check for valid input
       * Parameters: object sender, TextChangedEventArgs e
       * Returns: void
       */
        private void NumOfSides_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidSides = true;

            //check number of sides
            try
            {
                SavedNumOfSides = Convert.ToUInt16(numOfSides.Text);

                if (Convert.ToUInt16(numOfSides.Text) <= 1) //can't be 0 or 1 sided
                {
                    ValidSides = false;
                }
            }
            catch (Exception)
            {
                ValidSides = false;
            }

            CheckInput();
        }
        /*
       * Function: Modifier_TextChanged 
       * Description: check for valid input
       * Parameters: object sender, TextChangedEventArgs e
       * Returns: void
       */
        private void Modifier_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidMod = true;
            //check modifier
            try
            {
                SavedModifier = Convert.ToInt16(modifier.Text);
            }
            catch (Exception)
            {
                if (Convert.ToString(modifier.Text) == "") //check if text is blank
                {
                    SavedModifier = 0; //blank, default to 0
                }
                else
                {
                    ValidMod = false;
                }
            }
            CheckInput();
        }
       /*
       * Function: NeedToBeat_TextChanged 
       * Description: check for valid input
       * Parameters: object sender, TextChangedEventArgs e
       * Returns: void
       */
        private void DifficultyClass_TextChanged(object sender, TextChangedEventArgs e)
        {
            ValidDC = true;

            //check DC
            try
            {
                SavedDifficultyClass = Convert.ToInt16(DifficultyClass.Text);
            }
            catch (Exception)
            {
                if (Convert.ToString(DifficultyClass.Text) == "") //check if text is blank
                {
                    SavedDifficultyClass = 0; //blank, default to 0
                }
                else
                {
                    ValidDC = false;
                }
            }
            CheckInput();
        }

        /*
       * Function: ModPerDie_Click
       * Description: check for valid input
       * Parameters: object sender, RoutedEventArgs e
       * Returns: void
       */
        private void ModPerDie_Click(object sender, RoutedEventArgs e)
        {
            CheckInput();
        }


        /*
       * Function: RollButton_Click
       * Description: Rolls the dice and prints the results in a new window. Input must be valid before button can be pressed so it will be valid
       * Parameters: object sender, RoutedEventArgs e
       * Returns: void
       */
        private void RollButton_Click(object sender, RoutedEventArgs e)
        {
            //give the dice the valid values           
            DiceRoller roll = new DiceRoller(SavedNumOfSides, SavedNumOfDice, SavedModifier, SavedDifficultyClass, SavedAdvantage, SavedModPerDie);
            RollText.IsReadOnly = false; //enable writing
            if (advantageState.SelectedIndex == 0) //0 is N/A
            {
                PrintData(roll.RollNoVantage());
            }
            else if (advantageState.SelectedIndex == 1) //1 is Adv
            {
                PrintData(roll.RollVantage(kAdvantage));
            }
            else if (advantageState.SelectedIndex == 2) //2 is Disad
            {
                PrintData(roll.RollVantage(kDisadvantage));
            }
            RollText.IsReadOnly = true; //disable writing

        }


        /*
        * Function: PrintData
        * Description: changes the text in the window to match the data given
        * Parameters: List<string> diceRolls
        * Returns: void
        */
        private void PrintData(List<string> diceRolls)
        {
            for (int i = 0; i < diceRolls.Count; i++)
            {
                RollText.Text += diceRolls[i] + "\n";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RollText.IsReadOnly = false; //enable writing
            RollText.Text = ""; //clear text
            RollText.IsReadOnly = true; //disable writing
        }
    }
}
