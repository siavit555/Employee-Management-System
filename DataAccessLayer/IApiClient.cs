using CommonEntities.UsersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IApiClient
    {
         Task<T?> GetAsync<T>(string relativePath) where T : class;

         Task<UserDetail?> PostAsync<T>(T content);

         Task<UserDetail?> PutAsync<T>(string relativePath,T content);

         Task<UserDetail?> DeleteAsync(string relativePath);
        
    }
}
