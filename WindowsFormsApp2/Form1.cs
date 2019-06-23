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
using System.Xml.Linq;

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

            richTextBox2.Text = "";
                        
            int iloscdzialan;

            try
            {
                iloscdzialan = Int32.Parse(textBox1.Text);
            }
            catch (Exception)
            {
                richTextBox2.Text = "Podano błędną ilość działań. \r\n";
                return;
            }

            int nrzestawu = 1;
            
            string katalogDocelowy = textBox3.Text;

            string pathLog = Path.Combine(katalogDocelowy, "log.txt");

            string pathError = Path.Combine(katalogDocelowy, "log_errors.txt");

            XDocument lines;

            try
            {
                lines = XDocument.Load(textBox2.Text);
            }
            catch (Exception)
            {
                richTextBox2.Text = "błąd";
                return;
            }
            

            foreach (XElement line in lines.Root.Elements())
            {
                string A = line.Attribute("a").Value;
                string B = line.Attribute("b").Value;
                double a = Double.Parse(A);
                double b = Double.Parse(B);
                try
                {

                    if (iloscdzialan>=1)
                    {
                        richTextBox1.AppendText("Zestaw " + nrzestawu + ". \r\n");
                        for (int i = 1; i <= iloscdzialan; i++)
                        {
                            if (comboBox1.Text == "mnożenie")
                            {
                                double c = a * b;
                                string newLine = a + "*" + b + "=" + c + "\r\n";
                                richTextBox1.AppendText(a + "*" + b + "=" + c + "\r\n");
                                using (StreamWriter sw = File.AppendText(pathLog))
                                {
                                    sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                }
                                b = c;
                            }
                            else
                            {
                                if (comboBox1.Text == "dzielenie")
                                {
                                    if (b != 0)
                                    {
                                        double c = a / b;
                                        string newLine = a + "/" + b + "=" + c + "\r\n";
                                        richTextBox1.AppendText(newLine);
                                        using (StreamWriter sw = File.AppendText(pathLog))
                                        {
                                            sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                        }
                                        b = c;
                                    }
                                    else
                                    {
                                        richTextBox1.AppendText("Dzielenie przez zero jest niedozwolone. \r\n");
                                        string blad = "W zestawie " + nrzestawu + " w powtorzeniu " + i + " wystąpiło dzielenie przez zero, które jest niedozwolone. \r\n";
                                        richTextBox2.AppendText(blad);
                                        using (StreamWriter sw = File.AppendText(pathError))
                                        {
                                            sw.Write(blad);
                                        }
                                        break;
                                    }
                                }
                                else
                                {
                                    if (comboBox1.Text == "potęgowanie")
                                    {
                                        double c = Math.Pow(a, b);
                                        if (Double.IsInfinity(c))
                                        {
                                            richTextBox1.AppendText("Kolejny wynik jest za duży aby go wypisać. \r\n");
                                            string blad = "W zestawie " + nrzestawu + " w powtorzeniu " + i + " wynik jest za duży aby go wypisać. \r\n";
                                            richTextBox2.AppendText(blad);
                                            using (StreamWriter sw = File.AppendText(pathError))
                                            {
                                                sw.Write(blad);
                                            }
                                            break;
                                        }
                                        else
                                        {
                                            string newLine = a + "^" + b + "=" + c + "\r\n";
                                            richTextBox1.AppendText(newLine);
                                            using (StreamWriter sw = File.AppendText(pathLog))
                                            {
                                                sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                            }
                                            b = c;
                                        }
                                    }
                                    else
                                    {
                                        if (comboBox1.Text == "odejmowanie")
                                        {
                                            double c = a - b;
                                            string newLine = a + "-" + b + "=" + c + "\r\n";
                                            richTextBox1.AppendText(newLine);
                                            using (StreamWriter sw = File.AppendText(pathLog))
                                            {
                                                sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                            }
                                            b = c;
                                        }
                                        else
                                        {
                                            if (comboBox1.Text == "dodawanie")
                                            {
                                                double c = a + b;
                                                string newLine = a + "+" + b + "=" + c + "\r\n";
                                                richTextBox1.AppendText(newLine);
                                                using (StreamWriter sw = File.AppendText(pathLog))
                                                {
                                                    sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                                }
                                                b = c;
                                            }
                                            else
                                            {
                                                if (b != 0)
                                                {
                                                    double c = a % b;
                                                    string newLine = a + " modulo " + b + "=" + c + "\r\n";
                                                    richTextBox1.AppendText(newLine);
                                                    using (StreamWriter sw = File.AppendText(pathLog))
                                                    {
                                                        sw.Write("Zestaw " + nrzestawu + " powtórzenie " + i + ": " + newLine);
                                                    }
                                                    b = c;
                                                }
                                                else
                                                {
                                                    string blad = "W zestawie " + nrzestawu + " w powtorzeniu " + i + " wystąpiło dzielenie przez zero, które jest niedozwolone. \r\n";
                                                    richTextBox1.AppendText("Dzielenie przez zero jest niedozwolone. \r\n");
                                                    richTextBox2.AppendText(blad);
                                                    using (StreamWriter sw = File.AppendText(pathError))
                                                    {
                                                        sw.Write(blad);
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        richTextBox2.Text = "Podano błędną ilość działań\r\n";
                        using (StreamWriter sw = File.AppendText(pathError))
                        {
                            sw.Write("Podano błędną ilość działań\r\n");
                        }
                        break;
                    }
                }
                catch (System.FormatException)
                {
                    richTextBox2.Text = "Podano błędną ilość działań\r\n";
                    using (StreamWriter sw = File.AppendText(pathError))
                    {
                        sw.Write("Podano błędną ilość działań\r\n");
                    }
                    break;
                }
                catch (System.ArgumentException)
                {
                    richTextBox2.Text = "Podano błędną ścieżkę do pliku xml\r\n";
                    using (StreamWriter sw = File.AppendText(pathError))
                    {
                        sw.Write("Podano błędną ścieżkę do pliku xml\r\n");
                    }
                    break;
                }
                nrzestawu = nrzestawu + 1;
            }
        }
    }
}



