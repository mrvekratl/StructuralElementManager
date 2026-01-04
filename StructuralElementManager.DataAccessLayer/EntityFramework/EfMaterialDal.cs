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
    public class EfMaterialDal : GenericRepository<StructuralMaterial>, IMaterialDal
    {
        public StructuralMaterial GetMaterialByName(string name)
        {
            using var context = new StructuralContext();
            return context.StructuralMaterials
                .FirstOrDefault(x => x.MaterialName == name);
        }

        public List<StructuralMaterial> GetMaterialsWithElements()
        {
            using var context = new StructuralContext();
            return context.StructuralMaterials
                .Include(x => x.StructuralElements)
                .ToList();
        }
    }
}
