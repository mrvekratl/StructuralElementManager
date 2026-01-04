using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.DataAccessLayer.Abstract
{
    public interface IBeamDal :IGenericDal<StructuralBeam>
    {
        List<StructuralBeam> GetBeamsByFloor(int floorLevel);
        List<StructuralBeam> GetBeamsWithMaterial();
        double GetTotalLengthByFloor(int floorLevel);
    }
}
