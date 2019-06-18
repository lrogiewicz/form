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




        //this method is waaaaaaaaaaaay too long.
        //it should be handled in several smaller methods
        private void Button1_Click(object sender, EventArgs e) //What is button1? Add meaningful names to all methods 
        {
            richTextBox1.Text = ""; //what is richTextBox1? add meaningful names to all objects, variables, UI controls etc

            int nrzestawu = 1; //do not name *anything* in Polish. No comments, no code etc. Never, ever, especially at SDL:)
                        
            string iloscdzialantext = textBox1.Text; //use camelCase or PascalCase to split words

            try
            {
                int iloscdzialan = Int32.Parse(iloscdzialantext);

                string[] lines = File.ReadAllLines(textBox2.Text); //what if the XML is not 'pretty printed' and is all in one line?

                foreach (string line in lines)
                {
                    if (line.IndexOf("a=") != -1 && line.IndexOf("b=") != -1 && iloscdzialan >= 0)
                    {
                        richTextBox1.AppendText("Zestaw " + nrzestawu + ". \r\n");
                        int indexa = line.IndexOf("a=");
                        int indexb = line.IndexOf("b=");
                        int indexend = line.IndexOf("/");
                        string liczbaa = line.Substring((indexa + 3), (indexb - indexa - 5)); //what if there are more spaces or linebreaks in XML? or if attribute b is before a?
                        richTextBox1.AppendText("Liczba a=" + liczbaa + "\r\n");
                        string liczbab = line.Substring((indexb + 3), (indexend - indexb - 4));
                        richTextBox1.AppendText("Liczba b=" + liczbab + "\r\n");
                        try
                        {
                            double a = Double.Parse(liczbaa);
                            double b = Double.Parse(liczbab);
                            for (int i = 1; i <= iloscdzialan; i++)
                            {
                                if (comboBox1.Text == "mnożenie") //the combo box should not allow changing the text, it should be readonly
                                {
                                    richTextBox1.AppendText(a + "*" + b + "=" + (a * b) + "\r\n"); //repeating the same calculation (see comment in 'Potegowanie')
                                    b = a * b;//repeating the same calculation (see comment in 'Potegowanie')
                                }
                                else
                                {
                                    if (comboBox1.Text == "dzielenie") //and what if it's Dzielenie? or DZIELENIE?
                                    {
                                        if (b != 0)
                                        {
                                            richTextBox1.AppendText(a + "/" + b + "=" + (a / b) + "\r\n");
                                            b = a / b;
                                        }
                                        else
                                        {
                                            richTextBox1.AppendText("Dzielenie przez zero jest niedozwolone. \r\n"); //very good
                                            break;
                                        }

                                    }
                                    else
                                    {
                                        if (comboBox1.Text == "potęgowanie")
                                        {
                                            if (Double.IsInfinity(Math.Pow(a, b)) || Double.IsInfinity(b)) //it's good that you check if you are not 'out of range', however...
                                            //you are making the same calculation 3 times (or 2 times in case of other methods).
                                            //essentially, you are wasting 2-3 times too much resources - imagine that each calculation takes 10 second... or 10 minutes...?
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
                    richTextBox1.Text = "Wpisano błędną ilość działań. \r\n"; //use brackets for *all* 'ifs, elses, fors etc'. If you don't - trust me - you will have bugs sooner or later
                else
                {
                    richTextBox1.Text = "Wybrano błędne działanie i wpisano błędną ilość działań. ";
                }
            }

            catch (System.IO.FileNotFoundException)
            {
                richTextBox1.Text = "Plik o podanej ścieżce nie istnieje. \r\n";
            }

            catch (System.ArgumentException) //in your program, not always argument exception means 'missing file [ath'
            {
                richTextBox1.Text = "Nie podano ścieżki do pliku.  \r\n";
            }

            if (nrzestawu == 1) //so, if the first element is not OK, you print error.. and what if next elements are right/wrong?
            {
                richTextBox1.AppendText("Brak zmiennych a i b do przetworzenia. \r\n");
            }

            string path = @"log.txt";
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path)) //append text will create the file if it does not exist
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

        //do not leave any unused code in your files.
        private void Button2_Click(object sender, EventArgs e)
        {

        }

        private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //this is used, but... doesn't do anything, so - do not keep code that doesn't do anything.
        private void TextBox2_TextChanged_1(object sender, EventArgs e)
        {

        }
    }
}
