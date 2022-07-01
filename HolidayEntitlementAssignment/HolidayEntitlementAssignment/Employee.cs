using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayEntitlementAssignment
{
    internal class Employee
    {
        private DateTime dateOfbirth;
        private DateTime startDate;
        private int payRollNr;

        public Employee(DateTime dateOfbirth, DateTime startDate, int payRollNr)
        {
            this.dateOfbirth = dateOfbirth;
            this.startDate = startDate;
            this.payRollNr = payRollNr;
        }

        public DateTime DateOfBirth { get { return dateOfbirth; } }
        public DateTime StartDate { get { return startDate; } }
        public int PayRollNr { get { return payRollNr; } }

        public int getHolidays()
        {
            int holiDays = 0;
            string departmentNr = payRollNr.ToString().Substring(0, 1);
            if (departmentNr == "1")
            {
                holiDays += 24;
            }
            else
            {
                holiDays += 20;
            }
            if (detirmineYears(this.dateOfbirth) > 55)
            {
                holiDays += 5;
            }
            if (detirmineYears(this.startDate) > 10)
            {
                holiDays += 3;
            }
            return holiDays;
        }
        public int getAge()
        {
            return detirmineYears(this.dateOfbirth);
        }
        public int getYearsOfService()
        {
            return detirmineYears(this.StartDate);
        }
        private int detirmineYears(DateTime dateTime)
        {
            int years;
            int currentYear;
            currentYear = DateTime.Today.Year;
            years = currentYear - dateTime.Year;
            return years;
        }
    }
}
