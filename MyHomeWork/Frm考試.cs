using MyHomeWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinqLabs
{
    public partial class Frm考試 : Form
    {
        public Frm考試()
        {
            InitializeComponent();

            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },

                                          };

            this.comboBox1.DataSource = combostring.ToList();
            this.comboBox2.DataSource = combo2string.ToList();
        }

        List<Student> students_scores;

        NorthwindEntities dbContext = new NorthwindEntities();
        public class Student
        {
            public string Name { get; set; }
            public string Class { get; set; }
            public int Chi { get; set; }
            public int Eng { get; internal set; }
            public int Math { get; set; }
            public string Gender { get; set; }
        }

        string[] combostring = { "共幾個 學員成績 ?", "找出 前面三個 的學員所有科目成績	", "找出 後面兩個 的學員所有科目成績",
            "找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績","找出學員 'bbb' 的成績","找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)",
            "找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績","數學不及格 ... 是誰"};

        string[] combo2string = { "個人 sum, min, max, avg", "各科 sum, min, max, avg" };
        private void button36_Click(object sender, EventArgs e)
        {


            if (this.comboBox1.SelectedIndex == 0)
            {
                int q = this.students_scores.Count();
                MessageBox.Show($"共{q}個學生成績");
            }
            else if (this.comboBox1.SelectedIndex == 1)
            {
                var q = this.students_scores.Take(3);
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 2)
            {
                var q = this.students_scores.Skip(this.students_scores.Count - 2);
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 3)
            {
                var q = this.students_scores.Where(p => p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc").Select(s => new
                {
                    姓名 = s.Name,
                    國文成績 = s.Chi,
                    英文成績 = s.Eng
                });
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 4)
            {
                var q = this.students_scores.Where(p => p.Name == "bbb");
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 5)
            {
                var q = this.students_scores.Where(p => p.Name != "bbb");
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 6)
            {
                var q = this.students_scores.Where(s => s.Name == "aaa" || s.Name == "bbb" || s.Name == "ccc").Select(s => new
                {
                    姓名 = s.Name,
                    國文 = s.Chi,
                    數學 = s.Math
                });
                this.dataGridView1.DataSource = q.ToList();
            }
            else if (this.comboBox1.SelectedIndex == 7)
            {
                var q = this.students_scores.Where(s => s.Math < 60);
                this.dataGridView1.DataSource = q.ToList();
            }

            // 
            // 共幾個 學員成績 ?						

            // 找出 前面三個 的學員所有科目成績					
            // 找出 後面兩個 的學員所有科目成績					

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						

            // 找出學員 'bbb' 的成績	                          

            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	

            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績  |				
            // 數學不及格 ... 是誰 


        }

        private void button37_Click(object sender, EventArgs e)
        {

            if (comboBox2.SelectedIndex == 0)
            {
                var q = this.students_scores.Select(s => new
                {
                    姓名 = s.Name,
                    總分 = s.Chi + s.Eng + s.Math,
                    最小分數 = Min(s.Chi, s.Eng, s.Math),
                    最大分數 = Max(s.Chi, s.Eng, s.Math),
                    平均分數 = (s.Chi + s.Eng + s.Math) / 3
                });

                this.dataGridView1.DataSource = q.ToList();
            }

            if (comboBox2.SelectedIndex == 1)
            {
                this.listBox1.Items.Clear();

                int q1 = this.students_scores.Sum(s => s.Chi);
                int q2 = this.students_scores.Sum(s => s.Eng);
                int q3 = this.students_scores.Sum(s => s.Math);
 
                this.listBox1.Items.Add($"國文科加總={q1}  數學科加總={q2} 英文科加總={q3}");

                this.listBox1.Items.Add(string.Empty);

                q1 = this.students_scores.Min(s => s.Chi);
                q2 = this.students_scores.Min(s => s.Chi);
                q3 = this.students_scores.Min(s => s.Chi);

                this.listBox1.Items.Add($"國文科最低分={q1}  數學科最低分={q2} 英文科最低分={q3}");
                this.listBox1.Items.Add(string.Empty);

                q1 = this.students_scores.Max(s => s.Chi);
                q2 = this.students_scores.Max(s => s.Chi);
                q3 = this.students_scores.Max(s => s.Chi);

                this.listBox1.Items.Add($"國文科最高分={q1}  數學科最高分={q2} 英文科最高分={q3}");
                this.listBox1.Items.Add(string.Empty);

                q1 = (int)this.students_scores.Average(s => s.Chi);
                q2 = (int)this.students_scores.Average(s => s.Chi);
                q3 = (int)this.students_scores.Average(s => s.Chi);

                this.listBox1.Items.Add($"國文科平均分={q1}  數學科平均分={q2} 英文科平均分={q3}");
            }

            

            //個人 sum, min, max, avg

            //各科 sum, min, max, avg
        }

        private object Max(int chi, int eng, int math)
        {
            int result = chi;

            result = result < eng ? eng : result;

            result = result < math ? math : result;

            return result;
        }

        private object Min(int chi, int eng, int math)
        {
            int result = chi;

            if (result > eng)
            {
                result = eng;
            }

            if (result > math)
            {
                result = math;
            }
            return result;

        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100) 
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private void button35_Click(object sender, EventArgs e)
        {
            // 統計 :　所有隨機分數出現的次數/比率; sort ascending or descending
            // 63     7.00%
            // 100    6.00%
            // 78     6.00%
            // 89     5.00%
            // 83     5.00%
            // 61     4.00%
            // 64     4.00%
            // 91     4.00%
            // 79     4.00%
            // 84     3.00%
            // 62     3.00%
            // 73     3.00%
            // 74     3.00%
            // 75     3.00%
        }

        private void button34_Click(object sender, EventArgs e)
        {

            var q = this.dbContext.Order_Details.AsEnumerable().Select(o => new { Year = o.Order.OrderDate.Value.Year, Amount = o.UnitPrice * o.Quantity })
                .GroupBy(g => g.Year).Select(g => new { g.Key, Amount = g.Sum(o => o.Amount) }).OrderByDescending(g => g.Amount);

            this.listBox1.Items.Add($"年度最高銷售金額={q.First().ToString()}");

            // 年度最高銷售金額 年度最低銷售金額
            // 那一年總銷售最好 ? 那一年總銷售最不好 ?  
            // 那一個月總銷售最好 ? 那一個月總銷售最不好 ?

            // 每年 總銷售分析 圖
            // 每月 總銷售分析 圖
        }


    }
}
