using RSLERP.DataManager.DLL;
using RSLERP.DataManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSLERP.DataManager.BLL
{
    public class EmployeeBLL
    {
        public List<Employees> GetEmployeesAll()
        {
            return new EmployeeDLL().GetEmployee();
        }
        public List<Employees> GetEmployeesByID(String id)
        {
            return new EmployeeDLL().GetEmployee(id);
        }
        public List<Employees> GetEmployeesByUserID(String user_id)
        {
            return new EmployeeDLL().GetEmployee("", user_id);
        }
        public List<Employees> GetEmployeesByEmail(String email)
        {
            return new EmployeeDLL().GetEmployee("", "", "", email);
        }
        public List<Employees> GetEmployeesByMobile(String mobile)
        {
            return new EmployeeDLL().GetEmployee("", "", "", "", mobile);
        }

        public List<Employees> GetEmployeesBySearch(String name, int pageIndex, int pageSize)
        {
            return new EmployeeDLL().GetEmployee("", "", name, "", "", "", null, null, pageIndex, pageSize);
        }

        public String InsertEmployee(Employees emp)
        {
            String msg = GLobalMessage.Global_Success_Message;
            try
            {
                new EmployeeDLL().InsertEmployee("" + emp.id, "" + emp.user_id, emp.name, emp.email, emp.mobile, emp.address, emp.birthdate, emp.joining_date);
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }

        public String InsertEmployees(List<Employees> empLst)
        {
            String msg = GLobalMessage.Global_Success_Message;

            try
            {
                foreach (Employees emp in empLst)
                {
                    new EmployeeDLL().InsertEmployee("" + emp.id, "" + emp.user_id, emp.name, emp.email, emp.mobile, emp.address, emp.birthdate, emp.joining_date);
                }
            }
            catch
            {
                msg = GLobalMessage.Global_Error_Message;
            }
            return msg;
        }
    }
}
