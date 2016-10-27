using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryItBefore
{
    public class EmployeeDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

    public interface IReadOnlyRepository<out T> : IDisposable
    {
        T FindbyId(int id);
        IQueryable<T> FindAll();
    }
    public interface IRepository<T> : IDisposable, IReadOnlyRepository<T>
    {
        void Add(T newEntitiy);
        void Delete(T entity);
        int Commit();
    }

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity//where T class is a generic constraint!!! you can also force T to be Struct. If the new() is there the type T must have a default constructor.
    {
        DbContext _ctx;
        DbSet<T> _set;
        public SqlRepository(DbContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<T>();
        }
        public void Add(T newEntitiy)
        {
            if (newEntitiy.IsValid())
            {
                _set.Add(newEntitiy);
            }
        }

        public int Commit()
        {
            return _ctx.SaveChanges();
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public T FindbyId(int id)
        {
            return _set.Find(id);
        }
    }
}
