using CommonEntities.UsersModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccessLayer
{
    public interface IEmployee
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<UserDetail?> PostUserAsync<T>(T content);
        Task<UserDetail?> PutUserAsync<T>(int id,T content);
        Task<T?> GetUserDetailsByPageAsync<T>(int id) where T : class;
        Task<UserDetail?> DeleteUserByIdAsync(int id);
        Task<MemoryStream> GetEmployeeDetails(int id);
    }
}
