using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Co_po
{
    public partial class Mainform : Form
    {
        private string mil;
        public Mainform(string mail)
        {
            this.mil = mail;
            InitializeComponent();
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            DataTable t = new DataTable();
            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            db.openConnection();
            MySqlCommand command = new MySqlCommand("SELECT `coursecode`, `coursename`, `section`, `year`, `semester` FROM courselist WHERE `email` = @usn",db.getConnection());
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = mil;
            
            adapter.SelectCommand = command;
            adapter.Fill(t);
            dataGridView1.DataSource = t;
        }
        
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            new coas(this).Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
