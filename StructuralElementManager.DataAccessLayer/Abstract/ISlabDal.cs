using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.DataAccessLayer.Abstract
{
    public interface ISlabDal : IGenericDal<StructuralSlab>
    {
        List<StructuralSlab> GetSlabsByFloor(int floorLevel);
        List<StructuralSlab> GetSlabsWithMaterial();
        double GetTotalAreaByFloor(int floorLevel);
    }
}
