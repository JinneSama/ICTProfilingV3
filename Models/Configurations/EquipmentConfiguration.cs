using Models.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Models.Configurations
{
    public class EquipmentConfiguration : EntityTypeConfiguration<EquipmentCategoryBrand>
    {
        public EquipmentConfiguration()
        {
            ToTable("EquipmentCategoryBrands");
            HasMany(e => e.Models)
                .WithOptional(m => m.EquipmentCategoryBrand)
                .HasForeignKey(m => m.EquipmentCategoryBrandId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.EquipmentBrand)
                .WithMany(b => b.EquipmentCategoryBrands)
                .HasForeignKey(e => e.EquipmentBrandId)
                .WillCascadeOnDelete(false);

            HasRequired(e => e.EquipmentCategory)
                .WithMany(c => c.EquipmentCategoryBrands)
                .HasForeignKey(e => e.EquipmentCategoryId)
                .WillCascadeOnDelete(false);
        }
    }
}
