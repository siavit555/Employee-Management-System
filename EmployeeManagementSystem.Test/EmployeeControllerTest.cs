using BusinessAccessLayer;
using CommonEntities.UsersModels;
using EmployeeManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Formats.Asn1;
using System.Net.Mail;

namespace EmployeeManagementSystem.Test
{
    [TestClass]
    public class EmployeeControllerTest
    {
        private readonly Mock<IEmployee> _mockEmployee;
        private readonly ILogger<EmployeeController> _mocklogger;
        public EmployeeControllerTest()
        {
            _mockEmployee = new Mock<IEmployee>();
            _mocklogger = Mock.Of<ILogger<EmployeeController>>();
        }

        [TestMethod]
        public void Add_Employee()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
            
            //act
            var viewResult = empController.Add() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void Add_NewEmployee_Valid()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
            var newUser = new User()
            {
                Id = 0,
                Name = "Employee3",
                EmailAddress = "Employee3@gmail.com",
                Gender = "male",
                Status = "active"
            };

            //act
            var redirctResult = empController.Add(newUser).Result as RedirectToActionResult;

            //assert
            Assert.IsNotNull(redirctResult);
            Assert.IsNull(redirctResult.ControllerName);
            Assert.AreEqual("Index", redirctResult.ActionName);
        }

        [TestMethod]
        public void Add_NewEmployee_Invalid()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
            var inValidUser = new User()
            {
                Id = 0,
                EmailAddress = "Employee3@test.com",
                Gender = "male",
                Status = "active"
            };
            empController.ViewData.ModelState.AddModelError("Name", "The Name is Required");

            //act
            _ = empController.Add(inValidUser);

            //assert
            Assert.IsTrue(!empController.ModelState.IsValid);
        }

        [TestMethod]
        public void View_Employees_By_PageNo()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetUserDetailsByPageAsync<UserDetails>(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetails());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var viewResult = empController.Index(1).Result as ViewResult;
            var userDetails = viewResult?.ViewData.Model as UserDetails;

            //assert
            Assert.IsNotNull(userDetails);
            Assert.IsNotNull(viewResult?.Model);
            Assert.AreEqual(5, userDetails?.Users?.Count);
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Index");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void View_Employees_By_PageNo_Exception()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetUserDetailsByPageAsync<UserDetails>(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetails_Exception());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
        }

        [TestMethod]
        public void View_Employee_ById()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetUserByIdAsync(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetailsById());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var viewResult = empController.View(1).Result as ViewResult;
            var userDetail = viewResult?.ViewData.Model as User;

            //assert
            Assert.IsNotNull(userDetail);
            Assert.IsNotNull(viewResult?.Model);
            Assert.AreEqual(1, userDetail?.Id);
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "View");
        }

        [TestMethod]
        public void Edit_ViewEmployee_ById()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetUserByIdAsync(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetailsById());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var viewResult = empController.Edit(1).Result as ViewResult;
            var userDetail = viewResult?.ViewData.Model as User;

            //assert
            Assert.IsNotNull(userDetail);
            Assert.IsNotNull(viewResult?.Model);
            Assert.AreEqual(1, userDetail?.Id);
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
        }

        [TestMethod]
        public void Edit_Employee_Valid()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
            var newUser = new User()
            {
                Id = 76753,
                Name = "Employee4",
                EmailAddress = "Employee4@test.com",
                Gender = "male",
                Status = "active"
            };

            //act
            var redirctResult = empController.EditEmployee(newUser).Result as RedirectToActionResult;

            //assert
            Assert.IsNotNull(redirctResult);
            Assert.IsNull(redirctResult.ControllerName); 
            Assert.AreEqual("Index", redirctResult.ActionName);
        }

        [TestMethod]
        public void Edit_Employee_Invalid()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);
            var inValidUser = new User()
            {
                Id = 0,
                Name= "Employee3",
                Gender = "male",
                Status = "active"
            };
            empController.ViewData.ModelState.AddModelError("EmailAddress", "The Email Address is Required");

            //act
            _ = empController.EditEmployee(inValidUser);

            //assert
            Assert.IsTrue(!empController.ModelState.IsValid);
        }

        [TestMethod]
        public void Delete_ViewEmployee_ById()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetUserByIdAsync(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetailsById());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var viewResult = empController.Delete(1).Result as ViewResult;
            var userDetail = viewResult?.ViewData.Model as User;

            //assert
            Assert.IsNotNull(userDetail);
            Assert.IsNotNull(viewResult?.Model);
            Assert.AreEqual(1, userDetail?.Id);
            Assert.IsTrue(string.IsNullOrEmpty(viewResult.ViewName) || viewResult.ViewName == "Edit");
        }

        [TestMethod]
        public void Delete_Employee_ById()
        {
            //arrange
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var redirctResult = empController.DeleteByEmpId(1).Result as RedirectToActionResult;

            //assert
            Assert.IsNotNull(redirctResult);
            Assert.IsNull(redirctResult.ControllerName); 
            Assert.AreEqual("Index", redirctResult.ActionName);
        }

        [TestMethod]
        public void ExportToExcel_EmployeeDetails()
        {
            //arrange
            _mockEmployee.Setup(n => n.GetEmployeeDetails(It.IsAny<Int32>())).Returns(MockData.MockData.GetTestEmployeeDetailsForExcel());
            var empController = new EmployeeController(_mocklogger, _mockEmployee.Object);

            //act
            var fileResult = empController.ExportToExcel(1).Result as FileResult;

            //assert
            Assert.IsNotNull(fileResult);
            Assert.AreEqual(fileResult.ContentType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"); 
            Assert.AreEqual(fileResult.FileDownloadName, "Employee_Page1.xlsx");
        }
    }
}