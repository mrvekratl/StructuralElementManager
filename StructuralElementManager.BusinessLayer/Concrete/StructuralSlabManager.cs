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
    public class StructuralSlabManager : GenericManager<StructuralSlab>, IStructuralSlabService
    {
        private readonly ISlabDal _slabDal;

        public StructuralSlabManager(ISlabDal slabDal) : base(slabDal)
        {
            _slabDal = slabDal;
        }

        public List<StructuralSlab> TGetSlabsWithMaterial()
        {
            return _slabDal.GetSlabsWithMaterial();
        }

        public List<StructuralSlab> TGetSlabsByFloor(int floorLevel)
        {
            return _slabDal.GetSlabsByFloor(floorLevel);
        }

        public double TGetTotalAreaByFloor(int floorLevel)
        {
            return _slabDal.GetTotalAreaByFloor(floorLevel);
        }

        public SlabStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var slabs = _slabDal.GetSlabsByFloor(floorLevel);

            if (!slabs.Any())
            {
                return new SlabStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    TotalArea = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }

            return new SlabStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = slabs.Count,
                TotalArea = slabs.Sum(x => x.GetArea()),
                TotalVolume = slabs.Sum(x => x.CalculateVolume()),
                TotalWeight = slabs.Sum(x => x.CalculateWeight())
            };
        }
    }
}

