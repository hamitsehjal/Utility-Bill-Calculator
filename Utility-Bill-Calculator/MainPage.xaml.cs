using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.NetworkInformation;
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
        "NL",
        "ON",
        };

        // private fields
        private double utilityBill = 0.0;
        private double totalUsageCharges = 0.0;
        private double taxPercentage=0.0;
        private double taxAmount = 0.0;
        private const double REBATE_PERCENTAGE = 9.5;
        private double rebateAmount = 0.0;

        public MainPage()
            {
            InitializeComponent();
            province.ItemsSource = listOfProvinces;
            }

        // Province selected from Picker
        private void provinceSelected(System.Object sender, EventArgs e)
            {
            int selectedIndex = province.SelectedIndex;
            Console.WriteLine($"Selcted Item Index: {selectedIndex}");

            if (selectedIndex != -1)
                {

            string selectedValue = province.SelectedItem.ToString();

            Console.WriteLine($"Selected Item: {selectedValue}");

            // If Province is "BC", set the switch to True
            if (selectedValue.Equals("BC"))
                {

                isRenewable.IsToggled = true;
                }
            else
                {
                isRenewable.IsToggled = false;
                }
            }
         }
        
       

        // Calculate Btn clicked
        void Btn_Calculate(System.Object sender, System.EventArgs e)
            {
            Console.WriteLine($"Calculate Button Pressed");

            // Get the Province name
            int provinceIndex = province.SelectedIndex;

            Console.WriteLine($"Selcted Item Index: {provinceIndex}");

            double dayTimeCharges, eveningCharges;
            if (provinceIndex == -1)
                {
                Console.WriteLine("You must choose a Province!!");
                errors.Text = "You must choose a Province!!";
                }
            else if (string.IsNullOrEmpty(dTime.Text) || string.IsNullOrEmpty(eTime.Text))
                {
                Console.WriteLine("Empty Fields!!");
                errors.Text = "You must enter valid usage values";
                }
            else
                {
                 string provinceName = province.SelectedItem.ToString();
                 Console.WriteLine($"Selected Item: {provinceName}");
                errors.Text = "";
                bool success_day = Double.TryParse(dTime.Text, out dayTimeCharges);
                bool success_evening = Double.TryParse(eTime.Text, out eveningCharges);
                Console.WriteLine($"DayTime value is: ${dTime.Text}");
                Console.WriteLine($"DayTime value is: ${eTime.Text}");
                if (success_day && success_evening)
                    {
                    dayTimeCharges *= 0.314;
                    eveningCharges *= 0.186;
                    totalUsageCharges = dayTimeCharges + eveningCharges;

                    // if-else clause for sales Tax
                    if (provinceName.Equals("ON"))
                        {
                        taxPercentage = 13;
                        }
                    else if (provinceName.Equals("NL"))
                        {
                        taxPercentage = 15;
                        }
                    else if (provinceName.Equals("BC"))
                        {
                        taxPercentage = 12;
                        }
                    else
                        {
                        taxPercentage = 0;
                        }

                        taxAmount = (taxPercentage / 100) * totalUsageCharges;

                    //REBATE
                    if (provinceName.Equals("BC") || isRenewable.IsToggled == true)
                        {

                        rebateAmount = (REBATE_PERCENTAGE / 100) * totalUsageCharges;
                        }
                    frameForBill.IsVisible = true;
                    utilityBill = totalUsageCharges + taxAmount - rebateAmount;
                    Color redColor = Color.FromHex("#FF0000");
                    frameForBill.BackgroundColor = redColor;
                    titleForBreakdown.Text = "Charge Breakdwon";
                    dCharges.Text = $"Daytime Usage Charges: ${Math.Round(dayTimeCharges,2)}";
                    eCharges.Text = $"Daytime Usage Charges: ${Math.Round(eveningCharges,2)}";
                    totalCharges.Text = $"Total Charges: ${Math.Round(totalUsageCharges,2)}";
                    salesTax.Text = $"Sales Tax({taxPercentage})%: ${Math.Round(taxAmount, 2)}";
                    rebate.Text = $"Environmental Rebate: -${Math.Round(rebateAmount, 2)}";
                    finalBill.Text = $"You must Pay: ${Math.Round(utilityBill, 2)}";
                    }
                else
                    {

                    Console.WriteLine("Incorrect values!!");
                    errors.Text = "You must enter valid usage values";
                    }
                }
            }
        // Reset Btn clicked
        void Btn_Reset(System.Object sender, System.EventArgs e)
            {
            Console.WriteLine($"Reset Button Pressed");
            province.SelectedIndex = -1;
            titleForBreakdown.Text = "";
            dCharges.Text = "";
            eCharges.Text = "";
            dTime.Text = "";
            eTime.Text = "";
            errors.Text = "";
            totalCharges.Text = "";
            salesTax.Text = "";
            rebate.Text = "";
            isRenewable.IsToggled = true;
            finalBill.Text = "";
            }
        }   
    }
