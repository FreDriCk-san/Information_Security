namespace InformationSecurity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public class Configuration : DbMigrationsConfiguration<InformationSecurity.Context.ContextDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "InformationSecurity.Context.ContextDB";
        }

        protected override void Seed(InformationSecurity.Context.ContextDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Levels.AddOrUpdate(
                x => x.Name,
                new Models.Level { Name = "admin" }
                );

            context.Roles.AddOrUpdate(
                x => x.Name,
                new Models.Role { Name = "admin", Priority = 1, AllowedDays = 127 }
                );

            context.Subjects.AddOrUpdate(
                x => x.Login,
                new Models.Subject { Login = "hci_admin", Password = "hci_admin", AuthCount = 0, LevelId = 1, RoleId = 1 }
                );

            context.SaveChanges();
        }
    }
}
