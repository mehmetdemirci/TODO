using MD.TODO.Data.Mapping;
using MD.TODO.Domain;
using System;
using System.Data.Entity;
using System.Reflection;
using System.Linq;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity.EntityFramework;
using MD.TODO.Domain.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.SqlServer;
using System.Data.Entity.Migrations.Model;
using System.Collections.Generic;

namespace MD.TODO.Data.Context
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        int SaveChanges();
    }

    public class TodoDbContext : IdentityDbContext<AppUser>, IDbContext
    {
        #region Ctor
        public TodoDbContext()
            : base("DefaultConnection")
        {
            Database.SetInitializer<TodoDbContext>(new MigrateDatabaseToLatestVersion<TodoDbContext, Configuration>("DefaultConnection"));
        }

        //public TodoDbContext(string nameOrConnectionString)
        //    : base(nameOrConnectionString)
        //{

        //} 
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            //.Where(type => !String.IsNullOrEmpty(type.Namespace))
            //.Where(type => type.BaseType != null && type.BaseType.IsGenericType &&
            //    type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityMapping<>));
            //foreach (var type in typesToRegister)
            //{
            //    dynamic configurationInstance = Activator.CreateInstance(type);
            //    modelBuilder.Configurations.Add(configurationInstance);
            //}

            modelBuilder.Configurations.Add(new TodoItemMapping());
        }


        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }
    }

    // Migration

    public class TodoDbContextInitializer : DropCreateDatabaseIfModelChanges<TodoDbContext>
    {
        protected override void Seed(TodoDbContext context)
        {
            base.Seed(context);
        }
    }

    internal class DefaultSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(AddColumnOperation addColumnOperation)
        {
            SetCreatedUtcColumn(addColumnOperation.Column);

            base.Generate(addColumnOperation);
        }

        protected override void Generate(CreateTableOperation createTableOperation)
        {
            SetCreatedUtcColumn(createTableOperation.Columns);

            base.Generate(createTableOperation);
        }

        private static void SetCreatedUtcColumn(IEnumerable<ColumnModel> columns)
        {
            foreach (var columnModel in columns)
            {
                SetCreatedUtcColumn(columnModel);
            }
        }

        private static void SetCreatedUtcColumn(PropertyModel column)
        {
            if (column.Name == "CREATED_DATE")
            {
                column.DefaultValueSql = "GETDATE()";
            }
        }
    }


    internal class Configuration : DbMigrationsConfiguration<TodoDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            SetSqlGenerator("System.Data.SqlClient", new DefaultSqlGenerator());
        }

        protected override void Seed(TodoDbContext context)
        {
            //TodoItem td=new TodoItem();
            //td.Title="TD";

            //context.Set<TodoItem>().AddOrUpdate(td);

            var userManager = new UserManager<AppUser>(new UserStore<AppUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new
                                          RoleStore<IdentityRole>(context));

            string name = "Admin";
            string password = "123456";

            //Create Role Admin if it does not exist
            if (!roleManager.RoleExists(name))
            {
                var roleresult = roleManager.Create(new IdentityRole(name));
            }

            //Create User=Admin with password=123456
            var user = new AppUser();
            user.UserName = name;
            var adminresult = userManager.Create(user, password);

            //Add User Admin to Role Admin
            if (adminresult.Succeeded)
            {
                var result = userManager.AddToRole(user.Id, name);
            }
            base.Seed(context);

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
