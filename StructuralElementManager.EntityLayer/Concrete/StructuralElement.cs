using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.EntityLayer.Concrete
{
    public abstract class StructuralElement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int FloorLevel { get; set; }
        public DateTime CreatedDate { get; set; }

        //Foreign key
        public int MaterialID { get; set; }
        //Navigation Properties
        public StructuralMaterial Material { get; set; }
        //Abstract methods - Each subclass will do its own calculations.
        public abstract double CalculateVolume();
        public abstract double CalculateWeight();
        public abstract string GetElementType();

    }
}
