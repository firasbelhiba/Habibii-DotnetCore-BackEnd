using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(p => p.Photos).ToListAsync();

            return users;
        }

        public async Task<bool> SaveAll()
        {
            // returns more changes greater than 0 => it will return false 
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
