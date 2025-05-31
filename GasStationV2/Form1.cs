using System;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace GasStationV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void numericUpDown2_ValueChanged_1(object sender, EventArgs e)
        {

        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }



        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        public class Fuels
        {
            public string type { get; set; }
            public double price { get; set; }
            public double addedAmount { get; set; }
            public double saledAmount { get; set; }
            public double totalAmount { get; set; }

            public Fuels(string type = "", double price = 0, double add = 0, double sale = 0, double total = 0)
            {
                this.type = type;
                this.price = price;
                this.addedAmount = add;
                saledAmount = sale;
                totalAmount = total;
            }
        }
        Fuels gasoline95 = new Fuels("gasoline95");
        Fuels gasoline97 = new Fuels("gasoline97");
        Fuels diesel = new Fuels("diesel");
        Fuels euroDiesel = new Fuels("euroDiesel");
        Fuels lpg = new Fuels("lpg");

        string[] storageInfo = new string[5];
        string[] priceInfo;
        private void ReadStorage()
        {
            storageInfo = File.ReadAllLines(Application.StartupPath + "\\storage.txt");

            gasoline95.totalAmount = Convert.ToDouble(storageInfo[0]);
            gasoline97.totalAmount = Convert.ToDouble(storageInfo[1]);
            diesel.totalAmount = Convert.ToDouble(storageInfo[2]);
            euroDiesel.totalAmount = Convert.ToDouble(storageInfo[3]);
            lpg.totalAmount = Convert.ToDouble(storageInfo[4]);
        }
        private void WriteStorage()
        {
            label6.Text = gasoline95.totalAmount.ToString("n") + " L";
            label7.Text = gasoline97.totalAmount.ToString("n") + " L";
            label8.Text = diesel.totalAmount.ToString("n") + " L";
            label9.Text = euroDiesel.totalAmount.ToString("n") + " L";
            label10.Text = lpg.totalAmount.ToString("n") + " L";
        }

        private void ReadPrices()
        {
            priceInfo = File.ReadAllLines(Application.StartupPath + "prices.txt");
            gasoline95.price = Convert.ToDouble(priceInfo[0]);
            gasoline97.price = Convert.ToDouble(priceInfo[1]);
            diesel.price = Convert.ToDouble(priceInfo[2]);
            euroDiesel.price = Convert.ToDouble(priceInfo[3]);
            lpg.price = Convert.ToDouble(priceInfo[4]);
        }
        private void WritePrices()
        {
            label15.Text = gasoline95.price.ToString("n") + " TL";
            label14.Text = gasoline97.price.ToString("n") + " TL";
            label13.Text = diesel.price.ToString("n") + " TL";
            label12.Text = euroDiesel.price.ToString("n") + " TL";
            label11.Text = lpg.price.ToString("n") + " TL";
        }
        private void UpdateProgressBar()
        {
            progressBar2.Value = Convert.ToUInt16(gasoline95.totalAmount);
            progressBar3.Value = Convert.ToInt16(gasoline97.totalAmount);
            progressBar4.Value = Convert.ToInt16(diesel.totalAmount);
            progressBar5.Value = Convert.ToInt16(euroDiesel.totalAmount);
            progressBar6.Value = Convert.ToInt16(lpg.totalAmount);
        }
        private void NumericUpDownValue()
        {
            numericUpDown1.Maximum = decimal.Parse(gasoline95.totalAmount.ToString());
            numericUpDown2.Maximum = decimal.Parse(gasoline97.totalAmount.ToString());
            numericUpDown3.Maximum = decimal.Parse(diesel.totalAmount.ToString());
            numericUpDown4.Maximum = decimal.Parse(euroDiesel.totalAmount.ToString());
            numericUpDown5.Maximum = decimal.Parse(lpg.totalAmount.ToString());

        }
        private void NumericDisable(NumericUpDown num, int dec)
        {
            num.Enabled = false;
            num.DecimalPlaces = dec;
            num.Increment = 0.1M;
            num.ReadOnly = true;
        }
        private void NumericDisable(NumericUpDown num)
        {
            num.Enabled = false;
        }
        private void NumericEnable(NumericUpDown num)
        {
            num.Enabled = true;
        }
        string[] fuelTypes = { "Gasoline(95)", "Gasoline(97)", "Diesel", "Euro Diesel", "LPG" };
        private void Form1_Load(object sender, EventArgs e)
        {
            NumericSettings(numericUpDown1);
            NumericSettings(numericUpDown2);
            NumericSettings(numericUpDown3);
            NumericSettings(numericUpDown4);
            NumericSettings(numericUpDown5);

            this.Text = "GAS STATION OTOMATION";
            progressBar2.Maximum = 1000;
            progressBar3.Maximum = 1000;
            progressBar4.Maximum = 1000;
            progressBar5.Maximum = 1000;
            progressBar6.Maximum = 1000;

            ReadStorage();
            WriteStorage();
            ReadPrices();
            WritePrices();
            UpdateProgressBar();
            NumericUpDownValue();


            comboBox1.Items.AddRange(fuelTypes);

            NumericDisable(numericUpDown1, 2);
            NumericDisable(numericUpDown2, 2);
            NumericDisable(numericUpDown3, 2);
            NumericDisable(numericUpDown4, 2);
            NumericDisable(numericUpDown5, 2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fuels[] fuels = { gasoline95, gasoline97, diesel, euroDiesel, lpg };
            TextBox box = textBox1;
            for (int i = 0; i < fuels.Length; i++)
            {
                Fuels fuel = fuels[i];
                try
                {
                    switch (i)
                    {
                        case 0:
                            box = textBox1;
                            break;
                        case 1:
                            box = textBox2;
                            break;
                        case 2:
                            box = textBox3;
                            break;
                        case 3:
                            box = textBox4;
                            break;
                        case 4:
                            box = textBox5;
                            break;
                    }

                    fuel.addedAmount = Convert.ToDouble(box.Text);
                    if (fuel.totalAmount + fuel.addedAmount > 1000 || fuel.addedAmount < 0)
                        box.Text = "ERROR!";
                    else
                    {
                        storageInfo[i] = Convert.ToString(fuel.totalAmount + fuel.addedAmount);
                        box.Text = "Applied.";
                    }
                }
                catch (Exception)
                {
                    box.Text = "ERROR!";
                }
            }

            File.WriteAllLines(Application.StartupPath + "\\storage.txt", storageInfo);
            ReadStorage();
            WriteStorage();
            UpdateProgressBar();
            NumericUpDownValue();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fuels[] fuels = { gasoline95, gasoline97, diesel, euroDiesel, lpg };
            TextBox box = textBox6;
            for (int i = 0; i < fuels.Length; i++)
            {
                try
                {
                    switch (i)
                    {
                        case 0:
                            box = textBox6;
                            break;
                        case 1:
                            box = textBox7;
                            break;
                        case 2:
                            box = textBox8;
                            break;
                        case 3:
                            box = textBox9;
                            break;
                        case 4:
                            box = textBox10;
                            break;

                    }
                    fuels[i].price = fuels[i].price + (fuels[i].price * Convert.ToDouble(box.Text) / 100);
                    box.Text = "Updated.";
                    priceInfo[i] = Convert.ToString(fuels[i].price);
                }
                catch (Exception)
                {
                    box.Text = "ERROR!";
                }
            }
            File.WriteAllLines(Application.StartupPath + "\\prices.txt", priceInfo);
            ReadPrices();
            WritePrices();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    NumericEnable(numericUpDown1);
                    NumericDisable(numericUpDown2);
                    NumericDisable(numericUpDown3);
                    NumericDisable(numericUpDown4);
                    NumericDisable(numericUpDown5);
                    break;

                case 1:
                    NumericDisable(numericUpDown1);
                    NumericEnable(numericUpDown2);
                    NumericDisable(numericUpDown3);
                    NumericDisable(numericUpDown4);
                    NumericDisable(numericUpDown5);
                    break;

                case 2:
                    NumericDisable(numericUpDown1);
                    NumericDisable(numericUpDown2);
                    NumericEnable(numericUpDown3);
                    NumericDisable(numericUpDown4);
                    NumericDisable(numericUpDown5);
                    break;

                case 3:
                    NumericDisable(numericUpDown1);
                    NumericDisable(numericUpDown2);
                    NumericDisable(numericUpDown3);
                    NumericEnable(numericUpDown4);
                    NumericDisable(numericUpDown5);
                    break;

                case 4:
                    NumericDisable(numericUpDown1);
                    NumericDisable(numericUpDown2);
                    NumericDisable(numericUpDown3);
                    NumericDisable(numericUpDown4);
                    NumericEnable(numericUpDown5);
                    break;
            }
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            label27.Text = "_________________";
            CalculateTotal();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Fuels[] fuels = { gasoline95, gasoline97, diesel, euroDiesel, lpg };
            NumericUpDown numBox = numericUpDown1;
            for (int i = 0; i < fuels.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        numBox = numericUpDown1;
                        break;
                    case 1:
                        numBox = numericUpDown2;
                        break;
                    case 2:
                        numBox = numericUpDown3;
                        break;
                    case 3:
                        numBox = numericUpDown4;
                        break;
                    case 4:
                        numBox = numericUpDown5;
                        break;
                }

                fuels[i].saledAmount = double.Parse(numBox.Value.ToString());
                if (numBox.Enabled == true)
                {
                    fuels[i].totalAmount = fuels[i].totalAmount - fuels[i].saledAmount;
                    label27.Text = (fuels[i].saledAmount * fuels[i].price).ToString() + " TL";
                    storageInfo[i] = fuels[i].totalAmount.ToString();
                    File.WriteAllLines(Application.StartupPath + "\\storage.txt", storageInfo);
                    ReadStorage();
                    WriteStorage();
                    UpdateProgressBar();
                    NumericUpDownValue();
                    break;
                }
            }
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
        }

        private void CalculateTotal()
        {
            string selectedFuel = comboBox1.SelectedItem?.ToString();
            double quantity = 0;
            double unitPrice = 0;

            switch (selectedFuel)
            {
                case "GASOLINE(95)":
                    quantity = (double)numericUpDown1.Value;
                    unitPrice = gasoline95.price;
                    break;
                case "GASOLINE(97)":
                    quantity = (double)numericUpDown2.Value;
                    unitPrice = gasoline97.price;
                    break;
                case "DIESEL":
                    quantity = (double)numericUpDown3.Value;
                    unitPrice = diesel.price;
                    break;
                case "EURO DIESEL":
                    quantity = (double)numericUpDown4.Value;
                    unitPrice = euroDiesel.price;
                    break;
                case "LPG":
                    quantity = (double)numericUpDown5.Value;
                    unitPrice = lpg.price;
                    break;
                default:
                    label27.Text = "Yakýt türü seçiniz.";
                    return;
            }

            double total = quantity * unitPrice;
            label27.Text = "Toplam Tutar: " + total.ToString("C2");
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) => CalculateTotal();
private void numericUpDown2_ValueChanged(object sender, EventArgs e) => CalculateTotal();
private void numericUpDown3_ValueChanged(object sender, EventArgs e) => CalculateTotal();
private void numericUpDown4_ValueChanged(object sender, EventArgs e) => CalculateTotal();
private void numericUpDown5_ValueChanged(object sender, EventArgs e) => CalculateTotal();

        private void NumericSettings(NumericUpDown n)
        {
            n.ReadOnly = false;
            n.Maximum = 1000;
            n.DecimalPlaces = 2;
            n.ThousandsSeparator = true;
        }
   
    }
}
