

using Microsoft.EntityFrameworkCore;
using StructuralElementManager.EntityLayer.Concrete;
using StructuralElementManager.EntityLayer.Enum;

namespace StructuralElementManager.DataAccessLayer.Concrete.Context
{
    public class StructuralContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=StructuralElementsDB;Integrated Security=true;TrustServerCertificate=true;");
        }
        public DbSet<StructuralColumn> StructuralColumns { get; set; }
        public DbSet<StructuralBeam> StructuralBeams { get; set; }
        public DbSet<StructuralSlab> StructuralSlabs { get; set; }
        public DbSet<StructuralMaterial> StructuralMaterials { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TPH (Table Per Hierarchy) - Tek tablo stratejisi
            modelBuilder.Entity<StructuralElement>()
                .HasDiscriminator<string>("ElementType")
                .HasValue<StructuralColumn>("Column")
                .HasValue<StructuralBeam>("Beam")
                .HasValue<StructuralSlab>("Slab");

            // Seed Data - Başlangıç malzemeleri
            modelBuilder.Entity<StructuralMaterial>().HasData(
                new StructuralMaterial
                {
                    MaterialId = 1,
                    MaterialName = "C30 Concrete",
                    Density = 2.5,
                    CompressiveStrength = 30,
                    MaterialType = MaterialType.Concrete
                },
                new StructuralMaterial
                {
                    MaterialId = 2,
                    MaterialName = "C35 Concrete",
                    Density = 2.5,
                    CompressiveStrength = 35,
                    MaterialType = MaterialType.Concrete
                },
                new StructuralMaterial
                {
                    MaterialId = 3,
                    MaterialName = "S420 Steel",
                    Density = 7.85,
                    CompressiveStrength = 420,
                    MaterialType = MaterialType.Steel
                }
            );
        }

    }
}
