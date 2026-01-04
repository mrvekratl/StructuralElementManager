using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.EntityLayer.Concrete
{
    public class StructuralSlab : StructuralElement
    {
        public double Length { get; set; }     // cm
        public double Width { get; set; }      // cm
        public double Thickness { get; set; }  // cm

        public override double CalculateVolume()
        {
            return (Length * Width * Thickness) / 1_000_000;
        }
        public override double CalculateWeight()
        {
            return CalculateVolume() * (Material?.Density ?? 0);
        }
        public override string GetElementType()
        {
            return "Slab";
        }
        public double GetArea()
        {
            return (Length * Width) / 10_000; // m²
        }
    }
}
