using BackEndTest.Models;

namespace BackEndTest.Data
{
    public class UserGenericRepository : GenericRepository<User>
    {

        public UserGenericRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public User GetByUserName(string username)
        {
            return _context.Set<User>().FirstOrDefault(x => x.UserName == username);
        }

        public User GetByEmail(string email)
        {
            return _context.Set<User>().FirstOrDefault(x => x.Email == email);
        }
    }
}
