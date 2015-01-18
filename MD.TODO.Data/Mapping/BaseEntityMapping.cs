using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MD.TODO.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace MD.TODO.Data.Mapping
{
    public abstract class BaseEntityMapping<T> : EntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseEntityMapping()
        {
            // Primary Key
            HasKey(b => b.Id);

            // Base Properties
            Property(b => b.Id).HasColumnName("ID").HasColumnOrder(1);
            Property(b => b.Description).HasColumnName("DESCRIPTION").HasColumnOrder(2);
            Property(b => b.CreatedDate).IsOptional().HasColumnName("CREATED_DATE").HasColumnOrder(3).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            Property(b => b.ModifiedDate).IsOptional().HasColumnName("MODIFIED_DATE").HasColumnOrder(4);
            Property(b => b.Active).IsOptional().HasColumnName("ACTIVE").HasColumnOrder(5);
            Property(b => b.Deleted).IsOptional().HasColumnName("DELETED").HasColumnOrder(6);
            Property(b => b.RowVersion).IsRequired().IsFixedLength().HasMaxLength(8).IsRowVersion().HasColumnName("ROW_VERSION").HasColumnOrder(7);
        }
    }
}
