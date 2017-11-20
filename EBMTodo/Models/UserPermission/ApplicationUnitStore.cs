using EBMTodo.Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EBMTodo.Models.UserPermission
{
    public class ApplicationUnitStore : IDisposable
    {
        private bool _disposed;
        private UnitStoreBase _unitStore;


        public ApplicationUnitStore(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            this.Context = context;
            this._unitStore = new UnitStoreBase(context);
        }


        public IQueryable<RBUnit> Groups
        {
            get
            {
                return this._unitStore.EntitySet;
            }
        }

        public DbContext Context
        {
            get;
            private set;
        }


        public virtual void Create(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Create(unit);
            this.Context.SaveChanges();
        }


        public virtual async Task CreateAsync(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Create(unit);
            await this.Context.SaveChangesAsync();
        }


        public virtual async Task DeleteAsync(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Delete(unit);
            await this.Context.SaveChangesAsync();
        }


        public virtual void Delete(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Delete(unit);
            this.Context.SaveChanges();
        }


        public Task<RBUnit> FindByIdAsync(string UID)
        {
            this.ThrowIfDisposed();
            return this._unitStore.GetByIdAsync(UID);
        }


        public RBUnit FindById(string UID)
        {
            this.ThrowIfDisposed();
            return this._unitStore.GetById(UID);
        }

        public Task<RBUnit> FindByNameAsync(string unitName)
        {
            this.ThrowIfDisposed();
            return QueryableExtensions
                .FirstOrDefaultAsync<RBUnit>(this._unitStore.EntitySet,
                    (RBUnit u) => u.UnitName.ToUpper() == unitName.ToUpper());
        }


        public virtual async Task UpdateAsync(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Update(unit);
            await this.Context.SaveChangesAsync();
        }


        public virtual void Update(RBUnit unit)
        {
            this.ThrowIfDisposed();
            if (unit == null)
            {
                throw new ArgumentNullException("unit");
            }
            this._unitStore.Update(unit);
            this.Context.SaveChanges();
        }


        // DISPOSE STUFF: ===============================================

        public bool DisposeContext
        {
            get;
            set;
        }


        private void ThrowIfDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }


        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (this.DisposeContext && disposing && this.Context != null)
            {
                this.Context.Dispose();
            }
            this._disposed = true;
            this.Context = null;
            this._unitStore = null;
        }
    }
}