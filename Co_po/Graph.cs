using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Co_po
{
    public partial class Graph : Form
    {
        ArrayList finalper = new ArrayList();
        ArrayList finalstu = new ArrayList();
        private int cototal;
        private int stutotal;
        private int coc = 1;
        private int stu = 1;
        private int cocode;
        private string corname;
        private int corsec;
        private int totalstudent;
        private string cusems;
        private int year;

        public Graph(ArrayList pervalue, ArrayList stuvalue, int cot, int stu, int ccode, string couname, int coursec, int studet, string csem, int cyear)
        {
            foreach(string k in pervalue)
            {
                if (k.Contains("%"))
                {
                    k.Replace("%","");
                }
            }
            this.finalper = pervalue;
            this.cototal = cot;
            this.finalstu = stuvalue;
            this.stutotal = stu;
            this.cocode = ccode;
            this.corname = couname;
            this.corsec = coursec;
            this.totalstudent = studet;
            this.cusems = csem;
            this.year = cyear;

            InitializeComponent();


        }

        private void Graph_Load(object sender, EventArgs e)
        {
            foreach (string fper in finalper)
            {
                string k=fper;
                if(fper.Contains("%"))k= fper.Substring(0, fper.Length - 1);
                this.chart1.Series["Series1"].Points.AddXY("CO" + coc, k);
                coc++;
            }

            DataGridViewTextBoxColumn elem = new DataGridViewTextBoxColumn();
            elem.Name = "Details";

            elem.HeaderText = "Details";
            dataGridView1.Columns.Add(elem);
            for (int i = 0; i < cototal - 1; i++)
            {
                DataGridViewTextBoxColumn cotxt = new DataGridViewTextBoxColumn();
                cotxt.Name = "CO" + (i + 1);

                dataGridView1.Columns.Add(cotxt);
            }
            for (int i = 0; i < 1; i++)
            {
                dataGridView1.Rows.Add();
            }
            DataGridViewRow info = dataGridView1.Rows[0];
            info.Cells[0].Value = "Number of Students";
            DataGridViewRow info2 = dataGridView1.Rows[1];
            info2.Cells[0].Value = "Average Score";
            int a = 1;
            foreach (string fstu in finalstu)
            {
                info.Cells[a].Value = fstu;
                a++;
            }

            int x = 1;
            foreach (string dper in finalper)
            {
                if (dper.Contains("%")) info2.Cells[x].Value = dper;
                else info2.Cells[x].Value = dper + "%";
                x++;
            }
            for (int c = 0; c < 4; c++)
            {
                DataGridViewTextBoxColumn cotxt2 = new DataGridViewTextBoxColumn();
                cotxt2.Name = "";
                dataGridView2.Columns.Add(cotxt2);
            }
            for (int i = 0; i <= 2; i++)
            {
                dataGridView2.Rows.Add();
            }

            DataGridViewRow info3 = dataGridView2.Rows[0];
            info3.Cells[0].Value = "Course Code :";
            info3.Cells[1].Value = "CSE " + cocode;
            info3.Cells[2].Value = "Section :";
            info3.Cells[3].Value = corsec;
            DataGridViewRow info4 = dataGridView2.Rows[1];
            info4.Cells[0].Value = "Course Title :";
            info4.Cells[1].Value = corname;
            info4.Cells[2].Value = "Number of Students :";
            info4.Cells[3].Value = totalstudent + " Students";
            DataGridViewRow info5 = dataGridView2.Rows[2];
            info5.Cells[0].Value = "Credit :";
            info5.Cells[1].Value = "3.0";
            info5.Cells[2].Value = "Semester :";
            info5.Cells[3].Value = cusems + " " + year;

            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.AutoResizeColumns();
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            int width= 0;
            foreach (DataGridViewColumn row in dataGridView1.Columns)
            {
                width += row.Width;
            }

            dataGridView1.ClientSize = new Size(width + 2, dataGridView1.Rows[0].Height*3 );


             width = 0;
            foreach (DataGridViewColumn row in dataGridView2.Columns)
            {
                width += row.Width;
            }

            dataGridView2.ClientSize = new Size(width + 2, dataGridView2.Rows[0].Height *4);
            GoFullscreen(true);


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
        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void TableLayoutPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void Chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Co-Po Summary.pdf";
                bool fileError = false;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    if (File.Exists(sfd.FileName))
                    {
                        try
                        {
                            File.Delete(sfd.FileName);
                        }
                        catch (IOException ex)
                        {
                            fileError = true;
                            MessageBox.Show("It wasn't possible to write the data to the disk." + ex.Message);
                        }
                    }
                    if (!fileError)
                    {
                        try
                        {
                            PdfPTable pdfTable = new PdfPTable(dataGridView1.Columns.Count);
                            PdfPTable pdfTable2 = new PdfPTable(dataGridView2.Columns.Count);
                            pdfTable.DefaultCell.Padding = 5;
                            pdfTable.WidthPercentage = 80;
                            pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTable2.DefaultCell.Padding = 5;
                            pdfTable2.WidthPercentage = 80;
                            pdfTable2.HorizontalAlignment = Element.ALIGN_CENTER;
                            //pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable.AddCell(cell.Value.ToString());
                                }
                            }

                            foreach (DataGridViewColumn column in dataGridView2.Columns)
                            {
                                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                pdfTable2.AddCell(cell);
                            }

                            foreach (DataGridViewRow row in dataGridView2.Rows)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    pdfTable2.AddCell(cell.Value.ToString());
                                }
                            }
                            Document pdfDoc = new Document(PageSize.A4, 10f, 20f, 20f, 10f);

                            using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                            {

                                PdfWriter.GetInstance(pdfDoc, stream);
                                pdfDoc.Open();
                                var chartimage = new MemoryStream();
                                chart1.SaveImage(chartimage, ChartImageFormat.Png);
                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(chartimage.GetBuffer());
                                var scalePercent = (((pdfDoc.PageSize.Width / img.Width) * 100) - 4);
                                img.ScalePercent(scalePercent);
                                img.Alignment = Element.ALIGN_CENTER;
                                iTextSharp.text.Font f = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 20, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);
                                Paragraph t1 = new Paragraph("-----------Summary of Co Po-----------", f);
                                t1.Alignment = Element.ALIGN_CENTER;
                                pdfDoc.Add(t1);
                                pdfDoc.Add(new Paragraph("\n\n"));
                                pdfDoc.Add(pdfTable2);
                                pdfDoc.Add(new Paragraph("\n\n"));
                                pdfDoc.Add(img);
                                pdfDoc.Add(new Paragraph("\n\n"));
                                pdfDoc.Add(pdfTable);
                                pdfDoc.Close();
                                stream.Close();
                            }

                            MessageBox.Show("Co Po Summary Exported Successfully !!!", "Co Po Maping Summary");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error :" + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("No Record To Export !!!", "Co Po Maping Summary");
            }
        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
