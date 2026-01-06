using StructuralElementManager.BusinessLayer.Abstract;
using StructuralElementManager.DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Concrete
{
    public class GenericManager<T> : IGenericService<T> where T : class
    {
        private readonly IGenericDal<T> _genericDal;

        public GenericManager(IGenericDal<T> genericDal)
        {
            _genericDal = genericDal;
        }

        public void TAdd(T entity)
        {
            _genericDal.Insert(entity);
        }

        public void TDelete(T entity)
        {
            _genericDal.Delete(entity);
        }

        public void TUpdate(T entity)
        {
            _genericDal.Update(entity);
        }

        public List<T> TGetList()
        {
            return _genericDal.GetList();
        }

        public T TGetByID(int id)
        {
            return _genericDal.GetByID(id);
        }

        public List<T> TGetListByFilter(Expression<Func<T, bool>> filter)
        {
            return _genericDal.GetListByFilter(filter);
        }
    }
}
