using ServiceBook.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBook.DAL
{
    public class EmployeeRepository
    {
        ServiceBookContext ServiceBookContext = new ServiceBookContext();
        public Employee AddEmployee(Employee Employee)
        {
            WorkingPoint WorkingPoint = ServiceBookContext.WorkingPoint.FirstOrDefault(x => x.ID == Employee.WPID);
            Employee employee = WorkingPoint.Employees.FirstOrDefault(x => x.PhoneNumber == Employee.PhoneNumber);
            if (employee == null)
            {
                Employee.Flag = 0;
                WorkingPoint.Employees.Add(Employee);
                ServiceBookContext.SaveChanges();
                return Employee;
            }
           
          else
            {
                return null;
            }
            
        }

        public void DeletEmployee(Employee Employee)
        {
            Employee DeletEmployee = ServiceBookContext.Employee.FirstOrDefault(x => x.ID == Employee.ID);
            DeletEmployee.Flag = 1;
            ServiceBookContext.SaveChanges();

        }

        public Employee EditEmployee(Employee Employee)
        {
            Employee EditEmployee = ServiceBookContext.Employee.FirstOrDefault(x => x.ID == Employee.ID);
            Employee edit = EditEmployee.WorkingPoint.Employees.FirstOrDefault(x => x.PhoneNumber == Employee.PhoneNumber);
            if(edit==null || edit.ID==EditEmployee.ID)
            {
                EditEmployee.LastName = Employee.LastName;
                EditEmployee.FirstName = Employee.FirstName;
                EditEmployee.PhoneNumber = Employee.PhoneNumber;
                EditEmployee.Email = Employee.Email;
                ServiceBookContext.SaveChanges();
                return EditEmployee;
            }
            return null;
           
        }
    }
}
