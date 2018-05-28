using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSecurity.Methods
{
    public class Admin
    {
        public static bool Registration(string login, string password, string roleName, string levelName)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var roleId = db.Roles.FirstOrDefault(r => r.Name == roleName).Id;
                    var levelId = db.Levels.FirstOrDefault(l => l.Name == levelName).Id;
                    db.Subjects.Add(new Models.Subject { Login = login, Password = password, AuthCount = 0, RoleId = roleId, LevelId = levelId });
                    var sub = db.UnregSubjects.FirstOrDefault(u => u.Login == login);
                    db.UnregSubjects.Remove(sub);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }




    }
}
