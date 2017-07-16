using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using DL_NorthWind;
using System.IO;

namespace WCFBasicService
{
    [ServiceContract]
    //[ServiceContract(Namespace="http://localhost:39901/EmployeeService.svc")]
    public interface IEmployeeService
    {
        // GET http://localhost:39901/EmployeeService.svc

        [OperationContract]
        // GET http://localhost:39901/EmployeeService.svc/GetEmployeeAll
        [WebGet(UriTemplate = "/GetEmployeeAll", ResponseFormat = WebMessageFormat.Json)]
        List<Employee> GetEmployeeAll();

        [OperationContract]
        // GET http://localhost:39901/EmployeeService.svc/GetEmployeeById/?id=1     
        // ResponseFormat can be overrided by Accept: Application/json or Application/xml
        [WebGet(UriTemplate = "/GetEmployeeById/?id={id}", ResponseFormat = WebMessageFormat.Json)]
        Employee GetEmployeeById(int id);

        [OperationContract]
        // GET http://localhost:39901/EmployeeService.svc/GetEmployeeByLastName/Callahan
        // ResponseFormat can be overrided by Accept: Application/json or Application/xml
        [WebGet(UriTemplate = "/GetEmployeeByLastName/{lastName}", ResponseFormat = WebMessageFormat.Xml)]
        Employee GetEmployeeByLastName(string lastName);

        [OperationContract]
        // PUT http://localhost:39901/EmployeeService.svc/PutEmployee
        [WebInvoke(UriTemplate = "/PutEmployee/", Method = "PUT", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        bool PutEmployee(Employee employee);

        [OperationContract]
        // POST http://localhost:39901/EmployeeService.svc/PostEmployee
        [WebInvoke(UriTemplate = "/PostEmployee/", Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        bool PostEmployee(Stream streamData);

        [OperationContract]
        // DELETE http://localhost:39901/EmployeeService.svc/DeleteEmployee
        [WebInvoke(UriTemplate = "/DeleteEmployee/")]
        bool DeleteEmployee(int id);
    }

}
