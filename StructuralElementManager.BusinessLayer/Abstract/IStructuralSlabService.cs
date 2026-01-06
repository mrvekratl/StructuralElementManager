using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Abstract
{
    public interface IStructuralSlabService : IGenericService<StructuralSlab>
    {
        List<StructuralSlab> TGetSlabsWithMaterial();
        List<StructuralSlab> TGetSlabsByFloor(int floorLevel);
        double TGetTotalAreaByFloor(int floorLevel);

        SlabStatisticsDto TGetFloorStatistics(int floorLevel);
    }

    public class SlabStatisticsDto
    {
        public int TotalCount { get; set; }
        public double TotalArea { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public int FloorLevel { get; set; }
    }
}

