using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Autoverhuur
{
    public partial class Form1 : Form
    {
        private CarRental _newCarSubscription1, _newCarSubscription2;
        private List<CarRental> carSubscriptions;
        private DateTime dateStart, dateEnd;
        private double _priceTotal, milage, price, fuelSpent, capacity = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            carSubscriptions = new List<CarRental>();

            _newCarSubscription1 = new CarRental("personenauto", 50, 0.20, 14, 60);
            _newCarSubscription2 = new CarRental("personenbusje", 95, 0.30, 12, 70);

            carSubscriptions.Add(_newCarSubscription1);
            carSubscriptions.Add(_newCarSubscription2);

            // Fills labels
            int counterSubscriptions = 0;
            foreach (Label labelSubscription in panelSubscriptions.Controls)
            {
                //if (control.GetType() == typeof(Label))
                labelSubscription.Text = carSubscriptions[counterSubscriptions].Name;
                counterSubscriptions++;
            }

            // Fills labels
            int counterPrices = 0;
            foreach (Label labelPrice in panelPrices.Controls)
            {
                labelPrice.Text = carSubscriptions[counterPrices].Price.ToString();
                counterPrices++;
            }

            // Fills labels
            int counterMilages = 0;
            foreach (Label labelMilage in panelMilages.Controls)
            {
                labelMilage.Text = carSubscriptions[counterMilages].DistangeCharge.ToString("0.00");
                counterMilages++;
            }

            // Fills numericupdowns
            foreach (NumericUpDown control in panelYear.Controls)
            {
                control.Maximum = DateTime.Today.Year + 20;
                control.Minimum = DateTime.Today.Year;
                control.Value = DateTime.Today.Year;
            }

            foreach (NumericUpDown control in panelMonth.Controls)
            {
                control.Minimum = DateTime.Today.Month;
                control.Value = DateTime.Today.Month;
            }

            foreach (NumericUpDown control in panelDay.Controls)
            {
                control.Minimum = DateTime.Today.Day;
                control.Value = DateTime.Today.Day;
            }

            // X2
            labelTicketsTotal.Text = $@"afstand * kilometerheffing + dagen * dagprijs";
            labelPriceTotal.Text = $@"Totaal: € {Math.Round(_priceTotal, 2):0.00},-";
        }

        private void ButtonCalculate_Click(object sender, EventArgs e)
        {
            price = 0;
            milage = 0;
            _priceTotal = 0;
            // Checks if any car is selected
            if (CheckSelection())
            {
                // Checks if the correct dates are selected (in order)
                if (ValidateDate())
                {
                    int days = (int)(dateEnd - dateStart).TotalDays;
                    if (int.TryParse(textBoxDistance.Text, out int resultDistance) && resultDistance > 0)
                    {
                        // Checks which car is selected
                        if (radioButtonCar.Checked)
                        { 
                            milage = _newCarSubscription1.Milage;
                            price = _newCarSubscription1.Price;
                            capacity = _newCarSubscription1.FuelCapacity;
                            
                            resultDistance = resultDistance - (100 * days);
                            if (resultDistance > 0)
                            {
                                _priceTotal += resultDistance * _newCarSubscription1.DistangeCharge;
                                resultDistance = resultDistance + (100 * days);
                            }
                            else
                            {
                                resultDistance = 0;
                            }

                            _priceTotal += days * _newCarSubscription1.Price;
                        }
                        else if (radioButtonVan.Checked)
                        {
                            milage = _newCarSubscription2.Milage;
                            price = _newCarSubscription2.Price;
                            capacity = _newCarSubscription2.FuelCapacity;

                            _priceTotal += resultDistance * _newCarSubscription2.DistangeCharge 
                                           + days * _newCarSubscription2.Price;
                        }

                        // Calculates burnt fuel (not clear what to charge; times refueled * car capacity; fuel amount what is actually burnt distance / milage
                        fuelSpent = 1 * resultDistance / milage;
                        fuelSpent = fuelSpent / capacity;
                        fuelSpent = Math.Ceiling(fuelSpent - 1);
                        fuelSpent = fuelSpent * capacity;

                        // Displays costs/ charges, car fuel costs not yet implemented
                        labelTicketsTotal.Text = $@"afstand {resultDistance  - (100 * days)} km * kilometerheffing € {milage:0.00},- + dagen {days} * dagprijs € {price},-";
                        labelPriceTotal.Text = $@"Totaal: € {_priceTotal:0.00},-";
                    }
                    else
                    {
                        MessageBox.Show($@"Distance must contain only numbers > 0");
                    }
                }
            }
            else
            {
                MessageBox.Show($@"Please select a rental car");
            }
        }

        private bool CheckSelection()
        {
            int radioButtonCounter = 0;
            foreach (RadioButton control in panelSelection.Controls.OfType<RadioButton>())
            {
                if (control.Checked)
                {
                    radioButtonCounter++;
                }
            }

            if (radioButtonCounter == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ValidateDate()
        {
            int checkDay1 = DateTime.DaysInMonth((int)numericUpDownYear1.Value, (int)numericUpDownMonth1.Value);
            int checkDay2 = DateTime.DaysInMonth((int)numericUpDownYear2.Value, (int)numericUpDownMonth2.Value);
            
            if ((int)numericUpDownDay1.Value <= checkDay1 && (int)numericUpDownDay2.Value <= checkDay2)
            {
                dateStart = new DateTime((int)numericUpDownYear1.Value, (int)numericUpDownMonth1.Value, (int)numericUpDownDay1.Value);
                dateEnd = new DateTime((int)numericUpDownYear2.Value, (int)numericUpDownMonth2.Value, (int)numericUpDownDay2.Value);
                if (dateStart < dateEnd)
                {
                    
                    return true;
                }
                else
                {
                    MessageBox.Show($@"Please fill the dates in the correct order \nstart date < end date");
                    return false;
                }
                
            }
            else
            {
                MessageBox.Show($@"Please select the correct day of the month");
                return false;
            }
        }
    }
}
