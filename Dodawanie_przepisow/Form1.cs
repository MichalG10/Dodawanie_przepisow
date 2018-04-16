using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;

namespace Dodawanie_przepisow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
          
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            var qTyp = (from m in qm.Typ orderby m.Id_Typu select m.Nazwa).Distinct().ToList();
            TypcomboBox1.DataSource = qTyp;
            Typ_sklacomboBox1.DataSource = qTyp;
            qTyp = (from m in qm.Kategoria orderby m.Id_Kategorii select m.Nazwa).Distinct().ToList();
            Kate_sklacomboBox1.DataSource = qTyp;

        }

        private void Dodaj_przepisbutton1_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            int idtyp = (from i in qm.Typ where i.Nazwa == TypcomboBox1.Text select i.Id_Typu).FirstOrDefault();
            Przepis x = new Przepis
            {
                Nazwa=nazw_przeptextBox1.Text,
                Opis=OpisrichTextBox1.Text,
                Zdjecie=ZdjecietextBox1.Text,
                Czas=int.Parse(CzastextBox1.Text),
                Id_Typu=idtyp
            };
            DialogResult czy = MessageBox.Show("czy dodac przepis?","potwierdz", MessageBoxButtons.YesNo);
            if(czy == DialogResult.Yes){
                qm.Przepis.Add(x);
                qm.SaveChanges();
            }
        }

        private void pokaz_przepisybutton_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            int idtyp = (from i in qm.Typ where i.Nazwa == TypcomboBox1.Text select i.Id_Typu).FirstOrDefault();
            var prze = (from i in qm.Przepis where i.Id_Typu==idtyp select i.Nazwa).Distinct().ToList();
            Przepisy_sklacomboBox1.Text = " ";
            Przepisy_sklacomboBox1.DataSource = prze;
        }

        private void Poka_sklabutton1_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            int idkat = (from i in qm.Kategoria where i.Nazwa == Kate_sklacomboBox1.Text select i.Id_Kategorii).FirstOrDefault();
            var pro = (from i in qm.Produkt where i.Id_Kategorii == idkat select i.Nazwa).Distinct().ToList();
            Pod_sklacomboBox1.Text = " ";
            Pod_sklacomboBox1.DataSource = pro;

            
        }

        private void Dodaj_prosklabutton1_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            SqlConnection connection = new SqlConnection(@"Data Source=quickmealserv.database.windows.net;Initial Catalog=QuickMeal_db;Persist Security Info=True;User ID=Kierownik;Password=KieraS_246");
            string zap = "Select  Id_Produktu from Produkt Where Nazwa ='" + Pod_sklacomboBox1.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(zap, connection);
            DataTable table = new DataTable();
            sda.Fill(table);
            if (table.Rows.Count >= 1)
            {
                MessageBox.Show("Istnieje już taki produkt w bazie");
                return;
            }
            int idkat = (from i in qm.Kategoria where i.Nazwa == Kate_sklacomboBox1.Text select i.Id_Kategorii).FirstOrDefault();
            Produkt x = new Produkt
            {
                Id_Kategorii = idkat,
                Nazwa = Pod_sklacomboBox1.Text
            };
            DialogResult czy = MessageBox.Show("czy dodac produkt?", "potwierdz", MessageBoxButtons.YesNo);
            if (czy == DialogResult.Yes)
            {
                qm.Produkt.Add(x);
                qm.SaveChanges();
            }
        }

        private void Pod_sklacomboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            var pier = (from i in qm.Produkt where i.Nazwa == Pod_sklacomboBox1.Text select i.Id_Produktu).FirstOrDefault();
            var gra1 = (from i in qm.Skladnik where i.Id_Produktu == pier select i.Gramatura).FirstOrDefault();
           
            Gramatura_sklacomboBox1.Text = "";
            
            var gra2 = (from i in qm.Skladnik select i.Gramatura).Distinct().ToList();
            Gramatura_sklacomboBox1.DataSource = gra2;
            Gramatura_sklacomboBox1.Text = gra1;
        }

        private void Dodaj_sklabutton2_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            int przepis = (from i in qm.Przepis where i.Nazwa == Przepisy_sklacomboBox1.Text select i.Id_Przepisu).FirstOrDefault();
            int produkt = (from i in qm.Produkt where i.Nazwa == Pod_sklacomboBox1.Text select i.Id_Produktu).FirstOrDefault();
            Skladnik x = new Skladnik
            {
                Id_Przepisu = przepis,
                Id_Produktu = produkt,
                Gramatura = Gramatura_sklacomboBox1.Text,
                Ilosc = int.Parse(Ilosc_sklatextBox1.Text)
            };
            DialogResult czy = MessageBox.Show("czy dodac skladnik do przepisu?", "potwierdz", MessageBoxButtons.YesNo);
            if (czy == DialogResult.Yes)
            {
                qm.Skladnik.Add(x);
                qm.SaveChanges();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            var pier = (from i in qm.Przepis where i.Nazwa == comboBox1.Text select i.Id_Typu).FirstOrDefault();
            var typ = (from i in qm.Typ where i.Id_Typu == pier select i.Nazwa).FirstOrDefault();
            Typ_pokazlabel4.Text = typ;

            var czas = (from i in qm.Przepis where i.Nazwa == comboBox1.Text select i.Czas).FirstOrDefault();
            Czas_pokalabel5.Text = czas.ToString();

            var zdjecie = (from i in qm.Przepis where i.Nazwa == comboBox1.Text select i.Zdjecie).FirstOrDefault();
            Zdjecie_pokazlabel5.Text = zdjecie;

            var opis = (from i in qm.Przepis where i.Nazwa == comboBox1.Text select i.Opis).FirstOrDefault();
            Opis_poarichTextBox1.Text = opis;

            var prze = (from i in qm.Przepis where i.Nazwa == comboBox1.Text select i.Id_Przepisu).FirstOrDefault();
            var skla = (from i in qm.Skladnik where i.Id_Przepisu == prze select i).Distinct().ToList();
            dataGridView1.DataSource = skla;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuickMeal_dbEntities qm = new QuickMeal_dbEntities();
            var qTyp = (from m in qm.Przepis orderby m.Id_Przepisu select m.Nazwa).Distinct().ToList();
            comboBox1.DataSource = qTyp;
        }
    }
}
