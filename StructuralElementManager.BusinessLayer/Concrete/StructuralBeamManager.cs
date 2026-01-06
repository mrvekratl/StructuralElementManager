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
    public class StructuralBeamManager : GenericManager<StructuralBeam>, IStructuralBeamService    
    {
        private readonly IBeamDal _beamDal;

        public StructuralBeamManager(IBeamDal beamDal) : base(beamDal)
        {
            _beamDal = beamDal;
        }

        public List<StructuralBeam> TGetBeamsWithMaterial()
        {
            return _beamDal.GetBeamsWithMaterial();
        }

        public List<StructuralBeam> TGetBeamsByFloor(int floorLevel)
        {
            return _beamDal.GetBeamsByFloor(floorLevel);
        }

        public double TGetTotalLengthByFloor(int floorLevel)
        {
            return _beamDal.GetTotalLengthByFloor(floorLevel);
        }

        public BeamStatisticsDto TGetFloorStatistics(int floorLevel)
        {
            var beams = _beamDal.GetBeamsByFloor(floorLevel);

            if (!beams.Any())
            {
                return new BeamStatisticsDto
                {
                    FloorLevel = floorLevel,
                    TotalCount = 0,
                    TotalLength = 0,
                    TotalVolume = 0,
                    TotalWeight = 0
                };
            }

            return new BeamStatisticsDto
            {
                FloorLevel = floorLevel,
                TotalCount = beams.Count,
                TotalLength = beams.Sum(x => x.Length),
                TotalVolume = beams.Sum(x => x.CalculateVolume()),
                TotalWeight = beams.Sum(x => x.CalculateWeight())
            };
        }
    }
}
