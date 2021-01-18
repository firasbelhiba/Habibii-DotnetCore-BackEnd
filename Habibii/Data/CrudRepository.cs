using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habibii.Helpers;
using Habibii.Model;
using Microsoft.EntityFrameworkCore;

namespace Habibii.Data
{
    public class CrudRepository : ICrudRepository
    {
        private readonly DataContext _context;

        public CrudRepository(DataContext context)
        {
            _context = context;
        }

        // I won't use an async method because when we add something in our context we are not querrying with the database 
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

         public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId).FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<Photo> GetPhoto(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == id);
            return photo; 
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users =  _context.Users.Include(p => p.Photos).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            if(userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDateOfBirth = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDateOfBirth = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDateOfBirth && u.DateOfBirth <= maxDateOfBirth);
            }

            return await PagedList<User>.CreateAsync(users , userParams.PageNumber , userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            // returns more changes greater than 0 => it will return false 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
