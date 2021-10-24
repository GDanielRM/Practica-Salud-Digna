using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prueba_SaludDigna_DanielRodriguez
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bienvenido");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "C:\\";
            openFileDialog2.Filter = "Archivos de texto (*.txt)|*.txt";

            if (openFileDialog2.ShowDialog() == DialogResult.OK) {
                try
                {
                    string file = openFileDialog2.FileName;

                    string[] lines = File.ReadAllLines(file);

                    if (lines.Length <= 1)
                    {
                        throw new Exception("El archivo seleccionado no cumple con los requisitos y parametros necesarios para correr la prueba");
                    }

                    int totalPlayer1 = 0;
                    int totalPlayer2 = 0;

                    int cont = 1;
                    foreach (String line in lines)
                    {
                        if (cont > 1)
                        {
                            string[] data = line.Split(" ");
                            totalPlayer1 += Int32.Parse(data[0]);
                            totalPlayer2 += Int32.Parse(data[1]);
                        }
                        cont++;
                    }

                    int winner = getWinner(totalPlayer1, totalPlayer2);
                    

                    int vantage = Math.Abs(totalPlayer1 - totalPlayer2);

                    string[] linesResponceTxt = { winner + " " + vantage };

                    string outputFile = @"C:\prueba1_salida.txt";

                    System.IO.File.WriteAllLines(outputFile, linesResponceTxt);

                    MessageBox.Show("El resultado de la prueba se encuentra en la ruta: " + outputFile);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog2.InitialDirectory = "C:\\";
            openFileDialog2.Filter = "Archivos de texto (*.txt)|*.txt";

            if (openFileDialog2.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string file = openFileDialog2.FileName;

                    string[] lines = File.ReadAllLines(file);

                    if (lines.Length <= 3)
                    {
                        throw new Exception("El archivo seleccionado no cumple con los requisitos y parametros necesarios para correr la prueba");
                    }


                    string instruction1 = lines[1];
                    string instruction2 = lines[2];
                    char[] dirtyMessage = lines[3].ToCharArray();

                    char lastChar = '-';
                    string message = null;
                    int cont = 1;
                    foreach (char item in dirtyMessage)
                    {
                        if (item != lastChar)
                        {
                            message += item;
                            lastChar = item;
                        }
                        //MessageBox.Show(message);

                        cont++;
                    }

                    string responce1 = "NO";
                    string responce2 = "NO";

                    if (message.Contains(instruction1))
                    {
                        responce1 = "SI";
                    }

                    if (message.Contains(instruction2))
                    {
                        responce2 = "SI";
                    }

                    string[] linesResponceTxt = { responce1, responce2};

                    string outputFile = @"C:\prueba2_salida.txt";

                    System.IO.File.WriteAllLines(outputFile, linesResponceTxt);

                    MessageBox.Show("El resultado de la prueba se encuentra en la ruta: " + outputFile);
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }
        public int getWinner(int p1, int p2)
        {
            int win = 0;
            if (p1 > p2) {
                win = 1;
            } else if (p2 > p1) {
                win = 2;
            } else if (p1 == p2) {
                /*Debido a que la prueba especifica que "siempre existe un ganador unico", pero no dice
                como definir a este ganador, me decidí a elegir al gandor al azar en caso de un empate*/
                int rand = new Random().Next(1, 3);
                win = rand;
            }

            return win;
        }
    }
}
