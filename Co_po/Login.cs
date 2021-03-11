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
    public partial class Login : Form
    {
        public static string useremail;
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox2.BackgroundImage = Properties.Resources.cross;
            piclogo.BackgroundImage = Properties.Resources.logo;

            picemail.BackgroundImage = Properties.Resources.email;
            //panel3.BackColor = Color.White;
            textBox3.ForeColor = Color.White;

            //picuser.BackgroundImage = Properties.Resources.user;
            panel1.BackColor = Color.White;
            //textBox1.ForeColor = Color.White;

            picpass.BackgroundImage = Properties.Resources.passwordicon;
            panel1.BackColor = Color.FromArgb(78, 186, 206);
            //textBox1.ForeColor = Color.FromArgb(78, 186, 206);


        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

            //textBox1.Clear();

            if (textBox3.Text.Equals("")) textBox3.Text = "Email";
            if (textBox2.Text.Equals(""))
            {
                textBox2.Text = "Password";
                textBox2.PasswordChar = '\0';
            }
            //picuser.BackgroundImage = Properties.Resources.user;
            panel1.BackColor = Color.FromArgb(78, 186, 206);
            //textBox1.ForeColor = Color.FromArgb(78, 186, 206);

            picpass.BackgroundImage = Properties.Resources.passwordicon;
            panel2.BackColor = Color.White;
            textBox2.ForeColor = Color.White;

            picemail.BackgroundImage = Properties.Resources.email;
            //panel3.BackColor = Color.White;
            textBox3.ForeColor = Color.White;




        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            textBox2.Clear();
            //if (textBox1.Text.Equals("")) textBox1.Text = "Username";
            if (textBox3.Text.Equals("")) textBox3.Text = "Email";
            textBox2.PasswordChar = '*';
            picpass.BackgroundImage = Properties.Resources.passwordicon;
            panel2.BackColor = Color.FromArgb(78, 186, 206);
            textBox2.ForeColor = Color.FromArgb(78, 186, 206);

            //picuser.BackgroundImage = Properties.Resources.user;
            panel1.BackColor = Color.White;
            //textBox1.ForeColor = Color.White;

            picemail.BackgroundImage = Properties.Resources.email;
            //panel3.BackColor = Color.White;
            textBox3.ForeColor = Color.White;
        }

        private void textBox3_Enter(object sender, EventArgs e)
        {
            textBox3.Clear();
            //if (textBox1.Text.Equals("")) textBox1.Text = "Username";
            if (textBox2.Text.Equals(""))
            {
                textBox2.Text = "Password";
                textBox2.PasswordChar = '\0';
            }
            picemail.BackgroundImage = Properties.Resources.email;
            panel2.BackColor = Color.White;
            textBox2.ForeColor = Color.White;

            //picuser.BackgroundImage = Properties.Resources.user;
            panel1.BackColor = Color.White;
            //textBox1.ForeColor = Color.White;

            picpass.BackgroundImage = Properties.Resources.passwordicon;
            //panel3.BackColor = Color.FromArgb(78, 186, 206);
            textBox3.ForeColor = Color.FromArgb(78, 186, 206);


        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //this.Close();
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            this.Hide();
            new Register().Show();
        }
        public static bool login_Validate(String password,String email)
        {
            DataTable table = new DataTable();

            DB db = new DB();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `faculty` WHERE `email` = @usn and `password` = @pass", db.getConnection());

            command.Parameters.Add("@usn", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@pass", MySqlDbType.VarChar).Value = password;
            //command.Parameters.Add("@nme", MySqlDbType.VarChar).Value = user;

            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                
                Mainform mainform = new Mainform(email);
                mainform.Show();
                useremail = email;
                return true;
            }
            else
            {
                 if (password.Trim().Equals(""))
                {
                    MessageBox.Show("Enter Your Password To Login", "Empty Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                else
                {
                    MessageBox.Show("Wrong Username Or Password", "Wrong Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //String user = textBox1.Text;
            String password = textBox2.Text;
            String email = textBox3.Text;
           
            if (login_Validate(password, email)) this.Hide();
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
