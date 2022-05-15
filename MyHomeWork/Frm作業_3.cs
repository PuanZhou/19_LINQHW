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
    public partial class Frm作業_3 : Form
    {
        public Frm作業_3()
        {
            InitializeComponent();
        }

        NorthwindEntities dbContext = new NorthwindEntities();


        private void AllClear()
        {
            this.treeView1.Nodes.Clear();
            this.dataGridView1.DataSource = null;
            this.listBox1.Items.Clear();
        }

      
        private void button4_Click(object sender, EventArgs e)
        {
            AllClear();

            int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (int item in nums)
            {
                TreeNode node;
                if (item <= 3)
                {
                    if (this.treeView1.Nodes["小的數值"] == null)
                    {
                        node = this.treeView1.Nodes.Add("小的數值", "小的數值");
                        node.Nodes.Add(item.ToString());
                        node.Tag = 0;
                    }
                    else
                    {
                        node = this.treeView1.Nodes["小的數值"];
                        node.Nodes.Add(item.ToString());
                        node.Tag = int.Parse(node.Tag.ToString()) + 1;
                    }
                    node.Text = $"小的數值({int.Parse(node.Tag.ToString()) + 1})";
                }
                else if (item > 3 && item <= 6)
                {
                    if (this.treeView1.Nodes["中的數值"] == null)
                    {
                        node = this.treeView1.Nodes.Add("中的數值", "中的數值");
                        node.Nodes.Add(item.ToString());
                        node.Tag = 0;
                    }
                    else
                    {
                        node = this.treeView1.Nodes["中的數值"];
                        node.Nodes.Add(item.ToString());
                        node.Tag = int.Parse(node.Tag.ToString()) + 1;
                    }
                    node.Text = $"中的數值({int.Parse(node.Tag.ToString()) + 1})";
                }
                else if (item > 6)
                {
                    if (this.treeView1.Nodes["大的數值"] == null)
                    {
                        node = this.treeView1.Nodes.Add("大的數值", "大的數值");
                        node.Nodes.Add(item.ToString());
                        node.Tag = 0;
                    }
                    else
                    {
                        node = this.treeView1.Nodes["大的數值"];
                        node.Nodes.Add(item.ToString());
                        node.Tag = int.Parse(node.Tag.ToString()) + 1;
                    }
                    node.Text = $"大的數值({int.Parse(node.Tag.ToString()) + 1})";
                }
            }

        }
        IEnumerable<Object> q1;

        int? flag;

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (flag == 1)
            {
                this.dataGridView2.DataSource = q1.ToList();
            }
            else if (flag == 2)
            {
                this.dataGridView2.DataSource = q2.ToList();
            }
            else if (flag == 3)
            {
                this.dataGridView2.DataSource = q3.ToList();
            }
        }
        private void button38_Click(object sender, EventArgs e)
        {
            AllClear();

            flag = 1;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            this.dataGridView1.DataSource = files;


            var q = from f in files
                    orderby f.Length descending
                    group f by FileSize(f) into g
                    select new
                    {
                        FileSize = g.Key,
                        Filecount = g.Count(),
                        Filegroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            q1=files.Where(f => FileSize(f) == this.dataGridView1.CurrentRow.Cells[0].Value.ToString()).OrderByDescending(f => f.Length);

            this.dataGridView2.DataSource = q1.ToList();

            foreach (var group in q)
            {
                string s = $"{group.FileSize} ({group.Filecount})";
                TreeNode treeNode = this.treeView1.Nodes.Add(group.FileSize.ToString(), s);
                foreach (var item in group.Filegroup)
                {
                    treeNode.Nodes.Add(item.ToString());
                }
            }
        }


        private string FileSize(FileInfo f)
        {
            if (f.Length <= 100)
            {
                return "Small";
            }
            else if (f.Length < 500)
            {
                return "Medium";
            }
            else
            {
                return "Large";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AllClear();

            flag = 1;

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");

            System.IO.FileInfo[] files = dir.GetFiles();

            this.dataGridView1.DataSource = files;

            var q = from f in files
                    orderby f.CreationTime descending
                    group f by FilesTime(f) into g
                    orderby g.Key descending
                    select new
                    {
                        Filetime = g.Key,
                        Filecount = g.Count(),
                        Filegroup = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            q1 = files.Where(f => FilesTime(f) == dataGridView1.CurrentCell.Value.ToString()).OrderByDescending(f => f.CreationTime);

            dataGridView2.DataSource = q1.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Filetime} ({group.Filecount})";

                TreeNode node = this.treeView1.Nodes.Add(group.Filetime.ToString(), s);

                foreach (var item in group.Filegroup)
                {
                    node.Nodes.Add(item.ToString());
                }
            }
        }

        private string FilesTime(FileInfo f)
        {
            if (f.CreationTime.Year == 2022)
            {
                return "2022";
            }
            else if (f.CreationTime.Year >= 2021 && f.CreationTime.Year == 2021)
            {
                return "2021";
            }
            else if (f.CreationTime.Year == 2020)
            {
                return "2020";
            }
            else
            {
                return "2019";
            }
        }

        IEnumerable<Product> q2;
        private void button8_Click(object sender, EventArgs e)
        {
            AllClear();

            flag = 2;

            var q = from p in this.dbContext.Products.AsEnumerable()
                    group p by Unitprice(p) into g
                    select new
                    {
                        PriceLevel = g.Key,
                        Count = g.Count(),
                        Group = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            q2 = this.dbContext.Products.AsEnumerable()
                .Where(p => Unitprice(p) == dataGridView1.CurrentCell.Value.ToString());

            dataGridView2.DataSource = q2.ToList();

            foreach (var group in q)
            {
                string s = $"{group.PriceLevel} ({group.Count})";

                TreeNode node = this.treeView1.Nodes.Add(group.PriceLevel.ToString(), s);

                foreach (var item in group.Group)
                {
                    string p = $"{item.ProductName} UnitePrice={item.UnitPrice:C2}";
                    node.Nodes.Add(item.ToString(), p);
                }
            }


        }

        private string Unitprice(Product p)
        {
            if (p.UnitPrice < 20)
            {
                return "Super Low Price";
            }
            else if (p.UnitPrice >= 20 && p.UnitPrice < 50)
            {
                return " Low Price";
            }
            else if (p.UnitPrice >= 50 && p.UnitPrice < 100)
            {
                return "Mid Price";
            }
            else if (p.UnitPrice >= 100 && p.UnitPrice < 200)
            {
                return "High Price ";
            }
            else
            {
                return "Super High Price";
            }
        }

        IEnumerable<Order> q3;
        private void button15_Click(object sender, EventArgs e)
        {
            AllClear();

            flag = 3;

            var q = from o in this.dbContext.Orders
                    group o by o.OrderDate.Value.Year into g
                    select new
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        Group = g
                    };

            this.dataGridView1.DataSource = q.ToList();

            q3 = this.dbContext.Orders.AsEnumerable().Where(o => o.OrderDate.Value.ToString("yyyy") == this.dataGridView1.CurrentCell.Value.ToString());

            this.dataGridView2.DataSource = q3.ToList();

            foreach (var group in q)
            {
                string s = $"{group.Year} ({group.Count})";

                TreeNode node = this.treeView1.Nodes.Add(group.Year.ToString(), s);

                foreach (var item in group.Group)
                {
                    string p = $"{item.OrderID} Date({item.OrderDate})";
                    node.Nodes.Add(item.ToString(), p);
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            AllClear();

            flag = 3;

            var q = this.dbContext.Orders.AsEnumerable().GroupBy(o => o.OrderDate.Value.ToString("yyyy-MM"))
                    .Select(g => new 
                    { YearnMonth = g.Key,
                        Count = g.Count(), 
                        Group = g });

            this.dataGridView1.DataSource = q.ToList();

            q3 = this.dbContext.Orders.AsEnumerable().Where(o => o.OrderDate.Value.ToString("yyyy-MM") == this.dataGridView1.CurrentCell.Value.ToString());

            this.dataGridView2.DataSource = q3.ToList();

            foreach (var group in q)
            {
                string s = $"{group.YearnMonth} ({group.Count})";

                TreeNode node = this.treeView1.Nodes.Add(group.YearnMonth.ToString(), s);

                foreach (var item in group.Group)
                {
                    string p = $"{item.OrderID}";
                    node.Nodes.Add(item.ToString(), p);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllClear();

            decimal TotalAmount = this.dbContext.Order_Details.Sum(o => o.UnitPrice * o.Quantity);

            this.listBox1.Items.Add($"總銷售金額為{TotalAmount:c2}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AllClear();

            var q = this.dbContext.Order_Details.AsEnumerable()
               .Select(o => new { FullName = o.Order.Employee.FirstName + " " + o.Order.Employee.LastName, Amount = o.UnitPrice * o.Quantity }).GroupBy(o => o.FullName)

               .Select(o => new { FullName = o.Key, Amount = o.Sum(p => p.Amount) }).OrderByDescending(o => o.Amount)
               .Select(o => new { FullName = o.FullName, Amount = $"{o.Amount:C2}" }).Take(5);


            this.dataGridView1.DataSource = q.ToList();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AllClear();
            var q = this.dbContext.Products.OrderByDescending(p => p.UnitPrice).Take(5).Select(p => new { p.Category.CategoryName, p.ProductName, p.UnitPrice });
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AllClear();
            bool result;
            result = this.dbContext.Products.Any(p => p.UnitPrice > 300);

            MessageBox.Show(result.ToString());
        }

       
    }
}
