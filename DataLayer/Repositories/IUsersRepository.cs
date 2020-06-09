using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IUsersRepository
    {
        bool IsDuplicatedUser(RegisterViewModel user);
        Users GetByActiveCode(string id);
        Users GetExistUser(string email,string password);

        string[] GetRoles(string username);

        Users GetByEmail(string email);
        Users GetByUsername(string username);
    }

    


}
