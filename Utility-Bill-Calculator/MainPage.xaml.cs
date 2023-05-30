using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Utility_Bill_Calculator
{
    public partial class MainPage : ContentPage
    {
        // list of data
        private List<String> listOfProvinces = new List<String>() 
        { 
        "AB",
        "BC",
        "MB",
        "NB",
        "NL",
        "NS",
        "ON",
        "PE",
        "QC",
        "SK"
        };
        public MainPage()
        {
            InitializeComponent();
            province.ItemsSource= listOfProvinces;
        }

        // Province selected from Picker
        void province_selected(System.Object sender,Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            Console.WriteLine($"Selcted Item Index: {e.SelectedItemIndex}");
            Console.WriteLine($"Selected Item: {e.SelectedItem.ToString()}");
        }

        // Calculate Btn clicked
        void Btn_Calculate(System.Object sender,System.EventArgs e)
        {
            Console.WriteLine($"Calculate Button Pressed");

            // Get the Province name
            int province_index = province.SelectedIndex;
            double dayTimeCharges,eveningCharges;
            if (province_index == -1)
            {
                Console.WriteLine("You must choose a Province!!");
                errors.Text = "You must choose a Province!!";
            }
            else if (string.IsNullOrEmpty(dTime.Text)||string.IsNullOrEmpty(eTime.Text)){
                Console.WriteLine("Empty Fields!!");
                errors.Text = "You must enter valid usage values";
            }
            else
            {
                bool success_day=Double.TryParse(dTime.Text,out dayTimeCharges);
                bool success_evening = Double.TryParse(eTime.Text, out eveningCharges);
                Console.WriteLine($"DayTime value is: ${dTime.Text}");
                Console.WriteLine($"DayTime value is: ${eTime.Text}");
                if(success_day && success_evening)
                {
                    titleForBreakdown.Text = "Charge Breakdwon";
                    dTime.Text = $"Daytime Usage Charges: ${dayTimeCharges}";
                    dTime.Text = $"Daytime Usage Charges: ${eveningCharges}";
                    totalCharges.Text = $"Total Charges: ${dayTimeCharges+eveningCharges}";
                    salesTax.Text = $"Sales Tax(13)%: $7.67";
                    rebate.Text = $"Environmental Rebate: -$51.3";
                }
                else
                {

                    Console.WriteLine("Incorrect values!!");
                    errors.Text = "You must enter valid usage values";
                }
            }
        }
        // Reset Btn clicked
        void Btn_Reset(System.Object sender,System.EventArgs e)
        {
            Console.WriteLine($"Reset Button Pressed");
            province.SelectedIndex = -1;
            dTime.Text = "";
            eTime.Text = "";
            errors.Text = "";
            totalCharges.Text = "";
            salesTax.Text = "";
            rebate.Text = "";
        }
    }
}
