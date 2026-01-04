using Microsoft.EntityFrameworkCore;
using StructuralElementManager.DataAccessLayer.Abstract;
using StructuralElementManager.DataAccessLayer.Concrete.Context;
using StructuralElementManager.DataAccessLayer.Repository;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.DataAccessLayer.EntityFramework
{
    public class EfColumnDal : GenericRepository<StructuralColumn>, IColumnDal
    {
        public int GetColumnCountByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            return context.Set<StructuralColumn>()
                .Count(x => x.FloorLevel == floorLevel);

        }

        public List<StructuralColumn> GetColumnsByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            return context.Set<StructuralColumn>()
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();
        }

        public List<StructuralColumn> GetColumnsWithMaterial()
        {
            using var context = new StructuralContext();
            return context.Set<StructuralColumn>()
                .Include(c => c.Material)
                .ToList();
        }

        public double GetTotalVolumeByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            var columns = context.Set<StructuralColumn>()
                .Include(x => x.Material)
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();

            return columns.Sum(x => x.CalculateVolume());
        }
    }
}
