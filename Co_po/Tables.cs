using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Threading;

namespace Co_po
{
    public partial class Tables : Form
    {
        private Boolean init = true;
        private Boolean firstTime = true;
        private int cos;
        private int ass;
        private int totalRowNumber = 0;
        private int studentsNo;
        private string coursename;
        private int coursecode;
        private int coursesec;
        private int year;
        private string csemester;
        ArrayList assesmentNames = new ArrayList();
        private Mainform main;
        public Tables(Mainform k,string cos,string assesments,string cName,string cCode,string cSection,string cYear,string cSemester,string no)
        {
            main = k;
            this.studentsNo = Convert.ToInt32(no);
            this.cos = Convert.ToInt32(cos);
            this.ass = Convert.ToInt32(assesments);
            this.coursename = cName;
            this.coursecode = Convert.ToInt32(cCode);
            this.coursesec = Convert.ToInt32(cSection);
            this.year = Convert.ToInt32(cYear);
            this.csemester = cSemester;

            InitializeComponent();
            button4.Visible = false;
            label2.Text = cName;
            label3.Text = "Course Code: " + cCode + " Section: " + cSection;
            label4.Text = cSemester + ":" + cYear;


            assesmentNames.Add("Student Information");
            assesmentNames.Add("Mid");
            assesmentNames.Add("Final");
            assesmentNames.Add("Project");
            assesmentNames.Add("Presentation");
            assesmentNames.Add("Quiz");
            assesmentNames.Add("Viva");
            DataGridViewTextBoxColumn dgTetBox = new DataGridViewTextBoxColumn();
            dgTetBox.Name = "Assesments";
            
            dgTetBox.HeaderText = "Assesment Items";
            dataGridView1.Columns.Add(dgTetBox);
            for (int i = 0; i < Convert.ToInt32(cos); i++)
            {

                DataGridViewTextBoxColumn coTextBox = new DataGridViewTextBoxColumn();
                coTextBox.Name = "CO" + (i+1);

                dataGridView1.Columns.Add(coTextBox);
            }

            int rowId;
            for (int i = 0; i < ass; i++)
            {
                rowId=dataGridView1.Rows.Add();
                dataGridView1[0, rowId].Value = assesmentNames[i + 1];
            }
            rowId = dataGridView1.Rows.Add();
            DataGridViewRow total1 = dataGridView1.Rows[rowId];
            for(int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                total1.Cells[i].ReadOnly = true;
                if (i != 0)
                {

                    total1.Cells[i].Value = "0";
                }
                else total1.Cells[i].Value = "Total";
            }
            GoFullscreen(true);



            DataGridViewTextBoxColumn poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "COS";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "";
            dataGridView2.Columns.Add(poTextBox);
            for (int i = 0; i < 12; i++)
            {
                DataGridViewCheckBoxColumn coTextBox = new DataGridViewCheckBoxColumn();
                coTextBox.Name = "PO" + (i + 1);
                dataGridView2.Columns.Add(coTextBox);

            }


            for (int i = 0; i < Convert.ToInt32(cos); i++)
            {
                rowId= dataGridView2.Rows.Add();

                DataGridViewRow row = dataGridView2.Rows[rowId];
                row.Cells[0].Value = "CO"+(i+1);
            }
             rowId = dataGridView2.Rows.Add();

            DataGridViewRow total = dataGridView2.Rows[rowId];
            for(int i = 0; i < dataGridView2.ColumnCount; i++)
            {

                DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();

                total.Cells[i] = TextBoxCell;
                total.Cells[i].ReadOnly = true;
                if (i != 0) total.Cells[i].Value = "0";
                else total.Cells[i].Value = "Total";
            }


            GoFullscreen(true);
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.EditingControlShowing += dataGridView1_EditingControlShowing;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {

                for (int i = 1; i <= Convert.ToInt32(cos); i++)
                {
                    dataGridView1[i, j].Value = "0";
                }
            }
            int height = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                height += row.Height;
            }
            height += dataGridView1.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                width += col.Width;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            width += dataGridView1.RowHeadersWidth;

            dataGridView1.ClientSize = new Size(width + 14, height + 2);
             height = 0;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                height += row.Height;
            }
            height += dataGridView2.ColumnHeadersHeight;

            width = 0;
            foreach (DataGridViewColumn col in dataGridView2.Columns)
            {
                width += col.Width;
                
            }
            width += dataGridView2.RowHeadersWidth;

