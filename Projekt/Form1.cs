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


namespace Projekt
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //przycisk "Zapisz dane"
        private void button1_Click(object sender, EventArgs e)
        {
            int val,prize, result;
            if (comboBox2.Text == "godzinny")
            {
                prize = 5;
            }
            else prize = 20;
            
            
            val = Int32.Parse(comboBox1.Text);
            result = val * prize;
            label7.Text = Convert.ToString(result);

           
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-IURIS93\\MSSQL;Initial Catalog=Parking;User ID=sa;Password=yxofton1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            
            connection.Open();
            
            SqlCommand cmd = new SqlCommand("INSERT INTO Kierowca(KImie) VALUES('"+textBox1.Text+"')", connection);
            SqlCommand cma = new SqlCommand("INSERT INTO Kierowca(KNazwisko) values( '"+textBox2.Text+ "' )", connection);
            SqlCommand cmb = new SqlCommand("INSERT INTO Bilet(BRodzajBiletu)VALUES('"+comboBox2.Text+ "')", connection);
            SqlCommand cmc = new SqlCommand("INSERT INTO Bilet(BIlosc)VALUES('"+Int32.Parse(comboBox1.Text) + "')", connection);
            SqlCommand cme = new SqlCommand("INSERT INTO Bilet(BCena)VALUES('"+prize+"')",connection);

            int d = cmd.ExecuteNonQuery();
            int a = cma.ExecuteNonQuery();
            int b = cmb.ExecuteNonQuery();
            int c = cmc.ExecuteNonQuery();
            int k = cme.ExecuteNonQuery();

            if (d != 0 || a !=0 || b!=0 || c!=0 ||k!=0)
            {
                MessageBox.Show("Zapisano wartości");
            }
            else
            {
                MessageBox.Show("Błąd");
            }
        }

        //przycisk "Podgląd wydruku biletu"
        private void button2_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
        }

        //przycisk "Wydrukuj bilet"
        private void button3_Click(object sender, EventArgs e)
        {
            // printDialog1.ShowDialog();
            printDocument1.Print();
          
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bmp = Properties.Resources.parking;
            Image parkingloot = bmp;
            e.Graphics.DrawImage(parkingloot,300,10,parkingloot.Width,parkingloot.Height);

            e.Graphics.DrawString("Imię: "+ textBox1.Text, 
            new Font("Calibri",18,FontStyle.Bold),Brushes.Black,new Point(80,250));

            e.Graphics.DrawString("Nazwisko: " + textBox2.Text,
            new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80, 330));

            e.Graphics.DrawString("Ilość biletów: " + comboBox1.Text,
            new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80,410));

            e.Graphics.DrawString("Łączna cena: " + label7.Text,
            new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80, 490));

            e.Graphics.DrawString("Data pobrania biletu/biletów : " + DateTime.Now,
                new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80, 570));

            if (comboBox2.Text == "całodobowy")
            {
                e.Graphics.DrawString("Bilet/Bilety jest ważny do : " + DateTime.Now.AddDays(1),
                new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80, 650));
            }
            else
            {
                e.Graphics.DrawString("Bilet/Bilety jest ważny do : " + DateTime.Now.AddHours(1),
                new Font("Calibri", 18, FontStyle.Bold), Brushes.Black, new Point(80, 650));
            }

            


        }

        //przycisk do DataGridView
        private void button4_Click(object sender, EventArgs e)
        {
            var connectionString = new SqlConnection("Data Source=DESKTOP-IURIS93\\MSSQL;Initial Catalog=Parking;User ID=sa;Password=yxofton1;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            connectionString.Open();


            SqlDataAdapter sqa = new SqlDataAdapter("SELECT * FROM Kierowca",connectionString);
            DataTable dt = new DataTable();
            sqa.Fill(dt);

            dataGridView1.DataSource = dt;

        }
    }
}
