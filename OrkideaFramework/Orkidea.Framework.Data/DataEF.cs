using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Orkidea.Framework.Data
{
    public class DataEF<T>where T : class
    {

        public DbContext ctx { get; set; }

        private DataEF()
        {

        }

        public DataEF(DbContext dbContext)
        {
            ctx = dbContext;
        }

        public virtual IList<T> GetList()
        {
            ctx.Configuration.LazyLoadingEnabled = false;
            IList<T> list;
            IQueryable<T> dbQuery = ctx.Set<T>();

            list = dbQuery.ToList();

            return list;
        }

        public virtual IList<T> GetList(Expression<Func<T, bool>> where)
        {
            //IList<T> list;
            //IQueryable<T> dbQuery = ctx.Set<T>().Where(where).ToList();

            return ctx.Set<T>().Where(where).ToList();

            //return list;
        }

        public virtual T GetSingle(Expression<Func<T, bool>> where)
        {
            T record;
            IQueryable<T> dbQuery = ctx.Set<T>();

            record = dbQuery.Where(where).FirstOrDefault();

            return record;
        }

        //public virtual void Add(T item)
        //{
        //    DbSet<T> dbSet = ctx.Set<T>();

        //    dbSet.Add(item);

        //    foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
        //    {
        //        T entity = entry.Entity;
        //        entry.State = EntityState.Added;
        //    }

        //    ctx.SaveChanges();
        //}

        public virtual T Add(T item)
        {
            DbSet<T> dbSet = ctx.Set<T>();

            dbSet.Add(item);

            foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
            {
                T entity = entry.Entity;
                entry.State = EntityState.Added;
            }

            ctx.SaveChanges();

            return item;
        }

        public virtual void Add(params T[] items)
        {
            DbSet<T> dbSet = ctx.Set<T>();
            foreach (T item in items)
            {
                dbSet.Add(item);
                foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
                {
                    T entity = entry.Entity;
                    entry.State = EntityState.Added;
                }
            }

            ctx.SaveChanges();
        }

        public virtual void Update(T item)
        {
            DbSet<T> dbSet = ctx.Set<T>();

            dbSet.Add(item);
            foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
            {
                T entity = entry.Entity;
                entry.State = EntityState.Modified;
            }

            ctx.SaveChanges();
        }

        public virtual void Update(params T[] items)
        {
            DbSet<T> dbSet = ctx.Set<T>();

            foreach (T item in items)
            {
                dbSet.Add(item);
                foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
                {
                    T entity = entry.Entity;
                    entry.State = EntityState.Modified;
                }
            }
            ctx.SaveChanges();
        }

        public virtual void Remove(T item)
        {
            DbSet<T> dbSet = ctx.Set<T>();

            dbSet.Add(item);
            foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
            {
                T entity = entry.Entity;
                entry.State = EntityState.Deleted;
            }

            ctx.SaveChanges();
        }

        public virtual void Remove(params T[] items)
        {
            DbSet<T> dbSet = ctx.Set<T>();
            foreach (T item in items)
            {
                dbSet.Add(item);
                foreach (DbEntityEntry<T> entry in ctx.ChangeTracker.Entries<T>())
                {
                    T entity = entry.Entity;
                    entry.State = EntityState.Deleted;
                }
            }
            ctx.SaveChanges();
        }
    }
}
