using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.IO;
using Newtonsoft.Json;
using DL_NorthWind;

namespace WCFBasicService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EmployeeService : IEmployeeService
    {
        static string ConnString = string.Empty;
        static EmployeeService() 
        {
            //<add name="ConnString" connectionString="data source=(LocalDB)\v11.0;Integrated Security=True;AttachDbFilename=|DataDirectory|\App_Data\NORTHWND.MDF;" providerName="System.Data.SqlClient" />
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            AppDomain.CurrentDomain.SetData("DataDirectory", baseDir);      //--WCFBasicService/App_Data
            ConnString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString;
        }

        public List<Employee> GetEmployeeAll()
        {
            try
            {
                List<Employee> employee = EmployeeRepository.GetEmployeeAll(ConnString);
                if (employee == null)
                {
                    throw new FaultException("EMPLOYEE NOT FOUND");
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                Employee employee = EmployeeRepository.GetEmployeeById(ConnString, id.ToString());
                if (employee == null)
                {
                    throw new FaultException("EMPLOYEE NOT FOUND");
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public Employee GetEmployeeByLastName(string lastName)
        {
            try
            {
                Employee employee = EmployeeRepository.GetEmployeeByName(ConnString, lastName);
                if (employee == null)
                {
                    throw new FaultException("EMPLOYEE NOT FOUND");
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }

        public bool PutEmployee(Employee employee)
        {
            try
            {
                int stat = EmployeeRepository.PutEmployee(ConnString, employee);
                if (stat < 1)
                {
                    throw new FaultException("EMPLOYEE UPDATE FAILED");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }
        public bool PostEmployee(Stream streamData)
        {
            StreamReader reader = new StreamReader(streamData);
            string res = reader.ReadToEnd();
            reader.Close();
            reader.Dispose();
            Employee employee = JsonConvert.DeserializeObject<Employee>(res);
            try
            {
                int stat = EmployeeRepository.PostEmployee(ConnString, employee);
                if (stat < 1)
                {
                    throw new FaultException("EMPLOYEE INSERT FAILED");
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
        }
        public bool DeleteEmployee(int id)
        {
            try
            {
                Employee employee = GetEmployeeById(id);
                if (employee == null)
                {
                    throw new FaultException("EMPLOYEE NOT FOUND");
                }
                int stat = EmployeeRepository.DeleteEmployee(ConnString, id);
                if (stat > 0) return true;
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message + ex.StackTrace);
            }
            return false;
        }
    }

}
