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
        private bool validInput = true;

        private bool ValidInput
        {
            get { return validInput; }
            set { validInput = value; }
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
            validInput = true;
            try
            {
                if (numOfDice.Text != null || numOfDice.Text != "")
                {
                    if (0 >= Convert.ToUInt16(numOfDice.Text)) //unsigned so it is positive
                    {
                        validInput = false;
                    }
                }
                else
                {
                    validInput = false;
                }
                
            }
            catch (Exception)
            {
                validInput = false;
            }

            try
            {
                if (numOfSides.Text != null || numOfSides.Text != "") //unsigned so it is positive
                {
                    if (0 >= Convert.ToUInt16(numOfSides.Text))
                    {
                        validInput = false;
                    }
                }
                else
                {
                    validInput = false;
                }
            }
            catch (Exception)
            {
                validInput = false;
            }

            if (modifier.Text != null)
            {
                if (modifier.Text != "")
                {
                    try
                    {
                        Convert.ToInt16(modifier.Text);
                    }
                    catch (Exception)
                    {
                        validInput = false;
                    }
                }
                
            }
            

            if (DifficultyClass.Text != null)
            {
                if (DifficultyClass.Text != "")
                {
                    try
                    {
                        Convert.ToInt16(DifficultyClass.Text);
                    }
                    catch (Exception)
                    {
                        validInput = false;
                    }
                }           
            }
            

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
            DiceRoller roll = new DiceRoller(Convert.ToInt16(numOfSides.Text), Convert.ToInt16(numOfDice.Text), Convert.ToInt16(modifier.Text), Convert.ToInt16(DifficultyClass.Text), advantageState.Text, Convert.ToBoolean(modPerDie.IsChecked));


        }
    }
}
