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
    public partial class coas : Form
    {
        string userEmail = Login.useremail;
        Mainform main;
        public coas(Mainform k)
        {
            main = k;
            InitializeComponent();
            label10.Visible = false;
            label11.Visible = false;
            label12.Visible = false;
            label13.Visible = false;            
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            if(textBox3.Text==string.Empty || textBox4.Text == string.Empty || textBox6.Text == string.Empty || comboBox1.SelectedIndex == -1 || textBox5.Text == string.Empty)
            {
                CheckTextBox(textBox3);
                CheckTextBox(textBox4);
                CheckTextBox(textBox6);
                CheckComboBox(comboBox1);
                CheckTextBox(textBox5);
                CheckTextBox(textBox7);
                CheckTextBox(textBox1);
                CheckTextBox(textBox2);
            }
              
            
            
            else {
            DB db = new DB();
                MySqlCommand command = new MySqlCommand("INSERT INTO `courselist`(`coursename`, `coursecode`, `year`, `semester`,`section`,`email`) VALUES (@courseName, @courseCode, @year, @semester,@section,@email)", db.getConnection());

                command.Parameters.Add("@courseName", MySqlDbType.VarChar).Value = textBox3.Text;
                command.Parameters.Add("@courseCode", MySqlDbType.VarChar).Value = textBox4.Text;
                command.Parameters.Add("@year", MySqlDbType.VarChar).Value = textBox5.Text;
                command.Parameters.Add("@section", MySqlDbType.VarChar).Value = textBox6.Text;
                command.Parameters.Add("@email", MySqlDbType.VarChar).Value = userEmail;
                command.Parameters.Add("@semester", MySqlDbType.VarChar).Value = comboBox1.SelectedItem.ToString();

                db.openConnection();
                command.ExecuteNonQuery();
                db.closeConnection();
                db = new DB();
                string tableName = textBox3.Text.ToString() + textBox4.Text.ToString() + textBox5.Text.ToString() + textBox6.Text.ToString() + comboBox1.SelectedItem.ToString();
                string cos = "";
                for (int i = 0; i < Int32.Parse(textBox1.Text.ToString()); i++)
                {
                    cos += "CO" + (i + 1) + " varchar(20) NOT NULL,";
                }
                cos = cos.Substring(0, cos.Length - 1);
                string k = "Create table " + tableName + "(AssesmentNames VARCHAR(30) NOT NULL, " + cos + ");";
                command = new MySqlCommand(k, db.getConnection());

                db.openConnection();
                command.ExecuteNonQuery();
                db.closeConnection();

                new Tables(main, textBox1.Text, textBox2.Text, textBox3.Text.ToString(), textBox4.Text, textBox6.Text, textBox5.Text, comboBox1.SelectedItem.ToString(), textBox7.Text).Show();
                this.Hide();
            }
        }

        void CheckTextBox(TextBox tb)
        {
            if (string.IsNullOrEmpty(tb.Text))
            {
                tb.BackColor = Color.Red;
                
                label13.Visible = true;
            }
        }
        void CheckComboBox(ComboBox cs)
        {
            if (cs.SelectedIndex == -1)
            {
                cs.ForeColor = Color.Red;
                label13.Visible = true;
            }
        }

        private void coas_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Today;

            textBox5.Text = now.ToString("yyyy");
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
            textBox3.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox4.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox6.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox5.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if ((!char.IsDigit(c) ||
                Convert.ToInt32(textBox7.Text + e.KeyChar) >= 46 ||
                textBox7.Text == "0") && c != '\b')
            {
                e.Handled = true;
                label10.Visible = true;
            }
            else
            {
                label10.Visible = false;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox7.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if ((!char.IsDigit(c) ||
                Convert.ToInt32(textBox1.Text + e.KeyChar) >= 7 ||
                textBox1.Text == "0") && c != '\b')
            {
                e.Handled = true;
                label11.Visible = true;
            }
            else
            {
                label11.Visible = false;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox1.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if ((!char.IsDigit(c) ||
                Convert.ToInt32(textBox2.Text + e.KeyChar) >= 9 ||
                textBox2.Text == "0") && c != '\b')
            {
                e.Handled = true;
                label12.Visible = true;
            }
            else
            {
                label12.Visible = false;
            }
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            textBox2.BackColor = Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(195)))), ((int)(((byte)(255)))));
            label13.Visible = false;
        }
    }
}
