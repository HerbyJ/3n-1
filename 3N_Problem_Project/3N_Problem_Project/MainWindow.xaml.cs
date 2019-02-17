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

namespace _3N_Problem_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Values> vList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            int iValue;
            int jValue;

            //Verify values are integers
            if (!int.TryParse(txtBx_InputI.Text, out iValue) || !int.TryParse(txtBx_InputJ.Text, out jValue))
            {
                MessageBox.Show("Please Input Numeric Values Only");
                return;
            }

            //Verify values are > 0 & < 1,000,000
            if((iValue <= 0 || jValue <= 0) || (iValue >= 1000000 || jValue >= 1000000))
            {
                MessageBox.Show("Please ensure inputs are greater than 0, and less than 1,000,000");
                iValue = 0;
                jValue = 0;
                return;
            }


            int minVal;
            int maxVal;
            vList = new List<Values>();

            //Find out which input values are the minimum and maximum values
            if (iValue < jValue)
            {
                minVal = iValue;
                maxVal = jValue;
            }
            else
            {
                minVal = jValue;
                maxVal = iValue;
            }

            //Now that we have the min/max values, create classes for all values
            //inbetween the min/max values.
            for (int i = minVal; i <= maxVal; i++)
            {
                Values v = new Values();
                v.originalVal = i;
                v.val = i;
                v.cycleLength = 1;

                vList.Add(v);
            }

            //Perform the algorithm for each class value.
            foreach(Values v in vList)
            {
                theProblem(v);
            }
            
            //Sort the vList by cycle length
            vList.Sort((x, y) => y.cycleLength.CompareTo(x.cycleLength));

            //Display the outcome in the Output Textbox.
            txtBx_Output.Text = minVal + " " + maxVal + " " + vList[0].cycleLength;

            //These values are no longer needed.
            minVal = 0;
            maxVal = 0;
            iValue = 0;
            jValue = 0;
        }

        private void theProblem(Values v)
        {
            //Attain the data from the Values Class
            int val = v.val;
            int num = v.cycleLength + 1;

            //Return if the value is 1
            if (val == 1)
            {
                return;
            }

            //If the value is odd, put the value through the equation 3n+1
            if (val % 2 != 0)
            {
                val = (3 * val) + 1;
            }
            //If the value is even, divide the value by 2
            else
            {
                val = (val / 2);
            }         

            //Assign the new value and cycleLength to the Value Class
            v.val = val;
            v.cycleLength = num;

            //Clear the local params
            val = 0;
            num = 0;

            //Recursively call this method.
            theProblem(v);
        }

        //Close this application.
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            vList.Clear();
            this.Close();
        }
    }
}
