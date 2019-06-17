using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";

            int nrzestawu = 1;
                        
            string iloscdzialantext = textBox1.Text;

            try
            {
                int iloscdzialan = Int32.Parse(iloscdzialantext);

                string[] lines = File.ReadAllLines(textBox2.Text);

                foreach (string line in lines)
                {
                    if (line.IndexOf("a=") != -1 && line.IndexOf("b=") != -1 && iloscdzialan >= 0)
                    {
                        richTextBox1.AppendText("Zestaw " + nrzestawu + ". \r\n");
                        int indexa = line.IndexOf("a=");
                        int indexb = line.IndexOf("b=");
                        int indexend = line.IndexOf("/");
                        string liczbaa = line.Substring((indexa + 3), (indexb - indexa - 5));
                        richTextBox1.AppendText("Liczba a=" + liczbaa + "\r\n");
                        string liczbab = line.Substring((indexb + 3), (indexend - indexb - 4));
                        richTextBox1.AppendText("Liczba b=" + liczbab + "\r\n");
                        try
                        {
                            double a = Double.Parse(liczbaa);
                            double b = Double.Parse(liczbab);
                            for (int i = 1; i <= iloscdzialan; i++)
                            {
                                if (comboBox1.Text == "mnożenie")
                                {
                                    richTextBox1.AppendText(a + "*" + b + "=" + (a * b) + "\r\n");
                                    b = a * b;
                                }
                                else
                                {
                                    if (comboBox1.Text == "dzielenie")
                                    {
                                        if (b != 0)
                                        {
                                            richTextBox1.AppendText(a + "/" + b + "=" + (a / b) + "\r\n");
                                            b = a / b;
                                        }
                                        else
                                        {
                                            richTextBox1.AppendText("Dzielenie przez zero jest niedozwolone. \r\n");
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        if (comboBox1.Text == "potęgowanie")
                                        {
                                            if (Double.IsInfinity(Math.Pow(a, b)) || Double.IsInfinity(b))
                                            {
                                                richTextBox1.AppendText("Kolejny wynik jest za duży aby go wypisać. \r\n");
                                                break;
                                            }
                                            else
                                            {
                                                richTextBox1.AppendText(a + "^" + b + "=" + (Math.Pow(a, b)) + "\r\n");
                                                b = Math.Pow(a, b);
                                            }
                                        }
                                        else
                                        {
                                            if (comboBox1.Text == "odejmowanie")
                                            {
                                                richTextBox1.AppendText(a + "-" + b + "=" + (a - b) + "\r\n");
                                                b = a - b;
                                            }
                                            else richTextBox1.Text = "Wybrano błędne działanie. \r\n";
                                        }
                                    }
                                }
                            }
                        }
                        catch (FormatException)
                        {
                            richTextBox1.AppendText("Błędne dane w pliku xml. \r\n");
                        }

                        nrzestawu = nrzestawu + 1;

                    }
                }
            }
            catch (FormatException)
            {
                if (comboBox1.Text == "mnożenie" || comboBox1.Text == "dzielenie" || comboBox1.Text == "potęgowanie" || comboBox1.Text == "odejmowanie")
                    richTextBox1.Text = "Wpisano błędną ilość działań. \r\n";
                else
                {
                    richTextBox1.Text = "Wybrano błędne działanie i wpisano błędną ilość działań. ";
                }
            }

            catch (System.IO.FileNotFoundException)
            {
                richTextBox1.Text = "Plik o podanej ścieżce nie istnieje. \r\n";
            }

            catch (System.ArgumentException)
            {
                richTextBox1.Text = "Nie podano ścieżki do pliku.  \r\n";
            }

            if (nrzestawu == 1)
            {
                richTextBox1.AppendText("Brak zmiennych a i b do przetworzenia. \r\n");
            }

            string path = @"log.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(richTextBox1.Text);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.Write(richTextBox1.Text);
                }
            }


        }

        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
