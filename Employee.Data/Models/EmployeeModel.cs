using Employee.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Employee.Data.Models
{
    public class EmployeeModel : IEmployeeRepository
    {
     
        public string CreateEmployeeDetails(EmployeeData employeeData)
        {
            EmployeeData empdata = new EmployeeData()
            {
                EmpFirstName = employeeData.EmpFirstName,
                EmpLastName = employeeData.EmpLastName,
                EmpDepartment = employeeData.EmpDepartment
            };

            //Checking Firstname and lastname available or not

            List<EmployeeData> fdata = new List<EmployeeData>();
            fdata.Add(empdata);
            String json = JsonConvert.SerializeObject(fdata.ToArray(), Formatting.Indented);
          
            string strfilename = AppDomain.CurrentDomain.BaseDirectory + @"\EMPLOYEEINFO.TXT";
            if (File.Exists(strfilename))
            {
                string json1 = "";

                using (StreamReader r = new StreamReader(strfilename))
                {
                    json1 = r.ReadToEnd();

                }

                if (json1.StartsWith("["))
                {
                    var allEmployee = JsonConvert.DeserializeObject<List<EmployeeData>>(json1).ToList();

                    string secstring = json.Replace("[", string.Empty);

                    json1 = json1.Replace("]", string.Empty);

                    string jsonnew = json1 + "," + secstring;

                    string JSONObject = jsonnew;
                    

  
                    string strMessage = "";

                    //Filter the list using a property value
                    foreach (var items in allEmployee)
                    {
                        if (items.EmpFirstName == employeeData.EmpFirstName)
                        {
                            strMessage =  "FirstName Already Exists";
                            return strMessage;
                        }

                    }
                   

                    if (strMessage == "")
                    {
                        File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\EMPLOYEEINFO.TXT", jsonnew);
                    }

     
                }
            }
            else
            {

                File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + @"\EMPLOYEEINFO.TXT", json);
            }

            return "File Saved in " + AppDomain.CurrentDomain.BaseDirectory + @"EMPLOYEEINFO.TXT";

        }
    }
}
