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
        public static void Elimina(string nome, string filename,int numm, ListView listuccina)
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
                    writer.Write($"{$"0,{fields[1]},{fields[2]},{fields[3]},{fields[4]},{fields[5]},{fields[6]},{fields[7]},{fields[8]}".PadRight(numm-1)}#".ToCharArray());
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
