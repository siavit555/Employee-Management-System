using BusinessAccessLayer;
using ClosedXML.Excel;
using CommonEntities;
using CommonEntities.Constants;
using CommonEntities.UsersModels;
using DataAccessLayer;
using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace EmployeeManagementSystem.Controllers
{
   
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IEmployee _employee;

        public EmployeeController(ILogger<EmployeeController> logger,IEmployee employee)
        {
            _logger = logger;
            _employee = employee;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int pageId=1)
        {
            try
            {
                //if (pageId == 0) pageId++;
                var userDetails = await _employee.GetUserDetailsByPageAsync<UserDetails>(pageId);
                return View(userDetails);

            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :Index ", ex);
                throw;
            }
            
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                await _employee.PostUserAsync(user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :Add ", ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            try
            {
                var userDetail = await _employee.GetUserByIdAsync(id);
                return View(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :View ", ex);
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var userDetail = await _employee.GetUserByIdAsync(id);
                return View(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :Edit ", ex);
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(user);
                }
                await _employee.PutUserAsync(user.Id, user);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :EditEmployee ", ex);
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var userDetail = await _employee.GetUserByIdAsync(id);
                return View(userDetail);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :Delete ", ex);
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> DeleteByEmpId(int id)
        {
            try
            {
                await _employee.DeleteUserByIdAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :DeleteByEmpId ", ex);
                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> ExportToExcel(int pageId)
        {
            try
            {
                var stream = await _employee.GetEmployeeDetails(pageId);
                return File(stream.ToArray(), AppConstant.EXCEL_SPREADSHEET, AppConstant.EXCEL_FILENAME + pageId + AppConstant.EXCEL_TYPE);
            }
            catch (Exception ex)
            {
                _logger.LogError("Controller : Employee, Method :ExportToExcel ", ex);
                throw;
            }
            
        }


    }
}