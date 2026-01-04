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
    public class EfSlabDal : GenericRepository<StructuralSlab>, ISlabDal
    {
        public List<StructuralSlab> GetSlabsByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            return context.Set<StructuralSlab>()
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();
        }

        public List<StructuralSlab> GetSlabsWithMaterial()
        {
            using var context = new StructuralContext();
            return context.Set<StructuralSlab>()
                .Include(x => x.Material)
                .ToList();
        }

        public double GetTotalAreaByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            var slabs = context.Set<StructuralSlab>()
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();
            return slabs.Sum(x => x.GetArea());
        }
    }
}
