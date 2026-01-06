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
    public class StructuralColumnManager : GenericManager<StructuralColumn>, IStructuralColumnService
    {
        private readonly IColumnDal _columnDal;

        public StructuralColumnManager(IColumnDal columnDal) : base(columnDal)
        {
            _columnDal = columnDal;
        }

        public List<StructuralColumn> TGetColumnsWithMaterial()
        {
            return _columnDal.GetColumnsWithMaterial();
        }

        public List<StructuralColumn> TGetColumnsByFloor(int floorLevel)
        {
            return _columnDal.GetColumnsByFloor(floorLevel);
        }

        public double TGetTotalVolumeByFloor(int floorLevel)
        {
            return _columnDal.GetTotalVolumeByFloor(floorLevel);
        }

        public int TGetColumnCountByFloor(int floorLevel)
        {
            return _columnDal.GetColumnCountByFloor(floorLevel);
        }

        public ColumnStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var columns = _columnDal.GetColumnsByFloor(floorLevel);

            if (!columns.Any())
            {
                return new ColumnStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    TotalVolume = 0,
                    TotalWeight = 0,
                    AverageHeight = 0
                };
            }

            return new ColumnStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = columns.Count,
                TotalVolume = columns.Sum(x => x.CalculateVolume()),
                TotalWeight = columns.Sum(x => x.CalculateWeight()),
                AverageHeight = columns.Average(x => x.Height)
            };
        }
    }

}

