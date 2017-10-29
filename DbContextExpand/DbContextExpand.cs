using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Linq;
using HostelModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebDirectiveExpand.CustomAttribute;

namespace DbContextExpand
{
    public static class DbContextExpand
    {
        public static int AddModel<T>(this DbSet<T> dbset, T model, DbContext context) where T : BaseModel
        {
            dbset.Add(model);
            return context.SaveChanges();
        }

        public static int AddModel<T>(this DbSet<T> dbset, IEnumerable<T> model, DbContext context) where T : BaseModel
        {
            dbset.AddRange(model);
            return context.SaveChanges();
        }

        public static int UpdateModel<T>(this DbSet<T> dbset, T model, Expression<Func<T, bool>> filter, DbContext context) where T : BaseModel
        {
            var obj = dbset.FirstOrDefault(filter);
            if (obj != null)
            {
                var typeModel = typeof(T);
                foreach (var item in obj.GetType().GetProperties())
                {
                    var display = item.GetCustomAttribute<DisplayAttribute>();
                    if (display != null && display.GetAutoGenerateField() != false)
                    {
                        var value = typeModel.GetProperty(item.Name).GetValue(model);
                        item.SetValue(obj, value);
                    }
                }


            }
            return context.SaveChanges();
        }

        public static int RemoveModel<T>(this DbSet<T> dbset, Expression<Func<T, bool>> filter, DbContext context) where T : BaseModel
        {

            dbset.RemoveRange(dbset.Where(filter));
            return context.SaveChanges();
        }

        public static int RemoveModel<T>(this DbSet<T> dbset, IEnumerable<Expression<Func<T, bool>>> filter) where T : BaseModel
        {
            foreach (var item in filter)
            {
                dbset.RemoveRange(dbset.Where(item));
            }

            return 1;
        }


        public static T GetModel<T>(this DbSet<T> dbset, Expression<Func<T, bool>> filter) where T : BaseModel
        {
            return dbset.FirstOrDefault(filter);
        }


        private static IQueryable<T> InitQuearyable<T>(this DbSet<T> dbset, Expression<Func<T, bool>> filter) where T : BaseModel
        {
            var props = typeof(T).GetProperties();
            IQueryable<T> query = dbset.Where(filter);
            foreach (var item in props)
            {
                var fkAtt = item.GetCustomAttribute<FKeyControlAttribute>();
                if (fkAtt != null)
                {
                    query = query.Include(item.Name);
                }
            }
            return query;
        }
        public static IEnumerable<T> GetModels<T>(this DbSet<T> dbset, Expression<Func<T, bool>> filter) where T : BaseModel
        {

            return InitQuearyable(dbset, filter);
        }
        public static IEnumerable<T> GetModels<T, U>(this DbSet<T> dbset, Expression<Func<T, bool>> filter, Expression<Func<T, U>> order, bool isDes = false) where T : BaseModel
        {
            if (!isDes)
                return InitQuearyable(dbset, filter).OrderBy(order);
            return InitQuearyable(dbset, filter).OrderByDescending(order);
        }

        public static IEnumerable<T> GetModels<T, U>(this DbSet<T> dbset, Expression<Func<T, bool>> filter, Expression<Func<T, U>> order, int pageSize, int pageNumber, bool isDes = false) where T : BaseModel
        {
            IQueryable<T> list = null;
            if (!isDes)
            {
                list = InitQuearyable(dbset, filter).OrderBy(order);
            }
            else
            {
                list = InitQuearyable(dbset, filter).OrderByDescending(order);
            }
            if (pageNumber < 1)
                pageNumber = 1;
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }




    }
}
