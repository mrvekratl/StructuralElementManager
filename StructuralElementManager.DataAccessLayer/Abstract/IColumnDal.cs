using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.DataAccessLayer.Abstract
{
    public interface IColumnDal : IGenericDal<StructuralColumn>
    {
        List<StructuralColumn> GetColumnsByFloor(int floorLevel);
        List<StructuralColumn> GetColumnsWithMaterial();
        double GetTotalVolumeByFloor(int floorLevel);
        int GetColumnCountByFloor(int floorLevel);
    }
}
