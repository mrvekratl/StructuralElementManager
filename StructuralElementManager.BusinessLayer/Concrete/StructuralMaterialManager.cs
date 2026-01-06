using StructuralElementManager.BusinessLayer.Abstract;
using StructuralElementManager.DataAccessLayer.Abstract;
using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Concrete
{
    public class StructuralMaterialManager : GenericManager<StructuralMaterial>, IStructuralMaterialService
    {
        private readonly IMaterialDal _materialDal;

        public StructuralMaterialManager(IMaterialDal materialDal) : base(materialDal)
        {
            _materialDal = materialDal;
        }

        public List<StructuralMaterial> TGetMaterialsWithElements()
        {
            return _materialDal.GetMaterialsWithElements();
        }

        public StructuralMaterial TGetMaterialByName(string name)
        {
            return _materialDal.GetMaterialByName(name);
        }

        public MaterialUsageDto TGetMaterialUsageStatistics(int materialId)
        {
            var material = _materialDal.GetByID(materialId);
            var materialsWithElements = _materialDal.GetMaterialsWithElements();
            var targetMaterial = materialsWithElements.FirstOrDefault(m => m.MaterialId == materialId);

            if (material == null || targetMaterial == null)
            {
                return new MaterialUsageDto
                {
                    MaterialId = materialId,
                    MaterialName = "Unknown",
                    UsageCount = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }

            var elements = targetMaterial.StructuralElements ?? new List<StructuralElement>();

            return new MaterialUsageDto
            {
                MaterialId = material.MaterialId,
                MaterialName = material.MaterialName,
                UsageCount = elements.Count,
                TotalVolume = elements.Sum(e => e.CalculateVolume()),
                TotalWeight = elements.Sum(e => e.CalculateWeight())
            };
        }
    }
}
