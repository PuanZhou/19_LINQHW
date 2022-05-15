using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        public Frm作業_1()
        {
            InitializeComponent();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(nwDataSet1.Products);
            this.comboBox1.Text = "請選擇年份";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            FillFileData("Log");

            this.button12.Visible = false;
            this.button13.Visible = false;
        }
        private void FillFileData(string Solution)
        {
            this.dataGridView2.DataSource = null;
            this.dataGridView1.CellClick -= DataGridView1_CellClick;
            Isdelegate = false;
            lblMaster.Text = "電腦檔案";

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            System.Collections.Generic.IEnumerable<System.IO.FileInfo> files1 = null;

            if (Solution == "Log")
            {
                files1 = from f in files
                         where f.FullName.Contains("log")
                         select f;
            }
            else if (Solution == "CreationTime")
            {
                files1 = from f in files
                         where f.CreationTime.Year == 2021
                         select f;
            }
            else if (Solution == "LargeFiles")
            {
                files1 = from f in files
                         where f.Length > 100000
                         select f;
            }

            this.dataGridView1.DataSource = files1.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FillFileData("CreationTime");

            this.button12.Visible = false;
            this.button13.Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FillFileData("LargeFiles");

            this.button12.Visible = false;
            this.button13.Visible = false;
        }


        bool Isdelegate = false;
        private void button6_Click(object sender, EventArgs e)
        {

            RegisterCellClick();

            this.button12.Visible = false;
            this.button13.Visible = false;

            this.comboBox1.Text = "請選擇年份";

            this.bindingSource1.DataSource = this.nwDataSet1.Orders;
            this.dataGridView1.DataSource = this.nwDataSet1.Orders;
            this.dataGridView2.DataSource = this.nwDataSet1.Order_Details;
            Isdelegate = Isdelegate = true;
        }

        private void RegisterCellClick()
        {
            if (Isdelegate == false)
            {
                this.dataGridView1.CellClick += DataGridView1_CellClick;
            }
            this.lblMaster.Text = "Oreders 訂單";
            this.lblDetails.Text = "Oreders 訂單明細";
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int position = (int)this.dataGridView1.CurrentRow.Cells[0].Value;

            var q = from o in this.nwDataSet1.Order_Details
                    where o.OrderID == position
                    select o;

            this.dataGridView2.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterCellClick();
           
            this.button12.Visible = false;
            this.button13.Visible = false;

            if (comboBox1.Text == "請選擇年份")
            {
                MessageBox.Show("請選擇年份");
                return;
            }
            else
            {
                var q = from o in this.nwDataSet1.Orders
                        where o.OrderDate.Year == int.Parse(comboBox1.Text)
                        select o;

                this.dataGridView1.DataSource = q.ToList();
            }

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void comboBox1_Click(object sender, EventArgs e)
        {
            IEnumerable<int> q = (from o in nwDataSet1.Orders
                                  orderby o.OrderDate.Year descending
                                  select o.OrderDate.Year).Distinct();

            comboBox1.DataSource = q.ToList();
        }

        int productCount;
        private void button3_Click(object sender, EventArgs e)
        {
            this.dataGridView2.DataSource = this.nwDataSet1.Products;
            this.button12.Visible = true;
            this.button13.Visible = true;
            productCount = (from p in nwDataSet1.Products select p).Count();
            this.dataGridView1.DataSource = null;
            lblDetails.Text = "Products Table";
            rows = 0;
            num = 0;
        }



        int rows = 0, num = 0;
        private void button13_Click(object sender, EventArgs e)
        {
            //this.nwDataSet1.Products.Take(10);//Top 10 Skip(10)

            //Distinct()

            bool Isnum = int.TryParse(textBox1.Text, out rows);

            if (Isnum)
            {
                if (num+rows <=productCount)
                {
                    num += rows;
                    var q = (from p in nwDataSet1.Products 
                             where !p.IsCategoryIDNull()&& !p.IsQuantityPerUnitNull()
                             &&!p.IsReorderLevelNull()&&!p.IsSupplierIDNull()
                             &&!p.IsUnitPriceNull()&& !p.IsUnitsInStockNull()&&!p.IsUnitsOnOrderNull()
                             select p).Skip(num).Take(rows);
                    dataGridView2.DataSource = q.ToList();
                }
            }
            else
            {
                MessageBox.Show("請輸入正整數");
                return;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            bool Isnum = int.TryParse(textBox1.Text, out rows);

            if (Isnum)
            {
                if (num - rows >=0)
                {
                    num -= rows;
                    var q = (from p in nwDataSet1.Products
                             where !p.IsCategoryIDNull() && !p.IsQuantityPerUnitNull()
                             && !p.IsReorderLevelNull() && !p.IsSupplierIDNull()
                             && !p.IsUnitPriceNull() && !p.IsUnitsInStockNull() && !p.IsUnitsOnOrderNull()
                             select p).Skip(num).Take(rows);
                    dataGridView2.DataSource = q.ToList();
                }
                else
                {
                    num = 0;
                    var q = (from p in nwDataSet1.Products
                             where !p.IsCategoryIDNull() && !p.IsQuantityPerUnitNull()
                             && !p.IsReorderLevelNull() && !p.IsSupplierIDNull()
                             && !p.IsUnitPriceNull() && !p.IsUnitsInStockNull() && !p.IsUnitsOnOrderNull()
                             select p).Skip(num).Take(rows);
                    dataGridView2.DataSource = q.ToList();
                }
            }
            else
            {
                MessageBox.Show("請輸入正整數");
                return;
            }
        }
    }
}

