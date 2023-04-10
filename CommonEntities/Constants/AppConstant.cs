using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.Constants
{
    public static class AppConstant
    {
        //API
        public const string ENDPOINT_USER = "users";
        public const string ENDPOINT_TOKEN = "Bearer";
        public const string ENDPOINT_ID = "?id=";
        public const string ENDPOINT_PAGE = "?page=";
        public const string ENDPOINT_SLASH = "/";

        //EXCEL
        public const string EXCEL_FILENAME = "Employee_Page";
        public const string EXCEL_TYPE = ".xlsx";
        public const string EXCEL_SHEETNAME = "Employee";
        public const string EXCEL_FIELD_NAME_EMPLOYEEID = "EmployeeId";
        public const string EXCEL_FIELD_NAME_NAME = "Name";
        public const string EXCEL_FIELD_NAME_EMAIL = "Email Address";
        public const string EXCEL_FIELD_NAME_GENDER = "Gender";
        public const string EXCEL_FIELD_NAME_STATUS = "Status";
        public const string EXCEL_SPREADSHEET = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        
    }
}
