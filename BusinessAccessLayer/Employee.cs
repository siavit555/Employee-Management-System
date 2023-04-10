using ClosedXML.Excel;
using CommonEntities;
using CommonEntities.Constants;
using CommonEntities.UsersModels;
using DataAccessLayer;
using System.Data;

namespace BusinessAccessLayer
{
    public class Employee : IEmployee
    {
        public readonly IApiClient _apiClient;
        public Employee(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            try
            {
                string requestUrl = String.Concat(AppConstant.ENDPOINT_ID, id);
                var userDetails = await _apiClient.GetAsync<UserDetails>(requestUrl);
                return userDetails?.Users?.FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<UserDetail?> PostUserAsync<T>(T content)
        {
            try
            {
                return await _apiClient.PostAsync<T?>(content);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<UserDetail?> PutUserAsync<T>(int id,T content)
        {
            try
            {
                return await _apiClient.PutAsync<T?>(AppConstant.ENDPOINT_SLASH + id.ToString(), content);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<T?> GetUserDetailsByPageAsync<T>(int id) where T : class 
        {
            try
            {
                string requestUrl = String.Concat(AppConstant.ENDPOINT_PAGE, id);
                return await _apiClient.GetAsync<T>(requestUrl);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public async Task<UserDetail?> DeleteUserByIdAsync(int id)
        {
            try
            {
                string requestUrl = String.Concat(AppConstant.ENDPOINT_USER + "/" + id);
                return await _apiClient.DeleteAsync(requestUrl);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<MemoryStream> GetEmployeeDetails(int id)
        {
            MemoryStream memoryStream = new MemoryStream();
            DataTable dtEmployees = new DataTable(AppConstant.EXCEL_SHEETNAME);
            dtEmployees.Columns.AddRange(new DataColumn[5] { new DataColumn(AppConstant.EXCEL_FIELD_NAME_EMPLOYEEID),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_NAME),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_EMAIL),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_GENDER),
                                        new DataColumn(AppConstant.EXCEL_FIELD_NAME_STATUS) });

            var data = await GetUserDetailsByPageAsync<UserDetails>(id);
            for (int i = 0; i < data?.Users?.Count; i++)
            {
                dtEmployees.Rows.Add(data.Users[i].Id, data.Users[i].Name, data.Users[i].EmailAddress, data.Users[i].Gender, data.Users[i].Status);
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dtEmployees);
                wb.SaveAs(memoryStream);
                
            }
            return memoryStream;
        }
    }
}