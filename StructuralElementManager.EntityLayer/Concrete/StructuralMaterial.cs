using StructuralElementManager.EntityLayer.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.EntityLayer.Concrete
{
    public class StructuralMaterial
    {
        [Key]
        public int MaterialId { get; set; }
        public string MaterialName { get; set; }
        public double Density { get; set; }
        public double CompressiveStrength { get; set; }
        public MaterialType MaterialType { get; set; }

        //Navigation Properties
        public List<StructuralElement> StructuralElements { get; set; }
    }
}
