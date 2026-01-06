using StructuralElementManager.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StructuralElementManager.BusinessLayer.Abstract
{
    public interface IStructuralColumnService : IGenericService<StructuralColumn>
    {
        // Private methods
        List<StructuralColumn> TGetColumnsWithMaterial();
        List<StructuralColumn> TGetColumnsByFloor(int floorLevel);
        double TGetTotalVolumeByFloor(int floorLevel);
        int TGetColumnCountByFloor(int floorLevel);

        // Statistical method
        ColumnStatisticsDto TGetFloorStatistics(int floorLevel);
    }

    // DTO (inline definition, which we can later move to DtoLayer)
    public class ColumnStatisticsDto
    {
        public int TotalCount { get; set; }
        public double TotalVolume { get; set; }
        public double TotalWeight { get; set; }
        public double AverageHeight { get; set; }
        public int FloorLevel { get; set; }
    }

}

