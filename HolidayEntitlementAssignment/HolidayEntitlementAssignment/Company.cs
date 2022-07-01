using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayEntitlementAssignment
{
    internal class Company
    {
        List<Department> departments;

        public Company()
        {
            this.departments = new List<Department>();
        }
        public List<Department> getDepartments()
        {
            return departments;
        }
        public void addDepartment(Department department)
        {
            this.departments.Add(department);
        }
        public void removeDepartment(Department department)
        {
            this.departments.Remove(department);
        }
        public void addEmployee(Employee employee)
        {
            string departmentNr = employee.PayRollNr.ToString().Substring(0, 1);
            foreach (Department department in departments)
            {
                if (department.getDepartmentNr().ToString() == departmentNr)
                {
                    department.addEmployee(employee);
                }
            }
        }

        public int getAmountOfHolidays()
        {
            int holidays = 0;
            foreach (Department department in departments)      //I feel like there is an easier way to do this...
            {
                foreach (Employee employee in department.getEmployees())
                {
                    holidays += employee.getHolidays();
                }
            }
            return holidays;
        }
        public int getYearsOfServiceAvg()
        {
            int yearsOfService = 0;
            int amountOfEmployees = 0;
            foreach (Department department in departments)
            {
                foreach (Employee employee in department.getEmployees())
                {
                    yearsOfService += employee.getYearsOfService();
                    amountOfEmployees++;
                }
            }
            try
            {
                return (yearsOfService / amountOfEmployees);
            }
            catch
            {
                return 0;
            }
            
        }
        public int getOldestEmployee()
        {
            int age = 0;
            int payRollNr = 0;
            foreach (Department department in departments)
            {
                foreach (Employee employee in department.getEmployees())
                {
                    if (age < employee.getAge())
                    {
                        age = employee.getAge();
                        payRollNr = employee.PayRollNr;
                    }
                }
            }
            return payRollNr;
            
        }
    }
}
