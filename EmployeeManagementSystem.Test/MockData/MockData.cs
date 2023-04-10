﻿using ClosedXML.Excel;
using CommonEntities.Constants;
using CommonEntities.UsersModels;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Test.MockData
{
    public static class MockData
    {
        public static Task<UserDetails> GetTestEmployeeDetails()
        {
            var users = new List<User>() {
                   new User() {
                        Id=1,
                        Name="TestEmployee1",
                        EmailAddress="test1@gmail.com",
                        Gender="male",
                        Status="active"
                        },
                   new User() {
                        Id=2,
                        Name="TestEmployee2",
                        EmailAddress="test2@gmail.com",
                        Gender="male",
                        Status="active"
                        },
                   new User() {
                        Id=3,
                        Name="TestEmployee3",
                        EmailAddress="test3@gmail.com",
                        Gender="male",
                        Status="active"
                        },
                   new User() {
                        Id=4,
                        Name="TestEmployee4",
                        EmailAddress="test4@gmail.com",
                        Gender="male",
                        Status="active"
                        },
                   new User() {
                        Id=5,
                        Name="TestEmployee5",
                        EmailAddress="test5@gmail.com",
                        Gender="male",
                        Status="active"
                        }

            };

            var meta = new Meta() {
                Pagination=new Pagination() 
                            {
                                Limit=10,
                                Page=1,
                                Pages=250
                              }
            };

            UserDetails userDetails = new UserDetails()
            {
                StatusCode="200",
                Meta=meta,
                Users=users
            };
          
            return Task.FromResult(userDetails);
        }

        public static Task<UserDetail> GetTestEmployee()
        {
            var meta = new Meta()
            {
                Pagination = new Pagination()
                {
                    Limit = 10,
                    Page = 1,
                    Pages = 250
                }
            };

            UserDetail userDetail = new UserDetail()
            {
                StatusCode = "200",
                Meta = meta,
                User = new User()
                {
                    Id = 5,
                    Name = "TestEmployee5",
                    EmailAddress = "test5@gmail.com",
                    Gender = "male",
                    Status = "active"
                }
            };

            return Task.FromResult(userDetail);
        }

        public static Task<User> GetTestEmployeeDetailsById()
        {
            var users = new User()
                       {
                           Id = 1,
                           Name = "TestEmployee1",
                           EmailAddress = "test1@gmail.com",
                           Gender = "male",
                           Status = "active"
                       };

            return Task.FromResult(users);
        }

        public static Task<UserDetails> GetTestEmployeeDetails_Exception()
        {
            throw new Exception();
        }

        public static Task<MemoryStream> GetTestEmployeeDetailsForExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            DataTable dtEmployees = new DataTable(AppConstant.EXCEL_SHEETNAME);
            dtEmployees.Columns.AddRange(new DataColumn[5] { new DataColumn(AppConstant.EXCEL_FIELD_NAME_EMPLOYEEID),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_NAME),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_EMAIL),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_GENDER),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_STATUS) });

            var data = GetTestEmployeeDetails().Result;
            for (int i = 0; i < data?.Users?.Count; i++)
            {
                dtEmployees.Rows.Add(data.Users[i].Id, data.Users[i].Name, data.Users[i].EmailAddress, data.Users[i].Gender, data.Users[i].Status);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtEmployees);
                wb.SaveAs(memoryStream);
            }
            return Task.FromResult(memoryStream);
        }

    }
}
