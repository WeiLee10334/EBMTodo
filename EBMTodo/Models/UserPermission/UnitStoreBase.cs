using EBMTodo.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EBMTodo.Models.UserPermission
{
    public class UnitStoreBase
    {
        public DbContext Context
        {
            get;
            private set;
        }


        public DbSet<RBUnit> DbEntitySet
        {
            get;
            private set;
        }


        public IQueryable<RBUnit> EntitySet
        {
            get
            {
                return this.DbEntitySet;
            }
        }


        public UnitStoreBase(DbContext context)
        {
            this.Context = context;
            this.DbEntitySet = context.Set<RBUnit>();
        }


        public void Create(RBUnit entity)
        {
            this.DbEntitySet.Add(entity);
        }


        public void Delete(RBUnit entity)
        {
            this.DbEntitySet.Remove(entity);
        }


        public virtual Task<RBUnit> GetByIdAsync(object id)
        {
            return this.DbEntitySet.FindAsync(new object[] { id });
        }


        public virtual RBUnit GetById(object id)
        {
            return this.DbEntitySet.Find(new object[] { id });
        }


        public virtual void Update(RBUnit entity)
        {
            if (entity != null)
            {
                this.Context.Entry<RBUnit>(entity).State = EntityState.Modified;
            }
        }
    }
}