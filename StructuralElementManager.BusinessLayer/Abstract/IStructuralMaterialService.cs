using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Abstract
{
    public interface IStructuralMaterialService : IGenericService<StructuralMaterial>
    {
        List<StructuralMaterial> TGetMaterialsWithElements();
        StructuralMaterial TGetMaterialByName(string name);

        // Statistics
        MaterialUsageDto TGetMaterialUsageStatistics(int materialId);
    }

    public class MaterialUsageDto
    {
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public int UsageCount { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
    }
}

