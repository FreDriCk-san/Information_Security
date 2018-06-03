using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InformationSecurity.Methods
{
    public class Admin
    {
        //Add User
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

        //Add level
        public static bool AddLevel(string name, int countOfEnter, TimeSpan startTime, TimeSpan endTime)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var query = db.Levels.Add(new Models.Level { Name = name, CountOfEnter = countOfEnter, StartTime = startTime, EndTime = endTime });
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

        
        //Update level
        public static bool UpdateLevel(Models.Level level)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var query = db.Levels.FirstOrDefault(q => (q.Id == level.Id));
                        query.Name = level.Name;
                        query.CountOfEnter = level.CountOfEnter;
                        query.StartTime = level.StartTime;
                        query.EndTime = level.EndTime;
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }


        //Delete level
        public static bool DeleteLevel(Models.Level level)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var toRemove = db.Levels.FirstOrDefault(t => t.Id == level.Id);
                    var query = db.Levels.Remove(toRemove);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }


        //Add role
        public static bool AddRole(string name, int priority, TimeSpan allowedTime, int allowedDays)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var query = db.Roles.Add(new Models.Role { Name = name, Priority = priority, AllowedTime = allowedTime, AllowedDays = allowedDays } );
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

        //Update role
        public static bool UpdateRole(Models.Role role)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var query = db.Roles.FirstOrDefault(q => q.Id == role.Id);
                        query.Name = role.Name;
                        query.Priority = role.Priority;
                        query.AllowedTime = role.AllowedTime;
                        query.AllowedDays = role.AllowedDays;
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

        //Delete role
        public static bool DeleteRole(Models.Role role)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var toRemove = db.Roles.FirstOrDefault(t => t.Id == role.Id);
                    var query = db.Roles.Remove(toRemove);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

        //Update subject
        public static bool UpdateSubject(Models.Subject subject)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var query = db.Subjects.FirstOrDefault(q => q.Id == subject.Id);
                        query.Login = subject.Login;
                        query.Password = subject.Password;
                        query.RoleId = subject.RoleId;
                        query.LevelId = subject.Id;
                        query.BanId = subject.BanId;
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

        //Delete subject
        public static bool DeleteSubject(Models.Subject subject)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var toRemove = db.Subjects.FirstOrDefault(t => t.Id == subject.Id);
                    var query = db.Subjects.Remove(toRemove);
                    db.SaveChanges();
                    return true;
                }
            }
            catch { }

            return false;
        }

    }
}
