using StructuralElementManager.DataAccessLayer.Abstract;
using StructuralElementManager.DataAccessLayer.Concrete.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.DataAccessLayer.Repository
{
    public class GenericRepository<T> : IGenericDal<T> where T : class
    {
        public void Insert(T entity)
        {
            using var context = new StructuralContext();
            context.Add(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            using var context = new StructuralContext();
            context.Remove(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            using var context = new StructuralContext();
            context.Update(entity);
            context.SaveChanges();
        }

        public List<T> GetList()
        {
            using var context = new StructuralContext();
            return context.Set<T>().ToList();
        }

        public T GetByID(int id)
        {
            using var context = new StructuralContext();
            return context.Set<T>().Find(id);
        }

        public List<T> GetListByFilter(Expression<Func<T, bool>> filter)
        {
            using var context = new StructuralContext();
            return context.Set<T>().Where(filter).ToList();
        }
    }
}
