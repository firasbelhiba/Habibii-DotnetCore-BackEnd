using Habibii.Helpers;
using Habibii.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Habibii.Data
{
    public interface ICrudRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PagedList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
    }
}
