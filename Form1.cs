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

namespace stajornek17
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-FOREST;Initial Catalog=MyDatabase;Integrated Security=True;Encrypt=False");

        private void verigoster()
        {
            listView1.Items.Clear();
            baglan.Open();
            SqlCommand komut = new SqlCommand("Select *from kutuphane", baglan);
            SqlDataReader oku = komut.ExecuteReader();

            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["kitap"].ToString());
                ekle.SubItems.Add(oku["yazar"].ToString());
                ekle.SubItems.Add(oku["sayfa"].ToString());

                listView1.Items.Add(ekle);
            }
            baglan.Close();
        }

        private void btngoruntule_Click(object sender, EventArgs e)
        {
            verigoster();
        }
        int id = 0;
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("insert into kutuphane (id, kitap, yazar, sayfa) values('"+txtsira.Text.ToString()+"','"+txtkitap.Text.ToString()+"','"+txtyazar.Text.ToString()+"','"+txtsayfa.Text.ToString()+"' )",baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verigoster();

            txtsira.Clear();
            txtkitap.Clear();
            txtyazar.Clear();
            txtsayfa.Clear();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            SqlCommand komut = new SqlCommand("Delete from kutuphane where id=(" + id + ")", baglan);
            komut.ExecuteNonQuery();
            baglan.Close();
            verigoster();

            txtsira.Clear();
            txtkitap.Clear();
            txtyazar.Clear();
            txtsayfa.Clear();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            txtsira.Text = listView1.SelectedItems[0].SubItems[0].Text;
            txtkitap.Text = listView1.SelectedItems[0].SubItems[1].Text;
            txtyazar.Text = listView1.SelectedItems[0].SubItems[2].Text;
            txtsayfa.Text = listView1.SelectedItems[0].SubItems[3].Text;
        }
    }
}
