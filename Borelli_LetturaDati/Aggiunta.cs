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
    public partial class Aggiunta : Form
    {
        public int numm { get; set; }
        public Aggiunta()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)//ok
        {
            Scrivi(textBox1.Text.ToUpper(), textBox2.Text.ToUpper(), textBox3.Text.ToUpper(), textBox4.Text.ToUpper(), textBox5.Text.ToUpper(), textBox6.Text.ToUpper(), textBox7.Text.ToUpper(), textBox8.Text.ToUpper(), numm, @"dati.csv");
            this.Close();        
        }
        public static void Scrivi(string cmp1, string cmp2, string cmp3, string cmp4, string cmp5, string cmp6, string cmp7, string cmp8, int numm, string filename)
        {
            var f = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryWriter writer = new BinaryWriter(f);
            int numRecord = (Convert.ToInt32(f.Length) / numm);//trovo numero da mettere
            f.Seek(numm*numRecord, SeekOrigin.Begin);
            string tot = $"{$"{numRecord+1},{cmp1},{cmp2},{cmp3},{cmp4},{cmp5},{cmp6},{cmp7},{cmp8}".PadRight(numm - 1)}#";
            if (tot.Length <= numm)
            {
                writer.Write(tot.ToCharArray());
                MessageBox.Show("Aggiunto con successo");
            }
            else
                MessageBox.Show("Inserire valori più corti");
            f.Close();
        }
    }
}
