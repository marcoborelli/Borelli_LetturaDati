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

namespace Borelli_LetturaDati
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            button3.Text = $"AUMENTA LUNGHEZZA [ATTUALE:{int.Parse(Funzione1e2(1))}]";
        }
        private void button1_Click(object sender, EventArgs e)//conrolla che lunghezze siano uguali
        {
            MessageBox.Show(Funzione1e2(0));
        }
        private void button2_Click(object sender, EventArgs e)//max riga
        {
            MessageBox.Show(Funzione1e2(1));
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox1.Text) > int.Parse(Funzione1e2(1)))
                {
                    int numm = int.Parse(textBox1.Text);
                    string helo = "";

                    var f = new FileStream(@"dati.csv", FileMode.Open, FileAccess.ReadWrite);
                    BinaryReader reader = new BinaryReader(f);

                    f.Seek(0, SeekOrigin.Begin);

                    while (f.Position < f.Length)
                        helo += Encoding.ASCII.GetString(reader.ReadBytes(1));

                    // MessageBox.Show($"\"{helo}\"");

                    f.Seek(0, SeekOrigin.Begin);

                    BinaryWriter writer = new BinaryWriter(f);
                    string[] fields = helo.Split('#');
                    for (int i = 0; i < fields.Length - 1; i++)
                        writer.Write($"{fields[i].PadRight(numm)}#".ToCharArray());

                    f.Close();

                    //System.IO.File.Delete(@"dati.csv");
                    //System.IO.File.Move(@"dati1.csv", @"dati.csv");
                }
                else
                    MessageBox.Show("Inserire un valore maggiore di quello attuale");
            }
            catch
            {
                MessageBox.Show("Inserire valori validi AAAA");
            }
            Form1_Load(sender, e);
        }
        public static string Funzione1e2(int daDoveVieni)
        {
            bool helo = true;
            string linetot = "";
            int lunghezza = 0;
            var f = new FileStream(@"dati.csv", FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            //string line = sr.ReadLine();
            //lunghezza = line.Length;
            while (f.Position < f.Length)
                linetot += Encoding.ASCII.GetString(reader.ReadBytes(1));

            string[] fields = linetot.Split('#');
            for (int i = 0; i < fields.Length - 1 - 1; i++)
            {
                if (i == 0)
                    lunghezza = fields[i].Length;

                if (fields[i].Length != fields[i + 1].Length)
                {
                    helo = false;
                    if (daDoveVieni == 1 && fields[i].Length < fields[i + 1].Length)
                        lunghezza = fields[i + 1].Length;
                }
            }

            f.Close();


            if (!helo && daDoveVieni == 0)//primi due if se sto controllando che siano lunghi uguali
                return "LE LUNGHEZZE SONO DIVERSE";
            else if (helo && daDoveVieni == 0)
                return "LE LUNGHEZZE SONO UGUALI";
            else//mi interessa solo la lunghezza maggiore
                return $"{lunghezza}";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VisualizzaElimina vis = new VisualizzaElimina();
            vis.numm = int.Parse(Funzione1e2(1))+1;
            this.Visible = false;
            vis.ShowDialog();
            this.Visible = true;
        }
    }
}
