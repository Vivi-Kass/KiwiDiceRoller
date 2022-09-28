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
using System.Windows.Shapes;

namespace KiwiDiceRoller
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class RollResults : Window
    {
        public RollResults()
        {
            InitializeComponent();
        }



        /*
        * Function: PrintData
        * Description: changes the text in the window to match the data given
        * Parameters: List<string> diceRolls
        * Returns: void
        */
        internal void PrintData(List<string> diceRolls)
        {
            for(int i = 0; i < diceRolls.Count ;i++)
            {
                RollText.Text += diceRolls[i] + "\n";
            }

        }




    }
}
