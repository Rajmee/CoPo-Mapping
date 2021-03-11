using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Co_po
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            textBox1.Focus();
        }

       

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.cross;
            piclogo.BackgroundImage = Properties.Resources.logo;

            picemail.BackgroundImage = Properties.Resources.email;
            panel4.BackColor = Color.White;
            textBox4.ForeColor = Color.White;

            picuser.BackgroundImage = Properties.Resources.user;
            panel2.BackColor = Color.White;
            textBox2.ForeColor = Color.White;

            picpass.BackgroundImage = Properties.Resources.passwordicon;
            panel1.BackColor = Color.FromArgb(78, 186, 206);
            textBox1.ForeColor = Color.FromArgb(78, 186, 206);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //new Login().Show();
            //this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Login().Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            DB db = new DB();
            MySqlCommand command = new MySqlCommand("INSERT INTO `faculty`(`name`, `department`, `email`, `password`) VALUES (@n, @dep, @usn, @pass)", db.getConnection());

            command.Parameters.Add("@n", MySqlDbType.VarChar).Value = textBox1.Text;
            command.Parameters.Add("@dep", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = textBox2.Text;

            db.openConnection();

            if (!checkTextBoxesValues())
            {
             
                if (textBox2.Text.Equals(textBox3.Text))
                {
                  
                    if (checkUsername())
                    {
                        MessageBox.Show("This Username Already Exists, Select A Different One", "Duplicate Username", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    else
                    {
                      
                        if (command.ExecuteNonQuery() == 1)
                        {
                            MessageBox.Show("Your Account Has Been Created", "Account Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            if(Login.login_Validate(textBox2.Text.ToString(), textBox4.Text.ToString()))
                            {
                                this.Hide();

                            }
                        }
                        else
                        {
                            MessageBox.Show("ERROR");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Confirmation Password", "Password Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Enter Your Informations First", "Empty Data", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

            db.closeConnection();

        }

        public Boolean checkTextBoxesValues()
        {
            String fname = textBox1.Text;
            String department = textBox5.Text;
            String email = textBox4.Text;
            String pass = textBox2.Text;

            if (fname.Equals("name") ||
                department.Equals("department") || email.Equals("email")
                || pass.Equals("password"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public Boolean checkUsername()
        {
            DB db = new DB();

            String email = textBox4.Text;

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `faculty` WHERE `email` = @usn", db.getConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = email;

            adapter.SelectCommand = command;

            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {            
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text.ToLower().ToString().Equals("username"))
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Equals(""))
            {
                textBox1.Text = "Username";
            }
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text.ToLower().ToString().Equals("password"))
            {
                textBox2.Text = "";
                textBox2.PasswordChar = '*';
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text.ToString().Equals(""))
            {
                textBox2.Text = "Password";
                textBox2.PasswordChar = '\0';
            }
        }
        private void textBox3_Enter(object sender, EventArgs e)
        {
            if (textBox3.Text.ToLower().ToString().Equals("confirm password"))
            {
                textBox3.Text = "";
                textBox3.PasswordChar = '*';
            }
        }

        private void textBox3_Leave(object sender, EventArgs e)
        {
            if (textBox3.Text.ToString().Equals(""))
            {
                textBox3.Text = "Confirm Password";
                textBox3.PasswordChar = '\0';
            }
        }
        private void textBox4_Enter(object sender, EventArgs e)
        {
            if (textBox4.Text.ToLower().ToString().Equals("email"))
            {
                textBox4.Text = "";
            }
        }

        private void textBox4_Leave(object sender, EventArgs e)
        {
            if (textBox4.Text.ToString().Equals(""))
            {
                textBox4.Text = "Email";
            }
        }
        private void textBox5_Enter(object sender, EventArgs e)
        {
            if (textBox5.Text.ToLower().ToString().Equals("department"))
            {
                textBox5.Text = "";
            }
        }

        private void textBox5_Leave(object sender, EventArgs e)
        {
            if (textBox5.Text.ToString().Equals(""))
            {
                textBox5.Text = "Department";
            }
        }
    }
}
