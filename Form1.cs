using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Prakt_18_LINQ
{
    public partial class Form1 : Form
    {
        struct Workers
        {
            public string code;
            public string name;
            public string education;
            public string profession;
            public int year;
        }
        struct Salary
        {
            public string code;
            public float salary1;
            public float salary2;
        }

        List<Workers> lw = null;
        List<Salary> ls = null;

        public void Read_Workers()
        {
            StreamReader sr = File.OpenText(@"C:\Users\Lenovo\Desktop\Workers.txt");
            string[] fields;
            string line = null;
            Workers w;

            line = sr.ReadLine();

            while (line != null)
            {
                fields = line.Split(';');

                w.code = fields[0];
                w.name = fields[1];
                w.education = fields[2];
                w.profession = fields[3];
                w.year = int.Parse(fields[4]);

                lw.Add(w);

                listBox1.Items.Add(line);

                line = sr.ReadLine();
            }
        }

        public void Read_Salary()
        {
            StreamReader sr = File.OpenText(@"C:\Users\Lenovo\Desktop\Salary.txt");
            string[] fields;
            string line = null;
            Salary s;

            line = sr.ReadLine();

            while (line != null)
            {
                fields = line.Split(';');

                s.code = fields[0];
                s.salary1 = (float)Convert.ToDouble(fields[1]);
                s.salary2 = (float)Double.Parse(fields[2]);

                ls.Add(s);

                listBox2.Items.Add(line);

                line = sr.ReadLine();
            }
        }

        public Form1()
        {
            InitializeComponent();

            lw = new List<Workers>();
            ls = new List<Salary>();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            Read_Workers();
            Read_Salary();
        }

        // less than 30 years
        private void button1_Click(object sender, EventArgs e)
        {
            var names = from nm in lw
                        where nm.year > (2022 - 30)
                        select nm.name;

            listBox3.Items.Clear();

            foreach (string s in names) listBox3.Items.Add(s);
        }

        // Bachelors
        private void button2_Click(object sender, EventArgs e)
        {
            var bach = from b in lw
                       where b.education == " Bachelor"
                              select b.name;
            listBox3.Items.Clear();

            foreach (string s in bach)
                listBox3.Items.Add(s);
        }

        // salary less than average for year
        private void button3_Click(object sender, EventArgs e)
        {
            var result = from w in lw
                         from sl in ls
                         let avg = (from s in ls
                                    select (s.salary1 + s.salary2)).Average()
                         where ((sl.salary1 + sl.salary2) < avg) && (w.code == sl.code)
                         select w.name + " - " + w.profession;

            listBox3.Items.Clear();

            foreach (string s in result)
                listBox3.Items.Add(s);
        }  
    }    
}
