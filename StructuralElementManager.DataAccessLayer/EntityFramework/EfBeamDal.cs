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
    public class EfBeamDal : GenericRepository<StructuralBeam>, IBeamDal
    {
        public List<StructuralBeam> GetBeamsByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            return context.Set<StructuralBeam>()
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();
        }

        public List<StructuralBeam> GetBeamsWithMaterial()
        {
            using var context = new StructuralContext();
            return context.Set<StructuralBeam>()
                .Include(x => x.Material)
                .ToList();

        }

        public double GetTotalLengthByFloor(int floorLevel)
        {
            using var context = new StructuralContext();
            var beams = context.Set<StructuralBeam>()
                .Where(x => x.FloorLevel == floorLevel)
                .ToList();
            return beams.Sum(x => x.Length);
        }
    }
}
