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
    public partial class VisualizzaElimina : Form
    {
        public int numm { get; set; }
        Aggiunta agg = new Aggiunta();
        public VisualizzaElimina()
        {
            InitializeComponent();
        }
        private void VisualizzaElimina_Load(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("NUM", 50);
            listView1.Columns.Add("CAMPO 1", 70);
            listView1.Columns.Add("CAMPO 2", 70);
            listView1.Columns.Add("CAMPO 3", 70);
            listView1.Columns.Add("CAMPO 4", 70);
            listView1.Columns.Add("CAMPO 5", 70);
            listView1.Columns.Add("CAMPO 6", 70);
            listView1.Columns.Add("CAMPO 7", 70);
            listView1.Columns.Add("CAMPO 8", 70);

            CaricaElementi(numm, @"dati.csv", listView1);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                Elimina(listView1.SelectedItems[0].SubItems[1].Text, @"dati.csv", numm, listView1);
            VisualizzaElimina_Load(sender, e);
        }
        private void button2_Click(object sender, EventArgs e)//scambia
        {
            if (listView1.SelectedItems.Count == 2)
                ScambiaFisicamente(listView1.SelectedItems[0].SubItems[1].Text, listView1.SelectedItems[1].SubItems[1].Text, @"dati.csv", numm);
            VisualizzaElimina_Load(sender, e);
        }
        private void button3_Click(object sender, EventArgs e)//aggiungi
        {
            agg.numm = numm;
            this.Visible = false;
            agg.ShowDialog();
            this.Visible = true;
            VisualizzaElimina_Load(sender, e);
        }
        public static void ScambiaFisicamente(string nome1, string nome2, string filename, int numm)
        {
            byte[] br;
            string line;
            string line1;
            int i = 0;
            int[] pos = new int[2];
            var f = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            while (f.Position < f.Length)
            {
                br = reader.ReadBytes(numm);
                line = Encoding.ASCII.GetString(br, 0, br.Length);
                if (line.Split(',')[1] == nome1 || line.Split(',')[1] == nome2)
                {
                    pos[i] = Convert.ToInt32(f.Position - numm);
                    i++;
                }
            }

            BinaryWriter writer = new BinaryWriter(f);

            f.Seek(pos[0], SeekOrigin.Begin);
            br = reader.ReadBytes(numm);
            line = Encoding.ASCII.GetString(br, 0, br.Length);

            f.Seek(pos[1], SeekOrigin.Begin);
            br = reader.ReadBytes(numm);
            line1 = Encoding.ASCII.GetString(br, 0, br.Length);

            f.Seek(-numm, SeekOrigin.Current);
            writer.Write($"{line}".ToCharArray());

            f.Seek(pos[0], SeekOrigin.Begin);
            writer.Write($"{line1}".ToCharArray());

            f.Close();
        }
        public static void Elimina(string nome, string filename, int numm, ListView listuccina)
        {
            byte[] br;
            string line;
            var f = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            while (f.Position < f.Length)
            {
                br = reader.ReadBytes(numm);
                line = Encoding.ASCII.GetString(br, 0, br.Length);

                string[] fields = line.Split(',');
                fields[fields.Length - 1] = fields[fields.Length - 1].Substring(0, fields[fields.Length - 1].Length - 1);//così elimino #

                if (fields[1] == nome)
                {
                    f.Seek(-numm, SeekOrigin.Current);//torno all'inizio del record
                    BinaryWriter writer = new BinaryWriter(f);
                    writer.Write($"{$"0,{fields[1]},{fields[2]},{fields[3]},{fields[4]},{fields[5]},{fields[6]},{fields[7]},{fields[8]}".PadRight(numm - 1)}#".ToCharArray());
                    f.Position = f.Length;//cosi' esce
                }
            }

            f.Close();
        }
        public static void CaricaElementi(int numm, string fiilename, ListView listino)
        {
            listino.Items.Clear();
            byte[] br;
            string line;
            var f = new FileStream(fiilename, FileMode.Open, FileAccess.ReadWrite);
            BinaryReader reader = new BinaryReader(f);
            f.Seek(0, SeekOrigin.Begin);
            while (f.Position < f.Length)
            {
                br = reader.ReadBytes(numm);
                line = Encoding.ASCII.GetString(br, 0, br.Length);

                string[] fields = line.Split(',');
                fields[fields.Length - 1] = fields[fields.Length - 1].Substring(0, fields[fields.Length - 1].Length - 1);//così elimino #

                if (fields[0] != "0")
                {
                    ListViewItem item = new ListViewItem(fields);
                    listino.Items.Add(item);
                }
            }

            f.Close();
        }
    }
}
