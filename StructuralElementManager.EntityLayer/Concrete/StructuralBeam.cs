using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.EntityLayer.Concrete
{
    public class StructuralBeam : StructuralElement
    {
        public double Width { get; set; }   // cm
        public double Height { get; set; }  // cm
        public double Length { get; set; }  // cm

        public override double CalculateVolume()
        {
            return (Width * Height * Length) / 1_000_000;
        }

        public override double CalculateWeight()
        {
            return CalculateVolume() * (Material?.Density ?? 0);
        }

        public override string GetElementType()
        {
            return "Beam";
        }
    }
}