            dataGridView2.ClientSize = new Size(width + 10, height + 0);
            init = false;
        }



        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column1_KeyPress);
            if (dataGridView1.CurrentCell.ColumnIndex != 0) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column1_KeyPress);
                    tb.Leave += new EventHandler(Focus_Lost);
                    //tb.MouseClick += new MouseEventHandler(Enter_Text);
                    tb.LostFocus += new EventHandler(Enter_Text);
                }
            }
            if (dataGridView1.CurrentCell.ColumnIndex == 0) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(Column0_KeyPress);
                    tb.Leave += new EventHandler(Focus_Lost0);
                    //tb.MouseClick += new MouseEventHandler(Enter_Text);
                    tb.LostFocus += new EventHandler(Enter_Text);
                }
            }
        }


        private void dataGridView4_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(OnlyDigit);
            if (dataGridView4.CurrentCell.ColumnIndex > 3) //Desired Column
            {
                TextBox tb = e.Control as TextBox;
                if (tb != null)
                {
                    tb.KeyPress += new KeyPressEventHandler(OnlyDigit);
                    tb.Leave += new EventHandler(averageFocusLost);
                    tb.TextChanged += new EventHandler(textChanged);
                    //tb.MouseClick += new MouseEventHandler(Enter_Text);
                   // tb.LostFocus += new EventHandler(averageKeyLeave);
                }
            }
        }
        private void textChanged(object sender,EventArgs es)
        {
            try
            {
                TextBox textBox = sender as TextBox;
                if (textBox != null)
                {
                    string theText = textBox.Text;
                    double value = Double.Parse(theText);
                    int col = dataGridView4.CurrentCell.ColumnIndex;
                    int row = dataGridView4.CurrentCell.RowIndex;
                    if (value > Double.Parse(dataGridView4[col, 0].Value.ToString()))
                    {
                        textBox.Text = "0";
                        MessageBox.Show("Marks Exceeded!");
                    }

                }
            }catch(Exception ess)
            {

            }
        }
        private void averageFocusLost(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentCell.ColumnIndex >3)
            {
                double sum = 0;
                for (int i = 1; i < dataGridView4.RowCount - 1; i++)
                {
                    try
                    {
                        sum += Double.Parse(dataGridView4[dataGridView4.CurrentCell.ColumnIndex, i].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("ex");
                    }


                }
                dataGridView4[dataGridView4.CurrentCell.ColumnIndex, dataGridView4.RowCount - 1].Value = sum/studentsNo;
            }

        }
        private void averageKeyLeave(object sender, EventArgs e)
        {
            if (dataGridView4.CurrentCell.ColumnIndex >3)
            {

                for (int j = 4; j < dataGridView4.ColumnCount - 1; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                    {
                        try
                        {
                            sum += Double.Parse(dataGridView4[j, i].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ex");
                        }


                    }
                    dataGridView4[j, dataGridView4.RowCount - 1].Value = sum/studentsNo;
                }
            }
        }
        private void Focus_Lost0(object sender, EventArgs e)
        {
            try
            {
                if(dataGridView1.CurrentCell.ColumnIndex==0)assesmentNames[dataGridView1.CurrentCell.RowIndex + 1] = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                dataGridView4.Invalidate();
               // MessageBox.Show(assesmentNames[dataGridView1.CurrentCell.RowIndex + 1].ToString());
            }
            catch (Exception es)
            {

            }
        }
        private void Column0_KeyPress(object sender,KeyPressEventArgs e)
        {
            /*
            try
            {
                assesmentNames[dataGridView1.CurrentCell.RowIndex+1] = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                dataGridView4.Invalidate();
            } catch (Exception es)
            {

            }
             */
        }
        private void Enter_Text(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {

                for (int j = 1; j < dataGridView1.ColumnCount - 1; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        try
                        {
                            sum += Double.Parse(dataGridView1[j, i].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ex");
                        }


                    }
                    dataGridView1[j, dataGridView1.RowCount - 1].Value = sum;
                }
            }
        }
        private void Focus_Lost(object sender,EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex != 0)
            {
                double sum = 0;
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    try
                    {
                        sum += Double.Parse(dataGridView1[dataGridView1.CurrentCell.ColumnIndex, i].Value.ToString());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ex");
                    }


                }
                dataGridView1[dataGridView1.CurrentCell.ColumnIndex, dataGridView1.RowCount - 1].Value = sum;
            }
                
        }
        private void Column1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                  && !char.IsDigit(e.KeyChar)
                  && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }

            
        }
        private void OnlyDigit(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar)
                  && !char.IsDigit(e.KeyChar)
                  && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.'
                && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }


        }

        private void _data_grid_view_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dataGridView4.Rows[e.RowIndex].Height = 42;
            int height = 0;
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                height += row.Height;
            }
            height += dataGridView4.ColumnHeadersHeight;

            int width = 0;
            foreach (DataGridViewColumn col in dataGridView4.Columns)
            {
                width += col.Width;
            }
            width += dataGridView4.RowHeadersWidth;
            dataGridView4.ClientSize = new Size(width + 2, height + 2);
        }


        private void Tables_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
            Control.CheckForIllegalCrossThreadCalls = false;
            backgroundWorker1.RunWorkerAsync(0);
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.dataGridView1, new object[] { true });
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.cototal, new object[] { true });
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.copercentage, new object[] { true });
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.coattainment, new object[] { true });
            }
            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.dataGridView2, new object[] { true });
            }
            dataGridView4.DataError += new DataGridViewDataErrorEventHandler(DataGridView4_DataError);
            dataGridView4.Hide();
            DataGridViewTextBoxColumn poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "S/N";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "S/N";
            dataGridView4.Columns.Add(poTextBox);
            poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "ID";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "ID";
            dataGridView4.Columns.Add(poTextBox);
            poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "Name";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "Name";
            dataGridView4.Columns.Add(poTextBox);
            DataGridViewCheckBoxColumn coTextBox = new DataGridViewCheckBoxColumn();
            coTextBox.Name = "Withdrawn";
            coTextBox.HeaderText = "W/I";
            dataGridView4.Columns.Add(coTextBox);
            this.dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.dataGridView4.ColumnHeadersHeight = this.dataGridView4.ColumnHeadersHeight * 2;

            this.dataGridView4.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            this.dataGridView4.CellPainting += new DataGridViewCellPaintingEventHandler(dataGridView4_CellPainting);

            this.dataGridView4.Paint += new PaintEventHandler(dataGridView4_Paint);
            dataGridView4.RowsAdded += new DataGridViewRowsAddedEventHandler(this._data_grid_view_RowsAdded);


        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!init){if (e.ColumnIndex != 0)
                {

                    double sum = 0;
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        try
                        {
                            sum += Double.Parse(dataGridView1[e.ColumnIndex, i].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ex");
                        }


                    }
                    dataGridView1[e.ColumnIndex, dataGridView1.RowCount - 1].Value = sum;
                    try
                    {
                        int u, v;
                        u = (e.RowIndex * cos) + (e.ColumnIndex - 1) + 4;
                        v = totalRowNumber;
                        cototal[e.ColumnIndex+1, 0].Value = sum;
                    }
                    catch (Exception k)
                    {

                    }
                }
            }
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridView2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != dataGridView2.RowCount - 1)
            {
                if(dataGridView2[e.ColumnIndex, e.RowIndex].Value.ToString().Equals("True"))
                {
                    dataGridView2[e.ColumnIndex, dataGridView2.RowCount - 1].Value = (Double.Parse(dataGridView2[e.ColumnIndex, dataGridView2.RowCount - 1].Value.ToString())) + 1;
                    try
                    {

                    }catch (Exception rowException)
                    {

                    }
                }
                else
                {
                    dataGridView2[e.ColumnIndex, dataGridView2.RowCount - 1].Value = (Double.Parse(dataGridView2[e.ColumnIndex, dataGridView2.RowCount - 1].Value.ToString())) - 1;


                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            

            if (!System.Windows.Forms.SystemInformation.TerminalServerSession)
            {
                typeof(DataGridView).InvokeMember("DoubleBuffered",
 BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
 null, this.dataGridView4, new object[] { true });
            }
            button1.Enabled = false;
            button4.Visible = true;

            try
            {

                for (int q = 0; q < ass; q++)
                {
                    string k = dataGridView1[0, q].Value.ToString();
                    if (dataGridView1[0, q].Value.ToString() != null) continue;
                    else
                    {
                        assesmentNames = new ArrayList();
                        MessageBox.Show("Not all assesments are not given!");
                        return;
                    }



                    

                    
                }
            }
            catch(Exception es)
            {

                MessageBox.Show("Not all assesments are not given!");
                button1.Enabled = true;
                return;
            }
            try
            {
                
                tableLayoutPanel2.Show();
                dataGridView4.Show();
                firstTime = false;
            } catch (Exception es)
            {
                button1.Enabled = true;
            }
        }
        private void DataGridView4_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {

            if (anError.Context == DataGridViewDataErrorContexts.Commit)
            {

            }
            if (anError.Context == DataGridViewDataErrorContexts.CurrentCellChange)
            {

            }
            if (anError.Context == DataGridViewDataErrorContexts.Parsing)
            {

            }
            if (anError.Context == DataGridViewDataErrorContexts.LeaveControl)
            {

            }

            if ((anError.Exception) is ConstraintException)
            {
                DataGridView view = (DataGridView)sender;

                anError.ThrowException = false;
            }
        }
        void dataGridView4_Paint(object sender, PaintEventArgs e)

        {


            ArrayList monthes = assesmentNames;
            for (int j = 0; j < cos * ass + 3;)

            {

                string k;
                try { k = monthes[j / cos].ToString(); }catch (Exception es) { try { k = monthes[(j / cos) - 1].ToString(); } catch (Exception ks) { k = monthes[j - cos - 1].ToString(); } }
                Rectangle r1 = this.dataGridView4.GetCellDisplayRectangle(j, -1, true); //get the column header cell

                r1.X += 1;

                r1.Y += 1;

                if (k == "Student Information") r1.Width = dataGridView4.Columns[0].Width+ dataGridView4.Columns[1].Width + dataGridView4.Columns[2].Width + dataGridView4.Columns[3].Width - 2;
                else r1.Width = r1.Width * cos - 4;
                if (k != "Student Information")
                {
                    if (cos == 6) r1.X -= dataGridView4.Columns[4].Width * 2;
                    else if (cos == 5) r1.X -= dataGridView4.Columns[4].Width;
                    else if (cos == 4) r1.X += 0;
                    else if (cos == 3) r1.X += dataGridView4.Columns[4].Width;
                    else if (cos == 2) r1.X += dataGridView4.Columns[4].Width*2;
                    else if (cos == 1) r1.X += dataGridView4.Columns[4].Width*3;
                    if(cos==1&&ass==1)r1.X += dataGridView4.Columns[4].Width * 3;
                }
               
                r1.Height = r1.Height / 2 - 2;
                e.Graphics.FillRectangle(new SolidBrush(Color.White), r1);

                StringFormat format = new StringFormat();

                format.Alignment = StringAlignment.Center;

                format.LineAlignment = StringAlignment.Center;

                e.Graphics.DrawString(k,

                    this.dataGridView4.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(Color.Black),

                    r1,

                    format);

                 j += cos;

            }

        }



        void dataGridView4_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)

        {

            if (e.RowIndex == -1 && e.ColumnIndex > -1)

            {

                e.PaintBackground(e.CellBounds, false);



                Rectangle r2 = e.CellBounds;

                r2.Y += e.CellBounds.Height / 2;

                r2.Height = e.CellBounds.Height / 2;

                e.PaintContent(r2);

                e.Handled = true;

            }

        }

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void DataGridView4_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!init)
            {
                
                 if (e.ColumnIndex == 3)
                {
                    if ((e.RowIndex != dataGridView4.RowCount - 1) && (e.RowIndex > 0))
                    {
                        if (dataGridView4[e.ColumnIndex, e.RowIndex].Value.ToString().Equals("True"))
                        {
                            dataGridView4[e.ColumnIndex, dataGridView4.RowCount - 1].Value = (Double.Parse(dataGridView4[e.ColumnIndex, dataGridView4.RowCount - 1].Value.ToString())) + 1;
                        }
                        else
                        {
                            try
                            {
                                dataGridView4[e.ColumnIndex, dataGridView4.RowCount - 1].Value = (Double.Parse(dataGridView4[e.ColumnIndex, dataGridView4.RowCount - 1].Value.ToString())) - 1;
                            }
                            catch (Exception es)
                            {
                                string k = es.ToString();
                            }

                        }
                        
                    }
                    else if (e.RowIndex == dataGridView4.RowCount - 1)
                    {
                        try
                        {
                            if (!firstTime)
                            {
                                int i, j;

                                for ( i=1;i<copercentage.ColumnCount;i++)
                                {

                                    double sum = 0;
                                    for ( j = 1; j < copercentage.RowCount - 1; j++)
                                    {
                                        sum += Double.Parse(copercentage[i, j].Value.ToString().Substring(0, copercentage[i,j].Value.ToString().Length - 1));
                                    }
                                    double percentage = (sum / (studentsNo - Double.Parse(dataGridView4[3, dataGridView4.RowCount - 1].Value.ToString())));
                                    if (percentage > 100) percentage = 100;
                                    copercentage[i, cototal.RowCount - 1].Value = Math.Round(percentage, 2, MidpointRounding.ToEven) + "%";

                                }
                            }
                        }
                        catch (Exception kjk) {
                        }
                    }
                }
                if (e.ColumnIndex == 1)
                {
                    if((e.RowIndex != dataGridView4.RowCount - 1) && (e.RowIndex > 0))
                    {
                        try {
                            cototal[0, e.RowIndex].Value = dataGridView4[e.ColumnIndex, e.RowIndex].Value;
                            coattainment[0, e.RowIndex].Value = dataGridView4[e.ColumnIndex, e.RowIndex].Value;
                            copercentage[0, e.RowIndex].Value = dataGridView4[e.ColumnIndex, e.RowIndex].Value;
                        } catch(Exception kl) { }
                    }
                }
                if (e.ColumnIndex > 3)
                {
                    if ((e.RowIndex != dataGridView4.RowCount - 1) && (e.RowIndex > 0))
                    {
                        try {
                            double sum = 0;
                            int colNumber = (e.ColumnIndex-4)%cos;
                            for (int i = colNumber+4; i < cos * ass; i += cos)
                            {
                                sum += Double.Parse(dataGridView4[i, e.RowIndex].Value.ToString());
                            }

                            cototal[((e.ColumnIndex - 4) % cos)+1 , e.RowIndex].Value = sum.ToString();
                        }catch(Exception lk)
                        {

                        }
                        }
                }
               
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click_2(object sender, EventArgs e)
        {
            DataObject o = (DataObject)Clipboard.GetDataObject();
            if (o.GetDataPresent(DataFormats.Text))
            {
                int rowOfInterest = dataGridView4.CurrentCell.RowIndex;
                string[] selectedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");

                if (selectedRows == null || selectedRows.Length == 0)
                    return;

                foreach (string row in selectedRows)
                {
                    if (rowOfInterest >= dataGridView4.Rows.Count)
                        break;

                    try
                    {
                        string[] data = Regex.Split(row, "\t");
                        int col = dataGridView4.CurrentCell.ColumnIndex;
                        foreach (string ob in data)
                        {
                            if (col >= dataGridView4.Columns.Count)
                                break;
                            if (col == 3) col++;
                            if (ob != null && !ob.Equals(""))
                                dataGridView4[col, rowOfInterest].Value = Convert.ChangeType(ob, dataGridView4[col, rowOfInterest].ValueType);
                            col++;
                        }
                    }
                    catch (Exception enterException)
                    {
                        MessageBox.Show("enterException");
                    }
                    rowOfInterest++;

                }
            }
        }

        private void FlowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!init)
            {
                if (e.ColumnIndex != 0)
                {

                    double sum = 0;
                    for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        try
                        {
                            sum += Double.Parse(dataGridView1[e.ColumnIndex, i].Value.ToString());
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("ex");
                        }


                    }
                    dataGridView1[e.ColumnIndex, dataGridView1.RowCount - 1].Value = sum;
                    try
                    {
                        int u, v;
                        u = (e.RowIndex * cos) + (e.ColumnIndex - 1) + 4;
                        v = totalRowNumber;
                        cototal[e.ColumnIndex , 0].Value = sum;
                        
                        dataGridView4[u, v].Value = dataGridView1[e.ColumnIndex,e.RowIndex].Value;
                    }
                    catch (Exception k)
                    {

                    }

                }
                else if (e.ColumnIndex == 0)
                {
                    int row = e.RowIndex;
                    int col = 4 + row * cos;
                    int k = 1;
                    for(int i =col;i<col+cos;i++)
                    {
                        dataGridView4.Columns[i].Name = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()+"CO"+k;
                        k++;
                    }
                }
            }
        }

        private void DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.ColumnIndex + " " + e.RowIndex);
        }

        private void Cototal_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!firstTime)
            {
                if (e.RowIndex != 0 && e.ColumnIndex!=0 && e.RowIndex!=(cototal.RowCount-1))
                {
                    double sum=0;
                    for(int i = 1; i < cototal.RowCount - 1; i++)
                    {
                        sum+=Double.Parse(cototal[e.ColumnIndex, i].Value.ToString());
                    }
                    cototal[e.ColumnIndex, cototal.RowCount - 1].Value = Math.Round((sum/studentsNo),2,MidpointRounding.ToEven);
                    double value = Double.Parse(cototal[e.ColumnIndex, e.RowIndex].Value.ToString());
                    double percentage = Math.Round(((value / Double.Parse(cototal[e.ColumnIndex, 0].Value.ToString())) * 100), 0, MidpointRounding.ToEven);
                    if (percentage > 100) percentage = 100;
                    copercentage[e.ColumnIndex, e.RowIndex].Value = percentage + "%";
                    if (percentage >= 65) coattainment[e.ColumnIndex, e.RowIndex].Value = "1";
                    else coattainment[e.ColumnIndex, e.RowIndex].Value = "0";
                }
            }
        }

        private void Copercentage_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!firstTime)
            {
                if(e.RowIndex!=0 && e.ColumnIndex != 0 && e.RowIndex!=(copercentage.RowCount-1))
                {

                    double sum = 0;
                    for (int i = 1; i < copercentage.RowCount - 1; i++)
                    {
                        sum += Double.Parse(copercentage[e.ColumnIndex, i].Value.ToString().Substring(0, copercentage[e.ColumnIndex, i].Value.ToString().Length - 1));
                    }
                    double percentage = (sum / (studentsNo - Double.Parse(dataGridView4[3, dataGridView4.RowCount - 1].Value.ToString())));
                    if (percentage > 100) percentage = 100;
                    copercentage[e.ColumnIndex, cototal.RowCount - 1].Value =Math.Round(percentage,2,MidpointRounding.ToEven)+"%";//percentage total                                        

                    if (percentage >= 65) coattainment[e.ColumnIndex, e.RowIndex].Value = "1";
                    else coattainment[e.ColumnIndex, e.RowIndex].Value = "0";
                }
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {

                DataObject o = (DataObject)Clipboard.GetDataObject();
                if (o.GetDataPresent(DataFormats.Text))
                {
                    int rowOfInterest = dataGridView1.CurrentCell.RowIndex;
                    string[] selectedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");

                    if (selectedRows == null || selectedRows.Length == 0)
                        return;

                    foreach (string row in selectedRows)
                    {
                        if (rowOfInterest >= dataGridView1.Rows.Count)
                            break;

                        try
                        {
                            string[] data = Regex.Split(row, "\t");
                            int col = dataGridView1.CurrentCell.ColumnIndex;

                            foreach (string ob in data)
                            {
                                if (col >= dataGridView1.Columns.Count)
                                    break;
                                if (ob != null && !ob.Equals(""))
                                    dataGridView1[col, rowOfInterest].Value = Convert.ChangeType(ob, dataGridView1[col, rowOfInterest].ValueType);
                                col++;
                            }

                        }
                        catch (Exception enterException)
                        {
                            // MessageBox.Show("enterException");
                        }
                        rowOfInterest++;

                    }
                    dataGridView1.Invalidate();
                }
            }
        }

        private void Coattainment_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!firstTime)
            {
                if (e.RowIndex != 0 && e.ColumnIndex != 0 && e.RowIndex!=(coattainment.RowCount-1))
                {
                    double sum = 0;
                    for(int i = 1; i < coattainment.RowCount-1; i++)
                    {
                        sum += Double.Parse(coattainment[e.ColumnIndex, i].Value.ToString());
                    }
                    coattainment[e.ColumnIndex, coattainment.RowCount - 1].Value = sum ;//total student
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            ArrayList finalcoval = new ArrayList();
            ArrayList finalstudent = new ArrayList();
            for (int i = 1; i < copercentage.ColumnCount; i++)
            {
                finalcoval.Add(copercentage[i, copercentage.RowCount - 1].Value.ToString());

            }

            for (int j = 1; j < coattainment.ColumnCount; j++)
            {
                finalstudent.Add(coattainment[j, coattainment.RowCount - 1].Value.ToString());

            }
            new Graph(finalcoval,finalstudent, copercentage.ColumnCount, coattainment.ColumnCount, coursecode,coursename, coursesec, studentsNo, csemester, year).Show();

        }


        private void DataGridView4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.V && e.Modifiers == Keys.Control)
            {

                DataObject o = (DataObject)Clipboard.GetDataObject();
                if (o.GetDataPresent(DataFormats.Text))
                {
                    int rowOfInterest = dataGridView4.CurrentCell.RowIndex;
                    string[] selectedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");

                    if (selectedRows == null || selectedRows.Length == 0)
                        return;

                    foreach (string row in selectedRows)
                    {
                        if (rowOfInterest >= dataGridView4.Rows.Count)
                            break;

                        try
                        {
                            string[] data = Regex.Split(row, "\t");
                            int col = dataGridView4.CurrentCell.ColumnIndex;
                            foreach (string ob in data)
                            {
                                if (col >= dataGridView4.Columns.Count)
                                    break;
                                if (col == 3) col++;
                                if (ob != null && !ob.Equals(""))
                                    dataGridView4[col, rowOfInterest].Value = Convert.ChangeType(ob, dataGridView4[col, rowOfInterest].ValueType);
                                col++;
                            }
                        }
                        catch (Exception enterException)
                        {
                            MessageBox.Show("enterException");
                        }
                        rowOfInterest++;

                    }
                    try
                    {
                        for (int j = 4; j < dataGridView4.ColumnCount; j++)
                        {
                            double sum = 0;
                            for (int i = 1; i < dataGridView4.RowCount - 1; i++)
                            {
                                sum += Double.Parse(dataGridView4[j, i].Value.ToString());
                            }

                            dataGridView4[j, dataGridView4.RowCount - 1].Value = Math.Round((sum / studentsNo), 2, MidpointRounding.ToEven).ToString();

                        }
                    }
                    catch (Exception lk)
                    {

                    }

                }
            }
        }

        private void Copercentage_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundProcessLogicMethod();
        }
        private void BackgroundProcessLogicMethod()
        {
            DataGridViewTextBoxColumn poTextBox;
            for (int i = 0; i < ass; i++)
            {
                for (int j = 0; j < cos; j++)
                {
                    // MessageBox.Show(assesmentNames[i].ToString()+assesmentNames.Count);

                    poTextBox = new DataGridViewTextBoxColumn();
                    poTextBox.Name = assesmentNames[i+1].ToString() + "CO" + j;
                    poTextBox.ReadOnly = true;
                    poTextBox.HeaderText = "CO" + (j + 1);
                   dataGridView4.Invoke(new Action(()=> { dataGridView4.Columns.Add(poTextBox); }));

                }
            }
            poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "ID";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "ID";
            copercentage.Columns.Add(poTextBox);
            poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "ID";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "ID";
            cototal.Columns.Add(poTextBox);
            poTextBox = new DataGridViewTextBoxColumn();
            poTextBox.Name = "ID";
            poTextBox.ReadOnly = true;
            poTextBox.HeaderText = "ID";
            coattainment.Columns.Add(poTextBox);
            for (int i = 0; i < cos; i++)
            {

                poTextBox = new DataGridViewTextBoxColumn();
                poTextBox.Name = "totalCO" + (i + 1).ToString();
                poTextBox.ReadOnly = true;
                poTextBox.HeaderText = "CO" + ((i + 1).ToString());
                cototal.Columns.Add(poTextBox);
                poTextBox = new DataGridViewTextBoxColumn();
                poTextBox.Name = "percentageCO" + (i + 1).ToString();
                poTextBox.ReadOnly = true;
                poTextBox.HeaderText = "CO" + ((i + 1).ToString());
                copercentage.Columns.Add(poTextBox);
                poTextBox = new DataGridViewTextBoxColumn();
                poTextBox.Name = "attainCO" + (i + 1).ToString();
                poTextBox.ReadOnly = true;
                poTextBox.HeaderText = "CO" + ((i + 1).ToString());
                coattainment.Columns.Add(poTextBox);
            }


            cototal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            cototal.AutoResizeColumns();
            copercentage.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            copercentage.AutoResizeColumns();
            coattainment.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            coattainment.AutoResizeColumns();

            int row = cototal.Rows.Add();
            cototal[0, row].Value = "";
            cototal[0, row].ReadOnly = true;
            cototal[0, row].Style.BackColor = Color.White;
            cototal[0, row].Style.ForeColor = Color.Gray;
            cototal[0, row].Style.SelectionBackColor = Color.White;
            cototal[0, row].Style.SelectionForeColor = Color.Gray;
            for (int i = 1; i < cos; i++)
            {
                cototal[i, row].Value = dataGridView1[i + 1, dataGridView1.RowCount - 1].Value;
                cototal[i, row].ReadOnly = true;
                cototal[i, row].Style.BackColor = Color.White;
                cototal[i, row].Style.ForeColor = Color.Gray;
                cototal[i, row].Style.SelectionBackColor = Color.White;
                cototal[i, row].Style.SelectionForeColor = Color.Gray;
            }
            row = copercentage.Rows.Add();
            copercentage[0, row].Value = "";
            copercentage[0, row].ReadOnly = true;
            copercentage[0, row].Style.BackColor = Color.White;
            copercentage[0, row].Style.ForeColor = Color.Gray;
            copercentage[0, row].Style.SelectionBackColor = Color.White;
            copercentage[0, row].Style.SelectionForeColor = Color.Gray;
            for (int i = 1; i < copercentage.ColumnCount; i++)
            {
                copercentage[i, row].Value = "100%";
                copercentage[i, row].ReadOnly = true;
                copercentage[i, row].Style.BackColor = Color.White;
                copercentage[i, row].Style.ForeColor = Color.Gray;
                copercentage[i, row].Style.SelectionBackColor = Color.White;
                copercentage[i, row].Style.SelectionForeColor = Color.Gray;
            }
            row = coattainment.Rows.Add();

            List<DataGridViewRow> studentRows = new List<DataGridViewRow>();

            for (int i = 1; i <= studentsNo + 1; i++)
            {
                DataGridViewRow rows = new DataGridViewRow();
                rows.CreateCells(cototal);
                for (int j = 1; j < cos + 1; j++)
                {

                    rows.Cells[j].Value = "0";
                    rows.Cells[j].ReadOnly = true;
                }
                studentRows.Add(rows);
            }
            cototal.Rows.AddRange(studentRows.ToArray());
            cototal[0, cototal.RowCount - 1].Value = "Average"; //change............

            studentRows = new List<DataGridViewRow>();

            for (int i = 1; i <= studentsNo + 1; i++)
            {
                DataGridViewRow rows = new DataGridViewRow();
                rows.CreateCells(copercentage);
                for (int j = 1; j < cos + 1; j++)
                {
                    rows.Cells[j].Value = "0%";
                    rows.Cells[j].ReadOnly = true;
                }
                studentRows.Add(rows);
            }
            copercentage.Rows.AddRange(studentRows.ToArray());
            copercentage[0, cototal.RowCount - 1].Value = "Average";  //change............


            studentRows = new List<DataGridViewRow>();

            for (int i = 1; i <= studentsNo + 1; i++)
            {
                DataGridViewRow rows = new DataGridViewRow();
                rows.CreateCells(coattainment);
                for (int j = 1; j < cos + 1; j++)
                {
                    rows.Cells[j].Value = "0";
                    rows.Cells[j].ReadOnly = true;
                }
                studentRows.Add(rows);
            }
            coattainment.Rows.AddRange(studentRows.ToArray());
            coattainment[0, cototal.RowCount - 1].Value = "Total";  //change............

            cototal.AllowUserToAddRows = false;
            copercentage.AllowUserToAddRows = false;
            coattainment.AllowUserToAddRows = false;
            for (int j = 0; j < this.dataGridView4.ColumnCount; j++)

            {

                this.dataGridView4.Columns[j].Width = 42;

            }
            dataGridView4.Columns[0].Width = 45;
            dataGridView4.Columns[1].Width = 70;
            dataGridView4.Columns[2].Width = 150;
            dataGridView4.Columns[3].Width = 50;
            int rowId = dataGridView4.Rows.Add();
            totalRowNumber = rowId;
            DataGridViewRow total = dataGridView4.Rows[rowId];
            for (int i = 0; i < 4; i++)
            {

                DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();

                total.Cells[i] = TextBoxCell;
                total.Cells[i].ReadOnly = true;
                if (i != 0) total.Cells[i].Value = "-";
                else total.Cells[i].Value = "-";
            }
            dataGridView4[0, rowId].ReadOnly = true;
            dataGridView4[0, rowId].Style.BackColor = Color.White;
            dataGridView4[0, rowId].Style.ForeColor = Color.Gray;
            dataGridView4[1, rowId].ReadOnly = true;
            dataGridView4[1, rowId].Style.BackColor = Color.White;
            dataGridView4[1, rowId].Style.ForeColor = Color.Gray;
            dataGridView4[2, rowId].ReadOnly = true;
            dataGridView4[2, rowId].Style.BackColor = Color.White;
            dataGridView4[2, rowId].Style.ForeColor = Color.Gray;
            dataGridView4[3, rowId].ReadOnly = true;
            dataGridView4[3, rowId].Style.BackColor = Color.White;
            dataGridView4[3, rowId].Style.ForeColor = Color.Gray;
            dataGridView4[3, rowId].Style.SelectionBackColor = Color.White;
            dataGridView4[3, rowId].Style.SelectionForeColor = Color.Gray;
            dataGridView4[2, rowId].Style.SelectionBackColor = Color.White;
            dataGridView4[2, rowId].Style.SelectionForeColor = Color.Gray;
            dataGridView4[1, rowId].Style.SelectionBackColor = Color.White;
            dataGridView4[1, rowId].Style.SelectionForeColor = Color.Gray;
            dataGridView4[0, rowId].Style.SelectionBackColor = Color.White;
            dataGridView4[0, rowId].Style.SelectionForeColor = Color.Gray;
            int colCount = 4;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView1.Columns.Count; j++)
                {
                    dataGridView4[colCount, rowId].Value = dataGridView1[j, i].Value;
                    dataGridView4[colCount, rowId].ReadOnly = true;
                    dataGridView4[colCount, rowId].Style.BackColor = Color.White;
                    dataGridView4[colCount, rowId].Style.ForeColor = Color.Gray;
                    dataGridView4[colCount, rowId].Style.SelectionBackColor = Color.White;
                    dataGridView4[colCount, rowId].Style.SelectionForeColor = Color.Gray;
                    colCount++;
                }
            }
            List<DataGridViewRow> rowss = new List<DataGridViewRow>();

            for (int i = 1; i <= studentsNo; i++)
            {
                DataGridViewRow rows = new DataGridViewRow();
                rows.CreateCells(dataGridView4);
                rows.Cells[0].Value = String.Format(" {0}", i);
                rowss.Add(rows);
            }

            dataGridView4.Rows.AddRange(rowss.ToArray());
            dataGridView4.AllowUserToAddRows = false;

            rowId = dataGridView4.Rows.Add();

            total = dataGridView4.Rows[rowId];
            for (int i = 0; i < dataGridView4.ColumnCount; i++)
            {

                DataGridViewTextBoxCell TextBoxCell = new DataGridViewTextBoxCell();

                try
                {
                    total.Cells[i] = TextBoxCell;
                    total.Cells[i].ReadOnly = true;
                    if (i != 0) total.Cells[i].Value = "0";
                    else total.Cells[i].Value = "Total";
                }
                catch (Exception kll)
                {
                    String kl = kll.ToString();
                }
            }

            try
            {
                for (int j = 1; j < dataGridView4.RowCount - 1; j++)
                {

                    for (int i = 0; i < dataGridView4.ColumnCount; i++)
                    {
                        if (i != 3) dataGridView4[i, j].ReadOnly = false;
                        if (i > 3) dataGridView4[i, j].Value = "0";
                    }
                }
            }
            catch (Exception es)
            {

            }


            int height = 0;
            int heighttotal = 0;
            int heightpercentage = 0;
            int heightattainment = 0;
            foreach (DataGridViewRow rows in dataGridView4.Rows)
            {
                height += rows.Height;
            }
            height += dataGridView4.ColumnHeadersHeight;

            int width = 0;
            int widthtotal = 0;
            int widthpercentage = 0;
            int widthattainment = 0;
            foreach (DataGridViewColumn col in dataGridView4.Columns)
            {
                width += col.Width;
            }
            width += dataGridView4.RowHeadersWidth;
            dataGridView4.ClientSize = new Size(width + 2, height + 2);

            foreach (DataGridViewRow rows in cototal.Rows)
            {
                heighttotal += rows.Height;
                heightpercentage += rows.Height;
                heightattainment += rows.Height;
            }

            heighttotal += cototal.ColumnHeadersHeight;
            heightpercentage += copercentage.ColumnHeadersHeight;
            heightattainment += coattainment.ColumnHeadersHeight;
            foreach (DataGridViewColumn col in cototal.Columns)
            {
                widthtotal += col.Width;
                widthpercentage += col.Width;
                widthattainment += col.Width;
            }

            widthtotal += cototal.RowHeadersWidth;
            widthpercentage += cototal.RowHeadersWidth;
            widthattainment += cototal.RowHeadersWidth;
            cototal.ClientSize = new Size(widthtotal + 2, heighttotal + 2);
            copercentage.ClientSize = new Size(widthpercentage + 2, heightpercentage + 2);
            coattainment.ClientSize = new Size(widthattainment + 2, heightattainment + 2);
            dataGridView4.EnableHeadersVisualStyles = false;
            dataGridView4.EditingControlShowing += dataGridView4_EditingControlShowing;
            DataGridViewSelectedCellCollection selectedCells = dataGridView4.SelectedCells;
            // Call clearSelection 
            dataGridView4.ClearSelection();
            // Now You will get selectedCells count 0 here
            selectedCells = dataGridView4.SelectedCells;

            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                cototal[i, 0].Value = dataGridView1[i, dataGridView1.RowCount - 1].Value;
            }
            button1.Enabled = true;
        }

        private void Tables_FormClosed(object sender, FormClosedEventArgs e)
        {
            main.Show();
        }

        private void copercentage_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {

        }
    }
}
