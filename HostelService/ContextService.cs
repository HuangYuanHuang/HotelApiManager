using HostelModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DbContextExpand;
using System.Linq.Expressions;
using System.Collections;

namespace HostelService
{
    public class ContextService<T> where T : BaseModel

    {
        private HostelModel.HostelContext hostelContext;
        private DbSet<T> dbset;

        public ContextService(DbSet<T> dbset, HostelContext hostelContext)
        {
            this.hostelContext = hostelContext;
            this.hostelContext.Database.EnsureCreated();
            this.dbset = dbset;
        }
        public void AddEntity(T model)
        {
            dbset.AddModel(model, hostelContext);
        }

        public void UpdateEntity(T model, Expression<Func<T, bool>> filter)
        {
            dbset.UpdateModel(model, filter, hostelContext);
        }

        public void RemoveEntity(Expression<Func<T, bool>> filter)
        {
            dbset.RemoveModel(filter, hostelContext);
        }

        public void RemoveEntity(IEnumerable<Expression<Func<T, bool>>> list)
        {
            dbset.RemoveModel(list);
            hostelContext.SaveChanges();
        }
        public T GetEntity(Expression<Func<T, bool>> filter)
        {
           
            return dbset.GetModel(filter);
        }

        public IEnumerable<T> GetEntitys(Expression<Func<T, bool>> filter)
        {
            
            return dbset.GetModels(filter);
        }

    }
}
