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
        public List<Employee> GetEmployeeAll()
        {
            try
            {
                List<Employee> employee = EmployeeRepository.GetEmployeeAll();
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
                Employee employee = EmployeeRepository.GetEmployeeById(id.ToString());
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
                Employee employee = EmployeeRepository.GetEmployeeByName(lastName);
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
                int stat = EmployeeRepository.PutEmployee(employee);
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
                int stat = EmployeeRepository.PostEmployee(employee);
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
                int stat = EmployeeRepository.DeleteEmployee(id);
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
