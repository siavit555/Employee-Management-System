using BusinessAccessLayer;
using CommonEntities.Constants;
using CommonEntities.UsersModels;
using DataAccessLayer;
using DocumentFormat.OpenXml.Office2010.Excel;
using EmployeeManagementSystem.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Test
{
    [TestClass]
    public class EmployeeBALTest
    {
        private readonly Mock<IApiClient> _mockApiClient;
        public EmployeeBALTest()
        {
            _mockApiClient = new Mock<IApiClient>();
        }

        [TestMethod]
        public void Get_User_ById_Async_Test()
        {
            //arrange
            _mockApiClient.Setup(n => n.GetAsync<UserDetails>(It.IsAny<string>())).Returns(MockData.MockData.GetTestEmployeeDetails());
            var employee = new Employee(_mockApiClient.Object);

            //act
            var userResult = employee.GetUserByIdAsync(1).Result as User;

            //assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(1, userResult.Id);
        }

        [TestMethod]
        public void Get_User_Details_ByPage_Async_Test()
        {
            //arrange
            _mockApiClient.Setup(n => n.GetAsync<UserDetails>(It.IsAny<string>())).Returns(MockData.MockData.GetTestEmployeeDetails());
            var employee = new Employee(_mockApiClient.Object);

            //act
            var userResult = employee.GetUserDetailsByPageAsync<UserDetails>(1).Result;

            //assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(5, userResult?.Users?.Count());
        }

        [TestMethod]
        public void Post_User_Async()
        {
            //arrange
            _mockApiClient.Setup(n => n.PostAsync<User>(It.IsAny<User>())).Returns(MockData.MockData.GetTestEmployee());
            var employee = new Employee(_mockApiClient.Object);
            var newUser = new User()
            {
                Id = 0,
                Name = "Employee3",
                EmailAddress = "Employee3@gmail.com",
                Gender = "male",
                Status = "active"
            };
            //act
            var userResult = employee.PostUserAsync(newUser).Result as UserDetail;

            //assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(5, userResult?.User?.Id);
            Assert.AreEqual("200", userResult?.StatusCode);
        }

        [TestMethod]
        public void Put_User_Async()
        {
            //arrange
            _mockApiClient.Setup(n => n.PutAsync(It.IsAny<string>(),It.IsAny<User>())).Returns(MockData.MockData.GetTestEmployee());
            var employee = new Employee(_mockApiClient.Object);
            var newUser = new User()
            {
                Id = 5,
                Name = "Employee3",
                EmailAddress = "Employee3@gmail.com",
                Gender = "male",
                Status = "active"
            };
            //act
            var userResult = employee.PutUserAsync(5,newUser).Result;

            //assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(5, userResult?.User?.Id);
            Assert.AreEqual("200", userResult?.StatusCode);
        }

        [TestMethod]
        public void Delete_User_ById_Async()
        {
            //arrange
            _mockApiClient.Setup(n => n.DeleteAsync(It.IsAny<string>())).Returns(MockData.MockData.GetTestEmployee());
            var employee = new Employee(_mockApiClient.Object);

            //act
            var userResult = employee.DeleteUserByIdAsync(5).Result;

            //assert
            Assert.IsNotNull(userResult);
            Assert.AreEqual(5, userResult?.User?.Id);
            Assert.AreEqual("200", userResult?.StatusCode);
        }


        [TestMethod]
        public void Get_EmployeeDetails_ExportToExcel()
        {
            //arrange
            _mockApiClient.Setup(n => n.GetAsync<UserDetails>(It.IsAny<string>())).Returns(MockData.MockData.GetTestEmployeeDetails());
            var employee = new Employee(_mockApiClient.Object);
            //act
            var fileResult = employee.GetEmployeeDetails(1).Result;

            //assert
            Assert.IsNotNull(fileResult);
            Assert.AreNotEqual(0, fileResult.Length);
        }

    }
}
