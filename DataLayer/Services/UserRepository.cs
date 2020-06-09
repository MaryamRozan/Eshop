using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    class UserRepository : IUsersRepository
    {
        MyEshop_DBEntities _db;
        public UserRepository(MyEshop_DBEntities db)
        {
            _db = db;
        }

        public Users GetByActiveCode(string id)
        {
            return _db.Users.SingleOrDefault(u => u.ActiveCode == id);
        }

        public bool IsDuplicatedUser(RegisterViewModel user)
        {
            try
            {
               return _db.Users.Any(u => u.Email == user.Email.Trim().ToLower()|| u.UserName==user.UserName);
               
            }
            catch { return false; }
        }

        public Users GetExistUser(string email, string password)
        {
            return _db.Users.SingleOrDefault(u=> u.Email== email && u.Password== password);
        }

        public string[] GetRoles(string username)
        {
            return _db.Users.Where(u => u.UserName == username).Select(u => u.Roles.RoleName).ToArray();
        }

        public Users GetByEmail(string email)
        {
            return _db.Users.SingleOrDefault(u=> u.Email==email.Trim().ToLower());
        }

        public Users GetByUsername(string username)
        {
            return _db.Users.Single(u=> u.UserName==username);
        }
    }
}
