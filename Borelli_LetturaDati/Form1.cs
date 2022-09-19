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

        }
        public static string Funzione1e2(int daDoveVieni)
        {
            bool helo = true;
            int a = -1;
            int lunghezza;
            using (StreamReader sr = new StreamReader(@"dati.csv", true))
            {
                string line = sr.ReadLine();
                lunghezza = line.Length;
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();

                    if (a != -1)//se non è prima volta
                    {
                        if (a != line.Length)
                        {
                            helo = false;
                            if (daDoveVieni == 1 && a < line.Length)
                                lunghezza = line.Length;
                        }
                    }
                    a = line.Length;
                }
                if (!helo && daDoveVieni == 0)//primi due if se sto controllando che siano lunghi uguali
                    return "LE LUNGHEZZE SONO DIVERSE";
                else if (helo && daDoveVieni == 0)
                    return "LE LUNGHEZZE SONO UGUALI";
                else//mi interessa solo la lunghezza maggiore
                    return $"{line.Length}";
            }
        }
    }
}
