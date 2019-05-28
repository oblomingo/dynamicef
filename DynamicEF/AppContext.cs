using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using WebTest;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DynamicEF
{
    public class AppContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (System.Diagnostics.Debugger.IsAttached == false)
            {
                System.Diagnostics.Debugger.Launch();
            }
            //modelBuilder.Entity("EntityName", config =>
            //{
            //    config.Property("Id").HasColumnType("int");
            //    config.Property("Name").HasMaxLength(255);
            //    config.HasKey("Id");

            //});

            modelBuilder.Entity<Student>();
            var type = DynamicTypeBuilder.CreateType();
            var method = modelBuilder.GetType().GetMethods().Where(x => x.IsGenericMethod && x.Name == "Entity").First();
            method = method.MakeGenericMethod(new Type[] { type });
            method.Invoke(modelBuilder, null);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\\mssqllocaldb;Database=aspnet-WebTest-74B1E189-3A81-45DE-820A-4C60C96CD340;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

    public class MyBinder : Binder
    {
        public override MethodBase SelectMethod(BindingFlags bindingAttr, MethodBase[] match, Type[] types, ParameterModifier[] modifiers)
        {
            return match.First(m => m.IsGenericMethod);
        }

        #region not implemented
        
        
        public override PropertyInfo SelectProperty(BindingFlags bindingAttr, PropertyInfo[] match, Type returnType, Type[] indexes, ParameterModifier[] modifiers) => throw new NotImplementedException();
        public override void ReorderArgumentArray(ref object[] args, object state) => throw new NotImplementedException();

        public override FieldInfo BindToField(BindingFlags bindingAttr, FieldInfo[] match, object value, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override MethodBase BindToMethod(BindingFlags bindingAttr, MethodBase[] match, ref object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] names, out object state)
        {
            throw new NotImplementedException();
        }

        public override object ChangeType(object value, Type type, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
