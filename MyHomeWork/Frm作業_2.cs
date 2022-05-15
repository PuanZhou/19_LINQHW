using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();

            this.productPhotoTableAdapter1.Fill(this.awDataSet1.ProductPhoto);
            LoadDataToDataTimePicker();
            this.comboBox3.Text = "請選擇年份";
            this.comboBox2.Text = "請選擇季度";
        }

        private void LoadDataToDataTimePicker()
        {
            DateTime minDateTime = this.awDataSet1.ProductPhoto.Min(p => p.ModifiedDate);
            DateTime maxDateTime = this.awDataSet1.ProductPhoto.Max(p => p.ModifiedDate);
            this.dateTimePicker1.Value = minDateTime;
            this.dateTimePicker1.MinDate = minDateTime;
            this.dateTimePicker2.Value = maxDateTime;
            this.dateTimePicker2.MaxDate = maxDateTime;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var q = this.awDataSet1.ProductPhoto.OrderBy(p => p.ModifiedDate);

            int count = this.awDataSet1.ProductPhoto.Count;

            this.bindingSource1.DataSource = q.ToList();

            this.dataGridView1.DataSource = this.bindingSource1;

            this.lblMaster.Text = $"所有腳踏車總共有{count}筆";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var q = awDataSet1.ProductPhoto.Where(p => p.ModifiedDate >= this.dateTimePicker1.Value && p.ModifiedDate <= this.dateTimePicker2.Value).OrderBy(p => p.ModifiedDate);

            int count = awDataSet1.ProductPhoto.Where(p => p.ModifiedDate >= this.dateTimePicker1.Value && p.ModifiedDate <= this.dateTimePicker2.Value).Count();

            this.bindingSource1.DataSource = q.ToList();
            this.dataGridView1.DataSource = this.bindingSource1;
            this.lblMaster.Text = $"{dateTimePicker1.Value.ToString("Y")}到{dateTimePicker2.Value.ToString("Y")}區間的腳踏車共有{count}筆";
        }

        private void comboBox3_Click(object sender, EventArgs e)
        {
            var year = this.awDataSet1.ProductPhoto.Select(p => p.ModifiedDate.Year).Distinct();
            this.comboBox3.DataSource = year.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "請選擇年份")
            {
                MessageBox.Show("請選擇一個年份", "請選擇一個年份", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                var q = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Year == int.Parse(comboBox3.Text));

                int count = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Year == int.Parse(comboBox3.Text)).Count();

                this.bindingSource1.DataSource = q.ToList();
                this.dataGridView1.DataSource = q.ToList();

                this.lblMaster.Text = $"{comboBox3.Text}年間的腳踏車共有{count}筆";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    var q = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Month >= 1 && p.ModifiedDate.Month <= 3);
                    this.bindingSource1.DataSource = q.ToList();
                    this.dataGridView1.DataSource = this.bindingSource1;
                    this.lblMaster.Text = $"{comboBox2.Text}的腳踏車共有{q.Count()}筆";
                    break;

                case 1:
                    q = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Month >= 4 && p.ModifiedDate.Month <= 6);
                    this.bindingSource1.DataSource = q.ToList();
                    this.dataGridView1.DataSource = this.bindingSource1;
                    this.lblMaster.Text = $"{comboBox2.Text}的腳踏車共有{q.Count()}筆";
                    break;

                case 2:
                    q = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Month >= 7 && p.ModifiedDate.Month <= 9);
                    this.bindingSource1.DataSource = q.ToList();
                    this.dataGridView1.DataSource = this.bindingSource1;
                    this.lblMaster.Text = $"{comboBox2.Text}的腳踏車共有{q.Count()}筆";
                    break;

                case 3:
                    q = this.awDataSet1.ProductPhoto.Where(p => p.ModifiedDate.Month >= 10 && p.ModifiedDate.Month <= 12);
                    this.bindingSource1.DataSource = q.ToList();
                    this.dataGridView1.DataSource = this.bindingSource1;
                    this.lblMaster.Text = $"{comboBox2.Text}的腳踏車共有{q.Count()}筆";
                    break;
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int position = (int)this.dataGridView1.CurrentRow.Cells[0].Value;
            var photobytes = this.awDataSet1.ProductPhoto.Where(p => p.ProductPhotoID == position).SelectMany(p=>p.LargePhoto).ToArray();

            this.pictureBox1.Image = ConvertToImage(photobytes);


        }
        private Image ConvertToImage(byte[] photobytes)
        {
            Image photo = null;

            using (MemoryStream ms = new MemoryStream(photobytes))
            {
                photo = Image.FromStream(ms);
            }

            return photo;
        }

    }
}
