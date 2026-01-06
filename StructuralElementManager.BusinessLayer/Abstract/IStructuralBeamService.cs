using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Abstract
{
    public interface IStructuralBeamService : IGenericService<StructuralBeam>
    {
        List<StructuralBeam> TGetBeamsWithMaterial();
        List<StructuralBeam> TGetBeamsByFloor(int floorLevel);
        double TGetTotalLengthByFloor(int floorLevel);

        BeamStatisticsDto TGetFloorStatistics(int floorLevel);
    }

    public class BeamStatisticsDto
    {
        public int TotalCount { get; set; }
        public double TotalLength { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public int FloorLevel { get; set; }
    }
}

