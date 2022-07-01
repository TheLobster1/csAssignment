using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HolidayEntitlementAssignment
{
    public partial class Form1 : Form
    {
        Company company = new Company();
        string dbConnection = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\rjeve\source\repos\HolidayEntitlementAssignment\HolidayEntitlementAssignment\Database1.mdf;Integrated Security=True";
        SqlCommand command;
        SqlDataAdapter adapter = new SqlDataAdapter();
        String sql = "";
        public Form1()
        {
            InitializeComponent();
            SqlConnection con = new SqlConnection(dbConnection);
            con.Open();
            sql = "DELETE FROM Employees;";
            command = new SqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();
            company.addDepartment(new Department(1));
            company.addDepartment(new Department(2));
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            toolStripMenuItem1.Click += new EventHandler(addEmployeeEvent);
            toolStripMenuItem2.Click += new EventHandler(viewEmployeesEvent);
            toolStripMenuItem3.Click += new EventHandler(viewStatsEvent);
            toolStripMenuItem4.Click += new EventHandler(viewAboutEvent);
        }
        private void addEmployeeEvent(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
            this.tabControl1.SelectTab(0);
        }
        private void viewEmployeesEvent(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
            this.tabControl1.SelectTab(1);
        }
        private void viewStatsEvent(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
            this.tabControl1.SelectTab(2);
        }
        private void viewAboutEvent(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
            this.tabControl1.SelectTab(3);
        }

        private void addEmployee(int payRollNr, string startDate, string birthDate)
        {
            SqlConnection con = new SqlConnection(dbConnection);
            con.Open();
            sql = "INSERT INTO Employees values (@payRollNr, @dateOfBirth, @startDate)";
            command = new SqlCommand(sql, con);
            command.Parameters.AddWithValue("payRollNr", payRollNr);
            command.Parameters.AddWithValue("dateOfBirth", birthDate);
            command.Parameters.AddWithValue("startDate", startDate);
            command.ExecuteNonQuery();
            con.Close();
        }
        
        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();        //accidental button press that I didn't notice until it was too late...
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateOfBirth;
            string startDate;
            int payRollNr;
            string departmentNr;
            int holiDays = 0;
            
            if(!string.IsNullOrEmpty(dateOfBirthBox.Text) && !string.IsNullOrEmpty(startDateBox.Text) && !string.IsNullOrEmpty(payRollBox.Text))
            {
                dateOfBirth = dateOfBirthBox.Text;
                startDate = startDateBox.Text;
                if(payRollBox.Text.Length == 4 && isDate(dateOfBirth) && isDate(startDate))
                {
                    payRollNr = int.Parse(payRollBox.Text);
                    departmentNr = payRollBox.Text.Substring(0, 1);
                    addEmployee(payRollNr, startDate, dateOfBirth);
                    company.addEmployee(new Employee(getDateTime(dateOfBirth), getDateTime(startDate), payRollNr));
                    if (departmentNr == "1")
                    {
                        holiDays += 24;
                    }
                    else
                    {
                        holiDays += 20;
                    }
                    if(detirmineYears(getDateTime(dateOfBirth)) > 55)
                    {
                        holiDays += 5;
                    }
                    if(detirmineYears(getDateTime(startDate)) > 10)
                    {
                        holiDays += 3;
                    }
                    label6.Text = holiDays.ToString() + " days";
                }
            }
            else
            {
                warningLabel1.Text = "Please fill in all fields, and make sure all of them are filled in properly.";
            }
        }

        private bool isDate(string date)
        {
            int day;
            int month;
            int year;
            if(date.Length == 8)
            {
                day = int.Parse(date.Substring(0, 2));
                month = int.Parse(date.Substring(2, 2));
                year = int.Parse(date.Substring(4, 4));
                if(day > 0 && day <= 31)
                {
                    if(year > 0 && year <= DateTime.Now.Year)
                    {
                        if(month > 0 && month <= 12)
                        {   
                            if(day <= DateTime.DaysInMonth(year, month) && day > 0)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        private int detirmineYears(DateTime dateTime)
        {
            int years;
            int currentYear;
            currentYear = DateTime.Today.Year;
            years = currentYear - dateTime.Year;
            return years;
        }
        private DateTime getDateTime(string date)
        {
            //only use after validating that the given date is a possible date
            int day;
            int month;
            int year;
            day = int.Parse(date.Substring(0, 2));
            month = int.Parse(date.Substring(2, 2));
            year = int.Parse(date.Substring(4, 4));
            DateTime dateTime = new DateTime(year, month, day);
            return dateTime;
        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(dbConnection);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Employees", con);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = dataTable;
            con.Close();
            label12.Text = company.getAmountOfHolidays().ToString() + " days";
            label14.Text = company.getYearsOfServiceAvg().ToString() + " years";
            label16.Text = company.getOldestEmployee().ToString();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if(WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
            }
            else if(WindowState == FormWindowState.Normal)
            {
                this.Show();
                notifyIcon1.Visible = false;
            }
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        //StreamReader streamReader;
        //StreamWriter streamWriter;
        //public Form1()
        //{
        //    InitializeComponent();
        //    refresh();
        //}

        //private void submitButton(object sender, EventArgs e)
        //{
        //    FileStream fs = new FileStream(@"C:\Users\dimit\Desktop\it\test.csv", FileMode.Append);
        //    streamWriter = new StreamWriter(fs);
        //    string writeString = textBox1.Text + ";";
        //    writeString += textBox2.Text + ";";
        //    writeString += textBox3.Text;
        //    streamWriter.WriteLine(writeString);
        //    streamWriter.Close();
        //    fs.Close();
        //}

        //private void refreshButton(object sender, EventArgs e)
        //{
        //    refresh();
        //}

        //private void refresh()
        //{
        //    label1.Text = String.Empty;
        //    streamReader = new StreamReader(@"C:\Users\dimit\Desktop\it\test.csv");
        //    List<Champ> champs = new List<Champ>();
        //    if (streamReader.Peek().ToString() == null)
        //    {

        //    }
        //    else
        //    {
        //        do
        //        {
        //            string inputString = streamReader.ReadLine();
        //            char[] chars = { ';' };
        //            string[] outputData = inputString.Split(chars);
        //            Champ champ = new Champ();
        //            champ.Name = outputData[0];
        //            champ.Ult = outputData[1];
        //            champ.BestSkin = outputData[2];
        //            champs.Add(champ);
        //        }
        //        while (!streamReader.EndOfStream);

        //        foreach (Champ champ in champs)
        //        {
        //            label1.Text += champ.Name + " " + champ.Ult + " " + champ.BestSkin + "\n";
        //        }
        //    }

        //    streamReader.Close();
        //}
    }
}
