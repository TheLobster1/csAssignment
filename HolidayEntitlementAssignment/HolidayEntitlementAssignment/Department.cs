using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolidayEntitlementAssignment
{
    internal class Department
    {
        List<Employee> employees;
        int departmentNr;

        public Department(int departmentNr)
        {
            this.employees = new List<Employee>();
            this.departmentNr = departmentNr;
        }

        public List<Employee> getEmployees() { return employees; }
        public int getDepartmentNr() { return departmentNr; }   
        public void addEmployee(Employee employee)
        {
            this.employees.Add(employee);
        }
        public void removeEmployee(Employee employee)
        {
            this.employees.Remove(employee);
        }
    }
}
